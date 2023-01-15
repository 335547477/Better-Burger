/*
 * Author: Kinjal Padhiar
 * File Name: IngredientsManager.cs
 * Project Name: BetterBurger
 * Creation Date: May 20, 2022
 * Modified Date: June 20, 2022
 * Description: The ingredients manager class that holds queue of ingredients 
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
using Helper;

namespace BetterBurger
{
    class IngredientsManager
    {
        //list of ingredients 
        List<Ingredients> ingredients = new List<Ingredients>();

        //declares name variable
        string ingName;

        //Pre: ingName is a valid string
        //Post: none
        //Desc: overloaded constructer that takes parameters from base class and adds its specific parameters
        public IngredientsManager(string ingName)
        {
            this.ingName = ingName;
        }

        //Pre: none
        //Post: returns name of ingredient as a string
        //Desc: accessor that gets ingredient name
        public string GetName ()
        {
            return ingName;
        }

        //Pre: new ingredient
        //Post: none
        //Desc: adds new ingredient to list
        public void Enqueue(Ingredients newIng)
        {
            ingredients.Add(newIng);
        }

        //Pre: none
        //Post: returns the removed ingredient
        //Desc: if the ingredient count is more that 0, then remove and return the first ingredient in the list
        public Ingredients Dequeue()
        {
            Ingredients result = null;

            if (ingredients.Count > 0)
            {
                result = ingredients[0];
                ingredients.RemoveAt(0);
            }

            return result;
        }

        //Pre: none
        //Post: returns the first ingredient
        //Desc: if the ingredient count is more that 0, then return the first ingredient in the list
        public Ingredients Peek()
        {
            Ingredients result = null;

            if (ingredients.Count > 0)
            {
                result = ingredients[0];
            }

            return result;
        }

        //Pre: none
        //Post: returns integer of the size
        //Desc: returns the ingredient count
        public int Size()
        {
            return ingredients.Count;
        }

        //Pre: none
        //Post: returns boolean value
        //Desc: if the list is empty, then returns false
        public bool IsEmpty()
        {
            return ingredients.Count == 0;
        }

        //Pre: spriteBatch to allow for drawing
        //Post: none
        //Desc: draws the ingredient queue
        public void DrawQueue (SpriteBatch spriteBatch)
        {
            //loops through the ingredient list and draws each ingredient in the list
            for (int i = 0; i < ingredients.Count; i++)
            {
                spriteBatch.Draw(ingredients[i].GetImg(), ingredients[i].GetRect(), Color.White);
            }
        }
    }
}
