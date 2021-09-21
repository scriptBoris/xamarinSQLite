using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinSQLite.Core;
using XamarinSQLite.Models;
using XamarinSQLite.Services;
using XamarinSQLite.Views;

namespace XamarinSQLite.ViewModels
{
    public class DbContentVm : BaseVm<DbContentView>
    {
        public DbContentVm()
        {
            CommandCreateRecord = new Command(ActionCreateRecord);
            Load();
        }

        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        public ICommand CommandCreateRecord { get; set; }

        private async void ActionCreateRecord()
        {
            var vm = new CreateRecordVm();
            await GoTo(vm);

            var res = await vm.Result.Task;
            if (res == null)
                return;

            res.Number = Users.Count + 1;
            Users.Add(res);
        }

        public async void Load()
        {
            IsLoading = true;
            
            var users = await DataBaseService.GetUsers();

            for (int i = 0; i < users.Length; i++)
            {
                var user = users[i];
                user.Number = i + 1;
                Users.Add(user);
            }

            IsLoading = false;
        }
    }
}
