using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

public static class VersionChecker
{
    private static readonly string CurrentVersion = "1.0.1";
    private static readonly string VersionUrl = "https://raw.githubusercontent.com/mmtcoder/Metin2-FishBot-AutoFish-Balikbotu-otomatik-balik-botu-metin2-level-botu-metin2-enerji-botu/main/version.txt";
    private static readonly string UpdatePageUrl = "https://github.com/mmtcoder/Metin2-FishBot-AutoFish-Balikbotu-otomatik-balik-botu-metin2-level-botu-metin2-enerji-botu";

    public static void CheckForUpdate()
    {
        try
        {
            using (WebClient client = new WebClient())
            {
                string latestVersion = client.DownloadString(VersionUrl).Trim();
                //Console.WriteLine("latesversion = " + latestVersion);
                if (!latestVersion.Equals(CurrentVersion))
                {
                    DialogResult result = MessageBox.Show(
                         $"Yeni sürüm mevcut: {latestVersion}\nGüncelleme sayfasına gitmek ister misiniz?",
                         "Güncelleme Mevcut",
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Information
                     );
                    if (result == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = UpdatePageUrl,
                            UseShellExecute = true
                        });
                    }
                }
                else
                {
                    Console.WriteLine("Program zaten güncel.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Sürüm kontrolü başarısız: " + ex.Message);
        }
    }
}
