using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Enums;

namespace TravelPal.Models;

public class Vacation : Travel
{
    public bool AllInclusive { get; set; }

    public Vacation(bool allInclusive, string destination, Countries country, int travellers) : base(destination, country, travellers)
    {
        AllInclusive = allInclusive;
    }

    // Skicka en sträng med info
    public string GetInfo()
    {
        return base.GetInfo();
    }

    // Hämtar att traveltype är en vaccation
    public override string GetTravelType()
    {
        return "Vacation";
    }

    // Returnar till label i traveldetails om det är aic 
    public override string GetTravelInfo()
    {
        if (AllInclusive)
        {
            return $"Has AllInclusive";
        }
        else
        {
            return "Doesn't have AllInculsive";
        }
    }
}
