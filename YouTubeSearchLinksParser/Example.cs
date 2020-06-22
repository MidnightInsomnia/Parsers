using System;

namespace YouTubeSearchLinksParser
{
    class Example
    {
        static void Main(string[] args)
        {
            //Создаём объект класса и передаём конструктору поисковый запрос
            YTParser parser = new YTParser(videoName: "you're gonna go far kid");
            //Пользуемся результатом
            var result = parser.GetLinksAsync().Result;
            Console.WriteLine(result[0]);
            Console.WriteLine(result.Count);
        }
    }
}
