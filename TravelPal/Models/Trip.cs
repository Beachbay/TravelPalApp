using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Enums;

namespace TravelPal.Models;

public class Trip : Travel
{

	public TripTypes Type { get; set; }

    public Trip(TripTypes tripType, string destination, Countries country, int travellers) : base(destination, country, travellers)
    {
        Type = tripType;
    }

    // Skicka en sträng med info
    public string GetInfo()
	{
        return base.GetInfo();
	}

    // Hämtar att traveltype är en trip
    public override string GetTravelType()
    {
        return "Trip";
    }
}
