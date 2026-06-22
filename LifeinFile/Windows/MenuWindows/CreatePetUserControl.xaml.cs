using LifeinFile.Core.Cage;
using LifeinFile.Core.Facade;
using LifeinFile.Core.Pets;
using LifeinFile.Models.Pets;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Reactive.Disposables;
using System.Reactive.Linq; 
using System.Windows;
using System.Windows.Controls;

namespace LifeinFile.Windows
{
    public partial class CreatePetUserControl : UserControl
    {
        //入力
        public ReactiveProperty<string> PetName { get; } = new ReactiveProperty<string>("PetName");
        public ReactiveProperty<string> PetId { get; } = new ReactiveProperty<string>(PetSpritesDictionary.DEFAULT_PET_ID.ToString());
        
        // エラーメッセージ
        public ReactiveProperty<string> PetNameError { get; } = new ReactiveProperty<string>("");
        public ReactiveProperty<string> PetIdError { get; } = new ReactiveProperty<string>("");
        
        // 「作成」を押せるか
        public ReadOnlyReactiveProperty<bool> CanCreate { get; private set; }
        
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        
        private PetExternal _previewPet;
        private CageExternal _previewCage;
        
        public CreatePetUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
            InitializePreview();
        }

        private void InitializePreview()
        {
            // 1. ReactivePropertyのリアルタイム監視（入力のたびにエラー判定も行う）
            PetName.Subscribe(UpdatePreviewName).AddTo(_disposables);
            PetId.Subscribe(UpdatePreviewSprite).AddTo(_disposables);

            // 2. ★「ボタンを押せるか」の判定ルールを作成！
            // 名前エラーとIDエラーの両方が「空っぽ（エラー無し）」ならTrue（押せる）になる
            CanCreate = PetNameError.CombineLatest(PetIdError, (nameErr, idErr) => 
                string.IsNullOrEmpty(nameErr) && string.IsNullOrEmpty(idErr)
            ).ToReadOnlyReactiveProperty().AddTo(_disposables);

            _previewCage = CageManager.CreateCage(new CageInitData("Review"));
            
            if (PetSpritesDictionary.TryGetDefault(out var sprites))
            {
                _previewPet = PetManager.CreatePet(new PetInitData(PetName.Value, sprites));
                PetCageConnector.MovePetToCage(_previewPet, _previewCage);
            }
        }

        // --- リアルタイム判定 兼 プレビュー更新 ---
        private void UpdatePreviewName(string newName)
        {
            // 空白チェック
            if (string.IsNullOrWhiteSpace(newName))
            {
                PetNameError.Value = "名前を入力してください";
                return;
            }
            
            //エラーなし
            PetNameError.Value = "";
            if (_previewPet != null)
                _previewPet.Model.Name.Value = newName;
        }

        private void UpdatePreviewSprite(string newIdString)
        {
            // ① 空白チェック
            if (string.IsNullOrWhiteSpace(newIdString))
            {
                PetIdError.Value = "IDを入力してください";
                return;
            }

            // ② 数字チェック
            if (!int.TryParse(newIdString, out int newId))
            {
                PetIdError.Value = "IDは数字で入力してください";
                return;
            }

            // ③ 存在チェック
            if (!PetSpritesDictionary.TryGet(newId, out var sprites))
            {
                PetIdError.Value = $"ID:{newId}は見つかりません";
                return;
            }

            //エラーなし
            PetIdError.Value = "";
            if (_previewPet != null)
                _previewPet.Model.Sprites.Value = sprites; 
        }

        // --- UI イベントハンドラ ---
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            PetCageConnector.MovePetToOut(_previewPet);
            _previewPet.Model.State.Value = PetState.Active;
            _previewPet = null; // Killさせないために外す
            CloseMenu();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            CloseMenu();
        }

        private void CloseMenu()
        {
            if (Window.GetWindow(this) as MenuWindow is { } mainWindow)
            {
                mainWindow.ResetMenu();
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            _previewPet?.Kill();
            _previewCage?.Kill();
            _disposables.Dispose();
        }
    }
}