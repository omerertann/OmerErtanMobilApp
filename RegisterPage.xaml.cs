using System.Text;
using Newtonsoft.Json;

namespace OmerErtanMobilApp;

public partial class RegisterPage : ContentPage
{
    private const string WebAPIKey = "\r\nAIzaSyDaqqFSuzbhEiTaqkq4JAGVUsSA2owINQ8"; 
    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        var httpClient = new HttpClient();
        var signupUrl = $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={WebAPIKey}";

        var payload = new
        {
            email = emailEntry.Text,
            password = passwordEntry.Text,
            returnSecureToken = true
        };

        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(signupUrl, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("Baþarýlý", "Kayýt baþarýlý!", "Tamam");
            await Navigation.PushAsync(new LoginPage());
        }
        else
        {
            errorLabel.Text = "Kayýt Hatasý: " + responseContent;
            errorLabel.IsVisible = true;
        }
    }

    private async void GoToLogin(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }
}
