using System.Net;

namespace L6POC
{
    class DownloadExternalTools
    {
        public static void DownloadCCleaner()
        {
            using(WebClient webClient = new WebClient())
            {
                webClient.DownloadFile("http://download.piriform.com/ccsetup517.exe", "../../3rd Party Tools/CCleaner.exe");
            }
        }
    }
}
