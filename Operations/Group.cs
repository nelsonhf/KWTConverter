using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        public class Group : Operation
        {
            public string Name { get; private set; }
            public Group(XElement data, XElement children) : base("Group", OperTypes.Group, data, children)
            {
                Name = data.Attribute("GroupName").Value;
            }

            public override string Display(int level)
            {
                // Group is different from all other operations in that its name is
                // not displayed; the group name is displayed, instead.
                var result = $"{Indent(level)}{Name}\n";
                foreach (var c in Children)
                {
                    result += c.Display(level + 1);
                }

                return result;
            }
        }
    }
}
