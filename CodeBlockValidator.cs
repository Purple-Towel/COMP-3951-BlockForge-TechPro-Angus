using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// BlockForge CodeBlockValidator 
/// Author: Angus Grewal
/// Date: Apr 14 2026
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
        public static List<string> Validate(List<CodeBlock> blocks)
        {
            List<string> errors = new List<string>();
            
            if (!HasBlocks(blocks))
            {
                errors.Add("Need at least 1 block.");
                return errors;
            }

            List<CodeBlock> blocksOrdered = blocks.OrderBy(block => block.Sequence).ToList();

            if (!FirstIsStart(blocksOrdered))
            {
                errors.Add("First block must be Start block.");
            }

            if (!LastIsEnd(blocksOrdered))
            {
                errors.Add("Last block must be End block.");
            }
           
            if (!OnlyOneStart(blocksOrdered))
            {
                errors.Add("Only one Start block in a project.");
            }

            if (!OnlyOneEnd(blocksOrdered))
            {
                errors.Add("Only one End block in a project.");
            }

            if (!NoBlocksAfterEnd(blocksOrdered))
            {
                errors.Add("End block cannot have blocks after it.");
            }

            if (!IfFollowedByThen(blocksOrdered))
            {
                errors.Add("An If block must be followed by a Then block.");
            }

            if (!ThenFollowsIf(blocksOrdered))
            {
                errors.Add("A Then block must follow an If block.");
            }

            if (!ElseFollowsThen(blocksOrdered))
            {
                errors.Add("An Else block must follow a Then block.");
            }

            if (!AllBlocksHaveUid(blocksOrdered))
            {
                errors.Add("Not all blocks have a valid UID.");
            }

            if (!UniqueUids(blocksOrdered))
            {
                errors.Add("Block UIDs are not unique.");
            }

            if (!UniqueSequences(blocksOrdered))
            {
                errors.Add("Block Sequence numbers are not unique.");
            }

            if (!RequiredTextBlocksHaveText(blocksOrdered))
            {
                errors.Add("A block requires additional text.");
            }

            if (!NoUnknownBlocks(blocksOrdered))
            {
                errors.Add("An Unknown block type is present.");
            }

            return errors;
        }

        private static bool HasBlocks(List<CodeBlock> blocks)
        {
            return blocks != null && blocks.Count > 0;
        }

        private static bool FirstIsStart(List<CodeBlock> blocks)
        {
            return (blocks.Count > 0 && blocks.First().BlockType == CodeBlockType.Start);
        }

        private static bool LastIsEnd(List<CodeBlock> blocks)
        {
            return (blocks.Count > 0 && blocks.Last().BlockType == CodeBlockType.End);
        }

        private static Func<CodeBlock, bool> IsType(CodeBlockType type)
        {
            return block => block.BlockType == type;
        }

        private static bool OnlyOneStart(List<CodeBlock> blocks)
        {
            return blocks.Count(IsType(CodeBlockType.Start)) == 1;
        }
        
        private static bool OnlyOneEnd(List<CodeBlock> blocks)
        {
            return blocks.Count(IsType(CodeBlockType.End)) == 1;
        }

        private static bool NoBlocksAfterEnd(List<CodeBlock> blocks)
        {
            bool foundEnd = false;

            foreach (CodeBlock block in blocks)
            {
                if (block.BlockType == CodeBlockType.End)
                {
                    foundEnd = true;
                }
                else if (foundEnd)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IfFollowedByThen(List<CodeBlock> blocks)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i].BlockType == CodeBlockType.If)
                {
                    if (i + 1 >= blocks.Count || blocks[i + 1].BlockType != CodeBlockType.Then)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool ThenFollowsIf(List<CodeBlock> blocks)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i].BlockType == CodeBlockType.Then)
                {
                    if (i == 0 || blocks[i - 1].BlockType != CodeBlockType.If)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool ElseFollowsThen(List<CodeBlock> blocks)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i].BlockType == CodeBlockType.Else)
                {
                    if (i == 0 || blocks[i - 1].BlockType != CodeBlockType.Then)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool AllBlocksHaveUid(List<CodeBlock> blocks)
        {
            foreach (CodeBlock block in blocks)
            {
                if (string.IsNullOrWhiteSpace(block.Uid))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool UniqueUids(List<CodeBlock> blocks)
        {
            HashSet<String> encountered = new HashSet<String>();

            foreach (CodeBlock block in blocks)
            {
                if (!encountered.Add(block.Uid))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool UniqueSequences(List<CodeBlock> blocks)
        {
            HashSet<int> encountered = new HashSet<int>();

            foreach (CodeBlock block in blocks)
            {
                if (!encountered.Add(block.Sequence))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool HasText(CodeBlock block)
        {
            return !string.IsNullOrWhiteSpace(block.BlockName);
        }

        private static readonly HashSet<CodeBlockType> TextRequiredTypes =
            new HashSet<CodeBlockType>
            {
                CodeBlockType.If,
                CodeBlockType.Do,
                CodeBlockType.Then,
                CodeBlockType.Else
            };

        private static bool RequiredTextBlocksHaveText(List<CodeBlock> blocks)
        {
            foreach (CodeBlock block in blocks)
            {
                if (TextRequiredTypes.Contains(block.BlockType) && !HasText(block))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool NoUnknownBlocks(List<CodeBlock> blocks)
        {
            foreach (CodeBlock block in blocks)
            {
                if (block.BlockType == CodeBlockType.Unknown)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
