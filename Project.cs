using System;
using System.Collections.Generic;
using System.Text;

namespace COMP_3951_BlockForge_TechPro
{
    public class Project
    {
        public int Version { get; set; } = 1;
        public string? ProjectName { get; set; }
        public List<CodeBlock>? CodeBlocks { get; set; } = new List<CodeBlock>();
    }
}
