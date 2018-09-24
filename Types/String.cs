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
        public class String : Variable
        {
            public string Default { get; private set; }
            public String(string name, VarTypes type, string description, XElement def) : base(name, type, description)
        	{
                Default = def.Elements().FirstOrDefault()?.Value;
            }

            public override string ToString()
            {
                return base.ToString() + "default: '" + Default + "'";
            }
        }
    }
}
