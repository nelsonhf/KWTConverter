using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        class ImageBasedAction : Operation
        {
            public ImageBasedAction(XElement data, XElement children) : base("Image Based Action", OperTypes.Catch, data, children)
            {
            }
        }
    }
}