using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragons_Game.Game.Locations
{
    class locationcontroller
    {
        /// <summary>
        /// Inicializace herních dat 
        /// </summary>
        GameData.data gameData = new GameData.data();

        private List<string> locationsData = new List<string>();

        /// <summary>
        /// locationcontroller() - Inicializace herních dat do listu s lokacema
        /// </summary>
        public locationcontroller()
        {
            locationsData = gameData.getLocationsData().ToList();
        }

        /// <summary>
        /// getLocationName(int location_id) - Zjištění jména lokace
        /// </summary>
        /// <param name="location_id">Unikátní identifikátor lokace</param>
        /// <returns>jméno lokace</returns>
        public string getLocationName(int location_id)
        {            
            if (locationsData[location_id].Length > 0)
            {
                string[] _returnData = locationsData[location_id].Split('#');
                return _returnData[1];
            }
            else return null;
        }

        /// <summary>
        /// isLocationExists(int location_id) - Zjištění existence lokace
        /// </summary>
        /// <param name="location_id">Unikátní identifikátor lokace</param>
        /// <returns>Existence true/false</returns>
        public bool isLocationExists(int location_id)
        {
            if (locationsData[location_id].Length > 0) return true;
            else return false;
        }
    }
}
