using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragons_Game.Game
{
    class dice
    {
        Random r = new Random();
        private const int DEFAULT_MAX_LUCK = 100;

        /// <summary>
        /// getPlayerLuckChance(int playerLuck, int MaxLuck = DEFAULT_MAX_LUCK) - zjištění zda-li má hráč štěstí
        /// </summary>
        /// <param name="playerLuck">Hráčovo štěstí</param>
        /// <param name="MaxLuck">Maximální štěstí (default 100)</param>
        /// <returns>true/false</returns>
        public bool getPlayerLuckChance(int playerLuck, int MaxLuck = DEFAULT_MAX_LUCK)
        {
            if (playerLuck > MaxLuck) return true;
            else if (playerLuck == 0) return false;

            List<bool> listOfPlayerChance = new List<bool>();
            for (int i = 0; i < MaxLuck; i++) listOfPlayerChance.Add(false);

            for (int y = 0; y < playerLuck; y++)
            {
                int _r = r.Next(0, MaxLuck);
                listOfPlayerChance.RemoveAt(_r);
                listOfPlayerChance.Insert(_r, true);
            }

            return listOfPlayerChance[r.Next(0, MaxLuck)];
        }

        /// <summary>
        /// getDefaultMaxLuck() - zjištění defaultního maximálního štěstí
        /// </summary>
        /// <returns>Defaultní hodnota maximálního štěstí</returns>
        public int getDefaultMaxLuck()
        {
            return DEFAULT_MAX_LUCK;
        }
    }
}