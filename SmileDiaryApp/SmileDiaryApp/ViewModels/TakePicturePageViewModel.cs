using Microsoft.ProjectOxford.Emotion;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace SmileDiaryApp.ViewModels
{
    public class TakePicturePageViewModel : BindableBase
    {
        private MediaFile _currentFile;
        private double _score;

        #region Binding Properties

        #region 是否可以拍照或選照片
        private bool _canDoTakePicture;
        /// <summary>
        /// 是否可以拍照或選照片
        /// </summary>
        public bool CanDoTakePicture
        {
            get { return this._canDoTakePicture; }
            set { this.SetProperty(ref this._canDoTakePicture, value); }
        }
        #endregion

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

        #region 是否可以確認選擇照片
        private bool _canConfirmPicture;
        /// <summary>
        /// 是否可以確認選擇照片
        /// </summary>
        public bool CanConfirmPicture
        {
            get { return this._canConfirmPicture; }
            set { this.SetProperty(ref this._canConfirmPicture, value); }
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

        #region 選擇的圖片來源
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
        private IFileService fileService;
        #endregion

        #region Commands
        public DelegateCommand TakePictureCommand { get; private set; }
        public DelegateCommand SelectFromAlbumCommand { get; private set; }
        public DelegateCommand UsePictureCommand { get; private set; }
        #endregion

        public TakePicturePageViewModel(IPageDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;

            HasGotPicture = false;
            CanConfirmPicture = false;
            CanDoTakePicture = true;

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
            getEmotionResult(file);
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
            getEmotionResult(file);
            return;
        }

        private async void getEmotionResult(MediaFile file)
        {
            _currentFile = file;

            var emotionServiceClient = new EmotionServiceClient(Config.EmotionApiKey);
            using (Stream stream = file.GetStream())
            {
                _score = 0;
                IsLoading = true;
                CanDoTakePicture = false;
                CanConfirmPicture = false;
                EmotionResultText = "分析中...";
                try
                {
                    await analyzePictureToResult(emotionServiceClient, stream);
                }
                catch
                {
                    EmotionResultText = "分析過程失敗，請檢查網路是否暢通，或換張圖片，或稍後再試試看";
                }
                
                IsLoading = false;
                CanDoTakePicture = true;
            }
        }

        private async System.Threading.Tasks.Task analyzePictureToResult(EmotionServiceClient emotionServiceClient, Stream stream)
        {
            var emotionResult = await emotionServiceClient.RecognizeAsync(stream);
            if (emotionResult.Length == 0)
            {
                EmotionResultText = "找不到人臉可以分析，建議換張清晰點的照片唷";
            }
            else if (emotionResult.Length > 1)
            {
                EmotionResultText = "太多張臉了，我分不出你是誰，換張獨照好嗎@@?";
            }
            else
            {
                _score = emotionResult[0].Scores.Happiness * 100;
                EmotionResultText = String.Format("微笑指數：{0}%", _score.ToString("0.00"));
                CanConfirmPicture = true;
            }
        }

        private void usePictureCommand()
        {
            var dataService = new DataService(fileService);
            dataService.SavePhotoData(_currentFile, _score);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

        }
    }
}
