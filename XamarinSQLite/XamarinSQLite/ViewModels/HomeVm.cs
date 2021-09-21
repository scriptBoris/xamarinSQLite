using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinSQLite.Core;
using XamarinSQLite.Services;
using XamarinSQLite.Views;
using static Xamarin.Essentials.Permissions;

namespace XamarinSQLite.ViewModels
{
    public class HomeVm : BaseVm<HomeView>
    {
        public HomeVm()
        {
            CommandDbContent = new Command(ActionDbContent);
        }

        public ICommand CommandDbContent { get; set; }

        private void ActionDbContent()
        {
            var vm = new DbContentVm();
            GoTo(vm);
        }

        public override async void OnFirstAppearing()
        {
            IsLoading = true;

            if (!await Check<Permissions.StorageRead>())
            {
                IsLoading = false;
                return;
            }

            if (!await Check<Permissions.StorageWrite>())
            {
                IsLoading = false;
                return;
            }

            await DataBaseService.Init();
            //await Test();
            IsLoading = false;
        }

        private async Task<bool> Check<T>() where T : BasePlatformPermission, new()
        {
            var status = await Permissions.CheckStatusAsync<T>();
            if (status == PermissionStatus.Granted)
                return true;

            status = await Permissions.RequestAsync<T>();
            if (status != PermissionStatus.Granted)
                return false;

            return true;
        }

        private async Task Test()
        {
            string path = await DependencyService.Get<IFileSystem>().GetAbsDbPath();
            path = Path.Combine(path, "test.txt");
            await File.WriteAllTextAsync(path, "test");
        }
    }
}