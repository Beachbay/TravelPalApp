using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Enums;

namespace TravelPal.Models;

public class Travel
{
    public string Destination { get; set; }
    public Countries Country { get; set; }
    public int Travellers { get; set; }

    public Travel(string destination, Countries country, int travellers)
    {
        Destination = destination;
        Country = country;
        Travellers = travellers;
    }

    public virtual string GetInfo()
    {
        return $"{Destination} / {Country}";
    }

    public virtual string GetTravelType()
    {
        return "TravelType";
    }

    public virtual string GetTravelInfo()
    {
        return "TravelInfo";
    }
}
