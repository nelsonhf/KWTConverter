using System;
using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        public class DataDrivenTest : Operation
        {
            public string DataTableName { get; private set; }
            public string DataTableType { get; private set; }
            public Boolean FromBegin { get; private set; }
            public Boolean ToEnd { get; private set; }
            public int First { get; private set; }
            public int Last { get; private set; }
            public DataDrivenTest(XElement data, XElement children) : base("Data-Driven Loop", OperTypes.DataDrivenTest, data, children)
            {
                DataTableName = data.Attribute("VariableName")?.Value;
                DataTableType = data.Attribute("VariableType")?.Value;
                var records = data.Element("Records");
                FromBegin = bool.Parse(records.Attribute("FromBegin").Value);
                ToEnd = bool.Parse(records.Attribute("ToEnd")?.Value);
                First = int.Parse(records.Attribute("StartRecord")?.Value);
                Last = int.Parse(records.Attribute("StopIndex")?.Value);
            }

            public override string Display(int level)
            {
                var result = PaddedOperationName(level);
                result = PadToColumn(result, ParametersColumn) + DataTableName;
                if (FromBegin && ToEnd)
                {
                    result += "(All records)\n";
                }
                else
                {
                    result += $"(From {First} to {Last})\n";
                }

                foreach (var c in Children)
                {
                    result += c.Display(level + 1);
                }

                return result;
            }
        }
    }
}