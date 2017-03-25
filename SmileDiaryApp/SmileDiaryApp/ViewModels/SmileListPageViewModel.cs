using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SmileDiaryApp.ViewModels
{
    public class SmileListPageViewModel : BindableBase
    {
        public SmileListPageViewModel(INavigationService navigationService)
        {
            navigationService.NavigateAsync("SmileListViewPage");
        }
    }
}
