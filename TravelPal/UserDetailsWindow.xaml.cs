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
using TravelPal.Interface;
using TravelPal.Models;

namespace TravelPal;

/// <summary>
/// Interaction logic for UserDetailsWindow.xaml
/// </summary>
public partial class UserDetailsWindow : Window
{
    private readonly UserManager userManager;
    private readonly TravelManager travelManager;

    private User user;

    public UserDetailsWindow(UserManager userManager, TravelManager travelManager)
    {
        InitializeComponent();

        this.userManager = userManager;
        this.travelManager = travelManager;

        lblFullInfo.Content = $"{this.userManager.SignedInUser.Username} from {this.userManager.SignedInUser.Location}";

        string[] getAllCountries = Enum.GetNames(typeof(Countries));

        cbNewCountry.ItemsSource = getAllCountries;
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {

        string newUsername = tbNewUsername.Text;
        string newPassword = pswNewPassword.Password;
        string confirmNewPassword = pswConfirmPassword.Password;
        
        string selectedItem = cbNewCountry.Text;
        Countries selectedCountry = (Countries)Enum.Parse(typeof(Countries), selectedItem);
        userManager.SignedInUser.Location = selectedCountry;
        

        if(this.userManager.UpdateUsername(user, newUsername))
        {
            userManager.SignedInUser.Username = newUsername;
 
        }

        if(newPassword == confirmNewPassword)
        {
            if (this.userManager.UpdatePassword(user, newPassword))
            {
                userManager.SignedInUser.Password = newPassword;
  
            }
        }
        else
        {
            MessageBox.Show("Password did not match");
        }

        TravelsWindow travelsWindow = new(userManager, travelManager);

        travelsWindow.Show();

        Close();

    }

    private void btnBack_Click(object sender, RoutedEventArgs e)
    {
        TravelsWindow travelsWindow = new(userManager, travelManager);

        travelsWindow.Show();
        Close();
    }
}
