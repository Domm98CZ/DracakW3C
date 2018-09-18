using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragons_Game.Game.Mobs
{
    class mobscontroller
    {
        /// <summary>
        /// Inicializace herních dat a herních modelů
        /// </summary>
        GameData.data gameData = new GameData.data();
        GameData.models gameModels = new GameData.models();

        /// <summary>
        /// Hlášky mobů
        /// </summary>
        private string[] mobVoices = {
          //"                ",
            "  Aaagggrrrrrr! ",
            "      ...       ",
            "      !!!       ",
            "   Grrrrrrrr!   "
        };

        private List<string> mobsData = new List<string>();

        /// <summary>
        /// mobscontroller() - Inicializace herních dat do listu s mobama
        /// </summary>
        public mobscontroller()
        {
            mobsData = gameData.getMobsData().ToList();
        }

        /// <summary>
        /// createMob(string mobName, int mobHealth = 20, int mobMana = 5, int mobAtt = 5, int mobDeff = 5, string modelName = "player_wss") - Vytvoření custom moba
        /// </summary>
        /// <param name="mobName">Jméno moba</param>
        /// <param name="mobHealth">Životy moba</param>
        /// <param name="mobMana">Mana moba</param>
        /// <param name="mobAtt">Útok moba</param>
        /// <param name="mobDeff">Deffence moba</param>
        /// <param name="modelName">Model moba</param>
        /// <returns>Pole s informacemi o mobovi</returns>
        public string[] createMob(string mobName, int mobHealth = 20, int mobMana = 5, int mobAtt = 5, int mobDeff = 5, string modelName = "player_wss")
        {
            List<string> _returnData = new List<string>();

            _returnData.Add(mobName);
            _returnData.Add("" + mobHealth);
            _returnData.Add("" + mobMana);
            _returnData.Add("" + mobAtt);
            _returnData.Add("" + mobDeff);
            _returnData.Add(modelName);

            return _returnData.ToArray();
        }

        /// <summary>
        /// createMobByID(int mobID) - Vytvoření moba podle Unikátního identifikátoru
        /// </summary>
        /// <param name="mobID">Unikátní identifikátor moba</param>
        /// <returns>Pole s informacemi o mobovi</returns>
        public string[] createMobByID(int mobID)
        {
            if (mobsData[mobID].Length > 0)
            {
                string[] _mobData = mobsData[mobID].Split('#');
                int mobHealth = 0; int mobMana = 0;int mobAtt = 0;int mobDeff = 0;
                Int32.TryParse(_mobData[3], out mobHealth);
                Int32.TryParse(_mobData[4], out mobMana);
                Int32.TryParse(_mobData[5], out mobAtt);
                Int32.TryParse(_mobData[6], out mobDeff);
                return this.createMob(_mobData[1], mobHealth, mobMana, mobAtt, mobDeff, _mobData[2]);
            }
            else return null;
        }

        /// <summary>
        /// getMobVoice() - Zjištění hlášky moba
        /// </summary>
        /// <returns>Náhodná hláška moba</returns>
        public string getMobVoice()
        {
            Random r = new Random();
            return this.mobVoices[r.Next(0, this.mobVoices.Length)];
        }

        /// <summary>
        /// playerFightWithMob(string[] mobData, string[] playerData) - Souboj
        /// </summary>
        /// <param name="mobData">Pole s informacemi o mobovi</param>
        /// <param name="playerData">Pole s informacemi o hráčovi</param>
        /// <returns>
        /// 1 - Hráč vyhrál nad mobem
        /// 0 - Hráč prohrál
        /// 2#loot_id - Hráč vyhrál nad mobem a dostal loot.
        /// </returns>
        public bool playerFightWithMob(string[] mobData, string[] playerData)
        {
            Random r = new Random();
            Game.dice d = new Game.dice();
            Screen.viewer v = new Screen.viewer();

            int _playerHealth = 0; int _mobHealth = 0; int _playerMana = 0; int _mobMana = 0; int _playerMaxHealth = 0; int _mobMaxHealth = 0; int _playerAttackDmg = 0; int _mobAttackDmg = 0; int _playerDex = 0; int _mobDeff = 0;int _playerLuck = 0;
            Int32.TryParse(playerData[1],   out _playerHealth);
            Int32.TryParse(mobData[1],      out _mobHealth);
            Int32.TryParse(playerData[2],   out _playerMana);
            Int32.TryParse(mobData[2],      out _mobMana);
            Int32.TryParse(playerData[3],   out _playerAttackDmg);
            Int32.TryParse(mobData[3],      out _mobAttackDmg);
            Int32.TryParse(playerData[4],   out _playerDex);
            Int32.TryParse(mobData[4],      out _mobDeff);
            Int32.TryParse(playerData[5],   out _playerLuck);

            _playerMaxHealth                = _playerHealth;
            _mobHealth                      = _mobHealth + _mobDeff;
            _mobMaxHealth                   = _mobHealth;

            int fight_hp_player             = _playerHealth;
            int fight_hp_mob                = _mobHealth;

            bool _returnValue             = false;
            int _tempDmg                    = 0;
            bool _playerTurn                = true;

            bool bool_fight                 = true;
            while(bool_fight == true)
            {
                if(_playerHealth > 0 && _mobHealth == 0)
                {
                    v.cwrite(playerData[0] + "> Přemohl " + mobData[0], ConsoleColor.Gray, ConsoleColor.Black);                    

                    bool_fight = false;
                    System.Threading.Thread.Sleep(5000);
                    _returnValue = true;
                }
                else if(_playerHealth == 0 && _mobHealth > 0)
                {
                    v.cwrite(mobData[0] + "> Přemohl " + playerData[0], ConsoleColor.DarkCyan, ConsoleColor.Black);
                    v.cwrite("Zemřel jsi.", ConsoleColor.Red, ConsoleColor.Black);

                    bool_fight = false;
                    System.Threading.Thread.Sleep(5000);
                    _returnValue = false;
                }
                else
                {
                    v.clear();
                    string layout = gameModels.v("fight_layout");

                    layout = gameModels.addmodelintolayout("left_model_", "player", layout);
                    layout = gameModels.addmodelintolayout("right_model_", mobData[5], layout);

                    layout = layout.Replace("@@mob_name@@", "");
                    layout = layout.Replace("@@mob_say@@", this.getMobVoice());

                    layout = layout.Replace("@@top_info_01@@", "> Souboj: ");
                    layout = layout.Replace("@@top_info_02@@", "[ " + playerData[0] + " vs " + mobData[0] + " ]");

                    v.write(layout, false);

                    v.cwrite("Funkční klávesy: [ ESC - Pauza;Ukončení hry ] [ A - Útok ] [ S - Silný útok ] [ H - Vypnout/Zapnout nápovědu ]", ConsoleColor.Green, ConsoleColor.Black);

                    // Player Stats
                    v.cwrite("<<" + playerData[0] + ">> [ " + _playerHealth + " HP | " + _playerMana + " MAN | " + playerData[3] + " ATT | " + _playerDex + " DEX | " + playerData[5] + " LUCK ]", ConsoleColor.Gray, ConsoleColor.Black);

                    // Mob Stats
                    v.cwrite("<<" + mobData[0] + ">> [ " + _mobHealth + " HP | " + _mobMana + " MAN | " + mobData[3] + " ATT | " + mobData[4] + " DEFF ]", ConsoleColor.DarkCyan, ConsoleColor.Black);


                    v.enter(1);
                    if (_playerTurn == true) v.cwrite("<" + playerData[0] + "> Útočí.", ConsoleColor.Gray, ConsoleColor.Black);
                    else v.cwrite("<" + mobData[0] + "> Útočí.", ConsoleColor.DarkCyan, ConsoleColor.Black);
                    // Fight Controll
                    if (_playerTurn == true)
                    {
                        ConsoleKeyInfo key = Console.ReadKey();
                        v.enter(1);
                        if (key.Key.ToString() == "Escape")
                        {
                            v.write(" ", false);
                            v.cwrite("Ze souboje nemůžeš jen tak odejít. Toto kolo zaspal, neútočíš.", ConsoleColor.Red, ConsoleColor.Black);
                        }
                        else if (key.Key == ConsoleKey.A)
                        {
                            if (_playerDex > 0)
                            {
                                _tempDmg = _playerAttackDmg;
                                if (d.getPlayerLuckChance(_playerLuck) == true)
                                {
                                    _tempDmg = _playerAttackDmg + ((_playerAttackDmg * _playerLuck) / 10);
                                    v.cwrite(playerData[0] + "> Slabý kritický zásah. [ " + _tempDmg + " DMG ]", ConsoleColor.Red, ConsoleColor.Black);
                                }
                                else
                                {
                                    v.cwrite(playerData[0] + "> Slabý zásah. [ " + _tempDmg + " DMG ]", ConsoleColor.Gray, ConsoleColor.Black);
                                }
                                _mobHealth = _mobHealth - _tempDmg;
                                if (_mobHealth < 0) _mobHealth = 0;
                                _playerDex--;
                            }
                            else
                            {
                                v.cwrite(playerData[0] + "> Nedostatek DEX. Toto kolo zaspal, neútočíš.", ConsoleColor.Gray, ConsoleColor.Black);
                                _playerDex++;
                            }
                        }
                        else if (key.Key == ConsoleKey.S)
                        {
                            if (_playerMana > 0)
                            {
                                _tempDmg = _playerAttackDmg + ((_playerAttackDmg * 5) / 10);
                                if (d.getPlayerLuckChance(_playerLuck) == true)
                                {
                                    _tempDmg = _playerAttackDmg + ((_playerAttackDmg * _playerLuck) / 10);
                                    v.cwrite(playerData[0] + "> Silný kritický zásah. [ " + _tempDmg + " DMG ]", ConsoleColor.Red, ConsoleColor.Black);
                                }
                                else
                                {
                                    v.cwrite(playerData[0] + "> Silný zásah. [ " + _tempDmg + " DMG ]", ConsoleColor.Gray, ConsoleColor.Black);
                                }
                                _mobHealth = _mobHealth - _tempDmg;
                                if (_mobHealth < 0) _mobHealth = 0;
                                _playerMana--;
                            }
                            else
                            {
                                v.cwrite(playerData[0] + "> Nedostatek many. Toto kolo zaspal, neútočíš.", ConsoleColor.Gray, ConsoleColor.Black);
                            }
                        }
                        else
                        {
                            v.cwrite(playerData[0] + "> Toto kolo zaspal, neútočíš.", ConsoleColor.Gray, ConsoleColor.Black);
                            _playerDex++;
                        }
                    }
                    else
                    {
                        int mobChoice = 0;
                        if (_mobMana > 0) mobChoice = r.Next(0, 1);
                        else mobChoice = 0;
                        if (mobChoice == 0)
                        {
                            _tempDmg = _mobAttackDmg;
                            _playerHealth = _playerHealth - _tempDmg;
                            if (_playerHealth < 0) _playerHealth = 0;

                            v.cwrite(mobData[0] + "> Slabý zásah. [ " + _tempDmg + " DMG ]", ConsoleColor.DarkCyan, ConsoleColor.Black);
                        }
                        else if (mobChoice == 1)
                        {
                            _tempDmg = _playerAttackDmg + ((_playerAttackDmg * 5) / 10);
                            _playerHealth = _playerHealth - _tempDmg;
                            if (_playerHealth < 0) _playerHealth = 0;
                            _mobMana--;

                            v.cwrite(mobData[0] + "> Silný zásah. [ " + _tempDmg + " DMG ]", ConsoleColor.DarkCyan, ConsoleColor.Black);
                        }
                    }
                    System.Threading.Thread.Sleep(5000);
                    _playerTurn = (_playerTurn == true) ? false : true;
                }
            }
            return _returnValue;
        }
    }
}
