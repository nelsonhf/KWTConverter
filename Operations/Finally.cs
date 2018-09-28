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