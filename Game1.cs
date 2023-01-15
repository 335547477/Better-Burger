/*
 * Author: Kinjal Padhiar
 * File Name: Game1.cs
 * Project Name: BetterBurger
 * Creation Date: May 20, 2022
 * Modified Date: June 20, 2022
 * Description: The driver class that brings together all parts of the Game
 * 
 * WHERE YOU CAN FINE COURSE CONTENT:
 * OOP - all the different classes
 * 2D arrays and list - throughout the customer manager, ingredient manager, burger and game1 class
 * File I/O - used to save coin data (should also be saving employee data and ingredients left data however that coudn't be implemented)
 * Queues - customer manager and ingredient manager both hold a queue for customers and ingredients respectfully
 * Sorting/Searching - was supposed to be multiple accounts but couldn't be implemented (in our hearts this is done xD )
 */
using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Animation2D;
using Helper;

namespace BetterBurger
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //declares variables to read and write to files
        StreamWriter outFile;
        StreamReader inFile;

        //declares menu images and corresponding rectangles
        Texture2D menuBg;
        Rectangle menuBgRect;
        Texture2D menuPlayBtn;
        Rectangle menuPlayBtnRect;
        Texture2D helpBtn;
        Rectangle helpBtnRect;
        Texture2D logInBtn;
        Rectangle logInBtnRect;
        Texture2D settingsBtn;
        Rectangle settingsBtnRect;

        //declares all the buttons and corresponding rectangles in the game
        Texture2D playBtn;
        Texture2D pauseBtn;
        Rectangle pauseBtnRect;
        Texture2D homeBtn;
        Rectangle homeBtnRect;
        Texture2D musicOffBtn;
        Rectangle musicOffRect;
        Texture2D musicOnBtn;
        Rectangle musicOnRect;
        Texture2D volumeOffBtn;
        Rectangle volumeOffRect;
        Texture2D volumeOnBtn;
        Rectangle volumeOnRect;
        Texture2D groceryBtn;
        Texture2D jotNotesBtn;
        Rectangle jotNotesBtnRect;
        Texture2D doneBurgerBtn;
        Rectangle doneBurgerBtnRect;
        Texture2D giveBurgerBtn;
        Rectangle giveBurgerBtnRect;

        //declares gamePlay bachground images and corresponding rectangles
        Texture2D orderBg;
        Rectangle orderBgRect;
        Texture2D buildStation;
        Texture2D inventoryBg;
        Texture2D orderBtn;
        Rectangle orderBtnRect;
        Texture2D inventoryBtn;
        Rectangle inventoryBtnRect;
        Texture2D buildingBtn;
        Rectangle buildingBtnRect;
        

        //declares plate image and rectangle
        Texture2D plate;
        Rectangle plateRect;

        //declares sound effect and sound effect instaces for the game
        SoundEffect bgMusic;
        SoundEffect ingSnd;
        SoundEffectInstance bgMusicInst;

        //initializes ingredient size (how many of each ingredients) and count (total types of ingredients)
        static int ingSize = 5;
        static int ingCount = 8;

        //declares images for each ingredient
        Texture2D topBurgerImg;
        Texture2D bottomBurgerImg;
        Texture2D pattyImg;
        Texture2D cheeseImg;
        Texture2D lettuceImg;
        Texture2D onionImg;
        Texture2D tomatoImg;
        Texture2D pickleImg;

        //creates location integers for scaling 
        int ingLocYLeft;
        int ingLocYRight;
        int ingLocYBurger;

        //boolean array for whether ingredient is to be drawn
        bool[] drawIng = new bool[ingCount];

        //queue of ingredient managers for build station
        List<IngredientsManager> ingManager = new List<IngredientsManager>();

        //queue of burger that user is going to make as well as location variable for scaling 
        List<Ingredients> burg = new List<Ingredients>();
        int burgLastLoc;

        //variables needed to draw ingredients into the inventory 
        bool[] drawInventory = new bool[ingCount];
        Random rnd = new Random();
        int num1;
        int num2;
        Vector2[] invLoc = new Vector2[ingCount];
        Texture2D tempInvImg;
        Rectangle invImgRect;

        //initializes font for text
        SpriteFont text;

        //variables to calculate coins earned 
        int coins = 0;
        int totalCoins = 0;
        bool drawNoCoins = false;
        bool addCoins = false;

        //images for each condiment in game
        Texture2D ketchupBottle;
        Texture2D mustardBottle;
        Texture2D mayoBottle;

        //integers for gamestates and screen size
        int screenWidth;
        int screenHeight;
        int gameState;
        int gamePlay = ORDER;

        //variables for current mouse state and previous mouse state 
        MouseState mouse;
        MouseState prevmb;

        //list of employees
        List<Employee> employees = new List<Employee>();

        //list of customer queues to allow for multiple lines of customers in future levels
        List<CustomerManager> customersQueues = new List<CustomerManager>();

        //array of possible customer images
        Texture2D[] customerImg = new Texture2D[10];

        //variables needed to view the order of the burger
        Texture2D orderTextboxImg;
        Rectangle orderTextboxRect;
        bool drawOrderTxt = true;
        Texture2D orderImg;
        Rectangle orderRect;
        int orderLoc = 0;
        List <Rectangle> orderIngRect = new List<Rectangle> ();
        int orderRectNum = 0;
        int custCount;

        //constants for different gamestates 
        const int MENU = 0;
        const int GAMEPLAY = 1;
        const int SETTINGS = 2;
        const int LOGIN = 3;
        const int HELP = 4;
        const int PAUSE = 5;
        const int ORDER = 6;
        const int BUILD = 7;
        const int INVENTORY = 8;

        //boolean values for music 
        bool play;
        bool music;
        bool volume;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //generates screenwidth and screen height
            screenWidth = graphics.GraphicsDevice.Viewport.Width;
            screenHeight = graphics.GraphicsDevice.Viewport.Height;

            //declares gamestate to be menue
            gameState = MENU;

            //declares mouse to be visible
            IsMouseVisible = true;

            //reads in the amount of coins player has
            ReadCoins();
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //loads in font
            text = Content.Load<SpriteFont>("Fonts/text");

            //loads in music and soundeffects 
            bgMusic = Content.Load<SoundEffect>("Audio/Music/Cafe Music");
            ingSnd = Content.Load<SoundEffect>("Audio/Sound Effects/PlaceIngredient");

            //sets volume of music and creates instance 
            SoundEffect.MasterVolume = 0.75f;
            bgMusicInst = bgMusic.CreateInstance();

            //loads menu background and creates corresponding rectangle
            menuBg = Content.Load<Texture2D>("Images/Backgrounds/Better Burger Game Background");
            menuBgRect = new Rectangle(0, 0, screenWidth, screenHeight);

            //loads menu play button and creates corresponding rectangle
            menuPlayBtn = Content.Load<Texture2D>("Images/Sprites/Buttons/Menu Play Button");
            menuPlayBtnRect = new Rectangle(screenWidth / 2 - 60, screenHeight / 2 + 25, menuPlayBtn.Width + 10, menuPlayBtn.Height + 10);

            //loads log in button and creates corresponding rectangle
            logInBtn = Content.Load<Texture2D>("Images/Sprites/Buttons/Log In Button");
            logInBtnRect = new Rectangle(screenWidth / 2 - 60, screenHeight / 2 + 30 * 2, logInBtn.Width + 10, logInBtn.Height + 10);

            //loads help button and creates corresponding rectangle
            helpBtn = Content.Load<Texture2D>("Images/Sprites/Buttons/Help Button");
            helpBtnRect = new Rectangle(screenWidth / 2 - 60, screenHeight / 2 + 35 * 3, helpBtn.Width + 10, helpBtn.Height + 10);

            //loads settings button and creates corresponding rectangle
            settingsBtn = Content.Load<Texture2D>("Images/Sprites/Buttons/Settings Button");
            settingsBtnRect = new Rectangle(screenWidth / 2 - 60, screenHeight / 2 + 35 * 4, settingsBtn.Width + 10, settingsBtn.Height + 10);

            //loads order background and creates corresponding rectangle
            orderBg = Content.Load<Texture2D>("Images/Backgrounds/Better Burger Order Background");
            orderBgRect = new Rectangle(-20, -20, screenWidth + 40, screenHeight + 45);

            //loads build station background
            buildStation = Content.Load<Texture2D>("Images/Backgrounds/Better Burger Game Building Station");

            //loads inventory background
            inventoryBg = Content.Load<Texture2D>("Images/Backgrounds/Better Burger Game Inventory Room");

            //loads play button
            playBtn = Content.Load<Texture2D>("Images/Sprites/Buttons/Play Button");

            //loads pause button and creates corresponding rectangle
            pauseBtn = Content.Load<Texture2D>("Images/Sprites/Buttons/Pause Button");
            pauseBtnRect = new Rectangle(screenWidth - pauseBtn.Width / 2, 0, pauseBtn.Width / 2, pauseBtn.Height / 2);

            //loads home button and creates corresponding rectangle
            homeBtn = Content.Load<Texture2D>("Images/Sprites/Buttons/Home Button");
            homeBtnRect = new Rectangle(0, 0, homeBtn.Width / 2, homeBtn.Height / 2);

            //loads music off button and creates corresponding rectangle
            musicOffBtn = Content.Load<Texture2D>("Images/Sprites/Buttons/Music Off Button");
            musicOffRect = new Rectangle((screenWidth - pauseBtn.Width / 2) - musicOffBtn.Width / 3, 0, musicOffBtn.Width / 2, musicOffBtn.Height / 2);

            //loads music on button and creates corresponding rectangle
            musicOnBtn = Content.Load<Texture2D>("Images/Sprites/Buttons/Music On Button");
            musicOnRect = new Rectangle(screenWidth / 2 - musicOffBtn.Width * 2, screenHeight / 2 - musicOffBtn.Height, musicOffBtn.Width * 2, musicOffBtn.Height * 2);

            //loads volume off button and creates corresponding rectangle
            volumeOffBtn = Content.Load<Texture2D>("Images/Sprites/Buttons/Volume Off Button");
            volumeOffRect = new Rectangle((screenWidth - pauseBtn.Width / 2 - musicOffBtn.Width / 3) - volumeOffBtn.Width / 3, 0, volumeOffBtn.Width / 2, volumeOffBtn.Height / 2);

            //loads volume on button and creates corresponding rectangle
            volumeOnBtn = Content.Load<Texture2D>("Images/Sprites/Buttons/Volume On Button");
            volumeOnRect = new Rectangle(screenWidth / 2 + volumeOffBtn.Width / 3, screenHeight / 2 - musicOffBtn.Height, musicOffBtn.Width * 2, musicOffBtn.Height * 2);

            //loads grocery button 
            groceryBtn = Content.Load<Texture2D>("Images/Sprites/Buttons/Grocery Button");

            //loads jot notes button and creates corresponding rectangle
            jotNotesBtn = Content.Load<Texture2D>("Images/Sprites/Buttons/Jot Notes Button");
            jotNotesBtnRect = new Rectangle(0, 0, jotNotesBtn.Width / 2, jotNotesBtn.Height / 2);

            //loads order button and creates corresponding rectangle
            orderBtn = Content.Load<Texture2D>("Images/Sprites/Buttons/Order Button");
            orderBtnRect = new Rectangle(screenWidth / 3 - orderBtn.Width, screenHeight - orderBtn.Height / 2, orderBtn.Width + 20, orderBtn.Height + 20);

            //loads building button and creates corresponding rectangle
            buildingBtn = Content.Load<Texture2D>("Images/Sprites/Buttons/Build Button");
            buildingBtnRect = new Rectangle((screenWidth / 3 - orderBtn.Width / 3) + buildingBtn.Width - 8, screenHeight - orderBtn.Height / 2 + 5, buildingBtn.Width + 7, buildingBtn.Height + 8);

            //loads inventory button and creates corresponding rectangle
            inventoryBtn = Content.Load<Texture2D>("Images/Sprites/Buttons/Inventory Button");
            inventoryBtnRect = new Rectangle((screenWidth / 3 - orderBtn.Width) * 3, screenHeight - orderBtn.Height / 2, orderBtn.Width + 20, orderBtn.Height + 20);

            //loads order textbox image and creates corresponding rectangle
            orderTextboxImg = Content.Load<Texture2D>("Images/Sprites/Order Textbox");
            orderTextboxRect = new Rectangle(screenWidth/2 - orderTextboxImg.Width / 2 + 15, 10, orderTextboxImg.Width/4, orderTextboxImg.Height/4);

            //loads order template image and creates corresponding rectangle
            orderImg = Content.Load<Texture2D>("Images/Sprites/Order Template");
            orderRect = new Rectangle((screenWidth / 3) * 2 - 10, 15, orderImg.Width, orderImg.Height);

            //loads done burger button and creates corresponding rectangle
            doneBurgerBtn = Content.Load<Texture2D>("Images/Sprites/Buttons/Done Burger");
            doneBurgerBtnRect = new Rectangle(screenWidth/2 - doneBurgerBtn.Width/2 + 15 , 15, doneBurgerBtn.Width/2, doneBurgerBtn.Height/2);

            //loads give burger button and creates corresponding rectangle
            giveBurgerBtn = Content.Load<Texture2D>("Images/Sprites/Buttons/Give Burger");
            giveBurgerBtnRect = new Rectangle(screenWidth / 2 - giveBurgerBtn.Width / 2 + 30, (screenHeight/3)*2 + 50, giveBurgerBtn.Width / 2, giveBurgerBtn.Height / 2);

            //loads ingredient images
            topBurgerImg = Content.Load<Texture2D>("Images/Sprites/Ingredients/Top Burger Bun");
            bottomBurgerImg = Content.Load<Texture2D>("Images/Sprites/Ingredients/Botton Burger Bun");
            pattyImg = Content.Load<Texture2D>("Images/Sprites/Ingredients/Ham Patty");
            cheeseImg = Content.Load<Texture2D>("Images/Sprites/Ingredients/Cheese");
            lettuceImg = Content.Load<Texture2D>("Images/Sprites/Ingredients/Lettuce");
            onionImg = Content.Load<Texture2D>("Images/Sprites/Ingredients/Onion");
            tomatoImg = Content.Load<Texture2D>("Images/Sprites/Ingredients/Tomato");
            pickleImg = Content.Load<Texture2D>("Images/Sprites/Ingredients/Pickle");

            //loads condiment bottle images
            ketchupBottle = Content.Load<Texture2D>("Images/Sprites/Ingredients/Ketchup Bottle");
            mustardBottle = Content.Load<Texture2D>("Images/Sprites/Ingredients/Mustard Bottle");
            mayoBottle = Content.Load<Texture2D>("Images/Sprites/Ingredients/Mayo Bottle");

            //loads plate image and creates corresponding rectangle
            plate = Content.Load<Texture2D>("Images/Sprites/Plate");
            plateRect = new Rectangle(screenWidth / 2 - plate.Width / 2 + 115, screenHeight - buildStation.Height / 3 + 20, plate.Width / 2, plate.Height / 2);

            //loop that loads image for each customer and stores into array
            for (int i = 0; i < customerImg.Length; i++)
            {
                customerImg[i] = Content.Load<Texture2D>("Images/Sprites/Customer/Customer " + (i+1).ToString());
            }

            //add a new queue for each ingredient
            ingManager.Add(new IngredientsManager("Top Burger Bun"));
            ingManager.Add(new IngredientsManager("Bottom Burger Bun"));
            ingManager.Add(new IngredientsManager("Ham Patty"));
            ingManager.Add(new IngredientsManager("Cheese"));
            ingManager.Add(new IngredientsManager("Lettuce"));
            ingManager.Add(new IngredientsManager("Onion"));
            ingManager.Add(new IngredientsManager("Tomato"));
            ingManager.Add(new IngredientsManager("Pickle"));

            //generate starting positions for all scaling variables (magic numbers used because this position is going to loop back to these set numbers)
            ingLocYLeft = 30;
            ingLocYRight = 30;
            ingLocYBurger = screenHeight - (buildStation.Height / 3 + 20) / 2 - plate.Height / 9;
            burgLastLoc = screenHeight - (buildStation.Height / 3 + 20) / 2 - plate.Height / 18;
            orderLoc = orderRect.Bottom - bottomBurgerImg.Height / 3 - 40;

            //loop that stocks ingredients into each queue
            for (int i = 0; i < ingManager.Count; i++)
            {
                //calls stock ingredients method
                StockIngredients(i);
            }

            //declares customer count and creates queue for one set of customers
            custCount = 10;
            customersQueues.Add(new CustomerManager());

            //loop that adds image and sets rectangle for each customer
            for (int i = 0; i < custCount; i++)
            {
                customersQueues[0].Enqueue(new Customer(customerImg[i], screenWidth/2 - customerImg[i].Width*2, screenHeight/3 - customerImg[i].Height + 5, customerImg[i].Width*1.2, customerImg[i].Height*1.2));
            }
            
            //loop that declares each ingredient to not be drawn at the start
            for (int i = 0; i < drawIng.Length; i++)
            {
                drawIng[i] = false;
            }

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //sets previous mouse state as current mouse state and then gets new mouse state
            prevmb = mouse;
            mouse = Mouse.GetState();

            //if statement for when left mouse button is pressed and previously the left mouse button was not pressed
            if (mouse.LeftButton == ButtonState.Pressed && !(prevmb.LeftButton == ButtonState.Pressed))
            {
                //if statement that changes gamestate based on which menu button has been clicked
                if (gameState == MENU)
                {
                    if (menuPlayBtnRect.Contains(mouse.X, mouse.Y))
                    {
                        gameState = GAMEPLAY;
                    }
                    else if (logInBtnRect.Contains(mouse.X, mouse.Y))
                    {
                        gameState = LOGIN;
                    }
                    else if (settingsBtnRect.Contains(mouse.X, mouse.Y))
                    {
                        gameState = SETTINGS;
                    }
                    else if (helpBtnRect.Contains(mouse.X, mouse.Y))
                    {
                        gameState = HELP;
                    }
                }

                //calls method to switch buttons if clicked
                SwitchButtons();

                //if gamestate is settings then switch the buttons for music and volume when clicked on
                if (gameState == SETTINGS)
                {
                    //if the music button is clicked, switch the boolean statement
                    if (musicOnRect.Contains(mouse.X, mouse.Y))
                    {
                        if (music == true)
                        {
                            music = false;
                        }
                        else
                        {
                            music = true;
                        }
                    }
                    
                    //if the volume button is clicked, switch the boolean statement
                    if (volumeOnRect.Contains(mouse.X, mouse.Y))
                    {
                        if (volume == true)
                        {
                            volume = false;
                        }
                        else
                        {
                            volume = true;
                        }
                    }
                }

                //if gamestate is gameplay, then based on which rectangle is clicked, change the gameplay room
                if (gameState == GAMEPLAY)
                {
                    if (orderBtnRect.Contains(mouse.X, mouse.Y))
                    {
                        gamePlay = ORDER;
                    }
                    else if (buildingBtnRect.Contains(mouse.X, mouse.Y))
                    {
                        gamePlay = BUILD;
                    }
                    else if (inventoryBtnRect.Contains(mouse.X, mouse.Y))
                    {
                        gamePlay = INVENTORY;
                    }
                }

            }

            //if gamestate is gameplay and gameplay room is order, then calls the order method
            if (gameState == GAMEPLAY && gamePlay == ORDER)
            {
                GameplayOrder();
            }

            //if gamestate is gameplay and gameplay room is build, then calls the build method
            if (gameState == GAMEPLAY && gamePlay == BUILD)
            {
                GameplayBuild();
            }

            //if gamestate is gameplay and gameplay room is the inventory, then calls the inventory method
            if (gameState == GAMEPLAY && gamePlay == INVENTORY)
            {
                GameplayInventory();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //switch statement that draws based on gamestate
            switch (gameState)
            {
                //if gamestate is menu, then calls menu method
                case MENU:
                    DrawMenu(spriteBatch);
                    break;

                //if gamestate is gameplay then uses switch statement to draw based on which room the game is in
                case GAMEPLAY:
                    switch (gamePlay)
                    {
                        //if gameplay is in the order rooms then draws accordingly
                        case ORDER:
                            //draws order background, give burger button, and customer 
                            spriteBatch.Draw(orderBg, orderBgRect, Color.White);
                            spriteBatch.Draw(giveBurgerBtn, giveBurgerBtnRect, Color.White);
                            spriteBatch.Draw(customersQueues[0].Peek().GetImg(), customersQueues[0].Peek().GetRect(), Color.White);

                            //if bool to draw the order textbox is true, then draw the order textbox, otherwise draw order template and customer order
                            if (drawOrderTxt == true)
                            {
                                spriteBatch.Draw(orderTextboxImg, orderTextboxRect, Color.White);
                            }
                            else
                            {
                                spriteBatch.Draw(orderImg, orderRect, Color.White);
                                DrawOrder(spriteBatch);
                            }
                            break;
                        
                        //if gameplay is in the build room then draws accordingly
                        case BUILD:
                            //draw buld station background, done burger button, and plate 
                            spriteBatch.Draw(buildStation, menuBgRect, Color.White);
                            spriteBatch.Draw(doneBurgerBtn, doneBurgerBtnRect, Color.White);
                            spriteBatch.Draw(plate, plateRect, Color.White);

                            //loop that goes through each ingredient queue and draws it
                            for (int i = 0; i < ingManager.Count; i++)
                            {
                                ingManager[i].DrawQueue(spriteBatch);
                                
                                //if the ingredient has been dropped onto the plate
                                if (drawIng[i] == true)
                                {
                                    //loops through burger on plate and draws it with newly added ingredient
                                    for (int j = 0; j < burg.Count; j++)
                                    {
                                        spriteBatch.Draw(burg[j].GetImg(), burg[j].GetRect(), Color.White);
                                    }
                                }
                            }
                            break;

                        //if gameplay is in the inventory then draws accordingly
                        case INVENTORY:
                            //draws inventory background
                            spriteBatch.Draw(inventoryBg, menuBgRect, Color.White);

                            //draws ingredient image in random position in inventory
                            DrawInventoryImg(spriteBatch);
                            break;
                    }

                    //draws extra buttons for basic game controls
                    DrawGamePlayButtons(spriteBatch);
                    break;
                
                //if gamestate is settings then draws new buttons 
                case SETTINGS:
                     
                    //draws menu background and home button
                    spriteBatch.Draw(menuBg, menuBgRect, Color.White);
                    spriteBatch.Draw(homeBtn, homeBtnRect, Color.White);

                    //depending on whether boolean is true and false, switches buttons on the screen
                    if (music == true)
                    {
                        spriteBatch.Draw(musicOnBtn, musicOnRect, Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(musicOffBtn, musicOnRect, Color.White);
                    }
                    if (volume == true)
                    {
                        spriteBatch.Draw(volumeOnBtn, volumeOnRect, Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(volumeOffBtn, volumeOnRect, Color.White);
                    }
                    break;

                //if gamestate is log in then draws background and home button (NOT COMPLETE)
                case LOGIN:
                    spriteBatch.Draw(menuBg, menuBgRect, Color.White);
                    spriteBatch.Draw(homeBtn, homeBtnRect, Color.White);
                    break;

                //if gamestate is help then draws background and home button (NOT COMPLETE)
                case HELP:
                    spriteBatch.Draw(menuBg, menuBgRect, Color.White);
                    spriteBatch.Draw(homeBtn, homeBtnRect, Color.White);
                    break;

                //if gamestate is pause then draws background and home button (NOT COMPLETE)
                case PAUSE:
                    spriteBatch.Draw(menuBg, menuBgRect, Color.White);
                    break;
            }

            spriteBatch.End();
        }

        //Pre: integer i (most likely from loop)
        //Post: none
        //Desc: adds ingredients to ingredient manager to restock them
        public void StockIngredients(int i)
        {
            //based on the name of the ingredient manager, add the corresponding ingredient
            switch (ingManager[i].GetName())
            {
                //if name is top burger bun, then add list of top burger buns with a scaled height to create a stack of them
                case "Top Burger Bun":
                    for (int j = 0; j < ingSize; j++)
                    {
                        ingManager[i].Enqueue(new Ingredients("Top Bun", topBurgerImg, screenWidth / 3 - plate.Width / 3, ingLocYBurger, topBurgerImg.Width / 3 + 20, topBurgerImg.Height / 3 + 20, 1));
                        ingLocYBurger += topBurgerImg.Height / 25;
                    }
                    ingLocYBurger = screenHeight - (buildStation.Height / 3 + 20) / 2 - plate.Height / 8;
                    break;

                //if name is bottom burger bun, then add list of bottom burger buns with a scaled height to create a stack of them
                case "Bottom Burger Bun":
                    for (int j = 0; j < ingSize; j++)
                    {
                        ingManager[i].Enqueue(new Ingredients("Bottom Bun", bottomBurgerImg, (screenWidth / 3) * 2, ingLocYBurger, bottomBurgerImg.Width / 3 + 20, bottomBurgerImg.Height / 3 + 20, 1));
                        ingLocYBurger += bottomBurgerImg.Height / 25;
                    }
                    ingLocYBurger = screenHeight - (buildStation.Height / 3 + 20) / 2 - plate.Height / 18;
                    break;

                //if name is patty, then add list of patties with a scaled height to create a stack of them
                case "Ham Patty":
                    for (int j = 0; j < ingSize; j++)
                    {
                        ingManager[i].Enqueue(new Ingredients("Patty", pattyImg, 0, ingLocYLeft, pattyImg.Width / 3 + 20, pattyImg.Height / 3 + 20, 1));
                        ingLocYLeft += pattyImg.Height / 25;
                    }
                    ingLocYLeft += 80;
                    break;

                //if name is cheese, then add list of cheese with a scaled height to create a stack of them
                case "Cheese":
                    for (int j = 0; j < ingSize; j++)
                    {
                        ingManager[i].Enqueue(new Ingredients("Cheese", cheeseImg, screenWidth - cheeseImg.Width / 2, ingLocYRight, cheeseImg.Width / 3 + 20, cheeseImg.Height / 3 + 20, 1));
                        ingLocYRight += cheeseImg.Height / 25;
                    }
                    ingLocYRight += 80;
                    break;

                //if name is lettuce, then add list of lettuce with a scaled height to create a stack of them
                case "Lettuce":
                    for (int j = 0; j < ingSize; j++)
                    {
                        ingManager[i].Enqueue(new Ingredients("Lettuce", lettuceImg, 0, ingLocYLeft, lettuceImg.Width / 3 + 20, lettuceImg.Height / 3 + 20, 1));
                        ingLocYLeft += lettuceImg.Height / 25;
                    }
                    ingLocYLeft += 80;
                    break;

                //if name is onion, then add list of onion with a scaled height to create a stack of them
                case "Onion":
                    for (int j = 0; j < ingSize; j++)
                    {
                        ingManager[i].Enqueue(new Ingredients("Onion", onionImg, screenWidth - onionImg.Width / 2, ingLocYRight, onionImg.Width / 3 + 20, onionImg.Height / 3 + 20, 1));
                        ingLocYRight += onionImg.Height / 25;
                    }
                    ingLocYRight += 80;
                    break;

                //if name is tomato, then add list of tomato with a scaled height to create a stack of them
                case "Tomato":
                    for (int j = 0; j < ingSize; j++)
                    {
                        ingManager[i].Enqueue(new Ingredients("Tomato", tomatoImg, screenWidth - tomatoImg.Width / 2, ingLocYRight, tomatoImg.Width / 3 + 20, tomatoImg.Height / 3 + 20, 1));
                        ingLocYRight += tomatoImg.Height / 25;
                    }

                    ingLocYRight += 80;
                    break;

                //if name is pickle, then add list of pickle with a scaled height to create a stack of them
                case "Pickle":
                    for (int j = 0; j < ingSize; j++)
                    {
                        ingManager[i].Enqueue(new Ingredients("Pickle", pickleImg, 0, ingLocYLeft, pickleImg.Width / 3 + 20, pickleImg.Height / 3 + 20, 1));
                        ingLocYLeft += pickleImg.Height / 25;
                    }
                    break;
            }
        }

        //Pre: spriteBatch to allow for drawing 
        //Post: none
        //Desc: draws the menu page
        public void DrawMenu (SpriteBatch spriteBatch)
        {
            //draws menu background
            spriteBatch.Draw(menuBg, menuBgRect, Color.White);

            //draws all the buttons of the menu
            spriteBatch.Draw(menuPlayBtn, menuPlayBtnRect, Color.White);
            spriteBatch.Draw(logInBtn, logInBtnRect, Color.White);
            spriteBatch.Draw(helpBtn, helpBtnRect, Color.White);
            spriteBatch.Draw(settingsBtn, settingsBtnRect, Color.White);
        }

        //Pre: spriteBatch to allow for drawing 
        //Post: none
        //Desc: draws the inventory image in the inventory room when you run out of an ingredient
        public void DrawInventoryImg (SpriteBatch spriteBatch)
        {
            //loop that loops through the length of inventory array which stores is an ingredient is run out and needs to be retrieved
            for (int i = 0; i < drawInventory.Length; i++)
            {
                //if ingredient has run out and needs to be retrieved
                if (drawInventory[i] == true)
                {
                    //use switch statement to figure out name of ingredient that has run out and then set temperory inventory image to the image of that ingredient 
                    switch (ingManager[i].GetName())
                    {
                        case "Top Burger Bun":
                            tempInvImg = topBurgerImg;
                            break;
                        case "Bottom Burger Bun":
                            tempInvImg = bottomBurgerImg;
                            break;
                        case "Ham Patty":
                            tempInvImg = pattyImg;
                            break;
                        case "Cheese":
                            tempInvImg = cheeseImg;
                            break;
                        case "Lettuce":
                            tempInvImg = lettuceImg;
                            break;
                        case "Onion":
                            tempInvImg = onionImg;
                            break;
                        case "Tomato":
                            tempInvImg = tomatoImg;
                            break;
                        case "Pickle":
                            tempInvImg = pickleImg;
                            break;
                    }

                    //draw image of run out ingredient at a randomized position in inventory room
                    spriteBatch.Draw(tempInvImg, invImgRect, Color.White);
                }
            }
        }

        //Pre: spriteBatch to allow for drawing 
        //Post: none
        //Desc: draws the order onto the order template in the order room
        public void DrawOrder (SpriteBatch spriteBatch)
        {
            //draws bottom burger and patty as they are in every order
            spriteBatch.Draw(bottomBurgerImg, orderIngRect[orderRectNum++], Color.White);
            spriteBatch.Draw(pattyImg, orderIngRect[orderRectNum++], Color.White);

            //if customer's order has tomato, then draws tomato
            if (customersQueues[0].Peek().Order().HasTomato())
            {
                spriteBatch.Draw(tomatoImg, orderIngRect[orderRectNum++], Color.White);
            }

            //if customer's order has pickle, then draws piclkle
            if (customersQueues[0].Peek().Order().HasPickle())
            {
                spriteBatch.Draw(pickleImg, orderIngRect[orderRectNum++], Color.White);
            }

            //if customer's order has onion, then draws onion
            if (customersQueues[0].Peek().Order().HasOnion())
            {
                spriteBatch.Draw(onionImg, orderIngRect[orderRectNum++], Color.White);
            }

            //if customer's order has cheese, then draws cheese
            if (customersQueues[0].Peek().Order().HasCheese())
            {
                spriteBatch.Draw(cheeseImg, orderIngRect[orderRectNum++], Color.White);
            }

            //if customer's order has lettuce, then draws lettuce
            if (customersQueues[0].Peek().Order().HasLettuce())
            {
                spriteBatch.Draw(lettuceImg, orderIngRect[orderRectNum++], Color.White);
            }

            //draws top burger bun (always is there)
            spriteBatch.Draw(topBurgerImg, orderIngRect[orderRectNum++], Color.White);

            //order rectangle num is set back to zero 
            orderRectNum = 0;
        }

        //Pre: spriteBatch to allow for drawing 
        //Post: none
        //Desc: draws the gameplay buttons for the gameplate state 
        public void DrawGamePlayButtons (SpriteBatch spriteBatch)
        {
            //based on boolean value draws play or pause button
            if (play == false)
            {
                spriteBatch.Draw(pauseBtn, pauseBtnRect, Color.White);
            }
            else
            {
                spriteBatch.Draw(playBtn, pauseBtnRect, Color.White);
            }

            //based on boolean value draws music on or off button
            if (music == true)
            {
                spriteBatch.Draw(musicOnBtn, musicOffRect, Color.White);
            }
            else
            {
                spriteBatch.Draw(musicOffBtn, musicOffRect, Color.White);
            }

            //based on boolean value draws volume on or off button
            if (volume == true)
            {
                spriteBatch.Draw(volumeOnBtn, volumeOffRect, Color.White);
            }
            else
            {
                spriteBatch.Draw(volumeOffBtn, volumeOffRect, Color.White);
            }

            //draws gameplay buttons
            spriteBatch.Draw(orderBtn, orderBtnRect, Color.White);
            spriteBatch.Draw(buildingBtn, buildingBtnRect, Color.White);
            spriteBatch.Draw(inventoryBtn, inventoryBtnRect, Color.White);
            spriteBatch.Draw(homeBtn, homeBtnRect, Color.White);
        }

        //Pre: none
        //Post: returns bool on whether burger made matches order
        //Desc: checks if the burger made matches the customers order 
        public bool CheckOrder ()
        {
            //creates boolean values that starts off as true
            bool correct = true;

            //loops through the customers order
            for (int i = 0; i < burg.Count; i++)
            {
                //if the burger made is not equal to the customers order at any point then changes boolean statement to false and breaks out of loop
                if (!(burg[i].GetName().Equals(customersQueues[0].Peek().Order().GetBurgIng()[i].GetName())))
                {
                    correct = false;
                    break;
                }
            }

            //return boolean value
            return correct;
        }

        //Pre: none
        //Post: none
        //Desc: creates rectangles to scale each ingredient on the order template in the order room
        public void AddOrderTemplate ()
        {
            //loops through ingredient count
            for (int i = 0; i < ingCount; i++)
            {
                //adds new rectangle with scaled height to draw each rectangle for the customers order in the order room
                orderIngRect.Add(new Rectangle(orderRect.Left + bottomBurgerImg.Width / 3 - 5, orderLoc,
                bottomBurgerImg.Width / 3, bottomBurgerImg.Height / 3));
                orderLoc -= 45;
            }
        }

        //Pre: none
        //Post: none
        //Desc: switches buttons of each general game control
        public void SwitchButtons ()
        {
            //if pause button is clicked then switch boolean values
            if (pauseBtnRect.Contains(mouse.X, mouse.Y))
            {
                if (play == false)
                {
                    play = true;
                }
                else
                {
                    play = false;
                }
            }

            ////if music button is clicked then switch boolean values
            if (musicOffRect.Contains(mouse.X, mouse.Y))
            {
                if (music == true)
                {
                    music = false;

                    //pause music
                    bgMusicInst.Pause();
                }
                else
                {
                    music = true;

                    //play music
                    bgMusicInst.Play();
                }
            }

            //if volume button is clicked then switch boolean values
            if (volumeOffRect.Contains(mouse.X, mouse.Y))
            {
                if (volume == true)
                {
                    volume = false;
                }
                else
                {
                    volume = true;
                }
            }

            //if home button is clicked then switch gamestate to menu
            if (homeBtnRect.Contains(mouse.X, mouse.Y))
            {
                gameState = MENU;
            }
        }

        //Pre: none
        //Post: none
        //Desc: controls the upates in the order room of gameplay 
        public void GameplayOrder ()
        {
            //if mouse left button is clicked and previously the left mouse button wasn't clicked
            if (mouse.LeftButton == ButtonState.Pressed && !(prevmb.LeftButton == ButtonState.Pressed))
            {
                //if order textbox if clicked then don't draw it and draw order template
                if (orderTextboxRect.Contains(mouse.X, mouse.Y))
                {
                    drawOrderTxt = false;
                    AddOrderTemplate();

                }

                //if give burger button is pressed
                if (giveBurgerBtnRect.Contains(mouse.X, mouse.Y))
                {
                    //if the coins from this order was 0 (incorrect order)
                    if (coins == 0)
                    {
                        //draw no coins string
                        drawNoCoins = true;
                        addCoins = false;
                    }

                    //if the coins form this order was not 0 (correct order)
                    else if (coins != 0)
                    {
                        //add coins from this order to total coins
                        totalCoins += coins;
                        
                        //draw the add coins string
                        drawNoCoins = false;
                        addCoins = true;

                        //write coins to file
                        WriteCoins();
                    }

                    //take customer off queue (new customer now shown)
                    customersQueues[0].Dequeue();

                    //draws order textbox 
                    drawOrderTxt = true;

                    //clears burger made in build station and resets burger last location
                    burg.Clear();
                    burgLastLoc = screenHeight - (buildStation.Height / 3 + 20) / 2 - plate.Height / 18;
                }
            }
        }

        //Pre: none
        //Post: none
        //Desc: controls the upates in the build room of gameplay 
        public void GameplayBuild ()
        {
            //loop that loops through the ingredients manager
            for (int i = 0; i < ingManager.Count; i++)
            {
                //if the first ingredient in the queue of each ingredient is not null
                if (ingManager[i].Peek() != null)
                {
                    //if left button is clicked
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        //if the rectangle of the first ingredient contains the mouse
                        if (ingManager[i].Peek().GetRect().Contains(mouse.X, mouse.Y))
                        {
                            //allow the ingredient to be dragged around the screen
                            ingManager[i].Peek().SetRectXY(mouse.X - ingManager[i].Peek().GetWidth() / 2, mouse.Y - ingManager[i].Peek().GetHeight() / 2);
                        }

                    }

                    //if the left button is released
                    if (mouse.LeftButton == ButtonState.Released && !(prevmb.LeftButton == ButtonState.Released))
                    {
                        //if the rectangle of the first ingredient intersects with the rectangle of the plate
                        if (ingManager[i].Peek().GetRect().Intersects(plateRect))
                        {
                            //allows for sound effect is volume is on 
                            if (volume == true)
                            {
                                ingSnd.CreateInstance().Play();
                            }

                            //the ingredient is too be draw so change boolean to true
                            drawIng[i] = true;

                            //add ingredient to the burger being made and take is off ingredient queue as well as scale burger rectangle
                            burg.Add(ingManager[i].Dequeue());
                            burg[burg.Count - 1].SetRectXY(screenWidth / 2 - plate.Width / 2 + 175, burgLastLoc);
                            burgLastLoc -= 11;

                        }
                    }
                }
            }

            //loop for each ingredients manager
            for (int i = 0; i < ingManager.Count; i++)
            {
                //if the queue size is zero (ran out of ingredients) the draw inventory
                if (ingManager[i].Size() == 0)
                {
                    drawInventory[i] = true;
                }
            }

            //loop for the arraw of inventory ingredients 
            for (int i = 0; i < drawInventory.Length; i++)
            {
                //if we ran out of ingredients 
                if (drawInventory[i] == true)
                {
                    //create two randome numbers
                    num1 = rnd.Next(0, screenWidth);
                    num2 = rnd.Next(0, screenHeight - buildingBtnRect.Height);

                    //use the two numbers to create a vector which can be helped to create rectangle for random position of ingredient
                    invLoc[i] = new Vector2(num1, num2);
                    invImgRect = new Rectangle((int)invLoc[i].X, (int)invLoc[i].Y,
                                                  lettuceImg.Width / 5, lettuceImg.Height / 5);

                }
            }

            //if left button is clicked
            if (mouse.LeftButton == ButtonState.Pressed && !(prevmb.LeftButton == ButtonState.Pressed))
            {
                //if done burger button is clicked
                if (doneBurgerBtnRect.Contains(mouse.X, mouse.Y))
                {
                    //if order matches the burger made
                    if (CheckOrder() == true)
                    {
                        //change gameplay to order
                        gamePlay = ORDER;

                        //coins earned will be the cost of the burger
                        coins = customersQueues[0].Peek().Order().Cost();
                    }
                    else
                    {
                        //changes gameplay to order
                        gamePlay = ORDER;

                        //set cost of the burger to 0
                        customersQueues[0].Peek().Order().SetCost(0);

                        //coins earned will be 0
                        coins = customersQueues[0].Peek().Order().Cost();
                    }
                }
            }
        }

        //Pre: none
        //Post: none
        //Desc: controls the upates in the inventory room of gameplay 
        public void GameplayInventory ()
        {
            //if left button of mouse is clicked and previously left button was not clicked
            if (mouse.LeftButton == ButtonState.Pressed && !(prevmb.LeftButton == ButtonState.Pressed))
            {
                //if the ingredient image in the inventory is clicked
                if (invImgRect.Contains(mouse.X, mouse.Y))
                {

                    //reset all the scaling variable
                    ingLocYLeft = 30;
                    ingLocYRight = 30;
                    ingLocYBurger = screenHeight - (buildStation.Height / 3 + 20) / 2 - plate.Height / 9;

                    //loop through the ingredients manager
                    for (int i = 0; i < ingManager.Count; i++)
                    {
                        //loops thorugh the size of each ingredient queue
                        for (int j = 0; j < ingSize; j++)
                        {
                            //clears the queue
                            ingManager[i].Dequeue();
                        }
                    }

                    //loops thorugh the ingredient manager
                    for (int i = 0; i < ingManager.Count; i++)
                    {
                        //calls method to restock ingredient
                        StockIngredients(i);
                        
                        //removes the ingredient from inventory (restocked on it)
                        drawInventory[i] = false;
                    }
                }
            }
        }

        //Pre: none
        //Post: none
        //Desc: read the coins saved from file
        public void ReadCoins ()
        {

            //try-catch block to try to read file
            try
            {
                //opens saved progress file
                inFile = File.OpenText("Saved Progress.txt");

                //reads total coins in from file and converts it to a integer
                totalCoins = Convert.ToInt32(inFile.ReadLine());
            }
            catch (Exception e)
            {
                
            }
            finally
            {
                //closes file if not null
                if (inFile != null)
                {
                    inFile.Close();
                }
            } 
        }

        //Pre: none
        //Post: none
        //Desc: writes the total numbers of coins to file
        public void WriteCoins ()
        {
            //try-catch block to try to write to file file
            try
            {
                //creates file to write too
                outFile = File.CreateText("Saved Progress.txt");

                //write total coins in the game so far
                outFile.WriteLine(totalCoins);
            }
            catch (Exception e)
            {

            }
            finally
            {
                //closes file if not null
                if (outFile != null)
                {
                    outFile.Close();
                }
            }   
        }
    }
}
