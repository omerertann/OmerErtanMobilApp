using Newtonsoft.Json.Linq;

namespace OmerErtanMobilApp;

public partial class KurlarPage : ContentPage
{
    public KurlarPage()
    {
        InitializeComponent();
        KurlariGetir();
    }

    public class KurGoster
    {
        public string Kod { get; set; }
        public string Satis { get; set; }
    }

    private async void KurlariGetir()
    {
        loadingIndicator.IsVisible = true;

        try
        {
            var url = "https://finans.truncgil.com/today.json";
            var client = new HttpClient();
            var json = await client.GetStringAsync(url);

            var jObject = JObject.Parse(json);
            var liste = new List<KurGoster>();

            foreach (var item in jObject)
            {
                if (item.Key == "Update_Date") continue;

                var satis = item.Value["Satýþ"]?.ToString();
                if (!string.IsNullOrEmpty(satis))
                {
                    liste.Add(new KurGoster
                    {
                        Kod = item.Key,
                        Satis = satis
                    });
                }
            }

            kurListesi.ItemsSource = liste;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", "Kurlar alýnamadý: " + ex.Message, "Tamam");
        }

        loadingIndicator.IsVisible = false;
    }
}
