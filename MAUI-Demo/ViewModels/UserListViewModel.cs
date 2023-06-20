//using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_Demo.Auth0;
using MAUI_Demo_Service.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MAUI_Demo.ViewModels
{
    public partial class UserListViewModel : ObservableObject, INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        [ObservableProperty]
        public ObservableCollection<MAUI_Demo_Service.Models.User> obsUserList;

        [ObservableProperty]
        private bool _isMonthActive;

        [ObservableProperty]
        private bool _IsListActive;



        public IEnumerable<MAUI_Demo_Service.Models.User> userList { get; set; }
     
        UserService userService = new UserService();

        public int DefaultLoad = 2;
        int CurrentPage = 1;
        int PreviousPage = 1;
        public int skipItems = 0;

        public UserListViewModel()
        {
            userList = getUserList().Result.Skip((CurrentPage - 1) * DefaultLoad).Take(DefaultLoad);
            ObsUserList = new ObservableCollection<MAUI_Demo_Service.Models.User>(userList);
            //            obsUserList = new ObservableCollection<MAUI_Demo_Service.Models.User>();
            //            //Binding the itemsource as Jason suggested
            //            obsUserList = new ObservableCollection<MAUI_Demo_Service.Models.User>
            //{
            //new User{Email = "Win"}
            //};
            
            CurrentPage = CurrentPage + 1;

            IsMonthActive = true;
            IsListActive = true;
        }

        public Task<List<MAUI_Demo_Service.Models.User>> getUserList()
        {
            return Task.Run(() => userService.GetUserList(TokenHolder.AccessToken));
        }

        [RelayCommand]
        public void Next()
        {
            IsListActive = true;
            userList = getUserList().Result.Skip((CurrentPage - 1) * DefaultLoad).Take(DefaultLoad);

            if (userList.Count() > 0)
            {
                ObsUserList = new ObservableCollection<MAUI_Demo_Service.Models.User>(userList);
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
                userList = getUserList().Result.Skip((PreviousPage - 1) * DefaultLoad).Take(DefaultLoad);
                if (userList.Count() > 0)
                {
                    ObsUserList = new ObservableCollection<MAUI_Demo_Service.Models.User>(userList);
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
