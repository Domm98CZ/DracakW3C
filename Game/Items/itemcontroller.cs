using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragons_Game.Game.Items
{
    class itemcontroller
    {
        /// <summary>
        /// Inicializace herních dat
        /// </summary>
        GameData.data gameData = new GameData.data();

        private List<string> itemsData = new List<string>();

        /// <summary>
        /// itemcontroller() - Inicializace herních dat do listu s herníma předmětama
        /// </summary>
        public itemcontroller()
        {
            itemsData = gameData.getItemsData().ToList();
        }

        /// <summary>
        /// countItems() - Zpočítání všech itemů ve hře
        /// </summary>
        /// <returns>počet itemů</returns>
        public int countItems()
        {
            return itemsData.Count;
        }

        /// <summary>
        /// getItemName(int item_id) - zjištění jména itemu
        /// </summary>
        /// <param name="item_id">Unikátní identifikátor itemu</param>
        /// <returns>jméno itemu</returns>
        public string getItemName(int item_id)
        {
            if (item_id > -1)
            {
                if (item_id < itemsData.Count && !String.IsNullOrEmpty(itemsData[item_id]))
                {
                    string[] _returnData = itemsData[item_id].Split('#');
                    return _returnData[1];
                }
                else return null;
            }
            else return null;
        }

        /// <summary>
        /// getItemStatsAsString(int item_id)
        /// </summary>
        /// <param name="item_id">Unikátní identifikátor itemu</param>
        /// <returns>string se statistikami itemu</returns>
        public string getItemStatsAsString(int item_id)
        {
            if (item_id > -1)
            {
                if(item_id < itemsData.Count && !String.IsNullOrEmpty(itemsData[item_id]))
                {
                    string[] _itemData = itemsData[item_id].Split('#');
                    string _returnString = null;

                    string[] varNames = { "Útok", "Obrana", "+HP", "+MANA", "++HP", "++MANA" };  

                    for (int i = 0; i < varNames.Length; i++)
                    {
                        int _tempVar = getItemStat(item_id, (i + 2));
                        if (_tempVar > 0)
                        {
                            _returnString = _returnString + " " + varNames[i] + ": " + _tempVar;
                        }
                    }

                    if (!String.IsNullOrEmpty(_returnString)) _returnString = "[ " + _returnString + " ]";
                    return _returnString;
                }
                else return null;
            }
            else return null;
        }

        /// <summary>
        /// getItemStat(int item_id, int stat = 2) - zjištění konkrétního statu itemu
        /// </summary>
        /// <param name="item_id">Unikátní identifikátor itemu</param>
        /// <param name="stat">Unikátní identifikátor statu</param>
        /// <returns>konkrétní hodnota konkrétní statistiky</returns>
        public int getItemStat(int item_id, int stat = 2)
        {
            if (item_id > -1)
            {
                if (item_id < itemsData.Count && !String.IsNullOrEmpty(itemsData[item_id]))
                {
                    string[] _itemData = itemsData[item_id].Split('#');
                    int[] itemVar = new int[_itemData.Length];
                    for (int i = 0; i < _itemData.Length; i++)
                    {
                        bool _result = Int32.TryParse(_itemData[i], out itemVar[i]);
                        if (_result != true) itemVar[i] = 0;
                    }
                    return itemVar[stat];
                }
                else return 0;
            }
            else return 0;
        }
    }
}
