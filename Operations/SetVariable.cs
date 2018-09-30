using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TestComplete.Variables;

namespace TestComplete
{
    namespace Operations
    {
        public class SetVariable : Operation
        {
            public string VariableName { get; private set; }
            public VarTypes VariableType { get; private set; }
            public SetVariable(XElement data, XElement children) : base("Set Variable Value", OperTypes.SetVariable, data, children)
            {
                VariableName = data.Attribute("VariableName")?.Value;
                var typenumber = int.Parse(data.Attribute("VariableType").Value);
                VariableType = Variable.TypeFromNumber[typenumber];
            }

            public override string Display(int level)
            {
                var result = $"{PaddedOperationName(level)}{VariableName}";
                result = PadToColumn(result, ParametersColumn);
                result += AllParameters();
                return $"{result}\n";
            }
        }
    }
}
