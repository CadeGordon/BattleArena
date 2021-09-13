using System;
using System.Collections.Generic;
using System.Text;

namespace BattleArena
{
    //Test


    /// <summary>
    /// Represents any entity that exists in game
    /// </summary>
    struct Character
    {


        public string name;
        public float health;
        public float attackPower;
        public float defensePower;
    }



    class Game
    {
        string playerName = "";
        bool gameOver;
        int currentScene;
        Character player;
        Character[] enemies;
        private int currentEnemyIndex = 0;
        private Character currentEnemy;

        //Playable Characters
        Character Batman;
        Character Robin;


        //Bad guys
        Character MrFreeze;
        Character Joker;
        Character Riddler;

        
        

        /// <summary>
        /// Function that starts the main game loop
        /// </summary>
        public void Run()
        {
            Start();

            
            
        }

        /// <summary>
        /// Function used to initialize any starting values by default
        /// </summary>
        public void Start()
        {
            //initalzie Characters
            Batman.name = "Batman";
            Batman.health = 200.0f;
            Batman.attackPower = 150.0f;
            Batman.defensePower = 125.0f;

            Robin.name = "Robin";
            Robin.health = 150.0f;
            Robin.attackPower = 100.0f;
            Robin.defensePower = 95;

            MrFreeze.name = "Mr.Freeze";
            MrFreeze.health = 85.0f;
            MrFreeze.attackPower = 75.0f;
            MrFreeze.defensePower = 70.0f;

            Joker.name = "Joker";
            Joker.health = 75.0f;
            Joker.attackPower = 65.0f;
            Joker.health = 60.0f;

            Riddler.name = "Riddler";
            Riddler.health = 50.0f;
            Riddler.attackPower = 40.0f;
            Riddler.defensePower = 35.0f;


            GetPlayerName();
            CharacterSelection();
            Battle();
        }

        /// <summary>
        /// This function is called every time the game loops.
        /// </summary>
        public void Update()
        {
            DisplayCurrentScene();
            Console.Clear();

        }

        /// <summary>
        /// This function is called before the applications closes
        /// </summary>
        public void End()
        {

            Console.WriteLine("add cool words later");

        }

        /// <summary>
        /// Gets an input from the player based on some given decision
        /// </summary>
        /// <param name="description">The context for the input</param>
        /// <param name="option1">The first option the player can choose</param>
        /// <param name="option2">The second option the player can choose</param>
        /// <returns></returns>
        int GetInput(string description, string option1, string option2)
        {
            string input = "";
            int inputReceived = 0;

            while (inputReceived != 1 && inputReceived != 2)
            {//Print options
                Console.WriteLine(description);
                Console.WriteLine("1. " + option1);
                Console.WriteLine("2. " + option2);
                Console.Write("> ");

                //Get input from player
                input = Console.ReadLine();

                //If player selected the first option...
                if (input == "1" || input == option1)
                {
                    //Set input received to be the first option
                    inputReceived = 1;
                }
                //Otherwise if the player selected the second option...
                else if (input == "2" || input == option2)
                {
                    //Set input received to be the second option
                    inputReceived = 2;
                }
                //If neither are true...
                else
                {
                    //...display error message
                    Console.WriteLine("Invalid Input");
                    Console.ReadKey();
                }

                Console.Clear();
            }
            return inputReceived;
        }

        /// <summary>
        /// Calls the appropriate function(s) based on the current scene index
        /// </summary>
        void DisplayCurrentScene()
        {

            


        }

        /// <summary>
        /// Displays the menu that allows the player to start or quit the game
        /// </summary>
        void DisplayMainMenu()
        {

            GetPlayerName();
            CharacterSelection();

        }

        /// <summary>
        /// Displays text asking for the players name. Doesn't transition to the next section
        /// until the player decides to keep the name.
        /// </summary>
        void GetPlayerName()
        {
            string input = "";
            Console.WriteLine("Welcome! Please enter your name.");
            playerName = Console.ReadLine();
            Console.WriteLine("You have entered, " + playerName );
            GetInput("are you sure you want to keep this name?", "Yes", "No");
            if (input == "1")
            {
                Console.WriteLine("welcome, " + playerName);
            }
           
            

        }

        /// <summary>
        /// Gets the players choice of character. Updates player stats based on
        /// the character chosen.
        /// </summary>
        public void CharacterSelection()
        {
            
            GetInput("Choose your character", "Batman", "Robin");
            
            
        }

        /// <summary>
        /// Prints a characters stats to the console
        /// </summary>
        /// <param name="character">The character that will have its stats shown</param>
        void DisplayStats(Character character)
        {

            Console.WriteLine("Name: " + character.name);
            Console.WriteLine("Health: " + character.health);
            Console.WriteLine("Attack: " + character.attackPower);
            Console.WriteLine("Defense: " + character.defensePower);



        }

        /// <summary>
        /// Calculates the amount of damage that will be done to a character
        /// </summary>
        /// <param name="attackPower">The attacking character's attack power</param>
        /// <param name="defensePower">The defending character's defense power</param>
        /// <returns>The amount of damage done to the defender</returns>
        float CalculateDamage(float attackPower, float defensePower)
        {
            float damage = attackPower - defensePower;
            if (damage <= 0)
            {
                damage = 0;
            }

            return damage;
        }

        

        /// <summary>
        /// Deals damage to a character based on an attacker's attack power
        /// </summary>
        /// <param name="attacker">The character that initiated the attack</param>
        /// <param name="defender">The character that is being attacked</param>
        /// <returns>The amount of damage done to the defender</returns>
        public float Attack(ref Character attacker, ref Character defender)
        {
            float damagetaken = CalculateDamage(attacker.attackPower, defender.defensePower);
            defender.health -= damagetaken;
            Console.WriteLine(defender.name + " has taken " + damagetaken);
            return damagetaken;
            
        }

        /// <summary>
        /// Simulates one turn in the current monster fight
        /// </summary>
        public void Battle()
        {
            string matchResult = "No Contest";
            while(Batman.health > 0 && MrFreeze.health > 0)
            {
                DisplayStats(Batman);

                DisplayStats(MrFreeze);

                float damagetaken = Attack(ref Batman, ref MrFreeze);
                Console.WriteLine(Batman.name + " has taken " + damagetaken);

                damagetaken = Attack(ref MrFreeze, ref Batman);
                Console.WriteLine(Batman.name + " has taken " + damagetaken);

                Console.ReadKey();
                Console.Clear();

                
            }
            


        }

        /// <summary>
        /// Checks to see if either the player or the enemy has won the current battle.
        /// Updates the game based on who won the battle..
        /// </summary>
        void CheckBattleResults()
        {
        }

    }
}
