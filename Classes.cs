using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntryExitCivy
{
    class Civy
    { }


    class Entry
    { }


    class Exit 
    { }


    class Nation
    {
        private string id;
        private string name;      

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Nation(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }

    public enum purpose
    { 
        LEARNING = 1,
        WORKING = 2,
        TRAVEL = 3
    }

}
