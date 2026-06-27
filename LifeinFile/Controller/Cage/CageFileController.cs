using LifeinFile.Core.Cage;
using LifeinFile.Helper;
using LifeinFile.Models.Cages;
using LifeinFile.Windows;
using System.Dynamic;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Windows;

namespace LifeinFile.Controller.CageSystem
{
    public class CageFileController
    {
        CageModel _model;
        Window _window;
        public CageFileController(CageModel model, Window window)
        {
            _model = model;
            _window = window;
        }
        
        public CageFile CreateInstance() => new CageFile(_model, _window);
        
        public void Export(ZipArchive archive)
        {
            // 自分自身のデータを読みやすいJSON文字列に変換する
            string jsonText = JsonSerializer.Serialize(CreateInstance(), JsonHelper.Options);

            // 1. ZIPの直下に CageData.json を作成してデータを書き込む
            ZipArchiveEntry cageEntry = archive.CreateEntry("CageData.json");
            using (StreamWriter writer = new StreamWriter(cageEntry.Open()))
            {
                writer.Write(jsonText);
            }

            // 2. 空の Pets ディレクトリを作成する
            var pets = archive.CreateEntry("Pets/");
        }

        public void Import(CageFile cageFile)
        {
            _model.InstanceId = cageFile.CageInstanceId;
            _model.Name.Value = cageFile.CageName;
            //Todo: Typeの実装
            _window.Left = cageFile.PositionX;
            _window.Top = cageFile.PositionY;
            _window.Width = cageFile.Width;
            _window.Height = cageFile.Height;
        }
    }
}