using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestComplete
{
    namespace Variables
    {
        public class Integer : Variable
        {
            public int? Default { get; private set; }
            public Integer(string name, VarTypes type, string description, XElement def) : base(name, type, description)
        	{
                int temp;
                if (int.TryParse(def.Elements().FirstOrDefault()?.ToString(), out temp))
                {
                    Default = temp;
                }
                else
                {
                    Default = null;
                }
            }
        }
    }
}
