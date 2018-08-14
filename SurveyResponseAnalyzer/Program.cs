using SurveyResponseAnalyzer.Domain;
using SurveyResponseAnalyzer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SurveyResponseAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Trek!");

            //hardcoded values for now..
            string url = "https://trekhiringassignments.blob.core.windows.net/interview/bikes.json";
            int topXResults = 20;

            //Simple repo and result querying
            SurveyResponseRepository surveyClient = new SurveyResponseRepository(url);

            IEnumerable<SurveyResponse> responses = surveyClient.GetResponses().Result;

            //Get top 20 combination of bikes based on survey results
            //1) Groupby our surveyresponse object directly and pass our custom comparer to do the grouping
            //2) Order by descending to ensure most popular combos are first
            //3) Take only 20
            //4) Select out our actual objects and their counts
            var result = responses.GroupBy(x => x, new SurveyResponseComparer())
                                   .OrderByDescending(uniqueResponses => uniqueResponses.Count())
                                   .Take(topXResults)
                                   .Select(t => new { Response = t.Key, Count = t.Count() })
                                   .ToList();

            Console.WriteLine($"Here are the {topXResults} most popular combinations of trek bikes: ");

            for(int i = 0; i< result.Count; i++)
            {
                Console.WriteLine($"Rank #{i+1}. ({result[i].Count} families)");
                List<string> bikes = result[i].Response.Bikes;
                bikes.ForEach(b =>
                {
                    Console.WriteLine($"\t\t {b}");
                });
                Console.WriteLine();
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
