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
        public class Else : Operation
        {
            public Else(XElement data, XElement children) : base("Else", OperTypes.Try, data, children)
            {
            }
        }
    }
}