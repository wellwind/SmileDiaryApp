using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Plugin.Media;
using Prism.Services;
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

		public DelegateCommand TakePictureCommand { get; private set; }

		public MainPageViewModel(IPageDialogService dialogService)
		{
			TakePictureCommand = new DelegateCommand(async () =>
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
					DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front
				});

				if (file == null)
					return;

				Photo = ImageSource.FromFile(file.Path);

				return;
			});

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

