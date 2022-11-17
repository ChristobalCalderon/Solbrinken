using System;
using System.Net.Http;
using HtmlAgilityPack;

namespace Solbrinken
{
    class Program
    {
        const string APIURL = "https://www.nacka.se/stadsutveckling-trafik/har-planerar-och-bygger-vi/sok-projekt-pa-namn/sydostra-boo/solbrinken-grundet/#panel-startpage";
        const string CLASS = "c-timestamp";


        static void Main(string[] args)
        {
            Console.WriteLine("Check if Nacka Kommun has updated their page!!");
            var client = new HttpClient();

            var response = client.GetStringAsync(APIURL).GetAwaiter();
            var result = response.GetResult();

            var doc = new HtmlDocument();
            doc.LoadHtml(result);

            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//div[@class='" + CLASS + "']"))
            {
                string value = node.InnerText.Trim()
                            .Replace("\r", "")
                            .Replace("\n", "")
                            .Replace("    ", "")
                            .Replace("\t", "");

                if (!value.Contains("30 september 2022 KL 15:42"))
                {
                    throw new Exception($"Page has been updated! Value: {value}");
                }
            }

            Console.WriteLine("No update!");
        }
    }
}
