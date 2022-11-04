using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Enums;
using TravelPal.Models;

namespace TravelPal;

public class TravelManager
{
    public List<Travel> AllTravels = new();


    public Travel AddTravel(Travel travel)
    {
        AllTravels.Add(travel);
        return travel;
    }

    public Travel CreateTravel(int travellers, Countries country, string destination, TripTypes tripType)
    {
        Trip trip = new(tripType, destination, country, travellers);

        return AddTravel(trip);

        //AllTravels.Add(trip);

        //return trip;
    }

    public Travel CreateTravel(int travellers, Countries country, string destination, bool allInclusive)
    {
        Vacation vacation = new(allInclusive, destination, country, travellers);

        return AddTravel(vacation);

        //AllTravels.Add(vacation);

        //return vacation;
    }

    public void RemoveTravel(Travel travelToRemove)
    {
        Travel foundTravel = null;

        foreach (Travel travel in AllTravels)
        {
            if (travel is Travel)
            {
                Travel travels = travel as Travel;

                if (travels.Country == travelToRemove.Country)
                {
                    foundTravel = travels;
                }
            }
        }
        if (foundTravel != null)
        {
            AllTravels.Remove(foundTravel);
        }

    }
}