using System;
using System.Collections.Generic;
using System.IO.Pipes;
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

    public RegisterWindow(UserManager userManager, TravelManager travelManager)
    {
        InitializeComponent();
        string [] getAllCountries = Enum.GetNames(typeof(Countries));

        cbCountry.ItemsSource = getAllCountries;

        this.userManager = userManager;
        this.travelManager = travelManager;
        

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
            else if(txtConfirmPassword.Password != txtPassword.Password)
            {
                MessageBox.Show("Password did not match", "Warning");
            }
            else
            {
                Countries selectedCountry = (Countries)Enum.Parse(typeof(Countries), country);


                bool isSuccessfullyCreatedUser = this.userManager.AddUser(username, password, selectedCountry);

                if(isSuccessfullyCreatedUser)
                {
                    MainWindow mainWindow = new(userManager, travelManager);

                    mainWindow.Show();

                    Close();
                }
                else
                {
                    MessageBox.Show("Username is already taken");
                }

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

    private void btnBack_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new(userManager, travelManager);

        mainWindow.Show();
        Close();
    }
}
