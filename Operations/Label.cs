using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        class Label : Operation
        {
            public Label(XElement data, XElement children) : base("Label", OperTypes.Catch, data, children)
            {
            }
        }
    }
}