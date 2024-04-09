using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jellyfin.Plugin.MetaTube.Configuration;

namespace Jellyfin.Plugin.MetaTube.Translation
{
    public static class TranslationGPT
    {
        private static PluginConfiguration Cfg => Plugin.Instance.Configuration;

        public static async Task<string> TranslationAsync(string sourceStr, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(sourceStr))
                return sourceStr;

            string apiUrl = Cfg.GPTTranslationUrl + "/translation";
            string data = $"\"{sourceStr}\"";
            string transStr = sourceStr;

            try
            {
                using (var httpClient = new HttpClient())
                {
                    HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage res = await httpClient.PostAsync(apiUrl, content, cancellationToken);
                    if (res.IsSuccessStatusCode)
                    {
                        transStr = await res.Content.ReadAsStringAsync(cancellationToken);
                    }
                }
            }
            catch (System.Exception)
            {

            }

            return transStr;
        }
    }
}