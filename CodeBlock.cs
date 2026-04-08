using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// BlockForge CodeBlock 
/// Author: Angus Grewal
/// Date: Apr 7 2026
/// Source: Self-written, with AI coaching. All code submitted is human written, based on ChatGPT guidance.
/// </summary>
namespace COMP_3951_BlockForge_TechPro
{
    /// <summary>
    /// Data Transfer Object that represents a "Block" from the UI with starting coordinates and a UID.
    /// Will expend to include different subtypes, inbound and outbound links to other code blocks and specific internal data such as basic primitives.
    /// </summary>
    public class CodeBlock
    {
        /// <summary>
        /// Gets or sets the horizontal pixel position for the block.
        /// </summary>
        public double PosX { get; set; }

        /// <summary>
        /// Gets or sets the vertical pixel position for the block.
        /// </summary>
        public double PosY { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the block.
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// Gets or sets the sequence number for execution order.
        /// </summary>
        public int Sequence { get; set; }

        public CodeBlockType BlockType { get; set; }
        public string? BlockName { get; set; }

        /// <summary>
        /// Constructor for a CodeBlock.
        /// </summary>
        /// <param name="posX">starting X coordinate to place from.</param>
        /// <param name="posY">starting Y coordinate to place from.</param>
        /// <param name="uid">unique identifier for an instance of CodeBlock. Uniqueness will be enforced elsewhere, for now the CodeBlockValidator will log duplicates.</param>
        /// <param name="sequence">The sequence number that will determine liner execution (from 0 to n).</param>
        public CodeBlock(
            double posX,
            double posY,
            String uid,
            int sequence = 0,
            CodeBlockType blockType = CodeBlockType.Unknown,
            string? blockName = null)
        {
            this.PosX = posX;
            this.PosY = posY;
            this.Uid = uid;
            this.Sequence = sequence;
            this.BlockType = blockType;
            this.BlockName = blockName;
        }

        /// <summary>
        /// Simple method that updates a CodeBlock's position properties,
        /// eg when dragged from the UI layer and resaved.
        /// </summary>
        /// <param name="posX">new X coordinate.</param>
        /// <param name="posY">new Y coordinate.</param>
        public void UpdatePosition(double posX, double posY)
        {
            this.PosX = posX;
            this.PosY = posY;
        }

        /// <summary>
        /// Updates the metadata associated with the block.
        /// </summary>
        /// <param name="blockType">The block type to store.</param>
        /// <param name="blockName">The display name of the block.</param>
        public void UpdateBlockMetadata(CodeBlockType blockType, string? blockName = null)
        {
            this.BlockType = blockType;
            this.BlockName = blockName;
        }
    }
}
