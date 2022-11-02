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

namespace TravelPal;

/// <summary>
/// Interaction logic for RegisterWindow.xaml
/// </summary>
public partial class RegisterWindow : Window
{
    private UserManager userManager;
    private TravelManager travelManager;

    public RegisterWindow(UserManager userManager)
    {
        InitializeComponent();
        string [] getAllCountries = Enum.GetNames(typeof(Countries));

        cbCountry.ItemsSource = getAllCountries;

        this.userManager = userManager;

        

    }

    private void btnRegister_Click(object sender, RoutedEventArgs e)
    {
        string username = txtUsername.Text;
        string password = txtPassword.Password;
        string country = cbCountry.SelectedItem as string;
        try
        {
            if (country.Count() == 0)
            {

                MessageBox.Show("Enter a country", "Information");

            }
            else if (username.Count() < 3)
            {
                tblUsername.Visibility = Visibility.Visible;
            }
            else if (password.Count() < 3)
            {
                tblPassword.Visibility = Visibility.Visible;
            }
            else
            {
                Countries selectedCountry = (Countries)Enum.Parse(typeof(Countries), country);

                this.userManager.AddUser(username, password, selectedCountry);

                MainWindow mainWindow = new(userManager, travelManager);

                mainWindow.Show();

                Close();
            }
        }
        catch(Exception ex)
        {
            MessageBox.Show("Please enter all fields", "Information");
        }
        

    }

    private void cbCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}
