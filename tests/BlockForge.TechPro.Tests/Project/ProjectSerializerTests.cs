using COMP_3951_BlockForge_TechPro;

/// <summary>
/// BlockForge ProjectSerializerTests 
/// Author: Angus Grewal
/// Date: Mar 25 2026
/// Source: Self-written, with AI coaching. All code submitted is human written, based on ChatGPT guidance.
/// </summary>
namespace BlockForge.TechPro.ProjectTests;

[TestClass]
public class ProjectSerializerTests
{
    [TestMethod]
    public void Serialize_RoundTrip()
    {
        List<CodeBlock> blocks = new List<CodeBlock>();
        blocks.Add(new CodeBlock(150, 300, "UID-1"));
        blocks.Add(new CodeBlock(300, 600, "UID-2"));
        blocks.Add(new CodeBlock(
            450,
            900,
            "UID-3",
            blockType: CodeBlockType.Variable,
            blockName: "score",
            variableType: VariableBlockType.Int,
            intValue: 42));

        var project = new Project("test_project", blocks);

        string json = ProjectSerializer.Serialize(project);
        var load = ProjectSerializer.Deserialize(json);

        Assert.IsNotNull(load);
        Assert.AreEqual("test_project", project.ProjectName);
        Assert.AreEqual(1, project.Version);
        Assert.HasCount(3, project.CodeBlocks);
        Assert.AreEqual("UID-1", load.CodeBlocks[0].Uid);
        Assert.AreEqual(150, load.CodeBlocks[0].PosX, 1e-6);
        Assert.AreEqual(300, load.CodeBlocks[0].PosY, 1e-6);
        Assert.AreEqual(CodeBlockType.Variable, load.CodeBlocks[2].BlockType);
        Assert.AreEqual(VariableBlockType.Int, load.CodeBlocks[2].VariableType);
        Assert.AreEqual(42, load.CodeBlocks[2].IntValue);
        Assert.IsNull(load.CodeBlocks[1].IntValue);
    }
}
