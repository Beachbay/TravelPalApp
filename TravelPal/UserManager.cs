using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using TravelPal.Enums;
using TravelPal.Interface;
using TravelPal.Models;

namespace TravelPal;

public class UserManager
{
    public List<IUser> users = new();

    public IUser SignedInUser { get; set; }
    public UserManager()
    {
        Admin admin = new("Admin", "password", Enums.Countries.Greece);
        users.Add(admin);


        User user = new("Gandalf", "password", Enums.Countries.Sweden);
        users.Add(user);
        
        Vacation travel1 = new(true, "Mount Doom", Enums.Countries.Mordor, 1);
        user.Travels.Add(travel1);
       
        Trip travel2 = new(Enums.TripTypes.Leisure, "Mormor", Enums.Countries.Denmark, 2);
        user.Travels.Add(travel2);
    }
    public List<IUser> GetAllUsers()
    {
        return users;
    }

    // Skapar en ny User
    public bool AddUser(string username, string password, Countries country)
    {
        if(ValidateUsername(username))
        {
            // Create new user
            User user = new(username,password,country);

            users.Add(user);

            return true;
        }
        else
        {
            return false;
        }
    }

    
    // Uppdaterar användarnamn
    public bool UpdateUsername(IUser user, string username)
    {
        if(username.Length < 3 || username == null)
        {
            MessageBox.Show("To short");
            return false;
        }
        
        return true;

    }

    // Uppdaterar lösenord
    public bool UpdatePassword(IUser user, string password)
    {
        if (password.Length < 5 || password == null)
        {
            MessageBox.Show("To short");
            return false;
        }

        return true;
    }

    // Ser till så att man inte kan skapa två användare med samma username
    private bool ValidateUsername(string username)
    {
        foreach (IUser user in users)
        {
            if (user.Username == username)
            {

                return false;

            }
        }
        return true;
    }
    // Logga in en användare
    public bool SignInUser(string username, string password)
    {
        foreach (IUser user in users)
        {
            if(username == user.Username && password == user.Password)
            {
                SignedInUser = user;
                return true;
            }
        }
        return false;
    }
}
        



