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



        public SmileListItemPageViewModel()
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            this._item = parameters["record"] as SmileRecordListItem;
            Photo = _item.ImageSource;
            Score = _item.Score;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
