using System;
using System.Collections.Generic;
using System.Text;

namespace COMP_3951_BlockForge_TechPro
{
    public class Project
    {
        public int Version { get; set; } = 1;
        public string? ProjectName { get; set; }
        public List<CodeBlock> CodeBlocks { get; set; } = new List<CodeBlock>();

        public Project(string projectName, List<CodeBlock> codeBlocks)
        {
            this.ProjectName = projectName;
            this.CodeBlocks = codeBlocks;
        }

        public void UpdateBlocks(List<CodeBlock> blocks)
        {
            this.CodeBlocks = blocks;
        }
    }
}
