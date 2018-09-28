using System.Linq;
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
	            if (int.TryParse(def.Elements().FirstOrDefault()?.ToString(), out var temp))
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
