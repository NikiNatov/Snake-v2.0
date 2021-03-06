﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Engine;

namespace SnakeGame
{
    /// <summary>
    /// Class that runs game logic
    /// </summary>
    public class GameViewModel : BaseViewModel
    {
        #region Constants

        /// <summary>
        /// Width of the map
        /// </summary>
        private const int TERRAIN_WIDTH = 15;

        /// <summary>
        /// Height of the map
        /// </summary>
        private const int TERRAIN_HEIGHT = 15;

        /// <summary>
        /// Width and height of a tile
        /// </summary>
        private const int TILE_SIZE = 64;

        /// <summary>
        /// Snake width
        /// </summary>
        private const int SNAKE_WIDTH = 32;

        /// <summary>
        /// Snake height
        /// </summary>
        private const int SNAKE_HEIGHT = 64;

        /// <summary>
        /// Fruit width and height
        /// </summary>
        private const int FRUIT_SIZE = 32;

        #endregion

        #region Data members

        /// <summary>
        /// Random number generator for spawnging fruits
        /// </summary>
        private Random mRNG;

        /// <summary>
        /// The index in the game object list where fruits are being inserted
        /// </summary>
        private int mFruitInsertIndex;

        /// <summary>
        /// The tail position in the objects collection. Every game object that is not a snake piece is inserted before that index. All snake pieces 
        /// live at the end of the collection
        /// </summary>
        private int mTailIndex;

        /// <summary>
        /// Game timer that calls the update function every frame
        /// </summary>
        private DispatcherTimer mGameTimer;

        /// <summary>
        /// Spawns a fruit after specific time has passed
        /// </summary>
        private DispatcherTimer mFruitSpawner;

        /// <summary>
        /// Regulates the currently active potion effect lifetime
        /// </summary>
        private DispatcherTimer mEffectTimer;

        /// <summary>
        /// World map data
        /// </summary>
        private Map mWorldMap;

        /// <summary>
        /// Remaining time of the effect
        /// </summary>
        private int mPotionEffectRemainingTime;

        /// <summary>
        /// Game objects list
        /// </summary>
        private ObservableCollection<GameObject> mGameObjects;

        /// <summary>
        /// Player score
        /// </summary>
        private int mScore;

        /// <summary>
        /// Currently active potion
        /// </summary>
        private Potion mActivePoition;

        /// <summary>
        /// Current game state
        /// </summary>
        private GameState mGameState;

        /// <summary>
        /// Game difficulty
        /// </summary>
        private GameDifficulty mGameDifficulty;

        #endregion

        #region Properties

        /// <summary>
        /// Gets and sets the world map
        /// </summary>
        public Map WorldMap 
        {
            get { return mWorldMap; }
            set
            {
                if(mWorldMap != value)
                {
                    mWorldMap = value;
                    OnPropertyChanged(nameof(WorldMap));
                }
            }
        }

        /// <summary>
        /// Gets and sets the remaining time of the potion effect in seconds
        /// </summary>
        public int PotionEffectRemainingTime
        {
            get { return mPotionEffectRemainingTime; }
            set
            {
                if (mPotionEffectRemainingTime != value)
                {
                    mPotionEffectRemainingTime = value;
                    OnPropertyChanged(nameof(PotionEffectRemainingTime));
                }
            }
        }

        /// <summary>
        /// Gets and sets all game objects in the world 
        /// </summary>
        public ObservableCollection<GameObject> GameObjects
        {
            get { return mGameObjects; }
            set
            {
                if (mGameObjects != value)
                {
                    mGameObjects = value;
                    OnPropertyChanged(nameof(GameObjects));
                }
            }
        }

        /// <summary>
        /// Gets and sets player's score points
        /// </summary>
        public int Score
        {
            get { return mScore; }
            set
            {
                if (mScore != value)
                {
                    mScore = value;
                    OnPropertyChanged(nameof(Score));
                }
            }
        }

        /// <summary>
        /// Gets and sets the current active potion
        /// </summary>
        public Potion ActivePotion
        {
            get { return mActivePoition; }
            set
            {
                if (mActivePoition != value)
                {
                    mActivePoition = value;
                    OnPropertyChanged(nameof(ActivePotion));
                }
            }
        }

        /// <summary>
        /// Gets and sets the state of the game
        /// </summary>
        public GameState State
        {
            get { return mGameState; }
            set
            {
                if (mGameState != value)
                {
                    mGameState = value;
                    OnPropertyChanged(nameof(State));
                }
            }
        }

        /// <summary>
        /// Gets and sets the game difficulty
        /// </summary>
        public GameDifficulty DifficultyLevel
        {
            get { return mGameDifficulty; }
            set
            {
                if (mGameDifficulty != value)
                {
                    mGameDifficulty = value;
                    OnPropertyChanged(nameof(DifficultyLevel));
                }
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Command called when mouse is moved
        /// </summary>
        public ICommand MouseMoveCommand { get; set; }

        /// <summary>
        /// Command called when a key is pressed
        /// </summary>
        public ICommand KeyPressedCommand { get; set; }

        /// <summary>
        /// Command for pausing the game
        /// </summary>
        public ICommand PauseCommand { get; set; }

        /// <summary>
        /// Command for unpausing the game
        /// </summary>
        public ICommand UnpauseCommand { get; set; }

        /// <summary>
        /// Command for restarting the game
        /// </summary>
        public ICommand RestartCommand { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="window">The handle to the application window</param>
        public GameViewModel(GameDifficulty difficulty)
        {
            DifficultyLevel = difficulty;
            InitializeTimersAndRNG(difficulty);
            InitializeCommands();
            LoadAssets();
            InitializeWorldMap(difficulty);
            Restart();
        }

        #endregion

        #region Input events

        /// <summary>
        /// The function that executes when the mouse is moved
        /// </summary>
        /// <param name="e">Event arguments</param>
        private void OnMouseMove(Point mousePos)
        {
            if (State == GameState.PLAY)
            {
                for (int i = GameObjects.Count - 1; i >= mTailIndex; i--)
                {
                    Point objectPos = new Point(GameObjects[i].Transformation.Position.X, GameObjects[i].Transformation.Position.Y);
                    Vector direction = new Vector();

                    Point prevObjectPos = new Point();

                    if (i == GameObjects.Count - 1)
                    {
                        direction.X = mousePos.X - objectPos.X;
                        direction.Y = mousePos.Y - objectPos.Y;
                    }
                    else
                    {
                        prevObjectPos = new Point(GameObjects[i + 1].Transformation.Position.X, GameObjects[i + 1].Transformation.Position.Y);
                        direction.X = prevObjectPos.X - objectPos.X;
                        direction.Y = prevObjectPos.Y - objectPos.Y;
                    }

                    if (direction.Length > 100)
                        return;

                    double angle = Math.Atan2(direction.Y, direction.X) * (180 / Math.PI) - 90;

                    var newPos = GameMath.MoveTowards(i == GameObjects.Count - 1 ? mousePos : prevObjectPos, objectPos, 5.0, 45.0);

                    if (newPos != objectPos)
                    {
                        GameObjects[i].Transformation = new Transformation(newPos, angle);              
                    }
                }
            }
        }

        /// <summary>
        /// The function that executes when a key is pressed
        /// </summary>
        /// <param name="key">The keycode</param>
        private void OnKeyPressed(Key key)
        {
            if (State == GameState.GAME_OVER)
                return;

            if (key == Key.Escape)
            {
                if (State == GameState.PLAY)
                    PauseGame();
                else
                    UnpauseGame();
            }
        }

        #endregion

        #region Game events

        /// <summary>
        /// Function that runs every frame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnUpdate(object sender, EventArgs e)
        {
            var head = GameObjects.Last();
            for (int i = 0; i < GameObjects.Count - 3; i++)
            {
                if (head.CollidesWith(GameObjects[i]))
                {
                    if (GameObjects[i] is Fruit fruit)
                    {
                        Score += ActivePotion?.Effect == PotionEffect.PointsX2 ? fruit.ScorePoints * 2 : fruit.ScorePoints;
                        RemoveGameObject(fruit);
                        GrowSnake();
                        return;
                    }
                    else if(GameObjects[i] is Potion potion)
                    {
                        ActivePotion = potion;
                        PotionEffectRemainingTime = 10;
                        mEffectTimer.Start();
                        RemoveGameObject(potion);
                        return;
                    }
                    else if (GameObjects[i] is Obstacle || GameObjects[i] is SnakeComponent)
                    {
                        State = GameState.GAME_OVER;
                        mFruitSpawner.Stop();
                        mGameTimer.Stop();
                        mEffectTimer.Stop();
                    }
                }
            }
        }

        /// <summary>
        /// Spawns a fruit in a random location
        /// </summary>
        private void OnFruitSpawn(object sender, EventArgs e)
        {
            if (mTailIndex - mFruitInsertIndex > 5)
                return;

            while (true)
            {
                bool validPosition = true;
                int x = mRNG.Next(80, 800);
                int y = mRNG.Next(80, 800);

                GameObject fruit = null;
                int fruitType = mRNG.Next(1, 5);

                switch(fruitType)
                {
                    case 1:
                        fruit = new Fruit(10, FRUIT_SIZE, FRUIT_SIZE, TextureManager.GetTexture("Assets\\Textures\\Fruits\\pineapple.png"), new Point(x, y), 0, new Point(1.0, 1.0));
                        break;
                    case 2:
                        fruit = new Fruit(8, FRUIT_SIZE, FRUIT_SIZE, TextureManager.GetTexture("Assets\\Textures\\Fruits\\carrot.png"), new Point(x, y), 0, new Point(1.0, 1.0));
                        break;
                    case 3:
                        fruit = new Fruit(5, FRUIT_SIZE, FRUIT_SIZE, TextureManager.GetTexture("Assets\\Textures\\Fruits\\apple.png"), new Point(x, y), 0, new Point(1.0, 1.0));
                        break;
                    case 4:
                        fruit = new Potion(PotionEffect.PointsX2, FRUIT_SIZE, FRUIT_SIZE, TextureManager.GetTexture("Assets\\Textures\\Potions\\potion_double_score.png"), new Point(x, y), 0, new Point(1.0, 1.0));
                        break;
                }
                

                foreach (var obj in GameObjects)
                {
                    if (obj.CollidesWith(fruit))
                    {
                        validPosition = false;
                        break;
                    }
                }

                if (validPosition)
                {
                    AddGameObject(fruit);
                    return;
                }
            }
        }

        /// <summary>
        /// Decreases the remaining lifetime of the currently active potion effect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEffectCountdown(object sender, EventArgs e)
        {
            if (--PotionEffectRemainingTime == 0)
            {
                ActivePotion = null;
                mEffectTimer.Stop();
            }
        }

        #endregion


        #region Private methods

        /// <summary>
        /// Creates all timer objects and random number generators and also initializes them when the game starts depending on the game difficulty
        /// </summary>
        /// <param name="difficulty">The game difficulty selected by the user</param>
        private void InitializeTimersAndRNG(GameDifficulty difficulty)
        {
            mRNG = new Random();

            mEffectTimer = new DispatcherTimer();
            mEffectTimer.Interval = TimeSpan.FromSeconds(1.0);
            mEffectTimer.Tick += OnEffectCountdown;

            mGameTimer = new DispatcherTimer();
            mGameTimer.Interval = TimeSpan.FromMilliseconds(16.7);
            mGameTimer.Tick += OnUpdate;
            mGameTimer.Start();

            mFruitSpawner = new DispatcherTimer();

            switch (difficulty)
            {
                case GameDifficulty.Easy:
                {
                    mFruitSpawner.Interval = TimeSpan.FromSeconds(6.0);
                    break;
                }
                case GameDifficulty.Medium:
                {
                    mFruitSpawner.Interval = TimeSpan.FromSeconds(5.0);
                    break;
                }
                case GameDifficulty.Hard:
                {
                    mFruitSpawner.Interval = TimeSpan.FromSeconds(4.0);
                    break;
                }
            }

            mFruitSpawner.Tick += OnFruitSpawn;
            mFruitSpawner.Start();
        }

        /// <summary>
        /// Creates all command objects that this viewmodel needs
        /// </summary>
        private void InitializeCommands()
        {
            MouseMoveCommand = new MouseMoveCommand(OnMouseMove);
            KeyPressedCommand = new KeyPressedCommand(OnKeyPressed);
            PauseCommand = new ApplicationCommand(PauseGame);
            UnpauseCommand = new ApplicationCommand(UnpauseGame);
            RestartCommand = new ApplicationCommand(Restart);
        }

        /// <summary>
        /// Creates the world map based on the difficulty 
        /// </summary>
        /// <param name="difficulty">The game difficulty specified by the player</param>
        private void InitializeWorldMap(GameDifficulty difficulty)
        {
            switch (difficulty)
            {
                case GameDifficulty.Easy:
                {
                    WorldMap = new Map("Assets\\Levels\\easy_map.png", TERRAIN_WIDTH, TERRAIN_HEIGHT);
                    break;
                }
                case GameDifficulty.Medium:
                {
                    WorldMap = new Map("Assets\\Levels\\medium_map.png", TERRAIN_WIDTH, TERRAIN_HEIGHT);
                    break;
                }
                case GameDifficulty.Hard:
                {
                    WorldMap = new Map("Assets\\Levels\\hard_map.png", TERRAIN_WIDTH, TERRAIN_HEIGHT);
                    break;
                }
            }
        }

        /// <summary>
        /// Loads every asset required for the game such as textures and levels
        /// </summary>
        private void LoadAssets()
        {
            string[] textures = Directory.GetFiles("Assets\\", "*.*", SearchOption.AllDirectories);

            foreach(var texture in textures)
                TextureManager.UploadTexture(texture);
            
        }

        /// <summary>
        /// Creates a new game world and initializes its map
        /// </summary>
        /// <param name="mapName">The image file name of the map</param>
        /// <param name="width">The number of tiles in the X direction</param>
        /// <param name="height">The number of tiles in the Y direction</param>
        private void CreateWorld()
        {
            GameObjects = new ObservableCollection<GameObject>();

            for (int i = 0; i < WorldMap.Width; i++)
                for (int j = 0; j < WorldMap.Height; j++)
                    if (WorldMap.MapData[j * 4 + i * WorldMap.Stride] == 0 && WorldMap.MapData[j * 4 + 1 + i * WorldMap.Stride] == 0 && WorldMap.MapData[j * 4 + 2 + i * WorldMap.Stride] == 0)
                        GameObjects.Add(new Obstacle(TILE_SIZE, TILE_SIZE, TextureManager.GetTexture("Assets\\Textures\\Obstacles\\bush.png"), new Point(j * TILE_SIZE, i * TILE_SIZE), 0, new Point(1.0, 1.0)));
        }

        /// <summary>
        /// Creates the snake
        /// </summary>
        private void CreateSnake()
        {
            SnakeComponent head = new SnakeComponent(SNAKE_WIDTH, SNAKE_HEIGHT, TextureManager.GetTexture("Assets\\Textures\\Snake\\snake_head.png"), new Point(80, 104), 0, new Point(1.0, 1.0));
            SnakeComponent tail = new SnakeComponent(SNAKE_WIDTH, SNAKE_HEIGHT, TextureManager.GetTexture("Assets\\Textures\\Snake\\snake_tail.png"), new Point(80, 64), 0, new Point(1.0, 1.0));

            GameObjects.Add(tail);
            mTailIndex = GameObjects.Count - 1;
            GameObjects.Add(head);

            GrowSnake();
        }

        /// <summary>
        /// Inserts a new piece to the snake's body
        /// </summary>
        private void GrowSnake()
        {
            double xPos = GameObjects[mTailIndex].Transformation.Position.X;
            double yPos = GameObjects[mTailIndex].Transformation.Position.Y;
            double rotation = GameObjects[mTailIndex].Transformation.RotationAngle;
            SnakeComponent piece = new SnakeComponent(SNAKE_WIDTH, SNAKE_HEIGHT, TextureManager.GetTexture("Assets\\Textures\\Snake\\snake_body.png"), new Point(xPos, yPos), rotation, new Point(1.0, 1.0));
            AddGameObject(piece);
        }

        /// <summary>
        /// Adds a game object in the list of objects. Objects that are not snake pieces get added before the snake's tail.
        /// </summary>
        /// <param name="obj">The object to be removed</param>
        private void AddGameObject(GameObject obj)
        {
            if (obj is Obstacle)
            {
                GameObjects.Insert(mFruitInsertIndex - 1, obj);
                mFruitInsertIndex++;
            }
            else if(obj is Fruit || obj is Potion)
            {
                GameObjects.Insert(mFruitInsertIndex, obj);
                mTailIndex++;
            }
            else
                GameObjects.Insert(mTailIndex + 1, obj);
        }

        /// <summary>
        /// Removes an object from the game objects list
        /// </summary>
        /// <param name="obj">The object to be removed</param>
        private void RemoveGameObject(GameObject obj)
        {
            if (obj is Obstacle)
            {
                mTailIndex--;
                mFruitInsertIndex--;
            }
            else if(obj is Fruit || obj is Potion)
            {
                mTailIndex--;
            }

            GameObjects.Remove(obj);
        }

        /// <summary>
        /// Pauses the game and opens the pause menu
        /// </summary>
        private void PauseGame()
        {
            if (State == GameState.GAME_OVER)
                return;

            State = GameState.PAUSE;
            mGameTimer.Stop();
            mFruitSpawner.Stop();
            mEffectTimer.Stop();
        }

        /// <summary>
        /// Unpauses the game and closes the pause menu
        /// </summary>
        private void UnpauseGame()
        {
            if (State == GameState.GAME_OVER)
                return;

            State = GameState.PLAY;
            mGameTimer.Start();
            mFruitSpawner.Start();
            if (ActivePotion != null)
                mEffectTimer.Start();
        }

        /// <summary>
        /// Restarts the game
        /// </summary>
        private void Restart()
        {
            State = GameState.PLAY;
            Score = 0;
            ActivePotion = null;
            PotionEffectRemainingTime = 0;
            CreateWorld();
            CreateSnake();
            mFruitInsertIndex = mTailIndex - 1;
            mFruitSpawner.Start();
            mGameTimer.Start();
        }

        #endregion
    }
}
