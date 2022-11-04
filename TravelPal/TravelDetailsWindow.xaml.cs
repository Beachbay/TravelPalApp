using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TravelPal.Enums;
using TravelPal.Models;

namespace TravelPal;

/// <summary>
/// Interaction logic for TravelDetailsWindow.xaml
/// </summary>
public partial class TravelDetailsWindow : Window
{
    private User user;
    private readonly UserManager userManager;
    private readonly TravelManager travelManager;
    AddTravelWindow addTravelWindow;
    private Travel travel;
    private TripTypes Type;

    public TravelDetailsWindow(UserManager userManager, TravelManager travelManager, Travel travel)
    {
        InitializeComponent();
        this.userManager = userManager;
        this.travelManager = travelManager;
        this.travel = travel;

        if (travel is Trip)
        {
            Trip trip = travel as Trip;

            lblSpecial.Content = $"{trip.Type}";
        }
        else if (travel is Vacation)
        {


            Vacation vacation = travel as Vacation;

            if (vacation != null && vacation.AllInclusive)
            {
                lblSpecial.Content = "All inclusive";
            }
            else if (vacation != null && !vacation.AllInclusive)
            {
                lblSpecial.Content = "Not all inclusive";
            }
        }

        UpdateDetails();
    }

    //Sätter nya värden till userdetails
    private void UpdateDetails()
    {
        lblDestiantion.Content = travel.Destination;
        lblCountry.Content = travel.Country;
        lblTravelers.Content = travel.Travellers;
        lblTripOrVac.Content = travel.GetTravelType();
        
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
        TravelsWindow travelsWindow = new(userManager, travelManager);

        travelsWindow.Show();
        Close();
    }
}



