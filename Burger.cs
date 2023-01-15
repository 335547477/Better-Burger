/*
 * Author: Kinjal Padhiar
 * File Name: Burger.cs
 * Project Name: BetterBurger
 * Creation Date: May 20, 2022
 * Modified Date: June 20, 2022
 * Description: The burger class holds an individual burger comprised of ingredients
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
    class Burger
    {
        //list of ingredients
        List<Ingredients> ingredients = new List<Ingredients>();

        //declares boolean value of each ingredient (buns and patty will always be true)
        bool topBun = true;
        bool lettuce;
        bool cheese;
        bool onion;
        bool pickle;
        bool tomato;
        bool patty = true;
        bool bottomBun = true;

        //declares count and cost of ingredients 
        int count = 3;
        int cost = 0;

        //Pre: lettuce, cheese, onion, pickle, and tomato is a valid boolean
        //Post: none
        //Desc: overloaded constructer that takes parameters from base class and adds its specific parameters
        public Burger(bool lettuce, bool cheese, bool onion, bool pickle, bool tomato)
        {
            this.lettuce = lettuce;
            this.cheese = cheese;
            this.onion = onion;
            this.pickle = pickle;
            this.tomato = tomato;

            //if the burger has that ingredient, then add the ingredient and cost to list (increase count)
            ingredients.Add(new Ingredients("Bottom Bun", 1));
            ingredients.Add(new Ingredients("Patty", 2));
            if (lettuce == true)
            {
                ingredients.Add(new Ingredients("Lettuce", 1));
                count++;
            }
            if (cheese == true)
            {
                ingredients.Add(new Ingredients("Cheese", 2));
                count++;
            }
            if (onion == true)
            {
                ingredients.Add(new Ingredients("Onion", 1));
                count++;
            }
            if (pickle == true)
            {
                ingredients.Add(new Ingredients("Pickle", 2));
                count++;
            }
            if (tomato == true)
            {
                ingredients.Add(new Ingredients("Tomato", 1));
                count++;
            }
            ingredients.Add(new Ingredients("Top Bun", 1));

            //loop the ingredients and add cost of each ingredient to get total cost
            for (int i = 0; i < ingredients.Count; i++)
            {
                cost += (int)ingredients[i].GetCost();
            }

        }

        //Pre: none
        //Post: returns list of ingredietns
        //Desc: returns list of the ingredients in the burger
        public List<Ingredients> GetBurgIng()
        {
            return ingredients;
        }

        //Pre: none
        //Post: returns boolean value
        //Desc: returns is burger has top bun
        public bool HasTopBun ()
        {
            return topBun;
        }

        //Pre: none
        //Post: returns boolean value
        //Desc: returns is burger has lettuce
        public bool HasLettuce ()
        {
            return lettuce;
        }

        //Pre: none
        //Post: returns boolean value
        //Desc: returns is burger has cheese
        public bool HasCheese ()
        {
            return cheese;
        }

        //Pre: none
        //Post: returns boolean value
        //Desc: returns is burger has onion
        public bool HasOnion ()
        {
            return onion;
        }

        //Pre: none
        //Post: returns boolean value
        //Desc: returns is burger has pickle
        public bool HasPickle ()
        {
            return pickle;
        }

        //Pre: none
        //Post: returns boolean value
        //Desc: returns is burger has tomato
        public bool HasTomato ()
        {
            return tomato;
        }

        //Pre: none
        //Post: returns boolean value
        //Desc: returns is burger has patty
        public bool HasPatty ()
        {
            return patty;
        }

        //Pre: none
        //Post: returns boolean value
        //Desc: returns is burger has bottom bun
        public bool HasBottomBun ()
        {
            return bottomBun;
        }

        //Pre: none
        //Post: returns integer value
        //Desc: returns is cost of the burger
        public int Cost ()
        {
            return cost;
        }

        //Pre: integer that holds new cost
        //Post: none
        //Desc: sets current cost to the new cost value
        public void SetCost (int newCost)
        {
            cost = newCost;
        }

        //Pre: integer that holds how many ingredients are in burger
        //Post: none
        //Desc: returns burger ingredient count
        public int BurgerIngCount ()
        {
           return count;
        }

    }
}
