using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            
            var resultStringBuilder = new StringBuilder($"Statement for {invoice.Customer}\n");
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayID];
                var revenue = CalculatePerformanceRevenue(play.Type, perf.Audience);

                volumeCredits += CalculateVolumeCredits(play.Type, perf.Audience);

                resultStringBuilder.Append(OutputOrderFormat(cultureInfo, play, revenue, perf));
                totalAmount += revenue;
            }
            resultStringBuilder.Append(string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100)));
            resultStringBuilder.Append(string.Format("You earned {0} credits\n", volumeCredits));
            return resultStringBuilder.ToString();
        }

        private static string OutputOrderFormat(CultureInfo cultureInfo, Play play, int revenue, Performance perf)
        {
            return string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(revenue / 100), perf.Audience);
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

        private int CalculatePerformanceRevenue(string playType, int audience)
        {
            int revenue = 0;
            switch (playType)
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
                    throw new Exception("unknown type: " + playType);
            }

            return revenue;
        }
    }
}
