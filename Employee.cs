/*
 * Author: Kinjal Padhiar
 * File Name: Employee.cs
 * Project Name: BetterBurger
 * Creation Date: May 20, 2022
 * Modified Date: June 20, 2022
 * Description: The employee class that holds information for each employee
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterBurger
{
    class Employee
    {

        //declares string for employee information
        string name;
        string gender;
        string username;
        string password;

        //Pre: name, username, password, and gender as a string
        //Post: none
        //Desc: overloaded constructer that takes parameters from base class and adds its specific parameters
        public Employee (string name, string username, string password, string gender)
        {
            this.name = name;
            this.username = username;
            this.password = password;
            this.gender = gender;
        }

        //Pre: none
        //Post: name as a string
        //Desc: accessor that returns employee name
        public string GetName ()
        {
            return name;
        }

        //Pre: none
        //Post: username as a string
        //Desc: accessor that returns employee username
        public string GetUserName ()
        {
            return username;
        }

        //Pre: none
        //Post: password as a string
        //Desc: accessor that returns employee password
        public string GetPassWord ()
        {
            return password;
        }

        //Pre: none
        //Post: gender as a string
        //Desc: accessor that returns employee gender
        public string GetGender ()
        {
            return gender;
        }

        //Pre: new name as a string
        //Post: none
        //Desc: modifer that sets current name to the new name
        public void SetName (string newName)
        {
            newName = name;
        }

        //Pre: new username as a string
        //Post: none
        //Desc: modifer that sets current username to the new username
        public void SetUsername (string newUsername)
        {
            newUsername = username;
        }

        //Pre: new password as a string
        //Post: none
        //Desc: modifer that sets current password to the new password
        public void SetPassword (string newPassword)
        {
            newPassword = password;
        }

        //Pre: new gender as a string
        //Post: none
        //Desc: modifer that sets current gender to the new gender
        public void SetGender (string newGender)
        {
            newGender = gender;
        }
    }
}
