/*
 * Author: Kinjal Padhiar
 * File Name: CustomerManager.cs
 * Project Name: BetterBurger
 * Creation Date: May 20, 2022
 * Modified Date: June 20, 2022
 * Description: The customer manager class that holds queue of customers
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Animation2D;

namespace BetterBurger
{
    class CustomerManager
    {
        //list of customers
        List<Customer> customers = new List<Customer>();

        //Pre: none
        //Post: none
        //Desc: constructer that takes allows to create instance of customer manager
        public CustomerManager ()
        {

        }

        //Pre: new customer
        //Post: none
        //Desc: adds new customer to list
        public void Enqueue (Customer newCust)
        {
            customers.Add(newCust);
        }

        //Pre: none
        //Post: returns the removed customer
        //Desc: if the customer count is more that 0, then remove and return the first customer in the list
        public Customer Dequeue ()
        {
            Customer result = null;

            if (customers.Count > 0)
            {
                result = customers[0];
                customers.RemoveAt(0);
            }

            return result;
        }

        //Pre: none
        //Post: returns the removed customer
        //Desc: if the customer count is more that 0, then return the first customer in the list
        public Customer Peek ()
        {
            Customer result = null;

            if (customers.Count > 0)
            {
                result = customers[0];
            }

            return result;
        }

        //Pre: none
        //Post: returns integer of the size
        //Desc: returns the customer count
        public int Size ()
        {
            return customers.Count;
        }

        //Pre: none
        //Post: returns boolean value
        //Desc: if the list is empty, then returns false
        public bool IsEmpty ()
        {
            return customers.Count == 0;
        }
    }
}
