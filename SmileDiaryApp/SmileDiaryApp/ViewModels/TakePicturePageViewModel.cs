using Plugin.Media;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace SmileDiaryApp.ViewModels
{
    public class TakePicturePageViewModel : BindableBase
    {
        #region Binding Properties

        #region 是否正在讀取中
        private bool _isLoading;

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                SetProperty(ref _isLoading, value);
            }
        }
        #endregion

        #region 是否已選取照片
        private bool _hasGotPicture;

        public bool HasGotPicture
        {
            get
            {
                return _hasGotPicture;
            }
            set
            {
                SetProperty(ref _hasGotPicture, value);
            }
        }
        #endregion

        #region 判斷後的情緒結果
        private string _emotionResultText;

        public string EmotionResultText
        {
            get
            {
                return _emotionResultText;
            }
            set
            {
                SetProperty(ref _emotionResultText, value);
            }
        }
        #endregion

        #region 選擇的圖片
        private ImageSource _ptoho;

        public ImageSource Photo
        {
            get
            {
                return _ptoho;
            }
            set
            {
                SetProperty(ref _ptoho, value);
            }
        }
        #endregion

        #endregion

        #region Services
        private IPageDialogService dialogService;
        #endregion

        #region Commands
        public DelegateCommand TakePictureCommand { get; private set; }
        public DelegateCommand SelectFromAlbumCommand { get; private set; }
        public DelegateCommand UsePictureCommand { get; private set; }
        #endregion

        public TakePicturePageViewModel(IPageDialogService dialogService)
        {
            this.dialogService = dialogService;
            HasGotPicture = false;

            TakePictureCommand = new DelegateCommand(takePictureCommand);
            SelectFromAlbumCommand = new DelegateCommand(selectFromAlbumCommand);
            UsePictureCommand = new DelegateCommand(usePictureCommand);
        }

        private async void takePictureCommand()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await dialogService.DisplayAlertAsync("沒有可用相機", "您的手機沒有相機可以用！？天阿！快去買新的吧！！", "這就去買！");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                Directory = "SmileDiary",
                Name = DateTime.Now.ToString("yyyyMMddHHmmss"),
                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front,
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
            });

            if (file == null)
                return;

            Photo = ImageSource.FromFile(file.Path);
            HasGotPicture = true;
            return;
        }

        private async void selectFromAlbumCommand()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await dialogService.DisplayAlertAsync("權限不足", "您沒有取用相簿的權限，沒辦法從相簿選擇照片喔", "好吧");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions()
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
            });
            if (file == null)
                return;

            Photo = ImageSource.FromFile(file.Path);
            HasGotPicture = true;
            return;
        }

        private void usePictureCommand()
        {

        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

        }
    }
}
