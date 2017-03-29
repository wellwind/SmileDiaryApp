using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using SmileDiaryApp.Events;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SmileDiaryApp;

namespace SmileDiaryApp.ViewModels
{
    public class SmileScorePageViewModel : BindableBase
    {
        #region DataSource
        private ObservableCollection<ChartDataPoint> _dataSource;

        /// <summary>
        /// DataSource
        /// </summary>
        public ObservableCollection<ChartDataPoint> DataSource
        {
            get { return _dataSource; }
            set { SetProperty(ref _dataSource, value); }
        }
        #endregion

        #region SmileBadges
        private ObservableCollection<SmileBadge> _smilaBadges;
        /// <summary>
        /// SmileBadges
        /// </summary>
        public ObservableCollection<SmileBadge> SmileBadges
        {
            get { return _smilaBadges; }
            set { SetProperty(ref _smilaBadges, value); }
        }
        #endregion


        private IFileService fileService;
        private IEventAggregator eventAggregator;

        public SmileScorePageViewModel(IFileService fileService, IEventAggregator eventAggregator)
        {
            this.fileService = fileService;
            this.eventAggregator = eventAggregator;

            this.eventAggregator.GetEvent<PhotoChangesEvent>().Subscribe(data =>
            {
                readDataSource(data);
            });

            initBadges();
        }

        private void readDataSource(IEnumerable<SmileRecord> records)
        {
            DataSource = new ObservableCollection<ChartDataPoint>();
            var dataService = new DataService(fileService);
            var last30DaysData = records
                .OrderByDescending(r => r.Date)
                .Take(30)
                .OrderBy(r => r.Date);
            foreach (var data in last30DaysData)
            {
                DataSource.Add(new ChartDataPoint(
                    data.Date,
                    Convert.ToDouble(data.Score.ToString("0.00"))));
            }
        }

        private void initBadges()
        {
            SmileBadges = new ObservableCollection<SmileBadge>();
            var badgeService = new BadgeService(fileService);
            var badgeData = badgeService.BadgeData;

            addBadgeRecord("第一天記錄微笑", badgeData.Badge001_FirstRecord);
            addBadgeRecord("連續三天記錄微笑", badgeData.Badge002_3DaysRecord);
            addBadgeRecord("連續十天記錄微笑", badgeData.Badge003_10DaysRecord);
            addBadgeRecord("連續三十天記錄微笑", badgeData.Badge004_30DaysRecord);
            addBadgeRecord("連續一百天記錄微笑", badgeData.Badge005_100DaysRecord);
            addBadgeRecord("連續三百天記錄微笑", badgeData.Badge006_300DaysRecord);

            addBadgeRecord("三天微笑指數超過80%", badgeData.Badge101_3Days80);
            addBadgeRecord("三天微笑指數超過90%", badgeData.Badge102_3Days90);
            addBadgeRecord("三天微笑指數達到100%", badgeData.Badge103_3Days100);

            addBadgeRecord("十天微笑指數達到100%", badgeData.Badge104_10Days100);
            addBadgeRecord("三十天微笑指數達到100%", badgeData.Badge105_30Days100);

            addBadgeRecord("三天微笑指數低於50%", badgeData.Badge801_3DaysLessThan50);
            addBadgeRecord("十天微笑指數低於50%", badgeData.Badge802_10DaysLessThan50);

        }

        public void addBadgeRecord(string name, bool state)
        {
            var stateName = state ? "已取得" : "未取得";
            SmileBadges.Add(new SmileBadge() { Name = name, State = stateName });
        }
    }
}
