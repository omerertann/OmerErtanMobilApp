using System.Text.Json;
using OmerErtanMobilApp.Models;

namespace OmerErtanMobilApp.Helpers
{
    public static class DosyaYardimcisi
    {
        private static readonly string DosyaYolu = Path.Combine(FileSystem.AppDataDirectory, "sehirler.json");

        public static async Task<List<SehirItem>> SehirleriYukleAsync()
        {
            if (!File.Exists(DosyaYolu))
                return new List<SehirItem>();

            var json = await File.ReadAllTextAsync(DosyaYolu);
            return JsonSerializer.Deserialize<List<SehirItem>>(json) ?? new();
        }

        public static async Task SehirEkleAsync(SehirItem yeni)
        {
            var liste = await SehirleriYukleAsync();
            if (!liste.Any(s => s.Isim.Equals(yeni.Isim, StringComparison.OrdinalIgnoreCase)))
                liste.Add(yeni);

            var json = JsonSerializer.Serialize(liste, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(DosyaYolu, json);
        }
    }
}
