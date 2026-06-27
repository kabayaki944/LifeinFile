using LifeinFile.Helper;

namespace LifeinFile.Core.Setting
{
    public class UserSettingData: IReadOnlyUserSettingData
    {
        public bool IsFirstStart = true;
        public List<String> StartupCageList { get; set; } = new List<string>();
        public string DefaultCreateCagePath { get; set; }

        IReadOnlyList<String> IReadOnlyUserSettingData.StartupCageList => StartupCageList;
        public UserSettingData()
        {
            StartupCageList.Add(PathHelper.DefaultCagesPath);
            DefaultCreateCagePath = PathHelper.DefaultCagesPath;
        }
    }
}