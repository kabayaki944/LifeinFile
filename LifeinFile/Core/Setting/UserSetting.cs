using LifeinFile.Helper;
using Microsoft.Win32;
using System.IO;
using System.Text.Json;

namespace LifeinFile.Core.Setting
{
    public static class UserSetting
    {
        static UserSettingData _data = new UserSettingData();
        public static IReadOnlyUserSettingData Data => _data;
        const string FILE_NAME = "UserSettings.json";
        
        public static string FilePath =>  Path.Combine(PathHelper.SettingsPath, FILE_NAME);

        //TODO: save, loadの実行タイミング
        public static void Save() => JsonHelper.Save(_data, FilePath);

        static bool TryLoad()
        {
            var result = JsonHelper.TryLoad<UserSettingData>(FilePath);
            if(result.success) _data = result.data;
            return result.success;
        }

        public static void StartUp(bool isFirst)
        {
            if(isFirst) _data =  new UserSettingData();
            else
            {
                bool success = TryLoad();
                if(!success)  _data = new UserSettingData();
            }
        }
    }
}