using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Dragons_Game
{
    class Program
    {
        /// The Witcher - Console edition v1.2
        /// Autor Procházka Dominik
        /// Průměrná hrací doba ~30 minut.
        /// Řádků kódu 1552
        /// Vytvoření projektu: 22. února 2016
        /// Dokončení projektu: 30. března 2016 
        static void Main(string[] args)
        {
            Controller.maincontroller main = new Controller.maincontroller();
            if (main.CheckForInternetConnection() == true)
            {
                Screen.viewer v = new Screen.viewer();
                v.init();
            }
            else
            {
                Console.WriteLine("Hra nelze hrát bez internetového připojení.");
                Console.ReadKey();
                System.Environment.Exit(1);
            }
        }
    }
}
