using LifeinFile.Core.Cage;
using LifeinFile.Core.Pets;
using LifeinFile.Models.Cages;
using Microsoft.Win32;
using System.IO;

namespace LifeinFile.Core.Facade
{
    public enum SaveResult
    {
        Success,
        Cancelled,
        Error
    }
    
    public static class SaveManager
    {
        public static SaveResult TrySave(CageExternal cageExternal)
        {
            var pets = PetCageConnector.GetPetsInCage(cageExternal);
            
            string directoryPath = GetSavePath(cageExternal.Model);
            if(directoryPath == null) return SaveResult.Cancelled;
            
            cageExternal.Model.Path = directoryPath;

            CageFile cageFile = cageExternal.InstanceFile();
            string cagePath = cageFile.CreateFile(directoryPath);

            foreach (var petExternal in pets)
            {
                PetFile petFile = petExternal.InstanceFile();
                petFile.CreateFile(cagePath);
            }
            return SaveResult.Success;
        }

        /// <summary>
        /// Cage見つからなかったら新しい場所を聞く
        /// </summary>
        /// <param name="cageExternal"></param>
        /// <returns></returns>
        static string GetSavePath(IExternalModel cageExternal)
        {
            Console.Write(cageExternal.Path);
            // 保存処理の直前でチェック
            if (!Directory.Exists(cageExternal.Path))
            {
                // 見つからない！ユーザーに新しい場所を聞く
                var dialog = new SaveFileDialog { Filter = "Cage Files|*.cage" };
                if (dialog.ShowDialog() == true)
                {
                    return dialog.FileName;
                }

                return null;
            }        
            return cageExternal.Path;
        }
    }
}