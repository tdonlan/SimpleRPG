using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DialogTreeTest
{
    public class DialogLoader
    {

        //Error Handling?
        public static DialogTree Load(string path)
        {
            string dialogText = File.ReadAllText(path);

            Dictionary<int, Node> nodeDictionary = new Dictionary<int,Node>();

            string[] lines = dialogText.Split('\n');
           
            foreach (string line in lines)
            {
                Node tempNode;
                string[] nodeSplit = line.Split('|');
                int index = ParseIntString(nodeSplit[1]);
                if (nodeSplit[0] == "D")
                {
                    tempNode = new DisplayNode(index, "Speaker", nodeSplit[2], getResponseNodeArray(nodeSplit[3]));
                }
                else
                {
                    tempNode = new ResponseNode(index, nodeSplit[2], ParseIntString(nodeSplit[3]));
                }
                nodeDictionary.Add(index, tempNode);
            }

            return new DialogTree(nodeDictionary);
        }

        //Error handling?
        private static int[] getResponseNodeArray(string str)
        {
            try
            {
                string[] nodeSplit = str.Split(':');
                int[] retval = new int[nodeSplit.Length];
                for (int i = 0; i < nodeSplit.Length; i++)
                {
                    retval[i] = Int32.Parse(nodeSplit[i]);
                }
                return retval;
            }
            catch (Exception ex)
            {
                int[] retval = new int[1]{-1};
                return retval;
            }
            
        }

        private static int ParseIntString(string s)
        {
            int retval;

            if (Int32.TryParse(s, out retval))
            {
                return retval;
            }
            else
            {
                return -1;
            }
        }




    }
}
