using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// BlockForge PseudoCodeGenerator 
/// Author: Angus Grewal
/// Date: Apr 7 2026
/// Source: Self-written, with AI coaching. All code submitted is human written, based on ChatGPT guidance.
/// </summary>
namespace COMP_3951_BlockForge_TechPro
{
    /// <summary>
    /// Generates pseudocode from a sequence of blocks supplied.
    /// </summary>
    public class PseudoCodeGenerator
    {
        /// <summary>
        /// Helper method that will translate blocks
        /// </summary>
        /// <param name="block">a CodeBlock to translate into pseudocode.</param>
        /// <returns>plaintext string of pseudocode</returns>
        private List<string> TranslateBlock(CodeBlock block)
        {
            switch (block.BlockType)
            {
                case CodeBlockType.Start:
                    return new List<string> { "BEGIN" };

                case CodeBlockType.End:
                    return new List<string> { "END" };

                case CodeBlockType.Do:
                    return new List<string> { $"DO \"{block.BlockName ?? "[EMPTY]"}\"" };

                case CodeBlockType.If:
                    return new List<string> { $"IF {block.BlockName ?? "[NO CONDITION]"}" };

                case CodeBlockType.Then:
                    return new List<string> 
                    { 
                        "    THEN", 
                        $"        {block.BlockName ?? "[NO THEN BRANCH]"}" 
                    };

                case CodeBlockType.Else:
                    return new List<string> 
                    { 
                        "    ELSE",
                        $"        {block.BlockName ?? "[NO ELSE BRANCH]"}" 
                    };

                default:
                    return new List<string> { $"Unsupported block: {block.BlockType}" };
            }
        }

        /// <summary>
        /// When given a Project containing CodeBlocks, will translate each block into pseudocode.
        /// </summary>
        /// <param name="project">The project to translate</param>
        /// <returns>pseudocode as a string</returns>
        /// <exception cref="ArgumentNullException">thrown when null.</exception>
        public string Generate(Project project)
        {
            if (project == null || project.CodeBlocks == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            var blocks = project.CodeBlocks.OrderBy(b => b.Sequence).ToList();

            List<string> lines = new List<string>();

            foreach (CodeBlock block in blocks)
            {
                lines.AddRange(TranslateBlock(block));
            }

            return string.Join(Environment.NewLine, lines);
        }
    }
}
