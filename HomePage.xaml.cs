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


        // Burada baþka bir sayfaya geçiþ yapabiliriz
        // Örnek: await Navigation.PushAsync(new MenuPage());
    }
}
