using LifeinFile.Helper;
using LifeinFile.Models.Cages;
using System.Diagnostics;
using System.Windows;

namespace LifeinFile.Core.Cage
{
    // Cageが持っているPetたちを監視するクラス
    public class CageCollider : IUpdateLate
    {
        private CageModel _model;
        private Window _window;

        public CageCollider(CageModel cageModel, Window cageWindow)
        {
            _model = cageModel;
            _window = cageWindow;
            ProvideUpdate.AddUpdateLate(this); // タイマーに登録
        }

        public void OnUpdateLate()
        {
            // CageModelに入っている（ドロップされた）すべてのPetをチェック
            foreach (var pet in _model.Pets)
            {
                CollisionResult result = CollisionHelper.CheckAndClamp(pet.Window, _window); 

                // どこかの壁に当たっていたら、PetExternalの OnCollision を呼び出す！
                if (result.hitX || result.hitY)
                {
                    Debug.WriteLine($"Collision detected for pet {pet.Model.Name} with cage {_model.Name}");
                    pet.OnCollision(result);
                }
            }
        }
    }
}