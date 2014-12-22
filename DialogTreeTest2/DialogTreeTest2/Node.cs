using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DialogTreeTest2
{

    public class ResponseData
    {
        public string Response;
        public int Node;

        public ResponseData(string Response, int node)
        {
            this.Response = Response;
            this.Node = node;
        }
    }

    public class Node
    {
        public string displayText;
        public List<ResponseData> responseList;

        public Node(string displayText, List<ResponseData> responseList)
        {
            this.displayText = displayText;
            this.responseList = responseList;
        }

        public override string ToString()
        {
            string retval = "";
            retval += displayText + "\r\n";
            int counter = 1;
            foreach(ResponseData rd in responseList)
            {
                retval += counter + ". " + rd.Response + "(" + rd.Node + ")" + "\r\n"; 
                counter++;
            }
            return retval;

        }

        //Returns the display Node of the response sent
        //Return -2 if the rsp is out of range, or there is no response
        public int CheckResponse(int rsp)
        {
            
            if (rsp <= responseList.Count && rsp > 0)
            {
                return responseList[rsp - 1].Node;
            }
            else
            {
                return -2;
            }
        }

    }
}
