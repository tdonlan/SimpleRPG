using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Newtonsoft.Json;

namespace SimpleRPG2
{
    public class JsonTest
    {
        public static void CreateJson()
        {
            Ability fireball= AbilityFactory.getFireball();
            string jsonStr= JsonConvert.SerializeObject(fireball);


        }

        public static void ReadJson()
        {
           

            string jsonStr = File.ReadAllText(@"C:\GameDev\Dev\SimpleRPG2\SimpleRPG2\DataFiles\Abilities.json");



            Ability fireball = JsonConvert.DeserializeObject<Ability>(jsonStr);


            
        }
    }
}
