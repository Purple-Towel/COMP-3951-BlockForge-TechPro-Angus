using COMP_3951_BlockForge_TechPro;

/// <summary>
/// BlockForge ProjectFileManagerTests 
/// Author: Angus Grewal
/// Date: Apr 7 2026
/// Source: Self-written, with AI coaching. All code submitted is human written, based on ChatGPT guidance.
/// </summary>
namespace BlockForge.TechPro.Tests;

[TestClass]
public class OutputTests
{
    [TestMethod]
    public void Generate_OutputPseudoCode()
    {
        PayloadTransformer transformer = new PayloadTransformer(3);
        ProjectFileManager manager = new ProjectFileManager(transformer);

        List<CodeBlock> blocks = new List<CodeBlock>
            {
                new CodeBlock(0, 0, "b1", 0, 0, CodeBlockType.Start, ""),
                new CodeBlock(0, 0, "b2", 0, 1, CodeBlockType.If, "X <= Y"),
                new CodeBlock(0, 0, "b3", 0, 2, CodeBlockType.Then, "PRINT \"Hello World\""),
                new CodeBlock(0, 0, "b4", 0, 3, CodeBlockType.Else, "PRINT \"Goodbye!\""),
                new CodeBlock(0, 0, "b5", 0, 4, CodeBlockType.End, "")
            };


        Project project = new Project("Test", blocks);
        string filepath = "output1.txt";

        if (File.Exists(filepath))
        {
            File.Delete(filepath);
        }

        manager.OutputCode(project, filepath);
        Assert.IsTrue(File.Exists(filepath));
        File.Delete(filepath);
    }

    [TestMethod]
    public void Compare_PseudoCode()
    {
        PayloadTransformer transformer = new PayloadTransformer(3);
        ProjectFileManager manager = new ProjectFileManager(transformer);

        List<CodeBlock> blocks = new List<CodeBlock>
            {
                new CodeBlock(0, 0, "b1", 0, 0, CodeBlockType.Start, ""),
                new CodeBlock(0, 0, "b2", 0, 1, CodeBlockType.If, "X <= Y"),
                new CodeBlock(0, 0, "b3", 0, 2, CodeBlockType.Then, "PRINT \"Hello World\""),
                new CodeBlock(0, 0, "b4", 0, 3, CodeBlockType.Else, "PRINT \"Goodbye!\""),
                new CodeBlock(0, 0, "b5", 0, 4, CodeBlockType.End, "")
            };


        Project project = new Project("Test", blocks);
        string filepath = "output2.txt";

        if (File.Exists(filepath))
        {
            File.Delete(filepath);
        }

        manager.OutputCode(project, filepath);

        string actual = File.ReadAllText(filepath);
        string expected = File.ReadAllText("Output/Output Pseudocode.txt");

        Assert.AreEqual(expected, actual);

        File.Delete(filepath);
    }
}
