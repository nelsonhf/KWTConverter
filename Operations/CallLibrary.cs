using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        public class CallLibrary
        {
            public string Moniker { get; private set; }
            public string StringId { get; private set; }

            public static Operation MakeLibrary(XElement operation, XElement data)
            {
                var moniker = operation.Attribute("Moniker").Value;
                var stringId = operation.Attribute("StringID").Value;

                // I only know about this library.
                if (moniker.Contains("CheckpointsAssertion"))
                {
                    return new CheckpointsAssertion(moniker, stringId, data);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}