using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace TestComplete
{
    namespace Operations
    {
        public class Condition
        {
            public string Value { get; private set; }

            public readonly Dictionary<OperatorType, Type> OperatorMap = new Dictionary<OperatorType, Type>
            {
                { OperatorType.Logical, typeof(LogicalOperator) },
                { OperatorType.Equality, typeof(ComparisonOperator) },
            };

            public enum OperatorType
            {
                Logical = 0,
                Equality = 1,
            };

            public enum LogicalOperator
            {
                And = 0,
                Or = 1,
            };

            public enum ComparisonOperator
            {
                Equals = 0,
                DoesNotEqual = 1,
                GreaterThan = 2,
                GreaterThanOrEqualTo = 3,
                LessThan = 4,
                LessThanOrEqualTo = 5,
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

                int t = int.Parse(branch.Attribute("Type").Value);
                if (Enum.IsDefined(typeof(OperatorType), t))
                {
                    Type type = OperatorMap[(OperatorType)t];
                    oper = Enum.GetName(type, int.Parse(branch.Attribute("Operator").Value));
                }
                else
                {
                    oper = "????";
                }

                return $"{left} {oper} {right}";
            }
        }
    }
}