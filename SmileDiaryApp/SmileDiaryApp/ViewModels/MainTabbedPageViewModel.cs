using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using SmileDiaryApp.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmileDiaryApp.ViewModels
{
    public class MainTabbedPageViewModel : BindableBase
    {
        public MainTabbedPageViewModel(IFileService fileService, IEventAggregator eventAggregator)
        {
            var dataService = new DataService(fileService);
            var records = dataService.LoadPhotoData();
            eventAggregator.GetEvent<PhotoChangesEvent>().Publish(records.ToList());
        }
    }
}
