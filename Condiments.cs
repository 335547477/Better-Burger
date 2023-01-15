/*
 * Author: Kinjal Padhiar
 * File Name: Condimnets.cs
 * Project Name: BetterBurger
 * Creation Date: May 20, 2022
 * Modified Date: June 20, 2022
 * Description: The condiments class that holds information for each condiment (NOT COMPLETE)
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
    class Condiments : Ingredients
    {
        //creates boolean values for whether each condiment is added
        bool ketchup;
        bool mustard;
        bool mayo;

        //declaring values for squirting animation
        Animation ketchupAnim;
        Vector2 ketchupPos;

        //Pre: rectangle information (x/y positions, width, and height as integer), boolean information for each condiment, image and vector position
        //Post: none
        //Desc: overloaded constructer that takes parameters from base class and adds its specific parameters as well as passes on inherited variables
        public Condiments (Vector2 ketchupPos, bool ketchup, bool mustard, bool mayo, string name, Texture2D ingImg, int x, int y, int width, int height, double cost) 
            : base (name, ingImg, x, y, width, height, cost)
        {
            this.ketchup = ketchup;
            this.mustard = mustard;
            this.mayo = mayo;
            this.ketchupPos = ketchupPos;
        }
    }
}
