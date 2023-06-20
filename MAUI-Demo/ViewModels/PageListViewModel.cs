using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_Demo.Auth0;
using MAUI_Demo_Service.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MAUI_Demo.ViewModels
{
    public partial class PageListViewModel : ObservableObject, INotifyPropertyChanged
    {

        [ObservableProperty]
        public ObservableCollection<MAUI_Demo_Service.Models.Page> obsPageList;

        [ObservableProperty]
        private bool _isMonthActive;

        [ObservableProperty]
        private bool _IsListActive;

        public IEnumerable<MAUI_Demo_Service.Models.Page> pageList { get; private set; }

        UserService userService = new UserService();

        public int DefaultLoad = 2;
        int CurrentPage = 1;
        int PreviousPage = 1;
        public int skipItems = 0;

        public PageListViewModel()
        {
            pageList = getPageList().Result.Skip((CurrentPage - 1) * DefaultLoad).Take(DefaultLoad);
            ObsPageList = new ObservableCollection<MAUI_Demo_Service.Models.Page>(pageList);


            CurrentPage = CurrentPage + 1;

            IsMonthActive = true;
            IsListActive = true;
        }

        public Task<List<MAUI_Demo_Service.Models.Page>> getPageList()
        {
            return Task.Run(() => userService.GetPageList(TokenHolder.AccessToken));
        }
        
        [RelayCommand]
        public void Next()
        {
            IsListActive = true;
            pageList = getPageList().Result.Skip((CurrentPage - 1) * DefaultLoad).Take(DefaultLoad);
            
            if (pageList.Count() > 0)
            {
                ObsPageList = new ObservableCollection<MAUI_Demo_Service.Models.Page>(pageList);
                CurrentPage++;
                PreviousPage++;
                //collectionId2.ItemsSource = bookingsList;
            }
            else
            {
                IsMonthActive = false;
            }
        }
        [RelayCommand]
        public void Previous()
        {
            PreviousPage--;
            if (PreviousPage != 0)
            {
                pageList = getPageList().Result.Skip((PreviousPage - 1) * DefaultLoad).Take(DefaultLoad);
                if (pageList.Count() > 0)
                {
                    ObsPageList = new ObservableCollection<MAUI_Demo_Service.Models.Page>(pageList);
                    IsListActive = true;
                    //collectionId2.ItemsSource = bookingsList;

                    if (PreviousPage == 0 || PreviousPage == 1)
                    {
                        IsListActive = false;
                    }
                    CurrentPage--;
                    IsMonthActive = true;
                }
                else
                {
                    IsListActive = false;
                }
            }
        }
    }
}
