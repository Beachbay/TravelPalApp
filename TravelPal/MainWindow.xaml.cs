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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelPal.Interface;
using TravelPal.Models;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserManager userManager;
        private TravelManager travelManager;


        public MainWindow()
        {
            InitializeComponent();

            

                this.userManager = new();
            this.travelManager = new();

            foreach (IUser user in this.userManager.GetAllUsers())
            {
                if (user is User)
                {
                    User u = user as User;

                    this.travelManager.AllTravels.AddRange(u.Travels);
                }
            }
        }

        public MainWindow(UserManager userManager, TravelManager travelManager)
        {
            InitializeComponent();

            this.userManager = userManager;
            this.travelManager = travelManager;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new(userManager, travelManager);

            registerWindow.Show();

            Close();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            List<IUser> users = userManager.GetAllUsers();

            string username = txtUserName.Text;
            string password = txtPassword.Password;

            if(userManager.SignInUser(username, password))
            {
                TravelsWindow travelsWindow = new(userManager, travelManager);

                travelsWindow.Show();
                Close();
                
            }
            else
            {
                MessageBox.Show("Incorrect Username or Password","Something went wrong");
            }
        }
    }
}
