using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        public class Condition
        {
            public Condition(XElement root)
            {

            }

            public string Display()
            {
                return "left operation right";
            }
        }
    }
}