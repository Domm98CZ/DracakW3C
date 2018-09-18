using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragons_Game.GameData
{
    class models
    {
        /// <summary>
        /// Inicializace main controlleru
        /// </summary>
        Controller.maincontroller main = new Controller.maincontroller();

        /// <summary>
        /// addmodelintolayout(string replace, string modelName, string layout) - Přidání modelu do zobrazení
        /// </summary>
        /// <param name="replace">Nahrazovaný string</param>
        /// <param name="modelName">Jméno modelu</param>
        /// <param name="layout">String aktuálního zobrazení</param>
        /// <returns>Nové zobrazení</returns>
        public string addmodelintolayout(string replace, string modelName, string layout)
        {
            string _layout = layout;
            string[] parsed_model = this.parser(modelName);

            for (int i = 0; i < parsed_model.Length; i++)
            {
                _layout = _layout.Replace("@@" + replace + ((i + 1 < 10) ? "0" : "") + (i + 1) + "@@", parsed_model[i]);
            }
            return _layout;
        }

        /// <summary>
        /// parser(string modelName) - Příprava modelu k přidání do zobrazení 
        /// </summary>
        /// <param name="modelName">Jméno modelu</param>
        /// <returns>Připravený model</returns>
        public string[] parser(string modelName)
        {
            string _tempString = this.v(modelName);
            string[] parsed_string = _tempString.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.None);
            return parsed_string;
        }

        /// <summary>
        /// v(string modelName) - Zobrazení modelu
        /// </summary>
        /// <param name="modelName">Jméno modelu</param>
        /// <returns>String modelu</returns>
        public string v(string modelName)
        {
            int[] _tempFirstStop = new int[] { 0, main.getConsoleTextWidth() - 1 };
            int[] _tempTwoColumsStop = new int[] { 0, 18, 36, main.getConsoleTextWidth() - 1 };
            int[] _tempTreeColumsStop = new int[] { 0, 20, 40, main.getConsoleTextWidth() - 1 };

            string _tempString = null;
            switch (modelName.ToLower())
            {
                case "gamelogo":

                    _tempString = _tempString + this.hr(_tempFirstStop);
                    _tempString = _tempString +
                        "                                                             " + "\n" +
                        "  < < < " + main.getGameName() + " > > >                     " + "\n" +
                        "                                                             " + "\n"
                    ;
                    _tempString = _tempString + this.hr(_tempFirstStop);
                    break;
                case "character_create_step":
                    _tempString =
                       "                " + "\n" +
                       "                " + "\n" +
                       "                " + "\n" +
                       "@@step_num@@" + "\n" +
                       "                " + "\n" +
                       "@@step_name@@" + "\n" +
                       "                " + "\n" +
                       "@@step_popis_1@@" + "\n" +
                       "@@step_popis_2@@" + "\n" +
                       "                " + "\n" +
                       "                " + "\n" +
                       "                " + "\n" +
                       "                " + "\n" +
                       "                " + "\n" +
                       "                " + "\n"
                   ;
                    break;
                case "character_info":
                    _tempString =
                       "                " + "\n" +
                       "@@character_info@@" + "\n" +
                       "                " + "\n" +
                       "@@stat_1@@" + "\n" +
                       "@@stat_2@@" + "\n" +
                       "@@stat_3@@" + "\n" +
                       "@@stat_4@@" + "\n" +
                       "@@stat_5@@" + "\n" +
                       "                " + "\n" +
                       "@@level@@" + "\n" +
                       "@@xp@@" + "\n" +
                       "                " + "\n" +
                       "@@kills@@" + "\n" +
                       "@@deaths@@" + "\n" +
                       "                " + "\n"
                   ;
                    break;
                case "player_noob":
                    _tempString =
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "      O         " + "\n" +
                        "      |         " + "\n" +
                        "     /|\\        " + "\n" +
                        "    / | \\       " + "\n" +
                        "     / \\        " + "\n" +
                        "   _/   \\_      " + "\n" +
                        "                " + "\n" +
                        "@@player_noob_name@@" + "\n" +
                        "@@player_noob_level@@" + "\n" +
                        "                " + "\n"
                    ;
                    break;
                case "player":
                    _tempString =
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "   |   O        " + "\n" +
                        "   |   |        " + "\n" +
                        "   |__/|\\       " + "\n" +
                        "       | \\      " + "\n" +
                        "      / \\       " + "\n" +
                        "    _/   \\_     " + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n"
                    ;
                    break;
                case "player_wss":
                    _tempString =
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "   |            " + "\n" +
                        "   |  O         " + "\n" +
                        "   |  |  }      " + "\n" +
                        "   |_/|\\_ |}    " + "\n" +
                        "      |  }      " + "\n" +
                        "     / \\        " + "\n" +
                        "   _/   \\_      " + "\n" +
                        "                " + "\n" +
                        "@@player_wss_name@@" + "\n" +
                        "@@player_wss_level@@" + "\n" +
                        "                " + "\n"
                    ;
                    break;
                case "mob_wolf":
                    _tempString =
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "   _____/\\      " + "\n" +
                        "  //   / _\\_____" + "\n" +
                        " /     \\   .. \\/" + "\n" +
                        "//     /~_____/ " + "\n" +
                        "/// \\/  /       " + "\n" +
                        "                " + "\n" +
                        "@@mob_name@@" + "\n" +
                        "@@mob_say@@" + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n"
                    ;
                    break;
                case "mob_bear":
                    _tempString =
                        "                " + "\n" +
                        "      ('-^-/ ') " + "\n" +
                        "      `O__O' ]  " + "\n" +
                        "      (_Y_) _ / " + "\n" +
                        "     _..`--'-.`, " + "\n" +
                        "   (__)_,--(__) " + "\n" +
                        "       7:   ; 1 " + "\n" +
                        "   _/ /,`-.- ': " + "\n" +
                        "  (_,) - ~~(_,) " + "\n" +
                        "                " + "\n" +
                        "@@mob_name@@" + "\n" +
                        "@@mob_say@@" + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n"
                    ;
                    break;
                case "mob_bandit":
                    _tempString =
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "      O         " + "\n" +
                        "      |         " + "\n" +
                        "     /|\\        " + "\n" +
                        "    / | \\       " + "\n" +
                        "     / \\        " + "\n" +
                        "   _/   \\_      " + "\n" +
                        "                " + "\n" +
                        "@@mob_name@@" + "\n" +
                        "@@mob_say@@" + "\n" +
                        "                " + "\n"
                    ;
                    break;
                case "mob_samurai":
                    _tempString =
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "   /   O        " + "\n" +
                        "   /   |        " + "\n" +
                        "   /__/|\\       " + "\n" +
                        "       | \\      " + "\n" +
                        "      / \\       " + "\n" +
                        "    _/   \\_     " + "\n" +
                        "                " + "\n" +
                        "@@mob_name@@" + "\n" +
                        "@@mob_say@@" + "\n" +
                        "                " + "\n"
                    ;
                    break;
                case "mob_gladiator":
                    _tempString =
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "   |            " + "\n" +
                        "   |  O         " + "\n" +
                        "   |  |  }      " + "\n" +
                        "   |_/|\\_ |}    " + "\n" +
                        "      |  }      " + "\n" +
                        "     / \\        " + "\n" +
                        "   _/   \\_      " + "\n" +
                        "                " + "\n" +
                        "@@mob_name@@" + "\n" +
                        "@@mob_say@@" + "\n" +
                        "                " + "\n"
                    ;
                    break;
                case "mob_dragon":
                    _tempString =
                        // Dragon - Ma větší rozměr, musí být v model slotu 2+
                        "                " + "\n" +
                        "     /     \\     " + "\n" +
                        "    ((     ))    " + "\n" +
                        "===   \\_v_//  ===" + "\n" +
                        " ====)_ ^ _(==== " + "\n" +
                        "  ===/ O O \\===  " + "\n" +
                        "  = | / _ _\\ | = " + "\n" +
                        " =   \\/ _ _\\/   =" + "\n" +
                        "      \\_ _ /     " + "\n" +
                        "      (o_o)      " + "\n" +
                        "       VwV       " + "\n" +      
                        "                " + "\n" +
                        "@@mob_name@@" + "\n" +
                        "@@mob_say@@" + "\n" +
                        "                " + "\n"
                    ;
                    break;
                case "mob_wghost":
                    _tempString =
                        " /|\\        /|\\ " + "\n" +
                        "  |         /|\\ " + "\n" +
                        "      ___   /|\\ " + "\n" +
                        "/|\\ '(O_o)'  |   " + "\n" +
                        "/|\\    |        " + "\n" +
                        "/|\\   /|\\       " + "\n" +
                        " |   / | \\      " + "\n" +
                        "      / \\       " + "\n" +
                        "    _/   \\_     " + "\n" +
                        "                " + "\n" +
                        "@@mob_name@@" + "\n" +
                        "@@mob_say@@" + "\n" +
                        "                " + "\n" +
                        "                " + "\n" +
                        "                " + "\n"
                    ;
                    break;
                case "5_cols_layout":
                    _tempString = _tempString + this.hr(_tempTreeColumsStop);
                    _tempString = _tempString +
                        "@@top_info_01@@ " + "@@top_info_02@@" + "\n";
                    _tempString = _tempString + this.hr(_tempTreeColumsStop);

                    _tempString = _tempString +
                        "@@2left_info_01@@ " + "@@left_info_01@@ " + "@@center_info_01@@ " + "@@right_info_01@@ " + "@@2right_info_01@@" + "\n" +
                        "@@2left_info_02@@ " + "@@left_info_02@@ " + "@@center_info_02@@ " + "@@right_info_02@@ " + "@@2right_info_02@@" + "\n" +
                        "@@2left_info_03@@ " + "@@left_info_03@@ " + "@@center_info_03@@ " + "@@right_info_03@@ " + "@@2right_info_03@@" + "\n" +
                        "@@2left_info_04@@ " + "@@left_info_04@@ " + "@@center_info_04@@ " + "@@right_info_04@@ " + "@@2right_info_04@@" + "\n" +
                        "@@2left_info_05@@ " + "@@left_info_05@@ " + "@@center_info_05@@ " + "@@right_info_05@@ " + "@@2right_info_05@@" + "\n" +
                        "@@2left_info_06@@ " + "@@left_info_06@@ " + "@@center_info_06@@ " + "@@right_info_06@@ " + "@@2right_info_06@@" + "\n" +
                        "@@2left_info_07@@ " + "@@left_info_07@@ " + "@@center_info_07@@ " + "@@right_info_07@@ " + "@@2right_info_07@@" + "\n" +
                        "@@2left_info_08@@ " + "@@left_info_08@@ " + "@@center_info_08@@ " + "@@right_info_08@@ " + "@@2right_info_08@@" + "\n" +
                        "@@2left_info_09@@ " + "@@left_info_09@@ " + "@@center_info_09@@ " + "@@right_info_09@@ " + "@@2right_info_09@@" + "\n" +
                        "@@2left_info_10@@ " + "@@left_info_10@@ " + "@@center_info_10@@ " + "@@right_info_10@@ " + "@@2right_info_10@@" + "\n" +
                        "@@2left_info_11@@ " + "@@left_info_11@@ " + "@@center_info_11@@ " + "@@right_info_11@@ " + "@@2right_info_11@@" + "\n" +
                        "@@2left_info_12@@ " + "@@left_info_12@@ " + "@@center_info_12@@ " + "@@right_info_12@@ " + "@@2right_info_12@@" + "\n" +
                        "@@2left_info_13@@ " + "@@left_info_13@@ " + "@@center_info_13@@ " + "@@right_info_13@@ " + "@@2right_info_13@@" + "\n" +
                        "@@2left_info_14@@ " + "@@left_info_14@@ " + "@@center_info_14@@ " + "@@right_info_14@@ " + "@@2right_info_14@@" + "\n" +
                        "@@2left_info_15@@ " + "@@left_info_15@@ " + "@@center_info_15@@ " + "@@right_info_15@@ " + "@@2right_info_15@@" + "\n" +
                        "\n"
                    ;

                    _tempString = _tempString + this.hr(_tempFirstStop);
                    break;
                case "3_cols_layout":
                    _tempString = _tempString + this.hr(_tempTreeColumsStop);
                    _tempString = _tempString +
                        "@@top_info_01@@ " + " @@top_info_02@@" + "\n";
                    _tempString = _tempString + this.hr(_tempTreeColumsStop);

                    _tempString = _tempString +
                        "@@left_model_01@@ " + " @@right_model_01@@" + " @@game_info_01@@" + "\n" +
                        "@@left_model_02@@ " + " @@right_model_02@@" + " @@game_info_02@@" + "\n" +
                        "@@left_model_03@@ " + " @@right_model_03@@" + " @@game_info_03@@" + "\n" +
                        "@@left_model_04@@ " + " @@right_model_04@@" + " @@game_info_04@@" + "\n" +
                        "@@left_model_05@@ " + " @@right_model_05@@" + " @@game_info_05@@" + "\n" +
                        "@@left_model_06@@ " + " @@right_model_06@@" + " @@game_info_06@@" + "\n" +
                        "@@left_model_07@@ " + " @@right_model_07@@" + " @@game_info_07@@" + "\n" +
                        "@@left_model_08@@ " + " @@right_model_08@@" + " @@game_info_08@@" + "\n" +
                        "@@left_model_09@@ " + " @@right_model_09@@" + " @@game_info_09@@" + "\n" +
                        "@@left_model_10@@ " + " @@right_model_10@@" + " @@game_info_10@@" + "\n" +
                        "@@left_model_11@@ " + " @@right_model_11@@" + " @@game_info_11@@" + "\n" +
                        "@@left_model_12@@ " + " @@right_model_12@@" + " @@game_info_12@@" + "\n" +
                        "@@left_model_13@@ " + " @@right_model_13@@" + " @@game_info_13@@" + "\n" +
                        "@@left_model_14@@ " + " @@right_model_14@@" + " @@game_info_14@@" + "\n" +
                        "@@left_model_15@@ " + " @@right_model_15@@" + " @@game_info_15@@" + "\n" +
                        "\n"
                    ;

                    _tempString = _tempString + this.hr(_tempTreeColumsStop);

                    _tempString = _tempString + "\n";
                    _tempString = _tempString + this.hr(_tempFirstStop);
                    break;
                case "2_cols_layout":
                    _tempString = _tempString + this.hr(_tempTwoColumsStop);
                    _tempString = _tempString +
                        "@@top_info_01@@ " + " @@top_info_02@@" + "\n";
                    _tempString = _tempString + this.hr(_tempTwoColumsStop);

                    _tempString = _tempString +
                        "@@left_model_01@@ " + " @@game_info_01@@" + "\n" +
                        "@@left_model_02@@ " + " @@game_info_02@@" + "\n" +
                        "@@left_model_03@@ " + " @@game_info_03@@" + "\n" +
                        "@@left_model_04@@ " + " @@game_info_04@@" + "\n" +
                        "@@left_model_05@@ " + " @@game_info_05@@" + "\n" +
                        "@@left_model_06@@ " + " @@game_info_06@@" + "\n" +
                        "@@left_model_07@@ " + " @@game_info_07@@" + "\n" +
                        "@@left_model_08@@ " + " @@game_info_08@@" + "\n" +
                        "@@left_model_09@@ " + " @@game_info_09@@" + "\n" +
                        "@@left_model_10@@ " + " @@game_info_10@@" + "\n" +
                        "@@left_model_11@@ " + " @@game_info_11@@" + "\n" +
                        "@@left_model_12@@ " + " @@game_info_12@@" + "\n" +
                        "@@left_model_13@@ " + " @@game_info_13@@" + "\n" +
                        "@@left_model_14@@ " + " @@game_info_14@@" + "\n" +
                        "@@left_model_15@@ " + " @@game_info_15@@" + "\n"
                    ;

                    _tempString = _tempString + this.hr(_tempTwoColumsStop);

                    _tempString = _tempString + "\n";
                    _tempString = _tempString + this.hr(_tempFirstStop);
                    break;
                case "fight_layout":
                    _tempString = _tempString + this.hr(_tempTwoColumsStop);
                    _tempString = _tempString +
                        "@@top_info_01@@ " + " @@top_info_02@@" + "\n";
                    _tempString = _tempString + this.hr(_tempTwoColumsStop);

                    _tempString = _tempString +
                        "@@left_model_01@@ " + "                                        " + " @@right_model_01@@" + "\n" +
                        "@@left_model_02@@ " + "                                        " + " @@right_model_02@@" + "\n" +
                        "@@left_model_03@@ " + "     < < < < < -  Souboj  - > > > > >   " + " @@right_model_03@@" + "\n" +
                        "@@left_model_04@@ " + "                                        " + " @@right_model_04@@" + "\n" +
                        "@@left_model_05@@ " + "                                        " + " @@right_model_05@@" + "\n" +
                        "@@left_model_06@@ " + "                                        " + " @@right_model_06@@" + "\n" +
                        "@@left_model_07@@ " + "                                        " + " @@right_model_07@@" + "\n" +
                        "@@left_model_08@@ " + "                    VS                  " + " @@right_model_08@@" + "\n" +
                        "@@left_model_09@@ " + "                                        " + " @@right_model_09@@" + "\n" +
                        "@@left_model_10@@ " + "                                        " + " @@right_model_10@@" + "\n" +
                        "@@left_model_11@@ " + "                                        " + " @@right_model_11@@" + "\n" +
                        "@@left_model_12@@ " + "                                        " + " @@right_model_12@@" + "\n" +
                        "@@left_model_13@@ " + "     < < < < < -  Souboj  - > > > > >   " + " @@right_model_13@@" + "\n" +
                        "@@left_model_14@@ " + "                                        " + " @@right_model_14@@" + "\n" +
                        "@@left_model_15@@ " + "                                        " + " @@right_model_15@@" + "\n"
                    ;
                    _tempString = _tempString + this.hr(_tempTwoColumsStop);
                    break;
                case "line":
                    _tempString = "+";
                    for (int i = 0; i < main.getConsoleTextWidth(); i++) _tempString = _tempString + "-";
                    _tempString = _tempString + "+";
                    break;
                default:
                    _tempString = null;
                    break;
            }

            return _tempString;
        }

        /// <summary>
        /// hr(int[] stop, bool x = true) - Vytvoření čáry 
        /// </summary>
        /// <param name="stop">Pole se "stop indexy"</param>
        /// <param name="x">Zalomení řádku</param>
        /// <returns>String čáry</returns>
        private string hr(int[] stop, bool x = true)
        {
            string lineString = null;
            for (int i = 0; i < main.getConsoleTextWidth(); i++)
            {
                if (stop.Contains(i))
                    lineString = lineString + "+";
                else
                    lineString = lineString + "-";
            }

            return (x == true) ? lineString + "\n" : lineString;
        }
    }
}