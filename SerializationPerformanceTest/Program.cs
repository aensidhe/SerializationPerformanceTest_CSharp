﻿using System;
using System.Collections.Generic;
using System.Linq;
using SerializationPerformanceTest.TestData.BelgianBeer;
using SerializationPerformanceTest.Testers;


namespace SerializationPerformanceTest
{
    internal static class Program
    {
        private static void Main()
        {
            List<Beer> beersList = BelgianBeerDataRetriever.GetDataFromXML();
            Beer beer = beersList.First();

            var testers = new SerializationTester[]
                {
                    //List of beers
                    new DataContractSerializationTester<List<Beer>>(beersList), 
                    new XmlSerializationTester<List<Beer>>(beersList),
                    new BinarySerializationTester<List<Beer>>(beersList),
                    new JsonNewtonsoftSerializationTester<List<Beer>>(beersList),
                    new JsonServiceStackSerializationTester<List<Beer>>(beersList),
                    new ProtobufSerializationTester<List<Beer>>(beersList),
                    new MsgPackSerializationTester<List<Beer>>(beersList),
                    new MsgPackLightTester<List<Beer>>(beersList),

                    //Single beer
                    new DataContractSerializationTester<Beer>(beer),
                    new XmlSerializationTester<Beer>(beer),
                    new BinarySerializationTester<Beer>(beer),
                    new JsonNewtonsoftSerializationTester<Beer>(beer),
                    new JsonServiceStackSerializationTester<Beer>(beer),
                    new ProtobufSerializationTester<Beer>(beer),
                    new MsgPackSerializationTester<Beer>(beer),
                    new MsgPackLightTester<Beer>(beer)
                };



            foreach (var tester in testers)
            {
                using (tester)
                {
                    tester.Test();

                    Console.WriteLine();
                }

                GC.Collect();
            }

        }
    }
}