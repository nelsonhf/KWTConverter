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
            Array,
            LastResult,
            Expression,
            Variable,
            Boolean,
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
            };

            // Attribute of <Parameter>
            public enum DefVarType
            {
                Unknown = 0,
            };

            // Attribute of <Parameter>, usually when attributes VarType and ValueValue are present
            public enum ValueType
            {
                Integer = 1,
                String = 6,
                Boolean = 7,
            };

             // Attribute of <Parameter>, usually when attribute DefValueValue is present
           public enum DefValueType
            {
                Unknown = 0,
            };

            public static readonly Dictionary<Guid, VarTypes> TypeFromGuid = new Dictionary<Guid, VarTypes>
            {
                { new Guid("{123F0C0F-44B4-4BAF-B0E6-F3F89FD873B5}"), VarTypes.String },
                { new Guid("{4AD4881B-176C-4C96-8288-FBBB7E3D1FE3}"), VarTypes.Table },
                { new Guid("{88422C25-DDF4-4EA1-B7CC-95779A023F5D}"), VarTypes.Integer },
                { new Guid("{F38B9AD1-7B22-410F-95FC-6D9420FDE947}"), VarTypes.Array },
                { new Guid("{D44DB91E-FD74-4F67-BE3D-951A1238A9AD}"), VarTypes.LastResult },
                { new Guid("{2B146992-A675-4CF3-9B71-7EE4A1CA11B2}"), VarTypes.Expression },
                { new Guid("{83D6F80C-4323-4034-92E3-FD2A65D2E6FC}"), VarTypes.Variable },
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