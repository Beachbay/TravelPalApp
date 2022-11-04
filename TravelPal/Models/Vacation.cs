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

    public string GetInfo()
    {
        return base.GetInfo();
    }

    public override string GetTravelType()
    {
        return "Vacation";
    }

    public override string GetTravelInfo()
    {
        if (AllInclusive)
        {
            return $"Have AllInclusive";
        }
        else
        {
            return "Doesn't have AllInculsive";
        }
    }
}
