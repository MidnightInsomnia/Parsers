using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace YouTubeSearchLinksParser
{
    public class YTParser
    {
        private static List<string> videoLinks = new List<string>();
        private static string query;
        public static string Query
        {
            get
            {
                return query;
            }
            set
            {
                //Меняет символы в запросе на + для корректного построения ссылки
                System.Text.StringBuilder sb = new System.Text.StringBuilder(value);

                for (int i = 0; i < sb.Length; i++)
                {
                    if (Char.IsSymbol(sb[i]) || Char.IsPunctuation(sb[i]) || Char.IsSeparator(sb[i]))
                    {
                        sb[i] = '+';
                    }
                }
                query = sb.ToString();
            }
        }
        public YTParser(string videoName)
        {
            Query = videoName;
        }
        public async Task<List<string>> GetLinksAsync()
        {
            string url = $"https://www.youtube.com/results?search_query={query}";
            ////Получение HTML документа
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            foreach (HtmlNode link in htmlDocument.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute att = link.Attributes["href"];
                //Отбирает только ссылки на видео                                
                if (att.Value.Contains("/watch?v="))
                {
                    videoLinks.Add(att.Value);
                }
            }
            return videoLinks;
        }
    }
}
