using Prism.Unity;
using SmileDiaryApp.Views;

namespace SmileDiaryApp
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("MainTabbedPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MainTabbedPage>();
            Container.RegisterTypeForNavigation<TakePicturePage>();
            Container.RegisterTypeForNavigation<SmileListPage>();
            Container.RegisterTypeForNavigation<SmileScorePage>();
            Container.RegisterTypeForNavigation<SmileListViewPage>();
        }
    }
}
