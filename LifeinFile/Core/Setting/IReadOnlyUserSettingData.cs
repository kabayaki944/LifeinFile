namespace LifeinFile.Core.Setting
{
    public interface IReadOnlyUserSettingData
    {
        public IReadOnlyList<String> StartupCageList { get;}
        public string DefaultCreateCagePath { get;}
    }
}