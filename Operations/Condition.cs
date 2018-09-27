using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        public class Condition
        {
            public string Value { get; private set; }

            public enum OperatorType
            {
                Or = 0,
                Equals = 1,
            };

            public Condition(XElement root)
            {
                Value = WalkTree(root);
            }

            public string Display()
            {
                return Value;
            }

            private string WalkTree(XElement branch)
            {
                string left;
                string right;
                string oper;

                if (branch.Element("Left") != null)
                {
                    left = new Parameter(branch.Element("Left")).ToString();
                }
                else
                {
                    left = "(" + WalkTree(branch.Element("Expression")) + ")";
                }

                if (branch.Element("Right") != null)
                {
                    right = new Parameter(branch.Element("Right")).ToString();
                }
                else
                {
                    right = "(" + WalkTree(branch.Elements("Expression").Skip(1).First()) + ")";
                }

                oper = Enum.GetName(typeof(OperatorType), int.Parse(branch.Attribute("Type").Value)) ?? "????";

                return $"{left} {oper} {right}";
            }
        }
    }
}