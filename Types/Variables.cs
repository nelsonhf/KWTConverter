using System;
using System.Collections.Generic;

namespace TestComplete
{
    namespace Variables
    {
        public enum VarTypes
        {
            String,
            Table,
            Integer,
            Double,
            Array,
            LastResult,
            Expression,
            Variable,
            Boolean,
            Parameter,
            OnScreenObject,
            TableData,
            ObjectProperty, // Process property, process method, or name mapping object
            Filename,
        };

        public class Variable
        {
            // Attribute of <Parameter>, usually when attribute ParamType (a Guid) is present
            public enum VariableType
            {
                String = 4,
            };

            // Attribute of <Parameter>, usually when attributes ValueType and ValueValue are present
            public enum VarType
            {
                Unknown = 0,
                Integer = 3,
                Double = 5,
                String = 8,
                Boolean = 11,
            };

            // Attribute of <Parameter>
            public enum DefVarType
            {
                Unknown = 0,
            };

            // Attribute of <Parameter>, usually when attributes VarType and ValueValue are present
            public enum ValueType
            {
                Null = 0,
                Integer = 1,
                String = 6,
                Boolean = 7,
                UnicodeString = 9,
            };

             // Attribute of <Parameter>, usually when attribute DefValueValue is present
           public enum DefValueType
            {
                Unknown = 0,
                Integer = 1,
                Double = 3,
                String = 6,
                Boolean = 7,
            };

            public static readonly Dictionary<Guid, VarTypes> TypeFromGuid = new Dictionary<Guid, VarTypes>
            {
                { new Guid("{123F0C0F-44B4-4BAF-B0E6-F3F89FD873B5}"), VarTypes.String },
                { new Guid("{4AD4881B-176C-4C96-8288-FBBB7E3D1FE3}"), VarTypes.Table },
                { new Guid("{88422C25-DDF4-4EA1-B7CC-95779A023F5D}"), VarTypes.Integer },
                { new Guid("{8562FD50-0B6E-489C-95A2-9C144116BD78}"), VarTypes.Double },
                { new Guid("{F38B9AD1-7B22-410F-95FC-6D9420FDE947}"), VarTypes.Array },
                { new Guid("{D44DB91E-FD74-4F67-BE3D-951A1238A9AD}"), VarTypes.LastResult },
                { new Guid("{2B146992-A675-4CF3-9B71-7EE4A1CA11B2}"), VarTypes.Expression },
                { new Guid("{83D6F80C-4323-4034-92E3-FD2A65D2E6FC}"), VarTypes.Variable },
                { new Guid("{91E40FC5-34AB-4B37-B86A-789030413699}"), VarTypes.Parameter },
                { new Guid("{FC0DF733-E97F-46DD-8307-EA4DFF891298}"), VarTypes.OnScreenObject },
                { new Guid("{874735FC-322E-4380-A0DD-AB0206EE8AA0}"), VarTypes.TableData },
                { new Guid("{5EF65F78-D6E6-4C84-90CA-F464F32D7A1D}"), VarTypes.ObjectProperty },
                { new Guid(), VarTypes.Filename }
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