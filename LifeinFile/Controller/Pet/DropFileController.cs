using LifeinFile.Models.Pets;
using LifeinFile.Windows;
using Reactive.Bindings.Extensions;
using System.IO;
using System.Reactive.Linq;

namespace LifeinFile.Controller.PetSystem
{
    public class DropFileController
    {
        const double ADD_BY_1MB = 2.0;
        PetModel _model;

        public DropFileController(PetModel model, IProvideWindowInput input)
        {
            _model = model;
            input.OnFileDroppedAsObservable
                .Where(_ => model.AbleToInteract)
                .Subscribe(EatFiles)
                .AddTo(model.Disposables);
        }

        void EatFiles(string[] files)
        {
            foreach (string file in files)
            {
                long fileSizeBytes = 0;
                try
                {
                    FileInfo fileInfo = new FileInfo(file);
                    if (fileInfo.Exists)
                    {
                        fileSizeBytes = fileInfo.Length;
                    }
                }
                catch (Exception) { }

                Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(
                    file,
                    Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                    Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin
                );
                
                double fileSizeMb = fileSizeBytes / (1024.0 * 1024.0);
                
                double recoverAmount = Math.Max(1.0, fileSizeMb * ADD_BY_1MB);

                _model.AddHunger(recoverAmount);
            }
        }
    }
}