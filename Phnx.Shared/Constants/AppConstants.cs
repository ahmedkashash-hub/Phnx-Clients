using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Shared.Constants
{

    public static class AppConstants
    {
        public static string RUN_TIME_CONNECTION_STRING => "Host=localhost;Port=5432;Database=Phnx_Clinet_db;Username=postgres;Password=123@";
        public static string UPLOAD_DIRECTORY => "uploads";
        public static string FilesLocation => "https://api.diwan-mhq.com/uploads/";
        public static string GetImagePath(string image) =>
            string.IsNullOrWhiteSpace(image) ? string.Empty : FilesLocation + image;
        public static List<string> GetImagesPaths(List<string> images)
        {
            List<string> result = [];
            foreach (var image in images.Where(x => !string.IsNullOrEmpty(x)))
            {
                result.Add(GetImagePath(image));
            }
            return result;
        }
    }
}