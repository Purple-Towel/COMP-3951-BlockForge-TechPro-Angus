using System;
using System.Collections.Generic;
using System.Text.Json;

/// <summary>
/// BlockForge CodeBlockSerializer 
/// Author: Angus Grewal
/// Date: Mar 4 2026
/// Source: Self-written, with AI coaching. All code submitted is human written, based on ChatGPT guidance.
/// </summary>
namespace COMP_3951_BlockForge_TechPro
{
    /// <summary>
    /// Handles serialization and deserialization of CodeBlocks.
    /// This API will be combined with a FileHandler in future to handle reading/writing the serialized data from disk storage.
    /// </summary>
    public class CodeBlockSerializer
    {
        /// <summary>
        /// Contains the options for the JsonSerializer
        /// </summary>
        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true
        };

        /// <summary>
        /// Serialize a single CodeBlock.
        /// </summary>
        /// <param name="codeBlock">CodeBlock to serialize</param>
        /// <returns>serialized CodeBlock</returns>
        public static string Serialize(CodeBlock codeBlock)
        {
            return JsonSerializer.Serialize(codeBlock, Options);
        }

        /// <summary>
        /// Serialize a list of CodeBlocks.
        /// </summary>
        /// <param name="blocks">list of CodeBlocks to serialize</param>
        /// <returns>serialized CodeBlocks</returns>
        public static string Serialize(List<CodeBlock> blocks)
        {
            return JsonSerializer.Serialize(blocks, Options);
        }

        /// <summary>
        /// Deserialize JSON data to a single CodeBlock.
        /// </summary>
        /// <param name="json">the JSON data to deserialize</param>
        /// <returns>deserialized CodeBlock</returns>
        /// <exception cref="InvalidOperationException">thrown when JSON data is invalid</exception>
        public static CodeBlock DeserializeSingle(string json)
        {
            var block = JsonSerializer.Deserialize<CodeBlock>(json);

            if (block == null)
            {
                throw new InvalidOperationException("Invalid JSON data for CodeBlock.");
            }

            return block;
        }

        /// <summary>
        /// Deserialize JSON data of multiple CodeBlocks.
        /// </summary>
        /// <param name="json">the JSON data to deserialize</param>
        /// <returns>list of CodeBlocks</returns>
        public static List<CodeBlock> DeserializeList(string json)
        {
            return JsonSerializer.Deserialize<List<CodeBlock>>(json) ?? new List<CodeBlock>();
        }
    }
}
