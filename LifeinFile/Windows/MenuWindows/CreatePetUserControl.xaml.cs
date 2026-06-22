using LifeinFile.Core.Cage;
using LifeinFile.Core.Facade;
using LifeinFile.Core.Pets;
using LifeinFile.Views.Pets;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Controls;

namespace LifeinFile.Windows
{
    public partial class CreatePetUserControl : UserControl
    {
        // --- プロパティ・フィールド ---
        public ReactiveProperty<string> PetName { get; } = new ReactiveProperty<string>("");
        public ReactiveProperty<string> PetId { get; } = new ReactiveProperty<string>("");
        
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        
        private PetExternal _previewPet;
        private CageExternal _previewCage;
        
        // --- 初期化 ---
        public CreatePetUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
            InitializePreview();
        }

        private void InitializePreview()
        {
            if (!PetSpritesDictionary.TryGetDefault(out var sprites))
            {
                MessageBox.Show($"ID:{PetSpritesDictionary.DEFAULT_PET_ID}のPetSpritesが見つかりませんでした！", "エラー");
                return;
            }
            
            // プレビュー用のPetとCageを生成
            _previewPet = PetManager.CreatePet(new PetInitData("Test", sprites));       
            _previewCage = CageManager.CreateCage(new CageInitData("Review"));

            
            // PetをCageに収容
            PetCageConnector.MovePetToCage(_previewPet, _previewCage);

            // ReactivePropertyの変更監視（独立したメソッドを登録）
            PetName.Subscribe(UpdatePreviewName).AddTo(_disposables);
            PetId.Subscribe(UpdatePreviewSprite).AddTo(_disposables);
        }

        // --- ReactiveProperty コールバック（リアルタイム更新） ---
        private void UpdatePreviewName(string newName)
        {
            Debug.WriteLine($"Preview Name: {newName}");
            if (string.IsNullOrWhiteSpace(newName) || _previewPet == null) return;
            
            _previewPet.Model.Name.Value = newName;
        }

        private void UpdatePreviewSprite(string newIdString)
        {
            Debug.WriteLine($"Preview ID: {newIdString}");
            if (!int.TryParse(newIdString, out int newId) || _previewPet == null) return;
            if (!PetSpritesDictionary.TryGet(newId, out var sprites)) return;
            
            _previewPet.Model.Sprites.Value = sprites; 
        }

        // --- UI イベントハンドラ ---
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // UIコントロールではなく、ReactivePropertyの裏側から値を取得する
            string petName = PetName.Value;
            string petIdString = PetId.Value;

            // バリデーションチェック
            if (string.IsNullOrWhiteSpace(petName))
            {
                MessageBox.Show("名前を入力してください！", "エラー");
                return;
            }

            if (!int.TryParse(petIdString, out int petId))
            {
                MessageBox.Show("IDを数字で入力してください！", "エラー");
                return;
            }

            if (!PetSpritesDictionary.TryGet(petId, out _))
            {
                MessageBox.Show($"ID:{petId}のPetSpritesが見つかりませんでした！", "エラー");
                return;
            }
            
            // プレビューしていたPetをデスクトップ(Out)へ解放し、本物のPetに昇格させる
            PetCageConnector.MovePetToOut(_previewPet);
            
            // Unloaded時に安全装置(Kill)が作動しないよう、参照を外しておく
            _previewPet = null;
            
            CloseMenu();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            CloseMenu();
        }

        // --- ヘルパーメソッド ---
        private void CloseMenu()
        {
            if (Window.GetWindow(this) as MenuWindow is { } mainWindow)
            {
                mainWindow.ResetMenu();
            }
        }

        // --- 破棄処理 ---
        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            // _previewPet がnullでない（キャンセルされた）場合のみ削除される
            _previewPet?.Kill();
            _previewCage?.Kill();
            _disposables.Dispose();
        }
    }
}