using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using HtmlAgilityPack;


namespace foolin_around
{
    class Answer
    {
        public string title { get; set; }
        public string link { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {

            List<Answer> posLinks = searchWeb();
            Console.WriteLine("Your options are: ");
            int i = 0;
            foreach (var item in posLinks)
            {
                Console.WriteLine(i + " --- " + ((Answer)item).title);
                i++;
            }
            i = int.Parse(Console.ReadLine());
            Console.WriteLine("\n\n\nThat was the search page, now for the album page,\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            searchWebAgain(posLinks[i].link);
            Console.Write("maybe");
            Console.Read();
        }
        static List<Answer> searchWeb()
        {
            HtmlWeb webHandler = new HtmlWeb();
            HtmlDocument doc = webHandler.Load("http://www.mostlymusic.com/catalogsearch/result/?q=lipa&cat=");
            HtmlNode node = doc.DocumentNode;
            var s = doc.DocumentNode.SelectNodes("//div[@class='product-name']");
            var d = from p in s
                    select new Answer
                       {
                           title = p.FirstChild.Attributes.Where(c => c.Name == "title").First().Value,
                           link = p.FirstChild.Attributes.Where(c => c.Name == "href").First().Value
                       };
            return d.ToList();                 
            
        }
        static void searchWebAgain(string url)
        {
            HtmlWeb webHandler = new HtmlWeb();
            HtmlDocument doc = webHandler.Load(url);
            HtmlNode node = doc.DocumentNode;
            var s = node.SelectNodes("//tr[@class='info']");
            var d = from p in s
                    select new
                    {
                        num = (from g in p.DescendantNodes()
                               where g.Attributes.FirstOrDefault(t=>t.Value=="number")!=null
                               select int.Parse(g.InnerText.Replace(".",""))).First(),
                        title = p.DescendantNodes().Where(g => g.Name == "label").First().InnerText
                    };
            foreach (var item in d)
            {
                Console.WriteLine("{0,-10}{1}", item.num, item.title);
            }
        }
    }
}
