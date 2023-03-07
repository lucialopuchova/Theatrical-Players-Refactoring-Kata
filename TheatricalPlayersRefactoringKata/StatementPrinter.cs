﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = string.Format("Statement for {0}\n", invoice.Customer);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayID];
                var thisAmount = CalculatePerformanceRevenue(play.Type, perf.Audience);

                // add volume credits
                volumeCredits += CalculateVolumeCredits(play.Type, perf.Audience);
                // add extra credit for every ten comedy attendees

                // print line for this order
                result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
                totalAmount += thisAmount;
            }
            result += string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
            result += string.Format("You earned {0} credits\n", volumeCredits);
            return result;
        }

        private int CalculateVolumeCredits(string playType, int audience)
        {
            
            int output = Math.Max(audience - 30, 0);
            if (playType == "comedy")
            {
                output += (int)Math.Floor((decimal)audience / 5);
            }
            return output;
        }

        private int CalculatePerformanceRevenue(string type, int audience)
        {
            int revenue = 0;
            switch (type)
            {
                case "tragedy":
                    revenue = 40000;
                    if (audience > 30)
                    {
                        revenue += 1000 * (audience - 30);
                    }
                    break;
                case "comedy":
                    revenue = 30000;
                    if (audience > 20)
                    {
                        revenue += 10000 + 500 * (audience - 20);
                    }
                    revenue += 300 * audience;
                    break;
                default:
                    throw new Exception("unknown type: " + type);
            }

            return revenue;
        }
    }
}
