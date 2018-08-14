using SurveyResponseAnalyzer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SurveyResponseAnalyzer
{
    public class SurveyResponseComparer : IEqualityComparer<SurveyResponse>
    {

        public bool Equals(SurveyResponse x, SurveyResponse y)
        {
            return x.Bikes.SequenceEqual(y.Bikes);
        }
        /// <summary>
        /// Custom GetHashcode. Aggregates all the strings (lowercased) 
        /// using the XOR operator to guarantee that regardless of order 
        /// we will have the same hashcode if any given list of bikes has the same responses
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(SurveyResponse obj)
        {
            return obj.Bikes.Aggregate(0, (x, y) => x ^ y.ToLower().GetHashCode());
        }
    }

}
