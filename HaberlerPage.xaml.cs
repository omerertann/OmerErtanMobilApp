using Newtonsoft.Json;
using Microsoft.Maui.ApplicationModel.DataTransfer;

namespace OmerErtanMobilApp;

public class HaberItem
{
    public string title { get; set; }
    public string link { get; set; }
    public string description { get; set; }
    public string pubDate { get; set; }
    public string thumbnail { get; set; } // RESİM desteği
}

public class HaberRoot
{
    public List<HaberItem> items { get; set; }
}

public partial class HaberlerPage : ContentPage
{
    private List<HaberItem> tumHaberler = new();

    public HaberlerPage()
    {
        InitializeComponent();
        HaberleriYukle();
    }

    private async void HaberleriYukle()
    {
        string[] rssUrlListesi =
        {
            "https://www.trthaber.com/manset_articles.rss",
            "https://www.trthaber.com/sondakika_articles.rss",
            "https://www.trthaber.com/gundem_articles.rss",
            "https://www.trthaber.com/ekonomi_articles.rss",
            "https://www.trthaber.com/spor_articles.rss"
        };

        try
        {
            var client = new HttpClient();

            foreach (string rssUrl in rssUrlListesi)
            {
                string apiUrl = $"https://api.rss2json.com/v1/api.json?rss_url={rssUrl}";
                var response = await client.GetStringAsync(apiUrl);
                var haberData = JsonConvert.DeserializeObject<HaberRoot>(response);

                if (haberData != null && haberData.items != null)
                    tumHaberler.AddRange(haberData.items);
            }

            haberListesi.ItemsSource = tumHaberler;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", "Haberler alınamadı: " + ex.Message, "Tamam");
        }
    }

    private async void OnHaberSecildi(object sender, SelectionChangedEventArgs e)
    {
        var secilen = e.CurrentSelection.FirstOrDefault() as HaberItem;
        if (secilen != null)
        {
            await Navigation.PushAsync(new ContentPage
            {
                Title = "Haber Detayı",
                Content = new ScrollView
                {
                    Content = new StackLayout
                    {
                        Padding = 15,
                        Spacing = 15,
                        Children =
                        {
                            new Label { Text = secilen.title, FontSize = 20, FontAttributes = FontAttributes.Bold },
                            new Label { Text = secilen.pubDate, FontSize = 12, TextColor = Colors.Gray },
                            new Image { Source = secilen.thumbnail, HeightRequest = 200 },
                            new Label { Text = secilen.description },
                            new Button
                            {
                                Text = "Haberi Paylaş",
                                Command = new Command(async () =>
                                {
                                    await Share.RequestAsync(new ShareTextRequest
                                    {
                                        Uri = secilen.link,
                                        Title = "Haberi Paylaş"
                                    });
                                })
                            }
                        }
                    }
                }
            });
        }

        haberListesi.SelectedItem = null;
    }
}
