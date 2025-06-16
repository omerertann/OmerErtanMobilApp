using Microsoft.Maui.ApplicationModel.DataTransfer;

namespace OmerErtanMobilApp;

public partial class HaberDetayPage : ContentPage
{
    private string _link;

    public HaberDetayPage(HaberItem haber)
    {
        InitializeComponent();
        titleLabel.Text = haber.title;
        descriptionLabel.Text = haber.description;
        _link = haber.link;
    }

    private async void PaylasClicked(object sender, EventArgs e)
    {
        await Share.RequestAsync(new ShareTextRequest
        {
            Uri = _link,
            Title = "Haberi Paylaþ"
        });
    }
}
