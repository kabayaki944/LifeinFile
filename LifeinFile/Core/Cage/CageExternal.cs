using LifeinFile.Controller.CageSystem;
using LifeinFile.Models.Cages;
using System.IO.Compression;
using CageWindow = LifeinFile.Windows.CageWindow;

namespace LifeinFile.Core.Cage
{
    public class CageExternal
    {
        private CageModel _model;
        public IExternalModel Model => _model;
        public CageWindow Window { get; private set;}

        LifeSystem _lifeSystem;
        CageFileController _fileController;
        public void Construct(CageModel model, CageWindow window, LifeSystem lifeSystem, CageFileController fileController)
        {
            _model = model;
            Window = window;
            _lifeSystem = lifeSystem;
            _fileController = fileController;
        }
        
        public void Kill() => _lifeSystem.Kill();
        public CageFile InstanceFile() => _fileController.CreateInstance();
        public void ExportFile(ZipArchive archive) => _fileController.Export(archive);

        public void ImportFile(CageFile cageFile) => _fileController.Import(cageFile);

        public void EnableWindow(bool enable) => Window.EnableWindow(enable);
    }
}
