using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using SerializationPerformanceTest.TestData.BelgianBeer;
using SerializationPerformanceTest.Testers;


namespace SerializationPerformanceTest
{
    internal static class Program
    {
        private static void Main()
        {
            var beers = BelgianBeerDataRetriever.GetDataFromWikipedia();
            List<Beer> beersList = BelgianBeerDataRetriever.GetDataFromXML();
            Beer testBeer = beersList.First();

            var testers = new SerializationTester[]
                {
                    //List of beers
                    new DataContract<List<Beer>>(beersList),
                    new XmlSerializer<List<Beer>>(beersList),
                    new Binary<List<Beer>>(beersList),
                    new JsonNewtonsoft<List<Beer>>(beersList),
                    new JsonServiceStack<List<Beer>>(beersList),
                    new Protobuf<List<Beer>>(beersList),
                    new MsgPackCli<List<Beer>>(beersList),
                    new MsgPackCliSingleObject<List<Beer>>(beersList),
                    new MsgPackLightStreams<List<Beer>>(beersList),

                    //Single beer
                    new DataContract<Beer>(testBeer),
                    new XmlSerializer<Beer>(testBeer),
                    new Binary<Beer>(testBeer),
                    new JsonNewtonsoft<Beer>(testBeer),
                    new JsonServiceStack<Beer>(testBeer),
                    new Protobuf<Beer>(testBeer),
                    new MsgPackCli<Beer>(testBeer),
                    new MsgPackCliSingleObject<Beer>(testBeer),
                    new MsgPackLightStreams<Beer>(testBeer)
                };



            //foreach (var tester in testers)
            //{
            //    using (tester)
            //    {
            //        tester.Test();

            //        Console.WriteLine();
            //    }

            //    GC.Collect();
            //}

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            var builder = new StringBuilder();
            foreach (var beer in beers)
            {
                builder.AppendLine($"new Beer {{ Brand = \"{beer.Brand.Replace("\"", "'")}\", Alcohol = {beer.Alcohol}F, Brewery = \"{beer.Brewery}\", Sort = new List<string> {{ {string.Join(", ", beer.Sort.Select(x => $"\"{x.Replace("\"", "'")}\""))} }} }},");
            }

            File.WriteAllText("beer.cs", builder.ToString(), new UTF8Encoding(false));
        }
    }
}