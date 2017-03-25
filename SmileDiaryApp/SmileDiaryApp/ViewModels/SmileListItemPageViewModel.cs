using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace SmileDiaryApp.ViewModels
{
    public class SmileListItemPageViewModel : BindableBase, INavigationAware
    {
        SmileRecordListItem _item;

        #region Photo
        private ImageSource _photo;
        /// <summary>
        /// Photo
        /// </summary>
        public ImageSource Photo
        {
            get { return this._photo; }
            set { this.SetProperty(ref this._photo, value); }
        }
        #endregion

        #region Score
        private string _score;
        /// <summary>
        /// Score
        /// </summary>
        public string Score
        {
            get { return this._score; }
            set { this.SetProperty(ref this._score, value); }
        }
        #endregion

        #region Title
        private string _title;
        /// <summary>
        /// PropertyDescription
        /// </summary>
        public string Title
        {
            get { return this._title; }
            set { this.SetProperty(ref this._title, value); }
        }
        #endregion

        public SmileListItemPageViewModel()
        {

        }
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            _item = parameters["record"] as SmileRecordListItem;
            Photo = _item.ImageSource;
            Score = _item.Score;
            Title = String.Format("{0} 微笑指數", _item.Date);
        }

    }
}
