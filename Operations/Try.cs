using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        public class Try : Operation
        {
            public Try(XElement data, XElement children) : base("Try", OperTypes.Try, data, children)
            {
            }
        }
    }
}
