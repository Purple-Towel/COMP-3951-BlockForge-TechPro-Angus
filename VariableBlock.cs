using System;

namespace COMP_3951_BlockForge_TechPro
{
    /// <summary>
    /// Stores a variable block's name, type, and strongly typed value.
    /// </summary>
    public class VariableBlock
    {
        public string VariableName { get; set; }
        public VariableBlockType VariableType { get; set; }
        public string? StringValue { get; set; }
        public int? IntValue { get; set; }
        public bool? BoolValue { get; set; }

        public object Value =>
            VariableType switch
            {
                VariableBlockType.String => StringValue ?? string.Empty,
                VariableBlockType.Int => IntValue ?? 0,
                VariableBlockType.Bool => BoolValue ?? false,
                _ => throw new InvalidOperationException("Unsupported variable type.")
            };

        public VariableBlock()
        {
            VariableName = string.Empty;
            VariableType = VariableBlockType.String;
            StringValue = string.Empty;
        }

        private VariableBlock(string variableName, VariableBlockType variableType)
        {
            if (string.IsNullOrWhiteSpace(variableName))
            {
                throw new ArgumentException("Variable name cannot be empty.", nameof(variableName));
            }

            VariableName = variableName;
            VariableType = variableType;
        }

        public static VariableBlock CreateString(string variableName, string value = "")
        {
            return new VariableBlock(variableName, VariableBlockType.String)
            {
                StringValue = value
            };
        }

        public static VariableBlock CreateInt(string variableName, int value = 0)
        {
            return new VariableBlock(variableName, VariableBlockType.Int)
            {
                IntValue = value
            };
        }

        public static VariableBlock CreateBool(string variableName, bool value = false)
        {
            return new VariableBlock(variableName, VariableBlockType.Bool)
            {
                BoolValue = value
            };
        }

        public void UpdateStringValue(string value)
        {
            EnsureType(VariableBlockType.String);
            StringValue = value;
        }

        public void UpdateIntValue(int value)
        {
            EnsureType(VariableBlockType.Int);
            IntValue = value;
        }

        public void UpdateBoolValue(bool value)
        {
            EnsureType(VariableBlockType.Bool);
            BoolValue = value;
        }

        private void EnsureType(VariableBlockType expectedType)
        {
            if (VariableType != expectedType)
            {
                throw new InvalidOperationException($"Variable block type is {VariableType} and cannot store {expectedType} values.");
            }
        }
    }
}
