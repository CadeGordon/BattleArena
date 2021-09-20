using System;
using System.Collections.Generic;
using System.Text;

namespace BattleArena
{
    public enum ItemType
    {
        DEFENSE,
        ATTACK,
        NONE
    }


    //Test
    public struct Item
    {
        public string Name;
        public float StatBoost;
        public ItemType Type;
    }


   



    class Game
    {
        private bool _gameOver;
        private int _currentScene;
        private Player _player;
        private Entity[] _enemies;
        private int _currentEnemyIndex = 0;
        private Entity _currentEnemy;
        private string _playerName;
        private Item[] _batmanItems;
        private Item[] _robinItems;

        
        

        
        

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
            InitializeItems();
        }

        public void InitializeItems()
        {
            //Batman Gadgets
            Item grapplingHook = new Item { Name = "Grappling Hook", StatBoost = 5, ItemType = 1 };
            Item batterRang = new Item { Name = "BatterRang", StatBoost = 10, ItemType = 0 };

            //Robin Gadgets
            Item bowStaff = new Item { Name = "Bow Staff", StatBoost = 10, ItemType = 1 };
            Item throwingBird = new Item { Name = "Throwing Bird", StatBoost = 5, ItemType = 0 };

            //Initialize arrays
            _batmanItems = new Item[] { grapplingHook, batterRang };
            _robinItems = new Item[] { bowStaff, throwingBird };
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
            Console.ReadKey(true);

        }

        /// <summary>
        /// Gets an input from the player based on some given decision
        /// </summary>
        /// <param name="description">The context for the input</param>
        /// <param name="option1">The first option the player can choose</param>
        /// <param name="option2">The second option the player can choose</param>
        /// <returns></returns>
        int GetInput(string description, params string[] options)
        {
            string input = "";
            int inputRecieved = -1;

            while(inputRecieved == -1)
            {
                //Print options
                Console.WriteLine(description);
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine((i + 1) + ". " + options[i]);
                }
                Console.WriteLine("> ");

                //get input from player
                input = Console.ReadLine();

                //If the player typed an int...
                if(int.TryParse(input, out inputRecieved))
                {
                    //...decrement the input and check if it's within the bounds of the array
                    inputRecieved--;
                    if(inputRecieved < 0 || inputRecieved >= options.Length)
                    {
                        //set input recieved to be the default value
                        inputRecieved = -1;
                        //display error message
                        Console.WriteLine("Invalid Input");
                        Console.ReadKey(true);
                    }
                }
                //if the player didnt type and int
                else
                {
                    //Set input recieved to be the default value
                    inputRecieved = -1;
                    Console.WriteLine("inavlid input");
                    Console.ReadKey(true);
                }

                Console.Clear();
            }

            return inputRecieved;
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

            if (choice == 0)
            {
                _currentScene = 0;
                InitalizeEnemies();
            }
            else if (choice == 1)
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
            
            Console.WriteLine("Welcome! Please enter your name.");
            _playerName = Console.ReadLine();

            Console.Clear();

            int choice = GetInput("You've entered " + _playerName + ", are you sure you want to keep this name?", "Yes", "No");
            
            if (choice == 0)
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

            if (choice == 0)
            {
                _player = new Player(_playerName, 200, 150, 150, _batmanItems);
                _currentScene++;
            }
            else if (choice == 1)
            {
                _player = new Player(_playerName, 150, 100, 85, _robinItems);
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

       public void DisplayEquipitemMenu()
        {
            //Get item index
            int choice = GetInput("Select and item to equip.", _player.GetItemNames());

            //Equip item at given index
            if (!_player.TryEquipItem(choice))
                Console.WriteLine("You couldny find that item in you bag.");


            //Print feedback
            Console.WriteLine("You equipped " + _player.CurrentItem.Name + "!");
        }

        /// <summary>
        /// Simulates one turn in the current monster fight
        /// </summary>
        public void Battle()
        {
            float damageDealt = 0;

            DisplayStats(_player);
            DisplayStats(_currentEnemy);

            int choice = GetInput(  _currentEnemy.Name + " stands in front of you! What will you do?", "Attack", "Equip Item", "Remove Current Item");

            if (choice == 0)
            {
                damageDealt = _player.Attack(_currentEnemy);
                Console.WriteLine("You dealt " + damageDealt + " damage!");
            }
            else if (choice == 1)
            {
                DisplayEquipitemMenu();
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
