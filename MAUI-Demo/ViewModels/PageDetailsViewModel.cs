using CommunityToolkit.Mvvm.ComponentModel;
using MAUI_Demo_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_Demo.ViewModels
{
    [QueryProperty(nameof(PageDetails), "SelectedPageItem")]
    [QueryProperty(nameof(PageRole), "SelectedPageRole")]
    public partial class PageDetailsViewModel : ObservableObject
    {

        [ObservableProperty]
        public MAUI_Demo_Service.Models.Page pageDetails;

        [ObservableProperty]
        public MAUI_Demo_Service.Models.Role pageRole;
        public PageDetailsViewModel()
        {
            
        }
    }
}
