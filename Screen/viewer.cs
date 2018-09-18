using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragons_Game.Screen
{
    class viewer
    {
        /// <summary>
        /// Inicializace maincontrolleru a herních dat
        /// </summary>
        Controller.maincontroller main = new Controller.maincontroller();
        GameData.data gameData = new GameData.data();

        int _menuSelector = 1;
        bool _boolselector = false;

        private int viewer_save_id = 0; 

        /// <summary>
        /// Inicializace vieweru
        /// </summary>
        public void init()
        {
            this.screen("menu");
        }

        /// <summary>
        /// screen(string s, bool clear = true) - Zobrazení konkrétního screenu 
        /// </summary>
        /// <param name="s">Název screenu</param>
        /// <param name="clear">Parametr určující zda-li se předchozí screen smaže</param>
        public void screen(string s, bool clear = true)
        {
            bool screen_show = true;
            bool screen_wait = true;
            while (screen_show == true)
            {
                if (clear == true) this.clear();
                GameData.models m = new GameData.models();
                //this.write(m.v("gameLogo"));
                switch (s.ToLower())
                {
                    case "menu":
                        screen_wait = false;
                        this.write(m.v("gamelogo"));

                        //this.write("menu:" + _menuSelector);

                        if (_menuSelector < 1) _menuSelector = 1;
                        else if (_menuSelector > 3) _menuSelector = 3;

                        this.write("\t", false);
                        if (_menuSelector == 1)
                            this.cwrite("Hrát hru", ConsoleColor.Black, ConsoleColor.Green);
                        else
                            this.cwrite("Hrát hru", ConsoleColor.Green, ConsoleColor.Black);

                        this.write("\t", false);
                        if (_menuSelector == 2)
                            this.cwrite("O hře", ConsoleColor.Black, ConsoleColor.Green);
                        else
                            this.cwrite("O hře", ConsoleColor.Green, ConsoleColor.Black);

                        this.write("\t", false);
                        if (_menuSelector == 3)
                            this.cwrite("Ukončit hru", ConsoleColor.Black, ConsoleColor.Green);
                        else
                            this.cwrite("Ukončit hru", ConsoleColor.Green, ConsoleColor.Black);

                        ConsoleKeyInfo key = Console.ReadKey();

                        if (key.Key.ToString() == "Escape")
                            this.screen("end_screen");
                        else if (key.Key.ToString() == "UpArrow")
                            _menuSelector -= 1;
                        else if (key.Key.ToString() == "DownArrow")
                            _menuSelector += 1;
                        else if (key.Key.ToString() == "Enter")
                        {
                            /*
                            this.write("_menuSelector: "+ _menuSelector);
                            Console.ReadKey();
                            */
                            switch (_menuSelector)
                            {
                                case 1:
                                    this.screen("game");
                                    break;
                                case 2:
                                    this.screen("about");
                                    break;
                                case 3:
                                    this.screen("end_screen");
                                    break;
                                default: break;
                            }
                        }

                        break;
                    case "game":
                        gameData.checkGameFiles();
                        this.cwrite("Načíst hru:", ConsoleColor.Yellow, ConsoleColor.Black);
                        this.write("1) Nová hra");

                        string[] _saveNames = gameData.getGameSaveNames();
                        for (int save_id = 0; save_id < _saveNames.Length; save_id++)
                        {
                            string[] _sNames = _saveNames[save_id].Split('\\');
                            string[] _sN = _sNames[_sNames.Length - 1].Replace(".w3csave", "").Split('_');
                            string saveName = _sN[1] + " " + _sN[2].Replace("T", " ");
                            this.write((save_id + 2) + ") Uložená hra - " + saveName);
                        }
                        if (_saveNames.Length == 0)
                            this.write("> Zatím nemáte uložený žádný postup.");

                        this.enter(2);

                        bool choice_result = false;
                        while (choice_result == false)
                        {
                            this.write("Pokud napíšete 0, budete přesměrování do menu.");
                            this.write("Načíst hru (číslo hry): ", false);
                            string savesChoice = Console.ReadLine();

                            int choice_id = 0;
                            choice_result = int.TryParse(savesChoice, out choice_id);
                            if(choice_result == true)
                            {
                                if (choice_id == 1)
                                {
                                    viewer_save_id = 0;
                                    this.clear();
                                    Game.game game = new Game.game();
                                    game.gameInit();
                                }
                                else if(choice_id == 0)
                                {
                                    viewer_save_id = 0;
                                    this.clear();
                                    this.screen("menu");
                                }
                                else if(choice_id > 1 && choice_id <= _saveNames.Length+1)
                                {
                                    viewer_save_id = choice_id - 2;
                                    this.clear();
                                    Game.game game = new Game.game();
                                    game.gameInit(viewer_save_id);
                                }
                                else
                                {
                                    choice_result = false;
                                    this.write("Neplatná volba, musí být zadáno platné číslo načítané hry.");
                                }

                            }
                            else
                            {
                                this.write("Neplatná volba, musí být zadáno číslo načítané hry.");
                            }
                        }
                        break;
                    case "about":
                        this.write("O hře ", false);
                        this.cwrite(main.getGameName(), ConsoleColor.Red, ConsoleColor.Black);
                        this.write("Verze hry: ", false);
                        this.cwrite(main.getGameVersion(), ConsoleColor.Yellow, ConsoleColor.Black);

                        this.write("Příběh zaklínače je zde upravený k obrazu hry, některé věci se nemusí shodovat se zaklínačem.");
                        this.write("Nesrovnalosti jsou tím pádem možny, a vyskytují se v celku častě.");

                        this.enter(1);
                        this.write("Naprogramoval: ", false);
                        this.cwrite(main.getGameAuthor(), ConsoleColor.Green, ConsoleColor.Black);

                        this.enter(1);
                        this.write("Pro odchod do menu stiskněte libovolnou klávesu.");

                        Console.ReadKey();
                        this.screen("menu");
                        break;
                    case "end_game":
                        screen_wait = false;
                        this.cwrite("Opravdu chcete ukončit hru?", ConsoleColor.Red, ConsoleColor.Black);

                        this.write("\t", false);
                        if (_boolselector == true)
                        {
                            this.cwrite("Ano", ConsoleColor.Black, ConsoleColor.Green, false);
                            this.write("\t", false);
                            this.cwrite("Ne", ConsoleColor.Green, ConsoleColor.Black, false);
                        }
                        else
                        {
                            this.cwrite("Ano", ConsoleColor.Green, ConsoleColor.Black, false);
                            this.write("\t", false);
                            this.cwrite("Ne", ConsoleColor.Black, ConsoleColor.Green, false);
                        }
                        this.enter(2);

                        ConsoleKeyInfo ka = Console.ReadKey();

                        if (ka.Key.ToString() == "Escape")
                            this.write("x", false);
                        else if (ka.Key.ToString() == "LeftArrow")
                            _boolselector = true;
                        else if (ka.Key.ToString() == "RightArrow")
                            _boolselector = false;
                        else if (ka.Key.ToString() == "Enter")
                        {
                            Game.game game = new Game.game();
                            switch (_boolselector)
                            {
                                case false:
                                    this.clear();                    
                                    game.gameInit(viewer_save_id);
                                    _boolselector = false;
                                    break;
                                case true:
                                    _menuSelector = 1;
                                    this.screen("menu");
                                    _boolselector = false;
                                    break;
                                default: break;
                            }
                        }
                        break;
                    case "end_screen":
                        screen_wait = false;
                        this.cwrite("Opravdu chcete ukončit hru?", ConsoleColor.Red, ConsoleColor.Black);

                        this.write("\t", false);
                        if (_boolselector == true)
                        {
                            this.cwrite("Ano", ConsoleColor.Black, ConsoleColor.Green, false);
                            this.write("\t", false);
                            this.cwrite("Ne", ConsoleColor.Green, ConsoleColor.Black, false);
                        }
                        else
                        {
                            this.cwrite("Ano", ConsoleColor.Green, ConsoleColor.Black, false);
                            this.write("\t", false);
                            this.cwrite("Ne", ConsoleColor.Black, ConsoleColor.Green, false);
                        }
                        this.enter(2);

                        ConsoleKeyInfo k = Console.ReadKey();

                        if (k.Key.ToString() == "Escape")
                            this.write("x", false);
                        else if (k.Key.ToString() == "LeftArrow")
                            _boolselector = true;
                        else if (k.Key.ToString() == "RightArrow")
                            _boolselector = false;
                        else if (k.Key.ToString() == "Enter")
                        {
                            switch (_boolselector)
                            {
                                case false:
                                    _boolselector = false;
                                    _menuSelector = 1;
                                    this.screen("menu");
                                    break;
                                case true:
                                    _boolselector = false;
                                    this.clear();
                                    this.cwrite("Hra bude ukončena.", ConsoleColor.Red, ConsoleColor.Black);
                                    System.Environment.Exit(1);
                                    break;
                                default: break;
                            }
                            _boolselector = false;
                        }
                        break;
                    default:
                        this.write("Screen \"", false);
                        this.cwrite(s.ToLower(), ConsoleColor.Red, ConsoleColor.Black, false);
                        this.write("\" doesn't exists.");
                        break;
                }
                if (screen_wait == true) System.Threading.Thread.Sleep(main.getConsoleScreenSleepTime());
            }
        }

        /// <summary>
        /// cwrite(string text, ConsoleColor color_text, ConsoleColor color_background, bool line = true) - Psaní barevně (text i bg), rozlišení konce řádku
        /// </summary>
        /// <param name="text">Vypisovaný text</param>
        /// <param name="color_text">Barva textu</param>
        /// <param name="color_background">Barva pozadí textu</param>
        /// <param name="line">Ukončení řádku \n, nebo pokračovat v řádku</param>
        public void cwrite(string text, ConsoleColor color_text, ConsoleColor color_background, bool line = true)
        {
            /* Get Actual Colors */
            ConsoleColor a_consoleColorText = Console.ForegroundColor;
            ConsoleColor a_consoleColorBackGround = Console.BackgroundColor;

            /* Set new colors */
            Console.ForegroundColor = color_text;
            Console.BackgroundColor = color_background;

            this.write(text, line);

            /* Set old colors */
            Console.ForegroundColor = a_consoleColorText;
            Console.BackgroundColor = a_consoleColorBackGround;
        }

        /// <summary>
        /// write(string text, bool line = true) - Vypsání textu do konzole, rozlišení konce řádku
        /// </summary>
        /// <param name="text">Vypisovaný text</param>
        /// <param name="line">Ukončení řádku \n, nebo pokračovat v řádku</param>
        public void write(string text, bool line = true)
        {
            if (line == true)
                Console.WriteLine(text);
            else
                Console.Write(text);
        }

        /// <summary>
        /// awrite(string[] texts, bool line = true) - Vypsání arraye do konzole, rozlišení konců řádků
        /// </summary>
        /// <param name="texts">Pole textů</param>
        /// <param name="line">Ukončení řádku \n, nebo pokračovat v řádku</param>
        public void awrite(string[] texts, bool line = true)
        {
            foreach (string text in texts)
            {
                if (line == true)
                    Console.WriteLine(text);
                else
                    Console.Write(text);
            }
        }

        /// <summary>
        /// clear() - Vyčištění konzole, smazání obsahu
        /// </summary>
        public void clear()
        {
            Console.Clear();
        }

        /// <summary>
        /// enter(int c) - odeslání enteru do konzole 
        /// </summary>
        /// <param name="c">Počet enterů</param>
        public void enter(int c)
        {
            for (int i = 0; i < c; i++) this.write("", true);
        }
    }
}