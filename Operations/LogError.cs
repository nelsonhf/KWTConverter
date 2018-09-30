using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        public class LogError : Operation
        {
            public string Description { get; private set; }
            public string MessageType { get; private set; }
            public LogError(XElement data, XElement children) : base("Log", OperTypes.LogError, data, children)
            {
                MessageType = data.Attribute("MessageType")?.Value;
                Description = GetDescription();
            }

            public override string Display(int level)
            {
                Parameter description = GetParameter("MessageText");
                Parameter additInfo = GetParameter("AdditionalInformation");
                var result = PaddedOperationName(level);
                result = PadToColumn(result, ParametersColumn);
                result += $"{description}, {additInfo}\n";
                return result;
            }
        }
    }
}