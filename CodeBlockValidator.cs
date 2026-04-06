using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// BlockForge CodeBlockValidator 
/// Author: Angus Grewal
/// Date: Mar 4 2026
/// Source: Self-written, with AI coaching. All code submitted is human written, based on ChatGPT guidance.
/// </summary>
namespace COMP_3951_BlockForge_TechPro
{
    /// <summary>
    /// Handles the validation related tasks of CodeBlocks.
    /// For now, it is a very basic implementation but will be expanded to throw custom exceptions instead of returning a list of error message strings.
    /// </summary>
    public class CodeBlockValidator
    {
        public static List<String> Validate(List<CodeBlock> blocks)
        {
            var errors = new List<String>();
            var encountered = new HashSet<String>();

            if (blocks.Count == 0)
            {
                errors.Add("Workspace is empty.");
                return errors;
            }

            foreach (var block in blocks)
            {
                if (string.IsNullOrWhiteSpace(block.Uid))
                {
                    errors.Add("Block UID is missing.");
                    continue;
                }

                if (!encountered.Add(block.Uid))
                {
                    errors.Add($"{block.Uid} is a duplicate.");
                }

                if (block.BlockType == CodeBlockType.Unknown)
                {
                    errors.Add($"Unsupported block type for {block.Uid}.");
                }

                if (block.BlockType == CodeBlockType.Variable)
                {
                    if (!block.VariableType.HasValue)
                    {
                        errors.Add($"Variable block {block.Uid} is missing a VariableType.");
                    }

                    if (string.IsNullOrWhiteSpace(block.BlockName))
                    {
                        errors.Add($"Variable block {block.Uid} is missing a variable name.");
                    }
                }
            }

            return errors;
        }
    }
}
