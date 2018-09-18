using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Dragons_Game.Controller
{
    class maincontroller
    {
        /// <summary>
        /// Inicializace jádra a jeho publicků
        /// </summary>
        Core.maincore m = new Core.maincore();

        public ConsoleColor getConsoleDefaultTextColor()
        {
            return m.getConsoleDefaultTextColor();
        }

        public ConsoleColor getConsoleDefaultBackGroundColor()
        {
            return m.getConsoleDefaultBackGroundColor();
        }

        public int getConsoleTextWidth()
        {
            return m.getConsoleTextWidth();
        }

        public int getConsoleScreenSleepTime()
        {
            return m.getConsoleScreenSleepTime();
        }

        public string getGameAuthor()
        {
            return m.getGameAuthor();
        }

        public string getGameName()
        {
            return m.getGameName();
        }

        public string getGameVersion()
        {
            return m.getGameVersion();
        }

        public bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}