using System.Text;
using Newtonsoft.Json;

namespace OmerErtanMobilApp;

public partial class LoginPage : ContentPage
{
    private const string WebAPIKey = "AIzaSyDaqqFSuzbhEiTaqkq4JAGVUsSA2owINQ8";

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var email = emailEntry.Text;
        var password = passwordEntry.Text;

        var loginUrl = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={WebAPIKey}";
        var client = new HttpClient();

        var payload = new
        {
            email = email,
            password = password,
            returnSecureToken = true
        };

        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync(loginUrl, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Giriş Başarılı", "Hoş geldin!", "Tamam");
                Application.Current.MainPage = new AppShell();

            }
            else
            {
                errorLabel.Text = "Giriş Hatası: " + responseBody;
                errorLabel.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            errorLabel.Text = "Bağlantı Hatası: " + ex.Message;
            errorLabel.IsVisible = true;
        }
    }

    private async void GoToRegister(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
}
