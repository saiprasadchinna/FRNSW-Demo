using MAUI_Demo_Service.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MAUI_Demo.ViewModels
{
    public class EmployeeListViewModel : INotifyPropertyChanged
    {
        List<MAUI_Demo_Service.Models.Employee> allEmployeeList;
        private int _pageSize = 5;
        public bool _isbusy;
        public ObservableCollection<MAUI_Demo_Service.Models.Employee> employeeList { get; set; } = new ObservableCollection<MAUI_Demo_Service.Models.Employee>();
        private readonly BookingService _bookingService;

        private const int LoadItemCount = 1;
        public int RemainingItemsTreshold
        {
            get { return LoadItemCount; }
        }

        private Command _loadMoreItemsCommand;
        public Command LoadMoreItemsCommand { get { return _loadMoreItemsCommand ?? (_loadMoreItemsCommand = new Command(() => LoadMoreItems())); } }


        public ICommand ReachedCommand => new Command(async () =>
        {
            _isbusy = false;
        });

        private int _itemCounter = 1;
        public ICommand ItemTresholdReachedCommand { get; set; }
        public EmployeeListViewModel(BookingService bookingService)
        {
            _bookingService = bookingService;

            ItemTresholdReachedCommand = new Command(async () => await ItemsTresholdReached());
            GetEmployees();

            LoadMoreItems();
        }
        private void GetEmployees()
        {
            employeeList.Clear();

            Task.Run(async () =>
            {
                allEmployeeList = await _bookingService.GetEmployees();

                App.Current.Dispatcher.Dispatch(() =>
                {
                    var recordTobeAdded = allEmployeeList.Take(_pageSize).ToList();
                    foreach (var item in recordTobeAdded)
                    {
                        employeeList.Add(item);
                    }
                });
            });
        }


        private void LoadMoreItems()
        {
            var recordToBeAdded = Items.Skip(Items.Count()).Take(_pageSize).ToList();
            for (int i = _itemCounter; i < _itemCounter + LoadItemCount; i++)
            {
                var item = new Item { Name = $"Item {i}" };
                Items.Add(item);
            }
            _itemCounter = _itemCounter + LoadItemCount;
        }

        public void OnCollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            // Custom logic
        }

        //public static readonly DependencyProperty ScrollChangedCommandProperty = DependencyProperty.RegisterAttached(
        //   "ScrollChangedCommand", typeof(ICommand), typeof(EmployeeListViewModel),
        //   new PropertyMetadata(default(ICommand), OnCollectionViewScrolled));
        private ICommand scrollCommand;


        //public static readonly BindableProperty SkuPoperty = BindableProperty.Create(nameof(MAUI_Demo.MVVM.ViewModels), typeof(EmployeeListViewModel), typeof(OnCollectionViewScrolled));


        private ObservableCollection<Item> _items = new ObservableCollection<Item>();
        public ObservableCollection<Item> Items
        {
            get { return _items; }
        }
        async Task ItemsTresholdReached()
        {

            try
            {

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
