using LifeinFile.Controller.PetSystem;
using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using LifeinFile.Views.Pets;
using System.IO.Compression;
using System.Windows;
using PetWindow = LifeinFile.Windows.PetWindow;

namespace LifeinFile.Core.Pets
{
    public class PetExternal
    {
        private PetModel  _model;
        public IExternalModel Model => _model;
        public PetWindow Window { get; private set; }
        LifeSystem _lifeSystem;
        PetCollision _collision;
        PetFileController _fileController;
        public void Construct(PetModel model, PetCollision collision, PetWindow window, LifeSystem lifeSystem, PetFileController fileController)
        {
            _model = model;
            _collision = collision;
            Window = window;
            _lifeSystem = lifeSystem;
            _fileController = fileController;
        }

        
        public void OnCollision(CollisionResult result) => _collision.OnCollision(result);
        public void Kill() => _lifeSystem.Kill();
        public void ExportFile(ZipArchive archive) => _fileController.Export(archive);
        public void ImportFile(PetFile petFile) => _fileController.Import(petFile);
        public void EnableWindow(bool enable) => Window.EnableWindow(enable);
        
        ~PetExternal() => System.Diagnostics.Debug.WriteLine("PetExternal is clear");
    }
}
