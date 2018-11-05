using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        class MenuAction : Operation
        {
            public MenuAction(XElement data, XElement children) : base("Menu Action", OperTypes.Catch, data, children)
            {
            }
        }
    }
}