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

        #region SelectedRecord
        private SmileRecordListItem _selectedRecord;
        /// <summary>
        /// 選擇的紀錄
        /// </summary>
        public SmileRecordListItem SelectedRecord
        {
            get { return this._selectedRecord; }
            set
            {
                this.SetProperty(ref this._selectedRecord, value);

                if(value != null)
                {
                    goDetailPage();
                    SelectedRecord = null;
                }
            }
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

            eventAggregator.GetEvent<PhotoChangesEvent>().Subscribe(() =>
            {
                reloadList();
            });

            reloadList();

        }

        private async void goDetailPage()
        {
            NavigationParameters param = new NavigationParameters();
            param.Add("record", SelectedRecord);
            await navigationService.NavigateAsync("SmileListItemPage", param);
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
