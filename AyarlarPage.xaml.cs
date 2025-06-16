namespace OmerErtanMobilApp;

public partial class AyarlarPage : ContentPage
{
    public AyarlarPage()
    {
        InitializeComponent();
    }

    private void OnKoyuModClicked(object sender, EventArgs e)
    {
        Application.Current.UserAppTheme = AppTheme.Dark;
    }

    private void OnAcikModClicked(object sender, EventArgs e)
    {
        Application.Current.UserAppTheme = AppTheme.Light;
    }
}
