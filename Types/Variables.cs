using System;
using System.Collections.Generic;

namespace TestComplete
{
    namespace Variables
    {
        /// <summary>
        /// Types of variable supported by TestComplete
        /// </summary>
        public enum VarTypes
        {
            Array,          // Array of objects (may have different types, e.g. integer and string)
            Boolean,
            Double,
            Expression,     // A string representing a calculable expression (e.g. "1 + 2")
            Filename,
            Integer,
            LastResult,     // Result of last operation. May be any type or even null
            ObjectProperty, // Process property, process method, or name mapping object
            OnScreenObject, // An object on the scren, e.g. a button
            Parameter,      // Parameter for the current keyword test
            String,
            Table,          // A table (may have multiple dimension but typically has 2; see array for one dimensional tables)
            TableData,      // One element from a table (e.g. table[0][0]), index may be integer or string
            Variable,       // Variable used in the current keyword test
        };

        /// <summary>
        /// Attribute of <Parameter>, usually when attribute ParamType (a Guid) is present
        /// </summary>
        public enum VariableType
        {
            String = 4,
        };

        /// <summary>
        /// Attribute of <Parameter>, usually when attributes ValueType and ValueValue are present
        /// </summary>
        public enum VarType
        {
            Unknown = 0,
            Integer = 3,
            Double = 5,
            String = 8,
            Boolean = 11,
        };

        /// <summary>
        /// Attribute of <Parameter>
        /// </summary>
        public enum DefVarType
        {
            Unknown = 0,
        };

        /// <summary>
        /// Attribute of <Parameter>, usually when attributes VarType and ValueValue are present
        /// </summary>
        public enum ValueType
        {
            Null = 0,
            Integer = 1,
            String = 6,
            Boolean = 7,
            UnicodeString = 9,
        };

        /// <summary>
        /// Attribute of <Parameter>, usually when attribute DefValueValue is present
        /// </summary>
        public enum DefValueType
        {
            Unknown = 0,
            Integer = 1,
            Double = 3,
            String = 6,
            Boolean = 7,
        };

        /// <summary>
        /// Each variable used in a keyword test. The types defined here are also used in Parameter
        /// </summary>
        public class Variable
        {
            /// <summary>
            /// Relationship between each variable type and the Guid that represents it.
            /// </summary>
            public static readonly Dictionary<Guid, VarTypes> TypeFromGuid = new Dictionary<Guid, VarTypes>
            {
                { new Guid("{123F0C0F-44B4-4BAF-B0E6-F3F89FD873B5}"), VarTypes.String },
                { new Guid("{2B146992-A675-4CF3-9B71-7EE4A1CA11B2}"), VarTypes.Expression },
                { new Guid("{4AD4881B-176C-4C96-8288-FBBB7E3D1FE3}"), VarTypes.Table },
                { new Guid("{5EF65F78-D6E6-4C84-90CA-F464F32D7A1D}"), VarTypes.ObjectProperty },
                { new Guid("{83D6F80C-4323-4034-92E3-FD2A65D2E6FC}"), VarTypes.Variable },
                { new Guid("{8562FD50-0B6E-489C-95A2-9C144116BD78}"), VarTypes.Double },
                { new Guid("{874735FC-322E-4380-A0DD-AB0206EE8AA0}"), VarTypes.TableData },
                { new Guid("{88422C25-DDF4-4EA1-B7CC-95779A023F5D}"), VarTypes.Integer },
                { new Guid("{91E40FC5-34AB-4B37-B86A-789030413699}"), VarTypes.Parameter },
                { new Guid("{D44DB91E-FD74-4F67-BE3D-951A1238A9AD}"), VarTypes.LastResult },
                { new Guid("{F38B9AD1-7B22-410F-95FC-6D9420FDE947}"), VarTypes.Array },
                { new Guid("{FC0DF733-E97F-46DD-8307-EA4DFF891298}"), VarTypes.OnScreenObject },
                { new Guid(), VarTypes.Filename },
            };

            public static readonly Dictionary<int, VarTypes> TypeFromNumber = new Dictionary<int, VarTypes>
            {
                { (int)VariableType.String, VarTypes.String },
            };

            public string Name { get; private set; }
            public VarTypes Type { get; private set; }
            public string Description { get; private set; }
            public Variable(string name, VarTypes type, string description)
            {
                Name = name;
                Type = type;
                Description = description;
            }
        }
    }
}