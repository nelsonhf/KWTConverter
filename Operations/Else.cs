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