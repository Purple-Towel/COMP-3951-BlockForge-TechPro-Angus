using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// BlockForge CodeBlock 
/// Author: Angus Grewal
/// Date: Mar 4 2026
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
        public double PosX { get; private set; }
        public double PosY { get; private set; }
        public string Uid { get; private set; }

        /// <summary>
        /// Constructor for a CodeBlock.
        /// </summary>
        /// <param name="posX">starting X coordinate to place from.</param>
        /// <param name="posY">starting Y coordinate to place from.</param>
        /// <param name="uid">unique identifier for an instance of CodeBlock. Uniqueness will be enforced elsewhere, for now the CodeBlockValidator will log duplicates.</param>
        public CodeBlock(double posX, double posY, String uid)
        {
            this.PosX = posX;
            this.PosY = posY;
            this.Uid = uid;
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
    }
}
