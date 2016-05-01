using System.Net;

namespace L6POC
{
    class DownloadExternalTools
    {
        public static void DownloadCCleaner()
        {
            using(WebClient webClient = new WebClient())
            {
                webClient.DownloadFile("https://www.piriform.com/ccleaner/download/standard/", "../../3rd Party Tools/CCleaner.exe");
            }
        }
    }
}
