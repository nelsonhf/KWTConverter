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
        public class Finally : Operation
        {
            public Finally(XElement data, XElement children) : base("Finally", OperTypes.Finally, data, children)
            {
            }
        }
    }
}