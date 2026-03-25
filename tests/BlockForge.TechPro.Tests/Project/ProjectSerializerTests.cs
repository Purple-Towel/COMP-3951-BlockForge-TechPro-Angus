using COMP_3951_BlockForge_TechPro;

namespace BlockForge.TechPro.Tests;

[TestClass]
public class ProjectSerializerTests
{
    [TestMethod]
    public void Serialize_RoundTrip()
    {
        List<CodeBlock> blocks = new List<CodeBlock>();
        blocks.Add(new CodeBlock(150, 300, "UID-1"));
        blocks.Add(new CodeBlock(300, 600, "UID-2"));
        blocks.Add(new CodeBlock(450, 900, "UID-3"));

        var project = new Project("test_project", blocks);

        string json = ProjectSerializer.Serialize(project);
        var load = ProjectSerializer.Deserialize(json);

        Assert.IsNotNull(load);
        Assert.AreEqual("test_project", project.ProjectName);
        Assert.AreEqual(1, project.Version);
        Assert.HasCount(3, project.CodeBlocks);
        Assert.AreEqual("UID-1", project.CodeBlocks[0].Uid);
        Assert.AreEqual(150, project.CodeBlocks[0].PosX, 1e-6);
        Assert.AreEqual(300, project.CodeBlocks[0].PosY, 1e-6);
    }
}
