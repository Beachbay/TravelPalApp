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

        public TravelsWindow(UserManager userManager)
        {
            InitializeComponent();
            this.userManager = userManager;
            this.travelManager = new();

            UpdateUI();

            UpdateTravelsList();

            
        }

        public TravelsWindow(UserManager userManager, TravelManager travelManager)
        {
            InitializeComponent();
            this.userManager = userManager;
            this.travelManager = travelManager;

            UpdateUI();


            DisplayTravels();
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

        private void UpdateUI()
        {
            lblUserName.Content = this.userManager.SignedInUser.Username;

            
            // Uppdatera Ui

            lvTravels.Items.Clear();

            
        }

        private void UpdateTravelsList()
        {
            if (this.userManager.SignedInUser is User)
            {
                User signedInUser = this.userManager.SignedInUser as User;

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
                //TravelDetailsWindow.Show();
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

            if (selectedItem != null)
            {
                Travel selectedTravel = selectedItem.Tag as Travel;

                // Ta Bort Resan
                travelManager.RemoveTravel(selectedTravel);

                if (userManager.SignedInUser is User)
                {
                    User signedInUser = userManager.SignedInUser as User;

                    signedInUser.Travels.Remove(selectedTravel);

                    userManager.SignedInUser = signedInUser;

                    
                }

                UpdateUI();
                UpdateTravelsList();
            }
            else
            {
                MessageBox.Show("Please select a travel first!");
            }
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is a app created for planing and keeping track of your trvels.", "Iformation");
        }
    }
}
