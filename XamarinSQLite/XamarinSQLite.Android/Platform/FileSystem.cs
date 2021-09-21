using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinSQLite.Core;
using XamarinSQLite.Droid.Platform;

[assembly: Xamarin.Forms.Dependency(typeof(FileSystem))]
namespace XamarinSQLite.Droid.Platform
{
    public class FileSystem : IFileSystem
    {
        public Task<string> GetAbsDbPath()
        {
            string absRoot = Environment.ExternalStorageDirectory.AbsolutePath;
            string absDirPath = Path.Combine(absRoot, Environment.DirectoryDownloads);
            return Task.FromResult(absDirPath);
        }
    }
}