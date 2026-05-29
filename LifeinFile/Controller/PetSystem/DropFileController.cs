using LifeinFile.Models.Pets;
using LifeinFile.Windows;
using Reactive.Bindings.Extensions;

namespace LifeinFile.Controller.PetSystem
{
    public class DropFileController
    {
        PetModel _model;
        public DropFileController(PetModel model, IProvideWindowInput input)
        {
            _model = model;
            input.OnFileDroppedAsObservable
                .Subscribe(EatFiles)
                .AddTo(model.Disposables);
        }

        void EatFiles(string[] files)
        {
            foreach (string file in files)
            {
                Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(
                    file,
                    Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                    Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin
                );
                //todo: ファイルのサイズによって変える
                _model.AddHunger(20.0);
            }
        }
    }
}