/*
 * Author: Kinjal Padhiar
 * File Name: Ingredients.cs
 * Project Name: BetterBurger
 * Creation Date: May 20, 2022
 * Modified Date: June 20, 2022
 * Description: The ingredients class that holds information for each ingredient
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
    class Ingredients
    {
        //declares position of ingredients and bool of whether ingredient is added
        bool add;
        int x;
        int y;
        int width;
        int height;

        //declares image and rectangle variable
        Texture2D ingImg;
        Rectangle ingRect;

        //declares name and cost of ingredient
        string name;
        double cost = 0;

        //Pre: name is a valid string, ingredient image is a valid texture 2D, x position, y position, width, height are all valid integers
        //     and cost is a valid double
        //Post: none
        //Desc: overloaded constructer that takes parameters from base class and adds its specific parameters
        public Ingredients(string name, Texture2D ingImg, int x, int y, int width, int height, double cost)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.cost = cost;
            this.ingImg = ingImg;

            //creates rectangle using x and y positions as well as given width and height
            ingRect = new Rectangle(x, y, width, height);
        }

        //Pre: name is a valid string and cost is a valid double
        //Post: none
        //Desc: overloaded constructer that takes parameters from base class and adds its specific parameters
        public Ingredients(string name, double cost)
        {
            this.name = name;
            this.cost = cost;
        }

        //Pre: none
        //Post: returns name of ingredient as a string
        //Desc: accessor that gets ingredient name
        public string GetName()
        {
            return name; 
        }

        //Pre: none
        //Post: returns rectangle of ingredient
        //Desc: accessor that gets ingredient rectangle
        public Rectangle GetRect()
        {
            return ingRect;
        }

        //Pre: none
        //Post: returns width as a int
        //Desc: accessor that gets ingredient image width
        public int GetWidth()
        {
            return width;
        }

        //Pre: none
        //Post: returns height as a int
        //Desc: accessor that gets ingredient image height
        public int GetHeight()
        {
            return height;
        }

        //Pre: none
        //Post: returns cost as a double
        //Desc: accessor that gets ingredient cost
        public double GetCost ()
        {
            return cost;
        }

        //Pre: none
        //Post: returns ingredient image
        //Desc: accessor that gets ingredient image 
        public Texture2D GetImg ()
        {
            return ingImg;
        }

        //Pre: a new rectangle 
        //Post: none
        //Desc: modifer that replaces current rectangle with new rectangle
        public void SetRect (Rectangle newRect)
        {
            ingRect = newRect;
        }

        //Pre: the x and y position of the rectangle 
        //Post: none
        //Desc: modifer that replaces x and y position of current rectangle with new rectangle
        public void SetRectXY (int x, int y)
        {
            this.x = x;
            this.y = y;
            ingRect = new Rectangle(x, y, width, height);
        }

        //Pre: a bool value 
        //Post: none
        //Desc: modifer that changes whether a ingredients is added or not
        public void IfIngredientAdded(bool changeAdd)
        {
            add = changeAdd;
        }

        //Pre: a new cost that is a valid double
        //Post: none
        //Desc: modifer that replaces current cost with new cost
        public void SetCost (double newCost)
        {
            cost = newCost;
        }

    }
}
