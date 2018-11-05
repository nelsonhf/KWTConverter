using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using TestComplete.Operations;
using TestComplete.NamedData;

namespace TestComplete
{
    public class Program
    {
        private const string MUsage =  "Usage:\n" +
                                        "KWTConverter <filename>\n" +
                                        "   where filename is the filename to convert, e.g. test.tcKDTest\n";
        private static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine(MUsage);
                return;
            }

            var input = args[0];
            if (!File.Exists(input))
            {
                Console.WriteLine("Filename: {0} does not exist or cannot be accessed.", input);
                return;
            }

            var output = Path.ChangeExtension(input, "out");
            File.Create(output).Close();
            var streamWriter = new StreamWriter(output);

            var doc = XDocument.Load(input);
            // Using Null-conditional Operators (oh we fancy)
            var operations = doc.Element("Root")?.Element("TestData")?.Element("Children")?.Elements() ?? new List<XElement>();
            var variables = doc.Element("Root")?.Element("Variables")?.Element("Items")?.Elements() ?? new List<XElement>();
            var parameters = doc.Element("Root")?.Element("Parameters")?.Elements() ?? new List<XElement>();

            var allVars = new List<Variable>();
            foreach (var v in variables)
            {
                allVars.Add(NamedData.Factory.BuildVariable(v));
            }

            var allParms = new List<Parameter>();
            foreach (var p in parameters)
            {
                allParms.Add(NamedData.Factory.BuildParameter(p));
            }

            var program = new List<Operation>();
            string prog = string.Empty;
            foreach (var o in operations)
            {
                var operation = Operations.Factory.BuildOperation(o);
                program.Add(operation);
                prog += operation.Display(0);
            }

            var functionName = Path.GetFileNameWithoutExtension(input);
            var functionDescription = doc.Element("Root")?.Element("TestData")?.Attribute("Description")?.Value;

            Console.WriteLine("/*");
            Console.WriteLine("This block contains data embedded in the Keyword Test file");
            Console.WriteLine($"Function: {functionName}");

            if (!string.IsNullOrWhiteSpace(functionDescription))
            {
                Console.WriteLine(functionDescription);
            }

            if (allParms.Count != 0)
            {
                Console.WriteLine();
                Console.WriteLine("Parameters:");
                foreach (var par in allParms)
                {
                    Console.Write($" {par.Name} - [{Variable.VarTypeFromNumber[par.VarType ?? (int)VarType.Unknown]}]:");
                    Console.Write(par.Optional ? $" (optional; default = {par.DefaultValue})" : "");
                    Console.WriteLine($" {par.Description}");
                }
            }

            if (allVars.Count != 0)
            {
                Console.WriteLine();
                Console.WriteLine("Variables:");
                foreach (var v in allVars)
                {
                    Console.WriteLine($" {v.Name} - [{v.Type}]: {v.Description}");
                    if (v is Table)
                    {
                        foreach (var line in ((Table)v).Data)
                        {
                            Console.WriteLine($"    |{string.Join("|", line)}|");
                        }
                    }
                }
            }

            Console.WriteLine("*/");
            Console.WriteLine();

            Console.WriteLine(prog);
            streamWriter.WriteLine(prog);
            streamWriter.Flush();
        }
    }
}
