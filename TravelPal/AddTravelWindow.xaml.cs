using System;
using System.Collections.Generic;
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
/// Interaction logic for AddTravelWindow.xaml
/// </summary>
public partial class AddTravelWindow : Window
{
    private UserManager userManager;
    private TravelManager travelManager;
    private string selectedTravelType;


    private int[] numberOfTravellers = new[] { 1, 2, 4, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };


    public AddTravelWindow(UserManager userManager, TravelManager travelManager)
    {
        InitializeComponent();

        this.userManager = userManager;
        this.travelManager = travelManager;

        string[] getTripVacation = Enum.GetNames(typeof(TripVacation));

        cbTripOrVacation.ItemsSource = getTripVacation;

        string[] getAllCountry = Enum.GetNames(typeof(Countries));

        cbCountry.ItemsSource = getAllCountry;

        foreach(int number in numberOfTravellers)
        {
            cbTravelers.Items.Add(number.ToString());
        }

        string[] getAllTripTypes = Enum.GetNames(typeof(TripTypes));

        cbTripType.ItemsSource = getAllTripTypes;
    }

    private void BtnAdd_Click(object sender, RoutedEventArgs e)
    {
        string destination = txtDestination.Text;
        string country = cbCountry.SelectedItem as string; 
        Countries selectedCountry = (Countries)Enum.Parse(typeof(Countries), country);

        string travellersString = cbTravelers.SelectedItem as string;

        int travellersInt = int.Parse(travellersString);

        string tripOrVacation = cbTripOrVacation.SelectedItem as string;

        if (tripOrVacation == "Trip")
        {
            string tripType = cbTripType.SelectedItem as string;

            if(tripType == "Work")
            {
                TripTypes selectedType = (TripTypes)Enum.Parse(typeof(TripTypes), tripType);

                Travel newTravel = this.travelManager.CreateTravel(travellersInt, selectedCountry, destination, selectedType);

                User user = userManager.SignedInUser as User;

                user.Travels.Add(newTravel);

                userManager.SignedInUser = user;
            }
            else if(tripType == "Leisure")
            {
                TripTypes selectedType = (TripTypes)Enum.Parse(typeof(TripTypes), tripType);

                Travel newTravel = travelManager.CreateTravel(travellersInt, selectedCountry, destination, selectedType);

                User user = userManager.SignedInUser as User;

                user.Travels.Add(newTravel);

                userManager.SignedInUser = user;
            }
        }
        else if(tripOrVacation == "Vacation")
        {
            bool allInclusive = (bool)chbAllInclusive.IsChecked;

            Travel newTravel = travelManager.CreateTravel(travellersInt, selectedCountry, destination, allInclusive);

            User user = userManager.SignedInUser as User;

            user.Travels.Add(newTravel);

            userManager.SignedInUser = user;
        }

        // Öppna travelswindow igen
        TravelsWindow travelsWindow = new(userManager, travelManager);

        travelsWindow.Show();

        

        Close();
    }

    private void cbTripOrVacation_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = cbTripOrVacation.SelectedItem as string;

        

        switch (selected)
        {
            case "Trip":
                {
                    cbTripType.Visibility = Visibility.Visible;
                    lblTripType.Visibility = Visibility.Visible;

                    chbAllInclusive.Visibility = Visibility.Hidden;
                    lblAllInclisuve.Visibility = Visibility.Hidden;
                    break;
                }
            case "Vacation":
                {
                    chbAllInclusive.Visibility = Visibility.Visible;
                    lblAllInclisuve.Visibility = Visibility.Visible;

                    cbTripType.Visibility = Visibility.Hidden;
                    lblTripType.Visibility = Visibility.Hidden;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
        TravelsWindow travelsWindow = new(userManager, travelManager);
        
        travelsWindow.Show();
        Close();

    }
}
