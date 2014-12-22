using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentGraph
{
    public class AgentRelationship
    {
        public Agent a;
        public float relationship;
    }

    public class Agent
    {
        public string name;
        public List<AgentRelationship> relationshipList = new List<AgentRelationship>();

        public Agent(string name)
        {
            this.name = name;
        }
    }
}
