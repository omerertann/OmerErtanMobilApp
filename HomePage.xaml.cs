namespace OmerErtanMobilApp;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void OnContinueClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new KurlarPage());


        // Burada ba�ka bir sayfaya ge�i� yapabiliriz
        // �rnek: await Navigation.PushAsync(new MenuPage());
    }
}
