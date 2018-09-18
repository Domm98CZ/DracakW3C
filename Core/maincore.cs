using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragons_Game.Core
{
    class maincore
    {
        /* Main Program Definitions */
        private string gameName = "The Witcher - Console edition";
        private string gameVersion = "1.2";
        private string gameAuthor = "Procházka Dominik";

        private int gameConsoleWidth = 128;
        private int gameConsoleHeight = 32;

        private int gameConsoleTextWidth = 120;

        private ConsoleColor gameConsoleTextColor = ConsoleColor.White;
        private ConsoleColor gameConsoleBackGroundColor = ConsoleColor.Black;

        private int gameConsoleScreenSleepTime = 1000;

        GameData.data gameData = new GameData.data();

        /* MainCore Init */

        /// <summary>
        /// maincore() - init jádra hry
        /// </summary>
        public maincore()
        {
            // Init
            gameData.checkGameFiles();

            Console.WindowWidth = this.getConsoleWidth();
            Console.WindowHeight = this.getConsoleHeight();

            Console.BackgroundColor = this.getConsoleDefaultBackGroundColor();
            Console.ForegroundColor = this.getConsoleDefaultTextColor();

            Console.Title = this.getGameName() + " v" + this.getGameVersion();
            // -- End Of Init
        }

        /* Main functions - values returning only */
        /// <summary>
        /// getConsoleDefaultTextColor() - zjištění defaultního nastavení pro barvu text konzole 
        /// </summary>
        /// <returns>barva</returns>
        public ConsoleColor getConsoleDefaultTextColor()
        {
            return this.gameConsoleTextColor;
        }

        /// <summary>
        /// getConsoleDefaultBackGroundColor() - zjištění defaultního nastavení pro barvu pozadí konzole 
        /// </summary>
        /// <returns>barva</returns>
        public ConsoleColor getConsoleDefaultBackGroundColor()
        {
            return this.gameConsoleBackGroundColor;
        }

        /// <summary>
        /// getConsoleScreenSleepTime() - zjištění defaultního nastavení pro čas čekání mezi snímky v konzoli
        /// </summary>
        /// <returns>čas v ms</returns>
        public int getConsoleScreenSleepTime()
        {
            return this.gameConsoleScreenSleepTime;
        }

        /// <summary>
        /// getConsoleTextWidth() - zjištění defaultního nastavení pro šířku textu v konzoli
        /// </summary>
        /// <returns>šířka textu</returns>
        public int getConsoleTextWidth()
        {
            return this.gameConsoleTextWidth;
        }

        /// <summary>
        /// getConsoleWidth() - zjištění defaultního nastavení pro šířku konzole
        /// </summary>
        /// <returns>šířka konzole</returns>
        public int getConsoleWidth()
        {
            return this.gameConsoleWidth;
        }

        /// <summary>
        /// getConsoleHeight() - zjištění defaultního nastavení pro výšku konzole
        /// </summary>
        /// <returns>výška konzole</returns>
        public int getConsoleHeight()
        {
            return this.gameConsoleHeight;
        }

        /// <summary>
        /// getGameName() - zjištění defaultního nastavení pro název hry
        /// </summary>
        /// <returns>název hry</returns>
        public string getGameName()
        {
            return this.gameName;
        }

        /// <summary>
        /// getGameVersion() - zjištění defaultního nastavení pro verzi hry
        /// </summary>
        /// <returns>verze hry</returns>
        public string getGameVersion()
        {
            return this.gameVersion;
        }

        /// <summary>
        /// getGameAuthor() - zjištění autora hry
        /// </summary>
        /// <returns>autor hry</returns>
        public string getGameAuthor()
        {
            return this.gameAuthor;
        }
    }
}
