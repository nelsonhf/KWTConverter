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
        public class Catch : Operation
        {
            public Catch(XElement data, XElement children) : base("Catch", OperTypes.Catch, data, children)
            {
            }
        }
    }
}
