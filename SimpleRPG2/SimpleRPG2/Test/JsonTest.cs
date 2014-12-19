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
            /*
            Ability fireball= AbilityFactory.getFireball();
            string jsonStr= JsonConvert.SerializeObject(fireball);
            */
            WriteJson();

        }

        public static void WriteJson()
        {
            Dictionary<string, string> testDict = new Dictionary<string, string>();
            testDict.Add("Foo", "bar");
            testDict.Add("Hello", "World");
            testDict.Add("Number","123123");

            string jsonStr = JsonConvert.SerializeObject(testDict);

            Dictionary<string, string> dict2 = new Dictionary<string, string>();
            dict2 = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonStr);
                
        }

        public static void ReadJson()
        {
           

            string jsonStr = File.ReadAllText(@"DataFiles\Abilities.json");



            Ability fireball = JsonConvert.DeserializeObject<Ability>(jsonStr);


            
        }
    }
}
