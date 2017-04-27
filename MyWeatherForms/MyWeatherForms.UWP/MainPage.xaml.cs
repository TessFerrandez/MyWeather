namespace MyWeatherForms.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            LoadApplication(new MyWeatherForms.App());
        }
    }
}