using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace Dragons_Game.GameData
{
    class data
    {
        // Game Dir
        private static string gameDir               = @"\temp\witcher3console\";

        // Game Files
        public string gameItems                     = gameDir + "items.txt";
        public string gameMobs                      = gameDir + "mobs.txt";
        public string gameLocations                 = gameDir + "locations.txt";
        public string gameSaves                     = @gameDir + "saves\\";

        // Check
        private static string checkFileUrl          = "http://test.domm98.cz/witcher3console/";
        private static string checkFileUrlItems     = checkFileUrl + "gameData.php?d=items";
        private static string checkFileUrlMobs      = checkFileUrl + "gameData.php?d=mobs";
        private static string checkFileUrlLocations = checkFileUrl + "gameData.php?d=locations";

        // Saves
        string[] gameSaveNames                      = null;

        // Public Game Dir
        /// <summary>
        /// getGameDirPath() - Zjištění cesty k herní složce
        /// </summary>
        /// <returns>Složka s datama hry</returns>
        public string getGameDirPath() { return gameDir; }

        /* Data Class Functions */
        /// <summary>
        /// Inicializace dat
        /// </summary>
        public data()
        {
            this.checkGameFiles();
            // Data Init
            // -- End of Data Init
        }

        /// <summary>
        /// getGameSaveName(int save_id) - Zjištění názvu herního savu
        /// </summary>
        /// <param name="save_id">Unikátní identifikátor savu</param>
        /// <returns>Název savu</returns>
        public string getGameSaveName(int save_id)
        {
            //string[] _tempSaveNames = Directory.GetFiles(gameSaves, "*.w3csave");
            return gameSaveNames[save_id];
        }

        /// <summary>
        /// getGameSaveNames() - Zjištění všech názvů herních savů
        /// </summary>
        /// <returns>Pole s názvama savů</returns>
        public string[] getGameSaveNames()
        {
            //gameSaveNames = Directory.GetFiles(gameSaves, "*.w3csave");
            return gameSaveNames;
        }

        /// <summary>
        /// getItemsData() - Zjištění všech informacích o herních itemech
        /// </summary>
        /// <returns>Pole s informacemi itemů</returns>
        public string[] getItemsData()
        {
            string _itemsDataString = readFileAsString(gameItems);
            string[] _itemsData = _itemsDataString.Split('|');
            return _itemsData;
        }

        /// <summary>
        /// getMobsData() - Zjištění všech informacích o mobech
        /// </summary>
        /// <returns>Pole s informacemi mobů</returns>
        public string[] getMobsData()
        {
            string _mobsDataString = readFileAsString(gameMobs);
            string[] _mobsData = _mobsDataString.Split('|');
            return _mobsData;
        }

        /// <summary>
        /// getLocationsData() - Zjištění všech informacích o lokacích
        /// </summary>
        /// <returns>Pole s informacemi lokací</returns>
        public string[] getLocationsData()
        {
            string _locationsDataString = readFileAsString(gameLocations);
            string[] _locationsData = _locationsDataString.Split('|');
            return _locationsData;
        }

        /// <summary>
        /// checkGameFiles() - Ověření herních souborů + aktualizace herních dat
        /// </summary>
        public void checkGameFiles()
        {
            // Check Game Directory
            if (this.checkDirectory(gameDir) == false) this.createDirectory(gameDir);

            // Check Game Files
            if (this.checkFile(gameItems) == false)         this.createFile(gameItems);
            if (this.checkFile(gameMobs) == false)          this.createFile(gameMobs);
            if (this.checkFile(gameLocations) == false)     this.createFile(gameLocations);

            // Write Files 
            string _wwwItemsDataString      = readWWWPage(checkFileUrlItems);
            string _wwwMobsDataString       = readWWWPage(checkFileUrlMobs);
            string _wwwLocationsDataString  = readWWWPage(checkFileUrlLocations);

            if (_wwwItemsDataString != "error")
            {
                this.clearFile(gameItems);
                this.writeFile(gameItems, _wwwItemsDataString);
            }

            if (_wwwMobsDataString != "error")
            {
                this.clearFile(gameMobs);
                this.writeFile(gameMobs, _wwwMobsDataString);
            }

            if (_wwwLocationsDataString != "error")
            {
                this.clearFile(gameLocations);
                this.writeFile(gameLocations, _wwwLocationsDataString);
            }

            // Check Game Save Directory
            if (this.checkDirectory(gameSaves) == false) this.createDirectory(gameSaves);

            // Load Saves
            this.gameSaveNames = Directory.GetFiles(gameSaves, "*.w3csave");
        }
        /* -- End of Data Class Functions */

        // Basic dir functions
        /// <summary>
        /// checkDirectory(string path) - Zjištění existence složky
        /// </summary>
        /// <param name="path">Cesta ke složce</param>
        /// <returns>true/false</returns>
        public bool checkDirectory(string path)
        {
            if (Directory.Exists(path)) return true;
            else return false;
        }

        /// <summary>
        /// deleteDirectory(string path) - Smazání složky
        /// </summary>
        /// <param name="path">Cesta ke složce</param>
        public void deleteDirectory(string path)
        {
            if (this.checkDirectory(path)) Directory.Delete(path);
        }

        /// <summary>
        /// createDirectory(string path) - Vytvoření složky
        /// </summary>
        /// <param name="path">Cesta ke složce</param>
        public void createDirectory(string path)
        {
            if (this.checkDirectory(path)) this.deleteDirectory(path);
            Directory.CreateDirectory(path);
        }

        // Basic file functions
        /// <summary>
        /// writeFile(string path, string str) - Zapsání stringu do souboru
        /// </summary>
        /// <param name="path">Cesta k souboru</param>
        /// <param name="str">Zapisovaný string</param>
        public void writeFile(string path, string str)
        {
            if (this.checkFile(path)) this.createFile(path);
            this.clearFile(path);
            File.WriteAllText(path, str);
        }

        /// <summary>
        /// writeRow(string path, int row, string str) - Zapsání do souboru na určitý řádek
        /// </summary>
        /// <param name="path">Cesta k souboru</param>
        /// <param name="row">Řádek</param>
        /// <param name="str">Zapisovaný string</param>
        public void writeRow(string path, int row, string str)
        {
            if (this.checkFile(path)) this.createFile(path);

            List<string> list = new List<string>();
            string[] rows = System.IO.File.ReadAllLines(path);

            this.clearFile(path);

            if (row > rows.Length - 1)
            {
                Array.Resize(ref rows, rows.Length + 1);
                rows[rows.Length - 1] = str;
            }
            list.AddRange(rows);
            if (row <= rows.Length) list.Insert(row, str);
            File.WriteAllLines(path, list.ToArray());
        }

        /// <summary>
        /// readFileAsArray(string path) - Přečtení celého souboru do pole
        /// </summary>
        /// <param name="path">Cesta k souboru</param>
        /// <returns>Pole s řádkami souboru</returns>
        public string[] readFileAsArray(string path)
        {
            string[] lines = null;
            if (this.checkFile(path)) lines = System.IO.File.ReadAllLines(path);
            return lines;
        }

        /// <summary>
        /// readFileAsString(string path) - Přečtení celého souboru jako string 
        /// </summary>
        /// <param name="path">Cesta k souboru</param>
        /// <returns>String s obsahem souboru</returns>
        public string readFileAsString(string path)
        {
            string fileString = null;
            if (this.checkFile(path)) fileString = System.IO.File.ReadAllText(path);
            return fileString;
        }

        /// <summary>
        /// countFileRows(string path) - Vypočítání řádků souboru
        /// </summary>
        /// <param name="path">Cesta k souboru</param>
        /// <returns>Počet řádků</returns>
        public int countFileRows(string path)
        {
            string[] rows = null;
            if (this.checkFile(path)) rows = System.IO.File.ReadAllLines(path);
            return rows.Length - 1;
        }

        /// <summary>
        /// clearFile(string path) - Vyprázdnění souboru
        /// </summary>
        /// <param name="path">Cesta k souboru</param>
        public void clearFile(string path)
        {
            if (this.checkFile(path)) System.IO.File.WriteAllText(path, string.Empty);
        }

        /// <summary>
        /// checkFile(string path) - Zjištění existence souboru
        /// </summary>
        /// <param name="path">Cesta k souboru</param>
        /// <returns>true/false</returns>
        public bool checkFile(string path)
        {
            if (File.Exists(path)) return true;
            else return false;
        }

        /// <summary>
        /// deleteFile(string path) - Smazání souboru
        /// </summary>
        /// <param name="path">Cesta k souboru</param>
        public void deleteFile(string path)
        {
            if (this.checkFile(path)) File.Delete(path);
        }

        /// <summary>
        /// createFile(string path) - Vytvoření souboru 
        /// </summary>
        /// <param name="path">Cesta k souboru</param>
        public void createFile(string path)
        {
            if (this.checkFile(path)) this.deleteFile(path);
            File.Create(path).Close();
        }

        // Other Functions
        /// <summary>
        /// readWWWPage(string url) - Přečtení www stránky
        /// </summary>
        /// <param name="url">Url adresa stránky</param>
        /// <returns>Obsah stránky</returns>
        private string readWWWPage(string url)
        {
            string _wwwString = "error";
            try
            {
                _wwwString = new WebClient().DownloadString(@url);
            }
            catch
            {
            }
            return _wwwString;
        }
    }
}