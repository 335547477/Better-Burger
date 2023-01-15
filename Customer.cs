/*
 * Author: Kinjal Padhiar
 * File Name: Customer.cs
 * Project Name: BetterBurger
 * Creation Date: May 20, 2022
 * Modified Date: June 20, 2022
 * Description: The customer class that holds information for each customer
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
    class Customer
    {
        //creates new random instance
        Random rnd = new Random();

        //declares list of new boolean values
        List<bool> nums = new List<bool>();

        //new burger order
        Burger order;

        //declares image and rectangle as well as x/y positions, width and height
        Texture2D custImg;
        Rectangle custImgRect;
        int x;
        int y;
        double width;
        double height;

        //Pre: customer image as well as integer for x/y positions and double for width and height
        //Post: none
        //Desc: overloaded constructer that takes parameters from base class and adds its specific parameters
        public Customer(Texture2D custImg, int x, int y, double width, double height)
        {
            this.custImg = custImg;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;

            //creates rectangle using given information
            custImgRect = new Rectangle(x, y, (int) width, (int) height);

            //calls method to create randomized order for customer 
            MakeOrder();
        }

        //Pre: none
        //Post: returns rectangle
        //Desc: accessor that returns customer rectangle
        public Rectangle GetRect ()
        {
            return custImgRect;
        }

        //Pre: none
        //Post: returns burger
        //Desc: accessor that returns burger order
        public Burger Order()
        {
            return order;
        }

        //Pre: none
        //Post: returns customer image
        //Desc: accessor that returns customer image
        public Texture2D GetImg ()
        {
            return custImg;
        }

        //Pre: none
        //Post: returns integer
        //Desc: accessor that returns x position as int
        public int GetX ()
        {
            return x;
        }

        //Pre: none
        //Post: returns integer
        //Desc: accessor that returns y position as int
        public int GetY()
        {
            return y;
        }

        //Pre: none
        //Post: returns double
        //Desc: accessor that returns width as double
        public double GetWidth()
        {
            return width;
        }

        //Pre: none
        //Post: returns double
        //Desc: accessor that returns height as double
        public double GetHeight()
        {
            return height;
        }

        //Pre: none
        //Post: none
        //Desc: makes randomized order for customer
        private void MakeOrder()
        {
            //loop that creates a list of trues and falses for each ingredient
            for (int i = 0; i < 5; i++)
            {
                //generates random number between 0 and 1 (0 is true, 1 is false)
                int temp = rnd.Next(0, 2);
                if (temp == 0)
                {
                    nums.Add(false);
                }
                else
                {
                    nums.Add(true);
                }
            }

            //based on list, creates burger order 
            order = new Burger(nums[0], nums[1], nums[2], nums[3], nums[4]);

            
        }
    }
}
