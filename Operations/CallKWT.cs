using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        /// <summary>
        /// Operation Run Keyword Test will run another keyword test, optionally passing some parameters and receiving
        /// its return value in variable LastResult
        /// </summary>
        /// <example>
        ///   &lt;Operation Type="{E8089E1C-53BC-4D9C-A69D-7CDDB49422CB}"&gt;
        ///      &lt;Data Description="Runs a keyword test." TestName="CalledKWT"&gt;
        ///         &lt;Parameters LangId="{90F89436-9452-4F95-9882-6B5210079F13}"&gt;
        ///            &lt;Parameter Name="Param1" DefVarType="8" DefValueType="6" DefValueValue="" VarType="8" ValueType="6" ValueValue="Param1Value" /&gt;
        ///            &lt;Parameter Name="Param2" DefVarType="8" DefValueType="6" DefValueValue="" VarType="8" ValueType="6" ValueValue="Param2Value" /&gt;
        ///         &lt;/Parameters&gt;
        ///      &lt;/Data&gt;
        ///   &lt;/Operation&gt;
        /// </example>
        public class CallKWT : Operation
        {
            public string Name { get; private set; }
            public string Description { get; private set; }

            public CallKWT(XElement data, XElement children) : base("Run KWT", OperTypes.CallKWT, data, children)
            {
                Name = data.Attribute("TestName")?.Value;
                Description = GetDescription();
            }

            public override string Display(int level)
            {
                var result = $"{PaddedOperationName(level)}{Name}";
                result = PadToColumn(result, ParametersColumn);
                result += AllParameters();

                if (Description != null)
                {
                    result = PadToColumn(result, CommentColumn) + Description;
                }

                result += "\n";

                foreach (var c in Children)
                {
                    result += c.Display(level + 1);
                }

                return result;
            }
        }
    }
}