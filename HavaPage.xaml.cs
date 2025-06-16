using Newtonsoft.Json;
using System.Net.Http;
using OmerErtanMobilApp.Helpers; // ✅ buraya ekle
using OmerErtanMobilApp.Models;  // ✅ buraya ekle

namespace OmerErtanMobilApp;

public partial class HavaPage : ContentPage
{
    public HavaPage()
    {
        InitializeComponent();
    }

    // geri kalan kod burada devam eder...

    private async void OnGetirClicked(object sender, EventArgs e)
    {
        string sehir = sehirEntry.Text;
        if (string.IsNullOrWhiteSpace(sehir))
        {
            await DisplayAlert("Uyarı", "Lütfen bir şehir adı girin.", "Tamam");
            return;
        }

        string apiKey = "776a4299caebe0e74951c2b4eb91bbc1"; // OpenWeatherMap API anahtarını buraya ekle
        string url = $"https://api.openweathermap.org/data/2.5/weather?q={sehir}&appid={apiKey}&units=metric&lang=tr";

        try
        {
            var client = new HttpClient();
            var response = await client.GetStringAsync(url);
            dynamic weatherData = JsonConvert.DeserializeObject(response);

            string durum = weatherData.weather[0].description;
            string derece = weatherData.main.temp;

            havaSonucLabel.Text = $"{sehir} için hava durumu: {durum}, {derece}°C";
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", "Hava durumu alınamadı: " + ex.Message, "Tamam");
        }
    }
}
