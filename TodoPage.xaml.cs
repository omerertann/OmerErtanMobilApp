using OmerErtanMobilApp.Models;
using System.Text.Json;

namespace OmerErtanMobilApp.Views;

public partial class TodoPage : ContentPage
{
    private List<TodoItem> todoItems = new();

    public TodoPage()
    {
        InitializeComponent();
        Yükle();
    }

    private async void OnEkleClicked(object sender, EventArgs e)
    {
        string baslik = await DisplayPromptAsync("Yeni Görev", "Baþlýk girin:");
        if (string.IsNullOrWhiteSpace(baslik)) return;

        string detay = await DisplayPromptAsync("Detay", "Detay girin:");
        DateTime tarih = DateTime.Now;

        todoItems.Add(new TodoItem { Baslik = baslik, Detay = detay, Tarih = tarih, YapildiMi = false });
        Kaydet();
        todoListesi.ItemsSource = null;
        todoListesi.ItemsSource = todoItems;
    }

    private void OnSilClicked(object sender, EventArgs e)
    {
        var item = (sender as SwipeItem).BindingContext as TodoItem;
        if (item != null)
        {
            todoItems.Remove(item);
            Kaydet();
            todoListesi.ItemsSource = null;
            todoListesi.ItemsSource = todoItems;
        }
    }

    private void Kaydet()
    {
        string json = JsonSerializer.Serialize(todoItems);
        Preferences.Set("TodoListesi", json);
    }

    private void Yükle()
    {
        string json = Preferences.Get("TodoListesi", "");
        if (!string.IsNullOrEmpty(json))
        {
            todoItems = JsonSerializer.Deserialize<List<TodoItem>>(json);
            todoListesi.ItemsSource = todoItems;
        }
    }
}
