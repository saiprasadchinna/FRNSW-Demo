using MAUI_Demo.MVVM.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MAUI_Demo.MVVM.Views;

public partial class EmployeeView : ContentPage
{
    public class Item
    {
        public string Name { get; set; }
    }
    public EmployeeView(EmployeeListViewModel viewmodel)
    {
        InitializeComponent();
        ItemListView.RemainingItemsThreshold = 5;
        ItemListView.RemainingItemsThresholdReached += MyCollectionView_RemainingItemsThresholdReached;
        this.BindingContext = viewmodel;
        //LoadMoreItems();
    }
    private const int LoadItemCount = 200;
    public int RemainingItemsTreshold
    {
        get { return LoadItemCount; }
    }

    private Command _loadMoreItemsCommand;
    public Command LoadMoreItemsCommand { get { return _loadMoreItemsCommand ?? (_loadMoreItemsCommand = new Command(() => LoadMoreItems())); } }

    private int _itemCounter = 1;
    private void LoadMoreItems()
    {
        for (int i = _itemCounter; i < _itemCounter + LoadItemCount; i++)
        {
            var item = new Item { Name = $"Item {i}" };
            Items.Add(item);
        }
        _itemCounter = _itemCounter + LoadItemCount;
    }
    private void MyCollectionView_RemainingItemsThresholdReached(object sender, EventArgs e)
    {
        foreach (var s in GetItems(15))
        {
            var item = new Item { Name = $"Item {s}" };
            Items.Add(item);
        }
    }


    private readonly Random randomizer = new Random();
    private List<string> GetItems(int numberOfItems)
    {
        var resultList = new List<string>();

        for (var i = 0; i <= numberOfItems; i++)
        {
            resultList.Add(randomizer.Next(10000, 99999).ToString());
        }

        return resultList;
    }

    private ObservableCollection<Item> _items = new ObservableCollection<Item>();
    public ObservableCollection<Item> Items
    {
        get { return _items; }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void clnEmployee_Scrolled(object sender, ItemsViewScrolledEventArgs e)
    {

    }
}