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
