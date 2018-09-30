using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        public class CheckpointsAssertion : Operation
        {
            public string Library { get; private set; }
            public string Function { get; private set; }
            public CheckpointsAssertion(string moniker, string stringId, XElement data) : base("Assertion Checkpoint", OperTypes.CallLibrary, data, null)
            {
                // These replaces shouldn't be necessary but RegEx (in this version of .NET) seems to be buggy.
                // stringId is "Operation: Assert True\r\nPlug-in: Assertion Checkpoint (Version: 1.0, Video Gaming Technologies, Inc.)"
                stringId = stringId.Replace('\n', '/');
                stringId = stringId.Replace('\r', '/');

                var items = Regex.Match(stringId, @"Operation: ([\w ]+).*Plug-in: (.*)");
                Function = items.Groups[1].Value;
                Library = items.Groups[2].Value;
            }

            public override string Display(int level)
            {
                var result = $"{PaddedOperationName(level)}{Function}";
                result = PadToColumn(result, ParametersColumn);
                result += AllParameters();
                return $"{result}\n";
            }
        }
    }
}