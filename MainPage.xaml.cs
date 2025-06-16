namespace OmerErtanMobilApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void KurlarTikla(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new KurlarPage());
    }

    private async void HaberlerTikla(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HaberlerPage());
    }

    private async void HavaTikla(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HavaPage());
    }
}
