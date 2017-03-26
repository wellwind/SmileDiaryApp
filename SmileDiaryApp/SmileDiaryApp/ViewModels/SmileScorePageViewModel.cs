using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using SmileDiaryApp.Events;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
    }
}
