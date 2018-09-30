using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        public class DataDrivenTest : Operation
        {
            public string DataTableName { get; private set; }
            public string DataTableType { get; private set; }
            public Boolean? FromBegin { get; private set; }
            public Boolean? ToEnd { get; private set; }
            public int? First { get; private set; }
            public int? Last { get; private set; }
            public DataDrivenTest(XElement data, XElement children) : base("Data-Driven Loop", OperTypes.DataDrivenTest, data, children)
            {
                int i;
                Boolean b;
                DataTableName = data.Attribute("VariableName")?.Value;
                DataTableType = data.Attribute("VariableType")?.Value;
                var records = data.Element("Records");

                // All these fields are probably mandatory but it never hurts (much) to be careful
                FromBegin = bool.TryParse(records.Attribute("FromBegin")?.Value, out b) ? b : (Boolean?)null;
                ToEnd = bool.TryParse(records.Attribute("ToEnd")?.Value, out b) ? b : (Boolean?)null;
                First = int.TryParse(records.Attribute("StartRecord")?.Value, out i) ? i : (int?)null;
                Last = int.TryParse(records.Attribute("StopIndex")?.Value, out i) ? i : (int?)null;
            }

            public override string Display(int level)
            {
                var result = PaddedOperationName(level);
                result = PadToColumn(result, ParametersColumn) + DataTableName;

                // Consider data not present as false
                if ((FromBegin ?? false) && (ToEnd ?? false))
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