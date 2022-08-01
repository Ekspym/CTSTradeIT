using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace CTSTradeIT
{
    internal class HtmlCareerReader
    {
        private int offersCount = 0;
        HtmlWeb Careers = new HtmlWeb();
        HtmlWeb WhatToExpect = new HtmlWeb();
        List<String> jobOffers = new List<String>();
        public HtmlCareerReader()
        {

        }

        /// <summary>
        /// Metoda na načtení nabídek práce do třídy. Možnost vypsání jobů do commandové řádky.
        /// </summary>
        /// <param name="print"></param>
        /// <returns>Vrací list stringů s názvy pracovních nabídek, pro případ dalšího použítí</returns>
        public List<String> getAllJobOffers(bool print)
        {

            HtmlDocument doc = Careers.Load("https://www.cts-tradeit.cz/kariera/");
            HtmlNodeCollection NodesNamesForUrl = doc.DocumentNode.SelectNodes("//a[@class=\"card card-lg card-link-bottom\"]"); //2x Node. 1 pro název, druhý pro url v downloadWhatToExpectSection()
            HtmlNodeCollection Nodes = doc.DocumentNode.SelectNodes("//h3[@class=\"card-title mb-0\"]");
            for (int z = 0; Nodes.Count > z; z++)
            {
                string jobOffer = Nodes[z].InnerText;
                jobOffer = Regex.Replace(jobOffer, @"\s+", " ");
                string jobOfferForUrl = NodesNamesForUrl[z].Attributes["href"].Value.Remove(0, 9);
                jobOfferForUrl = jobOfferForUrl.Remove(jobOfferForUrl.Length - 1, 1);
                jobOffers.Add(jobOfferForUrl);
                if (print == true)
                {
                    Console.WriteLine(z + 1 + "." + jobOffer);

                }
            }
          
            return jobOffers;
        }

        /// <summary>
        /// Zapíše sekcí zvoleného jobu pomocí <param name="numberOfJob"/> a podle pořadí v Listu JobOffers
        /// </summary>
        /// <param name="numberOfJob"></param>
        public void downloadWhatToExpectSection(int numberOfJob)
        {
            if (Careers.ResponseUri == null)
                getAllJobOffers(false);

            HtmlDocument doc = Careers.Load("https://www.cts-tradeit.cz/kariera/" + jobOffers[numberOfJob - 1]);
            HtmlNodeCollection Nodes = doc.DocumentNode.SelectNodes("//div[@class=\"container-9 mb-md-5\"]"); //ul[@class=\"list-check\"] pokud budete chtít jen vše pod "Co tě čeká"

            string htmlValue = Nodes[0].InnerText;
            htmlValue = Regex.Replace(htmlValue, @"\s+", " "); // Mazání formátování

            File.WriteAllText(jobOffers[numberOfJob - 1] + ".txt", htmlValue);
        }

    }
}


