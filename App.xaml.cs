namespace OmerErtanMobilApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new LoginPage(); // İlk açılışta kullanıcı girişi
    }

    // Giriş başarılı olduğunda çağırılır
    public void GirisBasariliysaYonlendir()
    {
        MainPage = new AppShell(); // Hamburger menülü Shell'e geç
    }
}
