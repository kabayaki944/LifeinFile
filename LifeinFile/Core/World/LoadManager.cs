using LifeinFile.Core.Cage;
using System.IO;
using System.Net;

namespace LifeinFile.Core.Facade
{
    public enum LoadResult
    {
        Success,
        Cancelled,
        Error,
    }
    
    public static class LoadManager
    {
        public static LoadResult TryLoad(FileInfo cageFile)
        {
            return LoadResult.Success;
        }
    }
}