using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinSQLite.Core
{
    public class BaseVm<T> : BaseVm where T : Page
    {
        public BaseVm()
        {

        }

        public override Page Page
        {
            get
            {
                if (_page == null)
                {
                    try
                    {
                        _page = Activator.CreateInstance<T>();
                    }
                    catch (Exception ex)
                    {
                        string vmName = GetType().Name;
                        string viewName = typeof(T).Name;
                        throw new Exception(
                            $"Не удалось создать представление для данной View model\n" +
                            $"BaseVm: {vmName}\n" +
                            $"View: {viewName}\n" +
                            $"Inner Exception: \n" +
                            $"{ex.Message}\n" +
                            $"{ex.InnerException?.Message}",
                            ex);
                    }
                }
                return _page;
            }
        }
        private Page _page;
    }

    public abstract class BaseVm : BaseNotify
    {
        private bool isAppeading;
        private NavigationPage navigationPage;

        public BaseVm()
        {
            Page.BindingContext = this;
            Page.Appearing += Page_Appearing;
            Page.Disappearing += Page_Disappearing;
        }

        private void Page_Appearing(object sender, EventArgs e)
        {
            if (!isAppeading)
            {
                isAppeading = true;
                OnFirstAppearing();
            }

            OnAppearing();
        }

        private void Page_Disappearing(object sender, EventArgs e)
        {
            OnDisappearing();
        }

        public bool IsLoading { get; set; }
        public abstract Page Page { get; }

        public NavigationPage InitNavigation()
        {
            navigationPage = new NavigationPage(Page);
            return navigationPage;
        }

        public Task GoTo(BaseVm vm)
        {
            vm.navigationPage = navigationPage;
            return navigationPage.Navigation.PushAsync(vm.Page);
        }

        public Task Close()
        {
            var stack = navigationPage.Navigation.NavigationStack;
            int count = stack.Count;
            int index = GetIndexVm();

            if (count <= 1)
            {
                return Task.CompletedTask;
            }
            else if (index == count - 1)
            {
                return navigationPage.Navigation.PopAsync();
            }
            else
            {
                navigationPage.Navigation.RemovePage(Page);
                return Task.CompletedTask;
            }
        }

        public Task ShowError(string message, string title = "Error")
        {
            return Page.DisplayAlert(title, message, "OK");
        }

        public virtual void OnFirstAppearing()
        {

        }

        public virtual void OnAppearing()
        {

        }

        public virtual void OnDisappearing()
        {

        }

        #region support
        private int GetIndexVm()
        {
            var stack = navigationPage.Navigation.NavigationStack;
            int count = stack.Count;
            int index = stack.ToList().IndexOf(Page);
            return index;
        }
        #endregion support
    }
}
