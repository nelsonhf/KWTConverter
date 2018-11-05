using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        class FindObject : Operation
        {
            public FindObject(XElement data, XElement children) : base("Find Object", OperTypes.Catch, data, children)
            {
            }
        }
    }
}