using System;
using System.Collections.Generic;
using System.Text;

namespace BattleArena
{
    //Test


   



    class Game
    {
        private string _playerName = "";
        private bool _gameOver;
        private int _currentScene;
        private Entity _player;
        private Entity[] _enemies;
        private int _currentEnemyIndex = 0;
        private Entity _currentEnemy;
        private string playerName;

        

        
        

        /// <summary>
        /// Function that starts the main game loop
        /// </summary>
        public void Run()
        {
            Start();

            while (!_gameOver)
            {
                Update();
            }

            End();
        }

        /// <summary>
        /// Function used to initialize any starting values by default
        /// </summary>
        public void Start()
        {
            _gameOver = false;
            _currentScene = 0;
            InitalizeEnemies();


           
        }

        public void InitalizeEnemies()
        {
            _currentEnemyIndex = 0;

            Entity riddler = new Entity("Riddler", 50, 180, 35);

            Entity mrfreeze = new Entity("Mr. Freeze", 85, 175, 70);

            Entity joker = new Entity("Joker", 120, 195, 60);

            _enemies = new Entity[] { riddler, mrfreeze, joker };

            _currentEnemy = _enemies[_currentEnemyIndex];
        }

        /// <summary>
        /// This function is called every time the game loops.
        /// </summary>
        public void Update()
        {
            DisplayCurrentScene();
            

        }

        /// <summary>
        /// This function is called before the applications closes
        /// </summary>
        public void End()
        {

            Console.WriteLine("You saved Gotham!!!");

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
            switch (_currentScene)
            {
                case 0:
                    GetPlayerName();
                    break;
                case 1:
                    CharacterSelection();
                    break;
                case 2:
                    Battle();
                    CheckBattleResults();
                    break;
                case 3:
                    DisplayMainMenu();
                    break;
                    
            }
            


        }

        /// <summary>
        /// Displays the menu that allows the player to start or quit the game
        /// </summary>
        void DisplayMainMenu()
        {
            int choice = GetInput("Would like to go back into Gotham?", "Yes", "No");

            if (choice == 1)
            {
                _currentScene = 0;
                InitalizeEnemies();
            }
            else if (choice == 2)
            {
                _gameOver = true;
            }
            

            
            

        }

        /// <summary>
        /// Displays text asking for the players name. Doesn't transition to the next section
        /// until the player decides to keep the name.
        /// </summary>
        void GetPlayerName()
        {
            string input = "";
            Console.WriteLine("Welcome! Please enter your name.");
            _playerName = Console.ReadLine();

            Console.Clear();

            int choice = GetInput("You've entered " + _playerName + ", are you sure you want to keep this name?", "Yes", "No");
            
            if (choice == 1)
            {
                _currentScene++;
            }
           
            

        }

        /// <summary>
        /// Gets the players choice of character. Updates player stats based on
        /// the character chosen.
        /// </summary>
        public void CharacterSelection()
        {
            int choice = GetInput("Welcome to Gotham " + _playerName + ", choose your character", "Batman", "Robin");

            if (choice == 1)
            {
                _player = new Entity(_playerName, 200, 150, 150);
                _currentScene++;
            }
            else if (choice == 2)
            {
                _player = new Entity(_playerName, 150, 100, 85);
                _currentScene++;
            }
            
            
        }

        /// <summary>
        /// Prints a characters stats to the console
        /// </summary>
        /// <param name="character">The character that will have its stats shown</param>
        void DisplayStats(Entity character)
        {

            Console.WriteLine("Name: " + character.Name);
            Console.WriteLine("Health: " + character.Health);
            Console.WriteLine("Attack: " + character.AttackPower);
            Console.WriteLine("Defense: " + character.DefensePower);
            Console.WriteLine();



        }

       

        /// <summary>
        /// Simulates one turn in the current monster fight
        /// </summary>
        public void Battle()
        {
            float damageDealt = 0;

            DisplayStats(_player);
            DisplayStats(_currentEnemy);

            int choice = GetInput(  _currentEnemy.Name + " stands in front of you! What will you do?", "Attack", "Dodge");

            if (choice == 1)
            {
                damageDealt = _player.Attack(_currentEnemy);
                Console.WriteLine("You dealt " + damageDealt + " damage!");
            }
            else if (choice == 2)
            {
                Console.WriteLine("You dodged the enemies attack!");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            damageDealt = _currentEnemy.Attack(_player);
            Console.WriteLine( _currentEnemy.Name + " dealt " + damageDealt + " damage!");

            Console.ReadKey(true);
            Console.Clear();

                
            
            


        }

        /// <summary>
        /// Checks to see if either the player or the enemy has won the current battle.
        /// Updates the game based on who won the battle..
        /// </summary>
        void CheckBattleResults()
        {
            if(_player.Health <= 0)
            {
                Console.WriteLine("You have failed and now Gotham shall fall!!!");
                Console.ReadKey(true);
                Console.Clear();
                _currentScene = 3;
            }
            else if (_currentEnemy.Health <= 0)
            {
                Console.WriteLine("You sent " + _currentEnemy.Name + " back to Arkham");
                Console.ReadKey();
                Console.Clear();
                _currentEnemyIndex++;

                if (_currentEnemyIndex >= _enemies.Length)
                {
                    _currentScene = 3;
                    Console.WriteLine("You defeated all the escaped villains and sent them back to Arkham");
                    return;
                }

                _currentEnemy = _enemies[_currentEnemyIndex];
            }




        }

    }
}
