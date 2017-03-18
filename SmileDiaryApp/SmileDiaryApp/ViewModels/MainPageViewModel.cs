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
    public class MainPageViewModel : BindableBase, INavigationAware
    {
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

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private IPageDialogService dialogService;

        public DelegateCommand TakePictureCommand { get; private set; }
        public DelegateCommand SelectFromAlbumCommand { get; private set; }

        public MainPageViewModel(IPageDialogService dialogService)
        {
            this.dialogService = dialogService;
            TakePictureCommand = new DelegateCommand(takePictureCommand);
            SelectFromAlbumCommand = new DelegateCommand(selectFromAlbumCommand);
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
            return;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("title"))
                Title = (string)parameters["title"] + " and Prism";
        }
    }
}
