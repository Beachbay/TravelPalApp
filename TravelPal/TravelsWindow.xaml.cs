using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
using TravelPal.Interface;
using TravelPal.Models;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for TravelsWindow.xaml
    /// </summary>
    public partial class TravelsWindow : Window
    {
        private UserManager userManager;
        private TravelManager travelManager;
        private User user;

        public TravelsWindow(UserManager userManager, TravelManager travelManager)
        {
            InitializeComponent();
            this.userManager = userManager;
            this.travelManager = travelManager;
            UpdateUI();

            if (this.userManager.SignedInUser is User)
            {
                UpdateTravelsList();
            }
            else if (this.userManager.SignedInUser is Admin)
            {
                BtnAddTravel.IsEnabled = false;
                btnUser.IsEnabled = false;

                foreach(var travel in travelManager.AllTravels)
                {
                    ListViewItem item = new();
                    item.Content = travel.GetInfo();
                    item.Tag = travel;
                    lvTravels.Items.Add(item);
                }
            }
        }

       

        private void DisplayTravels()
        {

            if(userManager.SignedInUser != null && userManager.SignedInUser is User)
            {
                User signedInUser = userManager.SignedInUser as User;

                List<Travel> travels = signedInUser.Travels;

                foreach(Travel travel in travels)
                {
                    ListViewItem item = new();

                    item.Content = travel.GetInfo();
                    item.Tag = travel;

                    lvTravels.Items.Add(item);
                }
            }
        }

        // Printa använderens username i Travelswindow
        private void UpdateUI()
        {
            lblUserName.Content = this.userManager.SignedInUser.Username;

        }

       // Printa användarens resor i en listview
        private void UpdateTravelsList()
        {
            User signedInUser = this.userManager.SignedInUser as User;

            if (this.userManager.SignedInUser is User)
            {
                lvTravels.Items.Clear();
                foreach (var travel in signedInUser.Travels)
                {
                    ListViewItem item = new();
                    item.Content = travel.GetInfo();
                    item.Tag = travel;

                    lvTravels.Items.Add(item);
                }
            }   
        }

        private void BtnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            AddTravelWindow addTravelWindow = new(userManager, travelManager);

            addTravelWindow.Show();
            Close();
        }

        private void BtnDetails_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem listViewItem = lvTravels.SelectedItem as ListViewItem;

            if(listViewItem != null)
            {
                Travel selectedTravel = listViewItem.Tag as Travel;

                TravelDetailsWindow travelDetailsWindow = new(userManager, travelManager, selectedTravel);
                travelDetailsWindow.Show();
                Close();
            }
            else if (listViewItem != null)
            {
                MessageBox.Show("Please select a destination");   
            }    
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            UserDetailsWindow userDetailsWindow = new(userManager, travelManager);

            userDetailsWindow.Show();

            Close();
        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new(userManager, travelManager);

            mainWindow.Show();
            Close();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem selectedItem = lvTravels.SelectedItem as ListViewItem;

            lvTravels.Items.Remove(selectedItem);

            if (selectedItem != null)
            {
                // Ta bort resa från listView

                Travel selectedTravel = selectedItem.Tag as Travel;

                foreach (IUser user in userManager.GetAllUsers())
                {
                    if (user is User)
                    {
                        User appUser = user as User;

                        if (appUser.Travels.Contains(selectedTravel))
                        {
                            appUser.Travels.Remove(selectedTravel);
                        }
                    }
                }

                travelManager.AllTravels.Remove(selectedTravel);

            }
            else
            {
                MessageBox.Show("You need to pick a travel");
            }
            UpdateTravelsList();
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is a app created for planing and keeping track of your trvels.", "Iformation");
        }
    }
}
