using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinSQLite.Core;
using XamarinSQLite.Models;
using XamarinSQLite.Services;
using XamarinSQLite.Views;

namespace XamarinSQLite.ViewModels
{
    public class CreateRecordVm : BaseVm<CreateRecordView>
    {
        public TaskCompletionSource<User> Result = new TaskCompletionSource<User>();

        public CreateRecordVm()
        {
            CommandSelectImage = new Command(ActionSelectImage);
            CommandCreate = new Command(ActionCreate);
        }

        public User User { get; set; } = new User();
        public ImageSource PreviewAvatar { get; set; }
        public ICommand CommandSelectImage { get; set; }
        public ICommand CommandCreate { get; set; }

        private async void ActionSelectImage()
        {
            var file = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();
            if (file == null)
                return;

            var stream = await file.OpenReadAsync();
            byte[] bytes = new byte[stream.Length];
            await stream.ReadAsync(bytes, 0, (int)stream.Length);
            stream.Position = 0;


            User.AvatarBase64 = Convert.ToBase64String(bytes);
            PreviewAvatar = ImageSource.FromStream(() => stream);
        }

        private async void ActionCreate()
        {
            IsLoading = true;
            if (string.IsNullOrWhiteSpace(User.FirstName))
            {
                await ShowError("First name is empty");
                IsLoading = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(User.LastName))
            {
                await ShowError("Last name is empty");
                IsLoading = false;
                return;
            }

            if (User.AvatarBase64 == null)
            {
                await ShowError("Please, select avatar file");
                IsLoading = false;
                return;
            }

            Result.SetResult(User);
            await DataBaseService.SaveUser(User);
            await Close();
        }
    }
}
