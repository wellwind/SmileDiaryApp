using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using SmileDiaryApp.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace SmileDiaryApp.ViewModels
{
    public class SmileListViewPageViewModel : BindableBase
    {
        private IEventAggregator eventAggregator;

        #region Properties
        #region 微笑清單
        private ObservableCollection<SmileRecordListItem> _smileRecords;

        /// <summary>
        /// 微笑清單
        /// </summary>
        public ObservableCollection<SmileRecordListItem> SmileRecords
        {
            get { return _smileRecords; }
            set { SetProperty(ref _smileRecords, value); }
        }
        #endregion
        #endregion

        #region Services
        private IFileService fileService;
        private INavigationService navigationService;
 
        #endregion

        public SmileListViewPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IFileService fileService)
        {
            this.navigationService = navigationService;
            this.fileService = fileService;
            this.eventAggregator = eventAggregator;

            reloadList();

            eventAggregator.GetEvent<PhotoChangesEvent>().Subscribe(() =>
            {
                reloadList();
            });
        }

        private void reloadList()
        {
            var dataService = new DataService(fileService);
            SmileRecords = new ObservableCollection<SmileRecordListItem>();
            foreach (var record in dataService.LoadPhotoData().OrderByDescending(rec => rec.Date))
            {
                 SmileRecords.Add(new SmileRecordListItem()
                {
                    Date = record.Date,
                    ImageSource = dataService.GetPhotoPath(record.Date.Replace("/", "")),
                    Score = String.Format("微笑指數: {0}%", record.Score.ToString("0.00"))
                });
            }
        }
    }
}
