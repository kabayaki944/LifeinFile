using LifeinFile.Models.Pets;

namespace LifeinFile.Controller.PetSystem
{
    public class PetFilesController
    {
        PetModel _model;
        public PetFilesController(PetModel model)
        {
            _model = model;
        }

        public void EatFiles(string[] files)
        {
            foreach (string file in files)
            {
                Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(
                    file,
                    Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                    Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin
                );
                //todo: ファイルのサイズによって変える
                _model.Feed(20.0);
            }
        }
    }
}