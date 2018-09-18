using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragons_Game.Game
{
    class game
    {
        // Game Basic
        /// <summary>
        /// Inicializace screenu, herních dat a herních modelů
        /// </summary>
        Screen.viewer v = new Screen.viewer();
        GameData.data gameData = new GameData.data();
        GameData.models gameModels = new GameData.models();

        Controller.maincontroller main = new Controller.maincontroller();
        Game.dice d = new Game.dice();
        Random r = new Random();

        // Get Mobs & Locations & Items
        /// <summary>
        /// Inicializace lokací, mobů a itemů
        /// </summary>
        Locations.locationcontroller location = new Locations.locationcontroller();
        Mobs.mobscontroller mob = new Mobs.mobscontroller();
        Items.itemcontroller item = new Items.itemcontroller();

        // Define Game Variables 
        private bool ingame = true;

        /// <summary>
        /// Definice zaklínačských škol
        /// </summary>
        private string[,] gameSchools = new string[,]
        {
            // Název - VIT, MAN, STR, DEX
            { "Škola Kočky",        "5", "5", "5", "10" },
            { "Škola Vlka",         "5", "2", "5", "7" },
            { "Škola Medvěda",      "10", "2", "10", "2" },
            { "Škola Zmije",        "5", "7", "2", "10" },
            { "Škola Gryfa",        "10", "10", "2", "5" }
        };

        /// <summary>
        /// Názvy úkolů
        /// </summary>
        private string[] questNames = new string[]
        {
            "Vytvoření postavy", "VZPOMÍNKY", // NONE
            "Ach ti vlci..", "Zpět v lese", "Obrana vesnice", "Útěk", // Malhoun
            "Cesta do Novigradu", "Aréna", "Zakázka na draka", "Setkání s čarodějkou", // Novigrad
            "Cesta do Govorožce", "Návrat do Malhoun", "Osvobození" // Malhoun po druhé
        };

        bool game_hook = true;

        // Player Variables
        private bool playerInGame = false;
        private bool playerInPlayerStats = false;
        private bool playerInInventory = false;
        private bool playerInFight = false;
        private bool playerHelpToggle = true;
        private bool playerInQuest = false;

        private string playerName = null;
        private int playerProgress = 0;

        private List<int> playerInventory = new List<int>();
        private int playerEquip = -1;

        private int playerLocation = 0;
        private int playerXP = 0;

        private int playerHealth = 0;
        private int playerMana = 0;

        private int playerVIT = 0;
        private int playerMAN = 0;
        private int playerSTR = 0;
        private int playerDEX = 0;
        private int playerLUCK = 0;

        private int playerKills = 0;
        private int playerDeaths = 0;

        /// <summary>
        /// gameInit(int loadgame_id = -1) - Spuštění hry + samotná hra
        /// </summary>
        /// <param name="loadgame_id">Unikátní identifikátor savu (-1 = nová hra)</param>
        public void gameInit(int loadgame_id = -1)
        {
            // First start
            if(playerInGame == false) playerInGame = true;

            // Char Creation
            if (this.playerProgress == 0)
            {
                if (loadgame_id == -1) this.gameCreateCharacter();
                else
                {
                    if (this.gameLoadGame(loadgame_id) == 1)
                    {
                        if (this.playerProgress == 0) this.gameCreateCharacter();
                    }
                    else this.gameCreateCharacter();
                }
            }

            /* Příběhová část, flashbacky, vzpomínky na dětství a pubertu... */
            if (this.playerProgress == 1)
            {
                string[] vzpominka_info = {
                    "Vzpomínky z učení umění mečů, souboj s mistrem.", "Vzpomínky z učení ve škole, hodina matematiky.",
                    "Vzpomínky z lovu první příšery.", "Vzpomínka z dokončení výcviku." };
                int vzpominka_counter = 0;
                bool bool_vzpominky = true;
                while(bool_vzpominky == true)
                {
                    v.clear();
                    v.enter(2);
                    v.cwrite("*** "+ questNames[this.playerProgress] + " ***", ConsoleColor.Magenta, ConsoleColor.Black);
                    v.cwrite("> " + vzpominka_info[vzpominka_counter], ConsoleColor.Gray, ConsoleColor.Black);
                    v.enter(1);

                    if(vzpominka_counter == 0)
                    {
                        playerSay(this.playerName, "Ha, dnes tě porazím mistře.", ConsoleColor.Gray);
                        playerSay("Mistr", "Ani v nejmenším, musíš se ještě hodně učit.", ConsoleColor.Yellow);
                        playerSay("Mistr", "Připrav se!", ConsoleColor.Yellow);
                        playerSay("Mistr", "3", ConsoleColor.Yellow);
                        System.Threading.Thread.Sleep(1000);
                        playerSay("Mistr", "2", ConsoleColor.Yellow);
                        System.Threading.Thread.Sleep(1000);
                        playerSay("Mistr", "1", ConsoleColor.Yellow);
                        System.Threading.Thread.Sleep(1000);
                        playerSay("Mistr", "Bojuj <" + this.playerName + ">!", ConsoleColor.Yellow);
                        System.Threading.Thread.Sleep(1000);

                        playerSay(this.playerName, "Porazím tě jak nic.", ConsoleColor.Gray);
                        System.Threading.Thread.Sleep(1000);
                        playerSay("Mistr", "Bojuj mečem, né pusou.", ConsoleColor.Yellow);
                    }
                    else if(vzpominka_counter == 1)
                    {
                        playerSay("Učitel", "A třeba tady <" + this.playerName + "> nám tady poví kolik je 6*6.", ConsoleColor.Cyan);
                        playerSay("Učitel", "Jistě již tuto látku pochopil.", ConsoleColor.Cyan);
                        System.Threading.Thread.Sleep(2000);
                        playerSay(this.playerName, "Výsledek je 36.", ConsoleColor.Gray);
                        System.Threading.Thread.Sleep(2000);
                        playerSay("Učitel", "Výborně, ale to že jsi látku pochopil, neznamená, že budeš rušit mou hodinu..", ConsoleColor.Cyan);
                        playerSay("Učitel", "VEN!", ConsoleColor.Cyan);
                    }
                    else if(vzpominka_counter == 2)
                    {
                        playerSay(this.playerName, "Prostě na něj vlítnem a zabijem ho, patří mu to!", ConsoleColor.Gray);
                        playerSay("Mistr", "Buď v klidu, nesmíš ho vyplašit.", ConsoleColor.Yellow);
                        playerSay(this.playerName, "Ach jo...", ConsoleColor.Gray);
                    }
                    else if(vzpominka_counter == 3)
                    {
                        playerSay("Mistr", "Blahopřeji ti <" + this.playerName + ">, dnešním dnem je z tebe opravdový zaklínač.", ConsoleColor.Yellow);
                        System.Threading.Thread.Sleep(1000);
                        playerSay("Mistr", "Prošel si výcvikem, zvládl všechna potřebná učení a umění.", ConsoleColor.Yellow);
                        System.Threading.Thread.Sleep(1000);
                        playerSay("Mistr", "Zde ti předávám tvůj první zaklínačský meč.", ConsoleColor.Yellow);
                        playerSay("Mistr", "Doufám, že jej budeš používat s rozvahou, a klidnou hlavou.", ConsoleColor.Yellow);

                        // Add Sword
                        playerInventory.Add(0);
                        System.Threading.Thread.Sleep(2000);
                        playerSay(this.playerName, "Děkuji mistře.", ConsoleColor.Gray);
                    }

                    if (vzpominka_counter == vzpominka_info.Length - 1) bool_vzpominky = false;

                    vzpominka_counter++;
                    if (vzpominka_counter < vzpominka_info.Length)
                    {
                        v.enter(1);
                        v.cwrite("Příběh pokračuje..", ConsoleColor.Magenta, ConsoleColor.Black);
                    }

                    System.Threading.Thread.Sleep(3000);
                    v.clear();
                }

                v.clear();
                v.enter(2);
                v.cwrite("*** O 30 let později ***", ConsoleColor.Magenta, ConsoleColor.Black);
                v.enter(1);
                v.write("> Příběh hry se posouvá o 30 let.");
                v.write("> Usadil ses v jedné malé vesnici na severu.");
                v.write("> Je zde klid a pomáháš místním obyvatelům.");
                v.enter(1);
                v.cwrite("Již prakticky zapomínáš na to, že jsi zaklínač.", ConsoleColor.DarkMagenta, ConsoleColor.Black);
                v.enter(1);
                this.playerProgress = 2;
                System.Threading.Thread.Sleep(2000);
                this.gameSaveGame();
                System.Threading.Thread.Sleep(8000); 
            }

            // GAME HOOK
           
            while (game_hook == true)
            {
                v.clear();

                if (this.playerInInventory == false && this.playerInFight == false && this.playerInPlayerStats == false && this.playerInQuest == false)
                {
                    if (this.playerHelpToggle == true) v.cwrite("Funkční klávesy: [ ESC - Pauza;Ukončení hry ] [ C - Informace o postavě ] [ I - Inventář ] [ Q - Spustit Úkol ] [ S - Uložení hry ] [ H - Vypnout/Zapnout nápovědu ]", ConsoleColor.Green, ConsoleColor.Black);
                    v.cwrite("Aktuální lokace: " + location.getLocationName(this.playerLocation), ConsoleColor.DarkGreen, ConsoleColor.Black);
                    if (questNames.Length > this.playerProgress) v.cwrite("Následující úkol: " + questNames[this.playerProgress] + " [ Úkol spustíte klávesou \"Q\" ]", ConsoleColor.Magenta, ConsoleColor.Black);

                }

                while (this.playerInQuest == true)
                {
                    if (this.playerProgress == 2)
                    {
                        v.enter(2);
                        v.cwrite("*** Spuštěn úkol: " + questNames[this.playerProgress] + " ***", ConsoleColor.Magenta, ConsoleColor.Black);
                        v.enter(1);
                        playerSay("Farmář", "Hele vím, že jsi zaklínač, ale pomohl by jsi mi?");
                        playerSay(this.playerName, "S čím?", ConsoleColor.Gray);
                        playerSay("Farmář", "Hele mám problém s jedním vlkem, neustále mi zabíjí můj dobytek.");
                        playerSay("Farmář", "Pomůžeš mi s ním? Dobře se ti odměním.");
                        System.Threading.Thread.Sleep(2000);
                        playerSay(this.playerName, "Vlka? No dobrá.", ConsoleColor.Gray);

                        v.enter(1);
                        v.cwrite("Příběh pokračuje..", ConsoleColor.Magenta, ConsoleColor.Black);
                        System.Threading.Thread.Sleep(5000);

                        v.clear();
                        playerSay(this.playerName, "Jsem mu na stopě už dobrých pár hodin.", ConsoleColor.Gray);
                        playerSay("Farmář", "Prosím zaklínači, jsi má jediná naděje.");
                        System.Threading.Thread.Sleep(3000);

                        v.clear();
                        playerSay("Farmář", "Támhle je!");
                        playerSay(this.playerName, "Tiše!", ConsoleColor.Gray);
                        v.enter(1);
                        v.cwrite("Příběh pokračuje..", ConsoleColor.Magenta, ConsoleColor.Black);
                        System.Threading.Thread.Sleep(5000);

                        bool wolf_fight = mob.playerFightWithMob(mob.createMobByID(2), this.getPlayerData());
                        if (wolf_fight == true)
                        {
                            v.clear();
                            playerSay("Farmář", "Pěkně zaklínači!");
                            playerSay("Farmář", "Konečně můžu spát s klidnou hlavou.");
                            System.Threading.Thread.Sleep(2000);
                            playerSay("Farmář", "Zde máš malou odměnu, dostal jsem to kdysi od jednoho čaroděje.");
                            playerSay("Farmář", "Snad to nějak využiješ, protože mě nenapadá co s tím.");
                            this.playerInventory.Add(item.countItems() - 7);
                            this.playerInventory.Add(item.countItems() - 7);
                            this.playerInventory.Add(item.countItems() - 6);
                            this.playerInventory.Add(item.countItems() - 5);
                            this.playerInventory.Add(item.countItems() - 4);
                            this.playerInventory.Add(item.countItems() - 3);

                            this.playerProgress = 3;
                            this.playerKills++;
                            this.playerXP += 5;
                        }
                        else
                        {
                            v.clear();
                            playerSay("Farmář", "Ach ne! Zaklínači!");
                            this.playerProgress = 2;
                            this.playerDeaths++;
                        }
                    }
                    else if (this.playerProgress == 3)
                    {
                        v.enter(2);
                        v.cwrite("*** Spuštěn úkol: " + questNames[this.playerProgress] + " ***", ConsoleColor.Magenta, ConsoleColor.Black);
                        v.enter(1);

                        playerSay("Farmář", "Zaklínači, zabil jsi vlka a můj dobytek stále umírá.");
                        playerSay("Farmář", "Co se to děje?");
                        playerSay(this.playerName, "Asi vím o co zde jde, myslel sem si to již v lese!", ConsoleColor.Gray);
                        playerSay(this.playerName, "Je to duch lesa, který z nějakého důvodu štve zvěř proti nám.", ConsoleColor.Gray);
                        System.Threading.Thread.Sleep(1000);
                        playerSay("Farmář", "Ten malý zmetek Rudolf, furt dělá bordel v lese.");

                        v.enter(1);
                        v.cwrite("Příběh pokračuje..", ConsoleColor.Magenta, ConsoleColor.Black);
                        System.Threading.Thread.Sleep(5000);

                        v.clear();
                        playerSay("Farmář", "Takže co teď <" + this.playerName + ">?");
                        playerSay(this.playerName, "No budu si s ním muset promluvit nebo ho zabít.", ConsoleColor.Gray);
                        playerSay("Farmář", "Dělej co budeš muset, dobře se ti odměním.");

                        v.enter(1);
                        v.cwrite("Příběh pokračuje..", ConsoleColor.Magenta, ConsoleColor.Black);
                        System.Threading.Thread.Sleep(5000);

                        v.clear();
                        playerSay("Duch lesa", "To poslali zaklínače, za všechno co mi provedli?", ConsoleColor.Red);
                        playerSay("Duch lesa", "Po tom všem co mi provádějí, pošlou zaklínače aby mě zabil.", ConsoleColor.Red);
                        playerSay("Duch lesa", "Ničí les, i mě, a poté na mě pošlou nájemného zabijáka.", ConsoleColor.Red);
                        System.Threading.Thread.Sleep(2000);
                        playerSay(this.playerName, "Přišel sem se s tebou dohodnout.", ConsoleColor.Gray);
                        playerSay("Duch lesa", "Není o čem mluvit, lidé z té vesnice si zaslouží umřít.", ConsoleColor.Red);
                        playerSay(this.playerName, "Takže po dobrém to nepůjde.", ConsoleColor.Gray);
                        System.Threading.Thread.Sleep(5000);

                        bool wolf_fight_1 = mob.playerFightWithMob(mob.createMobByID(2), this.getPlayerData());
                        bool wolf_fight_2 = false;
                        bool duch_lesa_fight = false;
                        if (wolf_fight_1 == true)
                        {
                            this.playerKills++;
                            this.playerXP += 5;

                            v.clear();
                            playerSay("Duch lesa", "To je vše co zvládneš?", ConsoleColor.Red);
                            System.Threading.Thread.Sleep(3000);

                            wolf_fight_2 = mob.playerFightWithMob(mob.createMobByID(2), this.getPlayerData());
                            if (wolf_fight_2 == true)
                            {
                                this.playerKills++;
                                this.playerXP += 5;

                                v.clear();
                                playerSay("Duch lesa", "Tak tě zabiju sám!", ConsoleColor.Red);
                                System.Threading.Thread.Sleep(3000);

                                duch_lesa_fight = mob.playerFightWithMob(mob.createMobByID(3), this.getPlayerData());
                                if (duch_lesa_fight == true)
                                {
                                    this.playerKills++;
                                    this.playerXP += 7;
                                    this.playerProgress = 4;

                                    v.clear();
                                    playerSay("Duch lesa", "AAAAAaaaa, proklínám tu zatracenou vesnici!", ConsoleColor.Red);
                                    playerSay(this.playerName, "Zatraceně!", ConsoleColor.Gray);
                                    System.Threading.Thread.Sleep(3000);

                                    v.clear();
                                    playerSay("Farmář", "Oh bože, co se stalo zaklínači?");
                                    playerSay(this.playerName, "O dohodě nechtěl ani slyšet, a když sem ho zabil, seslal kletbu.", ConsoleColor.Gray);
                                    playerSay("Farmář", "Nech to být, kletby jsou povídačky pro staré babky.");
                                    playerSay("Farmář", "Zde máš odměnu, je to taková menší sekera, každopádně je pěkně ostrá.");
                                    playerSay(this.playerName, "Tím bych si nebyl tak jist, a díky za tu sekeru.", ConsoleColor.Gray);
                                    playerSay("Farmář", "Ale nech to bejt! Pojď se mnou do hospody, zvu tě!");
                                    this.playerInventory.Add(3);
                                    System.Threading.Thread.Sleep(3000);
                                    v.enter(2);
                                    playerSay("Farmář", "HA HA HA! Prej kletba!...");
                                }
                                else this.playerDeaths++;
                            }
                            else this.playerDeaths++;
                        }
                        else this.playerDeaths++;
                    }
                    else if (this.playerProgress == 4)
                    {
                        v.enter(2);
                        v.cwrite("*** Spuštěn úkol: " + questNames[this.playerProgress] + " ***", ConsoleColor.Magenta, ConsoleColor.Black);
                        v.enter(1);

                        playerSay("Strážce", "Začíná noc!", ConsoleColor.DarkGreen);
                        System.Threading.Thread.Sleep(2000);
                        playerSay("Farmář", "Drž hubu!");
                        //playerSay("Mazánek", "Drž hubu!", ConsoleColor.Black, ConsoleColor.White);
                        playerSay(this.playerName, "Drž hubu..", ConsoleColor.Gray);

                        v.enter(1);
                        v.cwrite("Příběh pokračuje..", ConsoleColor.Magenta, ConsoleColor.Black);
                        System.Threading.Thread.Sleep(5000);

                        v.clear();
                        playerSay("Farmář", "Tak co pivko, chutnalo zaklínači?");
                        playerSay(this.playerName, "Ale jooo, chutnalo.", ConsoleColor.Gray);
                        playerSay(this.playerName, "Mam v hlavě furt tu zatracenou kletbu.", ConsoleColor.Gray);
                        playerSay("Farmář", "Ale neboj, to bude v pohodě.");

                        v.enter(1);
                        v.cwrite("Příběh pokračuje..", ConsoleColor.Magenta, ConsoleColor.Black);
                        System.Threading.Thread.Sleep(5000);

                        v.clear();
                        playerSay("Alina", "AAAAAAAAAAAAaaaaaaaaaaaaaaaaaaa, pomooooc!", ConsoleColor.Magenta);
                        playerSay(this.playerName, "Co se děje?!", ConsoleColor.Gray);
                        playerSay("Alina", "Tam v zadu, za stájema!", ConsoleColor.Magenta);
                        playerSay(this.playerName, "Do hajzlu!", ConsoleColor.Gray);
                        System.Threading.Thread.Sleep(1000);
                        playerSay(this.playerName, "Uteč!", ConsoleColor.Gray);

                        v.enter(1);
                        v.cwrite("Příběh pokračuje..", ConsoleColor.Magenta, ConsoleColor.Black);
                        System.Threading.Thread.Sleep(5000);

                        int mobKills = r.Next(2, 5);
                        for (int i = 0; i < mobKills; i++)
                        {
                            bool fight_result = mob.playerFightWithMob(mob.createMobByID(r.Next(0, 2)), this.getPlayerData());
                            if (fight_result == true)
                            {
                                this.playerKills++;
                                this.playerXP += r.Next(1, 5);
                            }
                            else
                            {
                                this.playerDeaths++;
                                goto QUEST_END_HOOK;
                            }
                        }

                        v.clear();
                        playerSay(this.playerName, "Je jich moc musíme všichni utéct!", ConsoleColor.Gray);
                        System.Threading.Thread.Sleep(2000);

                        v.clear();
                        v.write("> Všichni obyvatelé vesnice utíkají.");
                        System.Threading.Thread.Sleep(2000);
                        v.write("> Všude je zmatek.");
                        System.Threading.Thread.Sleep(2000);
                        v.write("> Spousta lidí umírá.");
                        System.Threading.Thread.Sleep(5000);

                        v.clear();
                        playerSay(this.playerName, "Buď se schovejte nebo utečte co nejdál od vesnice!", ConsoleColor.Gray);
                        this.playerProgress = 5;
                    }
                    else if (this.playerProgress == 5)
                    {
                        v.enter(2);
                        v.cwrite("*** Spuštěn úkol: " + questNames[this.playerProgress] + " ***", ConsoleColor.Magenta, ConsoleColor.Black);
                        v.enter(1);

                        playerSay(this.playerName, "Musíme utéc, nejlépe do okolních vesnic.", ConsoleColor.Gray);
                        playerSay(this.playerName, "Úplně nejlepší by byl Novigrad, ale je mi jasný že se tam spousta z vás nedostane.", ConsoleColor.Gray);
                        playerSay("Děda", "Na koho tím narážíš?!");
                        playerSay(this.playerName, "Dědo uklidněte se, stráže a já vás starší doprovodíme do sousední vesnice.", ConsoleColor.Gray);

                        v.enter(1);
                        v.cwrite("Příběh pokračuje..", ConsoleColor.Magenta, ConsoleColor.Black);
                        System.Threading.Thread.Sleep(5000);

                        int mobKills = r.Next(2, 5);
                        for (int i = 0; i < mobKills; i++)
                        {
                            bool fight_result = mob.playerFightWithMob(mob.createMobByID(r.Next(0, 2)), this.getPlayerData());
                            if (fight_result == true)
                            {
                                this.playerKills++;
                                this.playerXP += r.Next(1, 7);
                            }
                            else
                            {
                                this.playerDeaths++;
                                goto QUEST_END_HOOK;
                            }
                        }

                        v.clear();
                        playerSay("Děda", "Děkujeme ti zaklínači, že jsi nám pomohl, přežít cestu.");
                        playerSay(this.playerName, "Nemáte zač dědo, poděkujte spíš strážcům.", ConsoleColor.Gray);
                        playerSay("Děda", "Počkej zaklínači, zde máš má stará zbraň, snad se ti bude hodit.");
                        playerSay(this.playerName, "To nemusíte, ale děkuji.", ConsoleColor.Gray);
                        this.playerInventory.Add(7);
                        this.setPlayerLocation(4);
                        this.playerProgress = 6;
                    }
                    else if (this.playerProgress == 6)
                    {
                        v.enter(2);
                        v.cwrite("*** Spuštěn úkol: " + questNames[this.playerProgress] + " ***", ConsoleColor.Magenta, ConsoleColor.Black);
                        v.enter(1);

                        playerSay(this.playerName, "Musím vydělat nějaké peníze, a sehnat pár lidí.", ConsoleColor.Gray);
                        playerSay(this.playerName, "Snad budu mít v Novigradu štěstí.", ConsoleColor.Gray);
                        playerSay(this.playerName, "To by bylo kdyby mě po cestě přepadl nějaký bandita..", ConsoleColor.Gray);

                        if (d.getPlayerLuckChance(this.playerLUCK) == false)
                        {
                            System.Threading.Thread.Sleep(3000);
                            playerSay(this.playerName, "Jéé, jako bych o tom právě nemluvil.", ConsoleColor.Gray);
                            System.Threading.Thread.Sleep(2000);
                            int mobKills = r.Next(2, 5);
                            for (int i = 0; i < mobKills; i++)
                            {
                                bool fight_result = mob.playerFightWithMob(mob.createMobByID(0), this.getPlayerData());
                                if (fight_result == true)
                                {
                                    this.playerKills++;
                                    this.playerXP += r.Next(1, 3);
                                }
                                else
                                {
                                    this.playerDeaths++;
                                    goto QUEST_END_HOOK;
                                }
                            }
                        }
                        else
                        {
                            playerSay(this.playerName, "Koukám, že mám docela kliku.", ConsoleColor.Gray);
                            System.Threading.Thread.Sleep(2000);
                        }
                        v.clear();
                        playerSay(this.playerName, "Konečně vidím Novigrad..", ConsoleColor.Gray);
                        playerSay(this.playerName, "Tak snad se poštěstí.", ConsoleColor.Gray);
                        this.setPlayerLocation(3);
                        this.playerProgress = 7;
                    }
                    else if (this.playerProgress == 7)
                    {
                        v.enter(2);
                        v.cwrite("*** Spuštěn úkol: " + questNames[this.playerProgress] + " ***", ConsoleColor.Magenta, ConsoleColor.Black);
                        v.enter(1);

                        playerSay("Gladiátor", "Hej zaklínači, nechceš si trochu za zápasit?");
                        playerSay(this.playerName, "Nemam čas, musím sehnat peníze.", ConsoleColor.Gray);
                        playerSay("Gladiátor", "Hahaha, tak přijď na gladiátorský turnaj! Prej tam bude i nějakej zvláštní host.");
                        playerSay(this.playerName, "Tak mohl bych to zkusit..", ConsoleColor.Gray);
                        System.Threading.Thread.Sleep(5000);

                        int mobKills = r.Next(3, 8);
                        for (int i = 0; i < mobKills; i++)
                        {
                            bool fight_result = mob.playerFightWithMob(mob.createMobByID(5), this.getPlayerData());
                            if (fight_result == true)
                            {
                                this.playerKills++;
                                this.playerXP += 1;
                            }
                            else
                            {
                                this.playerDeaths++;
                                goto QUEST_END_HOOK;
                            }
                        }

                        bool samurai_fight = mob.playerFightWithMob(mob.createMobByID(4), this.getPlayerData());
                        if (samurai_fight == true)
                        {
                            v.clear();
                            playerSay("Samurai", "Pěkně zaklínači, překvapil jsi mě.", ConsoleColor.Red);
                            playerSay("Samurai", "Zde máš mojí katanu, jako projev úcty.", ConsoleColor.Red);
                            playerSay(this.playerName, "Pěkná, jen se sní budu muset naučet zacházet, byl jsi důstojný protivník.", ConsoleColor.Gray);
                            this.playerKills++;
                            this.playerXP += 10;
                            this.playerInventory.Add(19);
                        }
                        else
                        {
                            v.clear();
                            playerSay("Samurai", "Jak předvídatelné.", ConsoleColor.Red);
                            playerSay("Samurai", "Jak by mohl prostý zaklínač porazit samuraje..", ConsoleColor.Red);
                            this.playerDeaths++;
                        }
                        this.playerProgress = 8;
                    }
                    else if (this.playerProgress == 8)
                    {
                        v.enter(2);
                        v.cwrite("*** Spuštěn úkol: " + questNames[this.playerProgress] + " ***", ConsoleColor.Magenta, ConsoleColor.Black);
                        v.enter(1);

                        playerSay(this.playerName, "Hm tady píšou, že kouzelník hledá někoho kdo pro něj zabije draka.", ConsoleColor.Gray);
                        playerSay(this.playerName, "Hm a odměna je za to taky hezká.", ConsoleColor.Gray);
                        System.Threading.Thread.Sleep(5000);
                        playerSay("Kouzelník", "Takže ty si na tuhle zakázku troufáš zaklínači?", ConsoleColor.Cyan);
                        playerSay(this.playerName, "Ano, troufám kouzelníku, ale mám podmínku, pokud bude drakův poklad obsahovat zbraně.", ConsoleColor.Gray);
                        playerSay(this.playerName, "Vezmu si je.", ConsoleColor.Gray);
                        playerSay("Kouzelník", "Klidně si vem celý jeho poklad, mě nezajímá, jde mi jen o jeho kůži.", ConsoleColor.Cyan);
                        playerSay(this.playerName, "Dobrá tedy.", ConsoleColor.Gray);
                        System.Threading.Thread.Sleep(5000);

                        bool dragon_fight = mob.playerFightWithMob(mob.createMobByID(6), this.getPlayerData());
                        if (dragon_fight == true)
                        {
                            v.clear();
                            playerSay(this.playerName, "Hm kladivo s blesky po stranách? Co je to?", ConsoleColor.Gray);
                            this.playerInventory.Add(9);
                            this.playerKills++;
                            this.playerXP += 10;
                        }
                        else
                        {
                            this.playerDeaths++;
                            goto QUEST_END_HOOK;
                        }

                        playerSay("Kouzelník", "Díky ti za spolupráci zaklínači.", ConsoleColor.Cyan);
                        playerSay("Kouzelník", "Celou dobu jsem ti věřil, že to zvládneš.", ConsoleColor.Cyan);
                        playerSay("Kouzelník", "Zde máš nějakou odměnu..", ConsoleColor.Cyan);
                        this.playerInventory.Add(item.countItems() - 7);
                        this.playerInventory.Add(item.countItems() - 7);
                        this.playerInventory.Add(item.countItems() - 6);
                        this.playerInventory.Add(item.countItems() - 5);
                        this.playerInventory.Add(item.countItems() - 4);
                        this.playerInventory.Add(item.countItems() - 3);
                        this.playerProgress = 9;
                    }
                    else if (this.playerProgress == 9)
                    {
                        v.enter(2);
                        v.cwrite("*** Spuštěn úkol: " + questNames[this.playerProgress] + " ***", ConsoleColor.Magenta, ConsoleColor.Black);
                        v.enter(1);

                        playerSay("Čarodějka", "Ty jsi <" + this.playerName + ">, je to tak?", ConsoleColor.Magenta);
                        playerSay(this.playerName, "Jak, jak jak to víš?", ConsoleColor.Gray);
                        playerSay("Čarodějka", "Vím všechno.", ConsoleColor.Magenta);
                        playerSay(this.playerName, "Ahaa, dobřee.", ConsoleColor.Gray);
                        System.Threading.Thread.Sleep(2000);
                        playerSay("Čarodějka", "Vím o té vesnici.", ConsoleColor.Magenta);
                        System.Threading.Thread.Sleep(1000);
                        playerSay("Čarodějka", "Vím o tvém útěku.", ConsoleColor.Magenta);
                        System.Threading.Thread.Sleep(1000);
                        playerSay("Čarodějka", "Vím o tém \"poslání\" tady v Novigradu.", ConsoleColor.Magenta);
                        System.Threading.Thread.Sleep(2000);
                        playerSay(this.playerName, "Nebylo to tak úplně tak, ale co odemne chceš?", ConsoleColor.Gray);
                        playerSay("Čarodějka", "Já od tebe nic nechci, to ty odemne něco chceš.", ConsoleColor.Magenta);
                        playerSay(this.playerName, "?!", ConsoleColor.Gray);
                        playerSay("Čarodějka", "Ta kletba, byla uvalena na tu vesnici, Duchem lesa vyššího řádu.", ConsoleColor.Magenta);
                        playerSay("Čarodějka", "A ty jí chceš zrušit.", ConsoleColor.Magenta);
                        playerSay("Čarodějka", "Je několik možností, buď se usmíříš s duchem lesa..", ConsoleColor.Magenta);
                        playerSay("Čarodějka", "nebo....", ConsoleColor.Magenta);
                        System.Threading.Thread.Sleep(2000);
                        playerSay("Čarodějka", "Zabít nejvyššího z duchů lesa.", ConsoleColor.Magenta);
                        playerSay(this.playerName, "Usmiřování s paličatou mrtvolou nepřichází v úvahu.", ConsoleColor.Gray);
                        playerSay("Čarodějka", "To je jen na tobě. Muhahahaha", ConsoleColor.Magenta);

                        System.Threading.Thread.Sleep(5000);
                        v.clear();
                        v.write("> Čarodějka zmizela.");
                        v.write("> Přemýšlíš o tom co ti před svým zmizením řekla.");
                        v.write("> Zatím to vypadá tak, že budeš muset zabít nejvyššího z duchů lesa.");

                        this.playerProgress = 10;
                    }
                    else if (this.playerProgress == 10)
                    {
                        v.enter(2);
                        v.cwrite("*** Spuštěn úkol: " + questNames[this.playerProgress] + " ***", ConsoleColor.Magenta, ConsoleColor.Black);
                        v.enter(1);

                        playerSay(this.playerName, "Takže peníze, zbraně a lidi už mám.", ConsoleColor.Gray);
                        playerSay(this.playerName, "Takže se můžu vrátit zpět a pomoc vesnici.", ConsoleColor.Gray);

                        System.Threading.Thread.Sleep(3000);
                        playerSay(this.playerName, "Jasně, banditi....", ConsoleColor.Gray);
                        System.Threading.Thread.Sleep(2000);
                        int mobKills = r.Next(3, 8);
                        for (int i = 0; i < mobKills; i++)
                        {
                            bool fight_result = mob.playerFightWithMob(mob.createMobByID(0), this.getPlayerData());
                            if (fight_result == true)
                            {
                                this.playerKills++;
                                this.playerXP += r.Next(1, 3);
                            }
                            else
                            {
                                this.playerDeaths++;
                                goto QUEST_END_HOOK;
                            }
                        }

                        v.clear();
                        playerSay(this.playerName, "Nazdaaar dědo!", ConsoleColor.Gray);
                        this.setPlayerLocation(5);
                        this.playerProgress = 11;
                    }
                    else if (this.playerProgress == 11)
                    {
                        v.enter(2);
                        v.cwrite("*** Spuštěn úkol: " + questNames[this.playerProgress] + " ***", ConsoleColor.Magenta, ConsoleColor.Black);
                        v.enter(1);

                        playerSay(this.playerName, "Do hajzlu co to tady je!", ConsoleColor.Gray);
                        System.Threading.Thread.Sleep(3000);

                        int mobKills = r.Next(2, 8);
                        for (int i = 0; i < mobKills; i++)
                        {
                            bool fight_result = mob.playerFightWithMob(mob.createMobByID(r.Next(0, 2)), this.getPlayerData());
                            if (fight_result == true)
                            {
                                this.playerKills++;
                                this.playerXP += r.Next(1, 7);
                            }
                            else
                            {
                                this.playerDeaths++;
                                goto QUEST_END_HOOK;
                            }
                        }

                        v.clear();
                        playerSay(this.playerName, "Tak, vesnice by byla vyčištěná.", ConsoleColor.Gray);
                        playerSay(this.playerName, "Teď už jen obnovit hradby a postavit strážní věže..", ConsoleColor.Gray);
                        System.Threading.Thread.Sleep(5000);

                        this.playerInventory.Add(item.countItems() - 7);
                        this.playerInventory.Add(item.countItems() - 7);
                        this.playerInventory.Add(item.countItems() - 7);
                        this.playerInventory.Add(item.countItems() - 7);
                        this.playerInventory.Add(item.countItems() - 7);

                        v.clear();
                        v.write("> Hradby se podařilo plně obnovit, strážní věže byli nově postaveny.");
                        v.write("> Obyvatelé se vrátili do vesnice.");
                        v.write("> Vše se vrací do normálu, ale kletba je zde pořád ovšem strážím se daří držet zlo za hranicemi vesnice.");
                        v.cwrite("Obdržel jsi 5 bodů VIT.", ConsoleColor.Yellow, ConsoleColor.Black);

                        this.setPlayerLocation(0);
                        this.playerProgress = 12;
                    }
                    else if (this.playerProgress == 12)
                    {
                        v.enter(2);
                        v.cwrite("*** Spuštěn úkol: " + questNames[this.playerProgress] + " ***", ConsoleColor.Magenta, ConsoleColor.Black);
                        v.enter(1);

                        // Cesta do temného lesa, s pomocí party strážných, kouzelníka a čarodějnice
                        // Vyhledání vyšších duchů lesa - následné zabití 4 dalších vyšších duchů lesa
                        // Vyhledání nejvyššího ducha lesa - následné zabití jej

                        v.write("> Vydáváš se na cestu k velkému stromu, kde sídlí bájní vyšší duchové.");
                        System.Threading.Thread.Sleep(1000);
                        v.write("> Jsi na cestě spoustu hodin.");
                        System.Threading.Thread.Sleep(1000);
                        v.write("> Stále nejsi u cíle.");
                        System.Threading.Thread.Sleep(5000);

                        v.clear();
                        v.write("> Jsi na cestě již několik dní.");
                        System.Threading.Thread.Sleep(1000);
                        v.write("> Nevíš jestli jsi na správné cestě.");
                        System.Threading.Thread.Sleep(1000);
                        v.write("> Nevíš zda-li vesnice odolává útokům.");
                        System.Threading.Thread.Sleep(1000);
                        v.write("> Nevíš zda-li opravdu vyšší duchové existují.");
                        System.Threading.Thread.Sleep(5000);

                        v.clear();
                        playerSay(this.playerName, "On opravdu existuje!", ConsoleColor.Gray);
                        playerSay(this.playerName, "Velký strom!", ConsoleColor.Gray);
                        System.Threading.Thread.Sleep(5000);

                        int animalKills = r.Next(1, 4);
                        for (int i = 0; i < animalKills; i++)
                        {
                            bool fight_result = mob.playerFightWithMob(mob.createMobByID(r.Next(0, 2)), this.getPlayerData());
                            if (fight_result == true)
                            {
                                this.playerKills++;
                                this.playerXP += r.Next(1, 3);
                            }
                            else
                            {
                                this.playerDeaths++;
                                goto QUEST_END_HOOK;
                            }
                        }

                        v.clear();
                        playerSay(this.playerName, "Tak se ukažte!!");
                        System.Threading.Thread.Sleep(5000);

                        for (int i = 0; i < 5; i++)
                        {
                            bool fight_result = mob.playerFightWithMob(mob.createMobByID(7), this.getPlayerData());
                            if (fight_result == true)
                            {
                                this.playerKills++;
                                this.playerXP += 5;
                            }
                            else
                            {
                                this.playerDeaths++;
                                goto QUEST_END_HOOK;
                            }
                        }

                        v.clear();
                        playerSay("Nejvyšší duch lesa", "Podívejme se, zaklínač.", ConsoleColor.Red);
                        System.Threading.Thread.Sleep(5000);

                        bool finalBoss_Result = mob.playerFightWithMob(mob.createMobByID(8), this.getPlayerData());
                        if (finalBoss_Result == true)
                        {
                            this.playerKills++;
                            this.playerXP += 20;
                        }
                        else
                        {
                            this.playerDeaths++;
                            goto QUEST_END_HOOK;
                        }

                        v.clear();
                        playerSay("Nejvyšší duch lesa", "aaaaaa..", ConsoleColor.Red);
                        playerSay(this.playerName, "Konec kletby?!", ConsoleColor.Gray);
                        playerSay(this.playerName, "Doufám, že ano, KLEPNO, rychle do vesnice.", ConsoleColor.Gray);
                        System.Threading.Thread.Sleep(5000);

                        v.clear();
                        playerSay("Strážce", "Zaklínač <" + this.playerName + "> se vrátil!!", ConsoleColor.DarkGreen);
                        playerSay("Strážce", "Otevřete bránu!!", ConsoleColor.DarkGreen);
                        playerSay(this.playerName, "Nazdaar Strážci, tak co, jak to jde?", ConsoleColor.Gray);
                        playerSay("Strážce", "Tady paráda, co tvůj úkol zaklínači?", ConsoleColor.DarkGreen);
                        playerSay(this.playerName, "Úkol jsem splnil, co kletba?", ConsoleColor.Gray);
                        playerSay("Strážce", "No víš zaklínači", ConsoleColor.DarkGreen, ConsoleColor.Black, false);
                        System.Threading.Thread.Sleep(1000);
                        v.cwrite(".", ConsoleColor.DarkGreen, ConsoleColor.Black, false);
                        System.Threading.Thread.Sleep(1000);
                        v.cwrite(".", ConsoleColor.DarkGreen, ConsoleColor.Black, false);
                        System.Threading.Thread.Sleep(1000);
                        v.cwrite(".", ConsoleColor.DarkGreen, ConsoleColor.Black);
                        System.Threading.Thread.Sleep(3000);
                        playerSay("Strážce", "Už několik dní na nás neútočí..", ConsoleColor.DarkGreen);
                        playerSay(this.playerName, "Cože??!", ConsoleColor.Gray);
                        System.Threading.Thread.Sleep(3000);

                        playerSay("Kouzelník", "Nazdar zaklínači, vzpomínáš si na starého kouzelníka?", ConsoleColor.Cyan);
                        playerSay(this.playerName, "Ty jsi?..", ConsoleColor.Gray);
                        playerSay("Kouzelník", "Pomohl jsi mi zabít draka, kdybys mi řekl o té kletbě, pomohl bych ti již dříve.", ConsoleColor.Cyan);
                        playerSay("Kouzelník", "Náhodou sem jel kolem Malhounu a místní mě nějak nechtěli pustit, hlavně ten farmář.", ConsoleColor.Cyan);
                        playerSay("Farmář", "Ahoj kouzelníku, nazdáár <" + this.playerName + ">!");
                        playerSay(this.playerName, "Takže jsem vyvraždil, vyšší duchy úplně zbytečně?", ConsoleColor.Gray);
                        System.Threading.Thread.Sleep(2000);
                        playerSay("Farmář", "Jo...");
                        playerSay("Kouzelník", "Počkej tys je opravdu zabil?..", ConsoleColor.Cyan);
                        System.Threading.Thread.Sleep(1000);
                        playerSay(this.playerName, "Ano, jsou mrtví.", ConsoleColor.Gray);
                        playerSay("Kouzelník", "No co alespoň je o starost méně.", ConsoleColor.Cyan);
                        playerSay("Kouzelník", "Hahaha..", ConsoleColor.Cyan);

                        System.Threading.Thread.Sleep(10000);

                        v.clear();
                        v.cwrite(gameModels.v("gamelogo"), ConsoleColor.Green, ConsoleColor.Black);
                        System.Threading.Thread.Sleep(3000);

                        v.enter(2);
                        v.cwrite("> Konec hry.", ConsoleColor.Red, ConsoleColor.Black);
                        System.Threading.Thread.Sleep(3000);

                        v.enter(1);
                        v.write("> Hru naprogramoval: ", false);
                        v.cwrite(main.getGameAuthor(), ConsoleColor.Cyan, ConsoleColor.Black);
                        System.Threading.Thread.Sleep(3000);

                        v.enter(1);
                        v.write("> Aktuální verze hry: ", false);
                        v.cwrite(main.getGameVersion(), ConsoleColor.Cyan, ConsoleColor.Black);
                        System.Threading.Thread.Sleep(3000);

                        this.setPlayerLocation(0);
                        this.playerProgress = 13;
                    }
                
                // Hook pro konec questu
                // Jako hook použit je n v případě že hráč nesplní quest; resp. umře při něm.
                QUEST_END_HOOK:
                    System.Threading.Thread.Sleep(5000);
                    v.clear();
                    v.enter(1);
                    v.cwrite("**** Úkol byl ukončen. ***", ConsoleColor.Magenta, ConsoleColor.Black);
                    v.write("> Pro pokračování stiskni klávesu ENTER.");
                    this.playerInQuest = false;
                    this.gameSaveGame();
                    System.Threading.Thread.Sleep(2000);
                }
                // Frame Action
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key.ToString() == "Escape")
                    v.screen("end_game");

                // Quest
                else if (key.Key == ConsoleKey.Q && questNames.Length > this.playerProgress && this.playerInPlayerStats == false && this.playerInInventory == false && this.playerInFight == false && this.playerInQuest == false)
                {
                    this.playerInQuest = true;
                }

                // Save
                else if (key.Key == ConsoleKey.S && this.playerInPlayerStats == false && this.playerInInventory == false && this.playerInFight == false && this.playerInQuest == false)
                {
                    this.gameSaveGame();
                    System.Threading.Thread.Sleep(2000);
                }

                // Help
                else if (key.Key == ConsoleKey.H && this.playerInPlayerStats == false && this.playerInInventory == false && this.playerInFight == false && this.playerInQuest == false)
                {
                    this.playerHelpToggle = (this.playerHelpToggle == true) ? false : true;
                }

                // Player Info
                else if (key.Key == ConsoleKey.C && this.playerInPlayerStats == false && this.playerInInventory == false && this.playerInFight == false && this.playerInQuest == false)
                {
                    v.write("Otevírám informace o postavě..");
                    this.playerShowCharacter();
                }

                // Inventory
                else if (key.Key == ConsoleKey.I && this.playerInInventory == false && this.playerInFight == false && this.playerInPlayerStats == false && this.playerInQuest == false)
                {
                    v.write("Otevírám inventář..");
                    this.playerShowInventory();
                }

            }
        }

        /// <summary>
        /// gameCreateCharacter() - Vytvoření herního charakteru
        /// </summary>
        private void gameCreateCharacter()
        {
            playerInventory.Clear();
            playerEquip = -1;

            string layout = gameModels.v("5_cols_layout");

            layout = layout.Replace("@@top_info_01@@", ">");
            layout = layout.Replace("@@top_info_02@@", "Vytvoř si svou herní postavu. ");

            layout = gameModels.addmodelintolayout("2left_info_", "player_noob", layout);
            layout = layout.Replace("@@player_noob_name@@", "  Tvá postava   ");
            layout = layout.Replace("@@player_noob_level@@","                ");

            layout = gameModels.addmodelintolayout("left_info_", "character_create_step", layout);
            layout = layout.Replace("@@step_num@@",         "       1.       ");
            layout = layout.Replace("@@step_name@@",        "  Jméno postavy ");
            layout = layout.Replace("@@step_popis_1@@",     "Zadej jméno své-");
            layout = layout.Replace("@@step_popis_2@@",     "ho bojovníka.   ");

            layout = gameModels.addmodelintolayout("center_info_", "character_create_step", layout);
            layout = layout.Replace("@@step_num@@",         "       2.       ");
            layout = layout.Replace("@@step_name@@",        "   Výběr školy  ");
            layout = layout.Replace("@@step_popis_1@@",     "Vyber si školu a");
            layout = layout.Replace("@@step_popis_2@@",     "absolvuj výcvik.");

            layout = gameModels.addmodelintolayout("right_info_", "character_create_step", layout);
            layout = layout.Replace("@@step_num@@",         "       3.       ");
            layout = layout.Replace("@@step_name@@",        "      BOJ       ");
            layout = layout.Replace("@@step_popis_1@@",     "Vydej se zabíjet");
            layout = layout.Replace("@@step_popis_2@@",     "příšery.        ");

            layout = gameModels.addmodelintolayout("2right_info_", "player_wss", layout);
            layout = layout.Replace("@@player_wss_name@@", "  Tvůj učitel   ");
            layout = layout.Replace("@@player_wss_level@@","                ");
            v.write(layout);

            playerSay("Tvůj budoucí učitel", "Takže ty si myslíš, že máš na to být zaklínačem?", ConsoleColor.Yellow);
            playerSay("Ty", "Ano, mám!");
            while (playerName == null)
            {
                
                playerSay("Tvůj budoucí učitel", "Jak se jmenuješ bojovníku?", ConsoleColor.Yellow);
                v.write("Tvé jméno: ", false);
                string _temp_name = Console.ReadLine();
                if (_temp_name.Length > 2 && _temp_name.Length < 15)
                {
                    playerName = _temp_name;
                }
                else playerSay("Tvůj budoucí učitel", "Nikdo nemá jméno, které nemá ani 2 znaky nebo delší než 15 znaků, chci tvé opravdové jméno.", ConsoleColor.Yellow);
            }
            playerSay("Tvůj budoucí učitel", "Tak tě tedy vítám <" + playerName +">, tu máš seznam škol, na kterých můžeš začít svůj výcvik.", ConsoleColor.Yellow);
            playerSay("Tvůj budoucí učitel", "A pamatuj si, být zaklínačem je čest, bojovat proti nestvůrám a zlu.", ConsoleColor.Yellow);
            playerSay("Tvůj budoucí učitel", "Dost povídání, zde máš ten seznam, zvol si dobře a s rozmyslem.", ConsoleColor.Yellow);
            v.enter(1);

            v.write("+ ---------------------- | Seznam škol pro začínající zaklínače | ---------------------- +");
            for(int skola_id = 0;skola_id < gameSchools.GetLength(0); skola_id ++)
            {
                v.write("| " + (skola_id + 1) + ") " + gameSchools[skola_id, 0] + " [ +" + gameSchools[skola_id, 1] + " VIT, +" + gameSchools[skola_id, 2] + " MANA, +" + gameSchools[skola_id, 3] + " STR, +" + gameSchools[skola_id, 4] + " DEX ]");
            }
            v.write("+ -------------------------------------------------------------------------------------- +");

            v.enter(1);
            playerSay("Tvůj budoucí učitel", "Není jich sice tolik, ale každá tě na boj připravý trochu jinak.", ConsoleColor.Yellow);
            playerSay("Tvůj budoucí učitel", "Já jsem byl vycvičen ve škole vlka, připravila mě jak na boj s meči tak na boj s magií.", ConsoleColor.Yellow);
            playerSay("Tvůj budoucí učitel", "Ale někomu prostě magie nesedne, a volí školu Medvěda, která je zaměřená hlavně na boj se zbraněmi.", ConsoleColor.Yellow);
            playerSay("Tvůj budoucí učitel", "Tvoje volba?", ConsoleColor.Yellow);


            bool gameSchoolChoiceResult = false;
            while (gameSchoolChoiceResult == false)
            {
                v.write("Tvá volba (pouze číslo školy ze seznamu): ", false);
                string schoolChoiceInput = Console.ReadLine();

                int schoolChoice_ID = 0;
                gameSchoolChoiceResult = int.TryParse(schoolChoiceInput, out schoolChoice_ID);
                if (gameSchoolChoiceResult == true)
                {
                    if(schoolChoice_ID > 0 && schoolChoice_ID < gameSchools.GetLength(0))
                    {
                        int school_vit = 0; int school_man = 0; int school_str = 0; int school_dex = 0;
                        Int32.TryParse(gameSchools[schoolChoice_ID, 1], out school_vit);
                        Int32.TryParse(gameSchools[schoolChoice_ID, 2], out school_man);
                        Int32.TryParse(gameSchools[schoolChoice_ID, 3], out school_str);
                        Int32.TryParse(gameSchools[schoolChoice_ID, 4], out school_dex);

                        this.playerVIT += school_vit;
                        this.playerMAN += school_man;
                        this.playerSTR += school_str;
                        this.playerDEX += school_dex;
                        this.playerLUCK = 1;
                        this.playerProgress = 1;

                        this.gameSaveGame();
                        System.Threading.Thread.Sleep(5000);
                    }
                    else
                    {
                        gameSchoolChoiceResult = false;
                        v.write("Neplatná volba, musíš zadat platné číslo školy.");
                    }
                }
                else
                {
                    Random rand = new Random();
                    int r = rand.Next(0, 10);
                    switch(r)
                    {
                        case 7:
                            playerSay("Tvůj budoucí učitel", "Ne, nemůžeš jít na obchodní školu, budeš zaklínač!", ConsoleColor.Yellow);
                            break;
                        default:
                            v.write("Neplatná volba, musíš zadat číslo školy.");
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// countPlayerLevel() - Vypočítání hráčova levelu
        /// </summary>
        /// <returns></returns>
        private int countPlayerLevel()
        {
            int level = 0;
            level = (this.playerXP / 10) + 1;
            return level;
        }

        /// <summary>
        /// playerShowCharacter() - Zobrazení hráčova charakteru + informací o charakteru
        /// </summary>
        private void playerShowCharacter()
        {
            playerInPlayerStats = true;
            while (playerInPlayerStats == true)
            {
                v.clear();

                string layout = gameModels.v("2_cols_layout");
                layout = layout.Replace("@@top_info_01@@", ">");
                layout = layout.Replace("@@top_info_02@@", "Informace o postavě \"" + this.playerName + "\"");
                layout = gameModels.addmodelintolayout("left_model_", "player", layout);
                layout = gameModels.addmodelintolayout("game_info_", "character_info", layout);
                layout = layout.Replace("@@character_info@@", "  Statistiky    ");
                layout = layout.Replace("@@stat_1@@", " VIT: " + this.playerVIT + "\t=> " + this.stats_getPlayerVIT());
                layout = layout.Replace("@@stat_2@@", " MAN: " + this.playerMAN + "\t=> " + this.stats_getPlayerMAN());
                layout = layout.Replace("@@stat_3@@", " STR: " + this.playerSTR + "\t=> " + this.stats_getPlayerSTR());
                layout = layout.Replace("@@stat_4@@", " DEX: " + this.playerDEX + "\t=> " + this.stats_getPlayerDEX());
                layout = layout.Replace("@@stat_5@@", " LUCK: " + this.playerLUCK + "\t=> " + this.stats_getPlayerLUCK());
                layout = layout.Replace("@@level@@", " LVL: " + this.countPlayerLevel());
                layout = layout.Replace("@@xp@@", " XP: " + this.playerXP);
                layout = layout.Replace("@@kills@@", " Zabití: " + this.playerKills);
                layout = layout.Replace("@@deaths@@", " Úmrtí: " + this.playerDeaths);
                v.write(layout);

                if (playerEquip != -1)
                {
                    v.enter(1);
                    v.cwrite("> Výzbroj", ConsoleColor.Green, ConsoleColor.Black);
                    v.enter(1);
                    v.write("> " + item.getItemName(playerEquip) + " " + item.getItemStatsAsString(playerEquip));
                }
                v.enter(1);
                v.cwrite("Pro zavření informací zmáčkni klávesu 'X'.", ConsoleColor.Green, ConsoleColor.Black);

                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.X)
                {
                    v.write("Zavírám informace o postavě..");
                    v.clear();
                    playerInPlayerStats = false;
                }
            }
            
        }

        /// <summary>
        /// setPlayerLocation(int location_id) - Teleportace hráče do jiné oblasti
        /// </summary>
        /// <param name="location_id">Unikátní identifikátor oblasti</param>
        public void setPlayerLocation(int location_id)
        {
            if (location.isLocationExists(location_id) == true) this.playerLocation = location_id;
            else this.playerLocation = 0;
        }

        /* Count Player Stats */
        /// <summary>
        /// stats_getPlayerVIT() - Vypočítání hráčovi celkové VIT
        /// </summary>
        /// <returns>VIT</returns>
        private int stats_getPlayerVIT()
        {
            return this.playerVIT + ((this.playerEquip != -1) ? item.getItemStat(this.playerEquip, 3) : 0) + ((this.playerEquip != -1) ? item.getItemStat(this.playerEquip, 4) : 0);
        }

        /// <summary>
        /// stats_getPlayerMAN() - Vypočítání hráčovi celkové MAN
        /// </summary>
        /// <returns>MAN</returns>
        private int stats_getPlayerMAN()
        {
            return this.playerMAN + ((this.playerEquip != -1) ? item.getItemStat(this.playerEquip, 5) : 0);
        }

        /// <summary>
        /// stats_getPlayerSTR() - Vypočítání hráčovi celkové STR
        /// </summary>
        /// <returns>STR</returns>
        private int stats_getPlayerSTR()
        {
            return this.playerSTR + ((this.playerEquip != -1) ? item.getItemStat(this.playerEquip, 2) : 0);
        }

        /// <summary>
        /// stats_getPlayerDEX() - Vypočítání hráčovi celkové DEX
        /// </summary>
        /// <returns>DEX</returns>
        private int stats_getPlayerDEX()
        {
            return this.playerDEX;
        }

        /// <summary>
        /// stats_getPlayerLUCK() - Vypočítání hráčovo celkového LUCKu
        /// </summary>
        /// <returns>LUCK</returns>
        private int stats_getPlayerLUCK()
        {
            return this.playerLUCK + (this.countPlayerLevel() + this.playerKills / ((this.playerDeaths > 0) ? this.playerDeaths : 1));
        }

        /// <summary>
        /// playerShowInventory() - Zobrazení hráčova inventářu
        /// </summary>
        private void playerShowInventory()
        {
            playerInInventory = true;
            while (playerInInventory == true)
            {
                v.clear();
                v.cwrite("> Inventář", ConsoleColor.Green, ConsoleColor.Black);
                v.enter(1);

                if (playerInventory.Count > 0)
                {
                    for (int i = 0; i < playerInventory.Count; i++)
                    {
                        v.write("<" + (i + 1) + "> " + item.getItemName(playerInventory[i]) + " " + item.getItemStatsAsString(playerInventory[i]));
                    }
                }
                else v.write("Tvůj inventář je prázdný.");

                v.enter(1);
                if (playerInventory.Count > 0) v.cwrite("Pro vybavení zbraně zmáčni klávesu 'E.'", ConsoleColor.Green, ConsoleColor.Black);
                v.cwrite("Pro zavření inventáře zmáčkni klávesu 'X'.", ConsoleColor.Green, ConsoleColor.Black);

                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.E && playerInventory.Count > 0)
                {
                    bool gameInventoryEquipResult = false;
                    while (gameInventoryEquipResult == false)
                    {
                        v.enter(1);
                        v.cwrite("> Vybavení zbraně (číslo zbraně ze seznamu): ", ConsoleColor.Green, ConsoleColor.Black, false);
                        string equipInput = Console.ReadLine();
                        int equipID = 0;
                        gameInventoryEquipResult = int.TryParse(equipInput, out equipID);
                        if (gameInventoryEquipResult == true)
                        {
                            if (equipID <= this.playerInventory.Count && equipID > 0)
                            {
                                equipID = equipID - 1;
                                if (item.countItems() - 7 == this.playerInventory[equipID])
                                {
                                    this.playerInventory.RemoveAt(equipID);
                                    this.playerVIT++;
                                    v.cwrite("Status VIT vylepšen o jeden dovednostní bod.", ConsoleColor.Yellow, ConsoleColor.Black);
                                }
                                else if (item.countItems() - 6 == this.playerInventory[equipID])
                                {
                                    this.playerInventory.RemoveAt(equipID);
                                    this.playerMAN++;
                                    v.cwrite("Status MAN vylepšen o jeden dovednostní bod.", ConsoleColor.Yellow, ConsoleColor.Black);
                                }
                                else if (item.countItems() - 5 == this.playerInventory[equipID])
                                {
                                    this.playerInventory.RemoveAt(equipID);
                                    this.playerSTR++;
                                    v.cwrite("Status STR vylepšen o jeden dovednostní bod.", ConsoleColor.Yellow, ConsoleColor.Black);
                                }
                                else if (item.countItems() - 4 == this.playerInventory[equipID])
                                {
                                    this.playerInventory.RemoveAt(equipID);
                                    this.playerDEX++;
                                    v.cwrite("Status DEX vylepšen o jeden dovednostní bod.", ConsoleColor.Yellow, ConsoleColor.Black);
                                }
                                else if (item.countItems() - 3 == this.playerInventory[equipID])
                                {
                                    this.playerInventory.RemoveAt(equipID);
                                    this.playerLUCK++;
                                    v.cwrite("Status LUCK vylepšen o jeden dovednostní bod.", ConsoleColor.Yellow, ConsoleColor.Black);
                                }
                                else
                                {
                                    if (this.playerEquip != -1) this.playerInventory.Add(this.playerEquip);
                                    this.playerEquip = this.playerInventory[equipID];
                                    this.playerInventory.RemoveAt(equipID);
                                    v.cwrite("Od teď budeš používat " + item.getItemName(this.playerEquip) + " jako zbraň.", ConsoleColor.Yellow, ConsoleColor.Black);
                                }                           
                                System.Threading.Thread.Sleep(2000);
                            }
                            else
                            {
                                v.cwrite("Tolik zbraní v inventáři nemáš..", ConsoleColor.Red, ConsoleColor.Black);
                                System.Threading.Thread.Sleep(2000);
                            }
                        }
                        else
                        {
                            v.cwrite("Neplatné číslo zbraně..", ConsoleColor.Red, ConsoleColor.Black);
                            System.Threading.Thread.Sleep(2000);
                        }
                    }
                }
                else if (key.Key == ConsoleKey.X)
                {
                    this.gameSaveGame();
                    v.write("Zavírám inventář..");
                    v.clear();
                    playerInInventory = false;
                }
            }
        }

        // In Game Functions
        /// <summary>
        /// playerSay(string player, string say, ConsoleColor text_color = ConsoleColor.White, ConsoleColor bg_color = ConsoleColor.Black, bool line = true) - Vypsání textu pomalu
        /// </summary>
        /// <param name="player">Jméno hráče</param>
        /// <param name="say">Text</param>
        /// <param name="text_color">Barva Textu</param>
        /// <param name="bg_color">Pozadí textu</param>
        /// <param name="line">Zalomení řádku</param>
        private void playerSay(string player, string say, ConsoleColor text_color = ConsoleColor.White, ConsoleColor bg_color = ConsoleColor.Black, bool line = true)
        {
            //old - v.cwrite("<"+ player +">: " + say, text_color, bg_color, line);
            v.cwrite("<" + player + ">: ", text_color, bg_color, false);
            foreach(char c in say)
            {
                v.cwrite(""+ c, text_color, bg_color, false);
                System.Threading.Thread.Sleep(35);
            }
            if (line == true) v.write("\n", false);
        }

        /// <summary>
        /// gameLoadGame(int save_id) - Načtení hry podle ID savu
        /// </summary>
        /// <param name="save_id">Unikátní identifikátor savu</param>
        /// <returns>Úspěšnost načtení</returns>
        public int gameLoadGame(int save_id)
        {
            string saveName = gameData.getGameSaveName(save_id);
            if (gameData.checkFile(saveName))
            {
                string saveData = gameData.readFileAsString(saveName);
                string[] _saveSplit = saveData.Split('#');
                int[] saveVariables = new int[_saveSplit.Length];
                for (int i = 0; i < _saveSplit.Length - 2; i++)
                {
                    bool _result = Int32.TryParse(_saveSplit[i], out saveVariables[i]);
                    if (_result != true) saveVariables[i] = 0; 
                }

                string[] _saveNameSplit = saveName.Split('_');

               /* Load Game Variables */
                playerName          = _saveNameSplit[1];
                playerProgress      = saveVariables[0];
                playerXP            = saveVariables[1];
                playerHealth        = saveVariables[2];
                playerMana          = saveVariables[3];
                playerVIT           = saveVariables[4];
                playerMAN           = saveVariables[5];
                playerSTR           = saveVariables[6];
                playerDEX           = saveVariables[7];
                playerLUCK          = saveVariables[8];
                playerKills         = saveVariables[9];
                playerDeaths        = saveVariables[10];
                playerLocation      = saveVariables[11];

                /* Load Inventory */
                if (_saveSplit[12].Length > 0)
                {
                    string _tempItemString = _saveSplit[12];
                    if(_tempItemString.Contains('%'))
                    {
                        string[] _items = _tempItemString.Split('%');
                        foreach (string _item in _items)
                        {
                            int _item_id = 0;
                            bool _result = Int32.TryParse(_item, out _item_id);
                            if (_result == true) this.playerInventory.Add(_item_id);
                        }
                    }
                    else
                    {
                        int _item_id = 0;
                        bool _result = Int32.TryParse(_tempItemString, out _item_id);
                        if (_result == true) this.playerInventory.Add(_item_id);
                    }
                    
                }

                /* Load Equip */
                if (_saveSplit[13].Length > 0)
                {
                    string _tempEquipString = _saveSplit[13];
                    int _item_id = 0;
                    bool _result = Int32.TryParse(_tempEquipString, out _item_id);
                    if (_result == true) playerEquip = _item_id;
                    else playerEquip = -1;
                }
                return 1;
            }
            else return 0;
        }

        /// <summary>
        /// gameSaveGame() - Uložení hry
        /// </summary>
        private void gameSaveGame()
        {
            gameData.checkGameFiles();
            string saveName = gameData.gameSaves + "save_" + this.playerName + "_" + DateTime.Now.ToString("yyyy-MM-ddThh-mm-ss") + ".w3csave";
            v.cwrite("Hra byla uložena..", ConsoleColor.Magenta, ConsoleColor.Black);
            if (gameData.checkFile(saveName)) gameData.deleteFile(saveName);
            gameData.createFile(saveName);

            List<string> saveData = new List<string>();

            saveData.Add("" + this.playerProgress);
            saveData.Add("" + this.playerXP);
            saveData.Add("" + this.playerHealth);
            saveData.Add("" + this.playerMana);
            saveData.Add("" + this.playerVIT);
            saveData.Add("" + this.playerMAN);
            saveData.Add("" + this.playerSTR);
            saveData.Add("" + this.playerDEX);
            saveData.Add("" + this.playerLUCK);
            saveData.Add("" + this.playerKills);
            saveData.Add("" + this.playerDeaths);
            saveData.Add("" + this.playerLocation);

            saveData.Add("" + String.Join("%", this.playerInventory.ToArray()));
            saveData.Add("" + this.playerEquip);

            gameData.writeFile(saveName, String.Join("#", saveData.ToArray()));
        }

        // Out Game Functions

        /// <summary>
        /// isInGame() - Zjištění zda-li je spuštěna hra
        /// </summary>
        /// <returns>true/false</returns>
        public bool isInGame()
        {
            if (ingame == true) return true;
            else return false;
        }

        /// <summary>
        /// isPlayerInGame() - Zjištění zda-li je hráč ve hře
        /// </summary>
        /// <returns>true/false</returns>
        public bool isPlayerInGame()
        {
            if (playerInGame == true) return true;
            else return false;
        }

        /// <summary>
        /// getPlayerData() - Zjištění informací o hráči
        /// </summary>
        /// <returns>Pole hráčových informací</returns>
        public string[] getPlayerData()
        {
            List<string> _returnData = new List<string>();

            _returnData.Add(this.playerName);
            _returnData.Add("" + this.stats_getPlayerVIT());
            _returnData.Add("" + this.stats_getPlayerMAN());
            _returnData.Add("" + this.stats_getPlayerSTR());
            _returnData.Add("" + this.stats_getPlayerDEX());
            _returnData.Add("" + this.stats_getPlayerLUCK());

            return _returnData.ToArray();
        }

        /// <summary>
        /// getPlayerName() - Zjištění hráčova jména
        /// </summary>
        /// <returns>Hráčovo jméno</returns>
        public string getPlayerName()
        {
            return this.playerName;
        }
    }
}