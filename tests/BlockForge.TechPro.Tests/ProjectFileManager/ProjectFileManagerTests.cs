using COMP_3951_BlockForge_TechPro;

/// <summary>
/// BlockForge ProjectFileManagerTests 
/// Author: Angus Grewal
/// Date: Mar 25 2026
/// Source: Self-written, with AI coaching. All code submitted is human written, based on ChatGPT guidance.
/// </summary>
namespace BlockForge.TechPro.Tests.ProjectFileManagerTests;

[TestClass]
public class ProjectFileManagerTests
{
    [TestMethod]
    public void ExportValidWorkspace_CreatesProjectFile_AndRoundTripsDesiredFormat()
    {
        PayloadTransformer transformer = new PayloadTransformer(5);

        ProjectFileManager filemanager = new ProjectFileManager(transformer);

        List<CodeBlock> blocks = new List<CodeBlock>();
        blocks.Add(new CodeBlock(150, 300, "UID-1", 3, 7, CodeBlockType.Print, "Print"));
        blocks.Add(new CodeBlock(300, 600, "UID-2", 8, 15, CodeBlockType.Run, "Run"));
        blocks.Add(new CodeBlock(
            450,
            900,
            "UID-3",
            11,
            22,
            CodeBlockType.Variable,
            "score",
            VariableBlockType.Int,
            intValue: 42));

        Project project = new Project("test_project", blocks);

        string filepath = project.ProjectName + ".bfg";

        try
        {
            filemanager.SaveFile(project);

            Assert.IsTrue(File.Exists(filepath));
            Assert.IsTrue(new FileInfo(filepath).Length > 0);

            Project loaded = filemanager.LoadFile(filepath);

            Assert.AreEqual(project.ProjectName, loaded.ProjectName);
            Assert.AreEqual(project.Version, loaded.Version);
            Assert.HasCount(project.CodeBlocks.Count, loaded.CodeBlocks);
            Assert.AreEqual(project.CodeBlocks[0].Uid, loaded.CodeBlocks[0].Uid);
            Assert.AreEqual(project.CodeBlocks[0].PosX, loaded.CodeBlocks[0].PosX, 1e-6);
            Assert.AreEqual(project.CodeBlocks[0].PosY, loaded.CodeBlocks[0].PosY, 1e-6);
            Assert.AreEqual(project.CodeBlocks[2].Uid, loaded.CodeBlocks[2].Uid);
            Assert.AreEqual(project.CodeBlocks[2].GridColumn, loaded.CodeBlocks[2].GridColumn);
            Assert.AreEqual(project.CodeBlocks[2].GridRow, loaded.CodeBlocks[2].GridRow);
            Assert.AreEqual(project.CodeBlocks[2].BlockType, loaded.CodeBlocks[2].BlockType);
            Assert.AreEqual(project.CodeBlocks[2].BlockName, loaded.CodeBlocks[2].BlockName);
            Assert.AreEqual(project.CodeBlocks[2].VariableType, loaded.CodeBlocks[2].VariableType);
            Assert.AreEqual(project.CodeBlocks[2].IntValue, loaded.CodeBlocks[2].IntValue);
            Assert.IsNull(loaded.CodeBlocks[1].IntValue);

        }
        finally
        {
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }
    }

    [TestMethod]
    public void ExportEmptyWorkspace_IsBlockedWithClearValidationError()
    {
        PayloadTransformer transformer = new PayloadTransformer(5);
        ProjectFileManager filemanager = new ProjectFileManager(transformer);
        Project project = new Project("empty_workspace", new List<CodeBlock>());

        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => filemanager.SaveFile(project));

        StringAssert.Contains(ex.Message, "Validation failed");
        StringAssert.Contains(ex.Message, "empty");
        Assert.IsFalse(File.Exists("empty_workspace.bfg"));
    }

    [TestMethod]
    public void ExportUnsupportedBlockCombination_FailsGracefullyWithClearError()
    {
        PayloadTransformer transformer = new PayloadTransformer(5);
        ProjectFileManager filemanager = new ProjectFileManager(transformer);
        List<CodeBlock> blocks = new()
        {
            new CodeBlock(150, 300, "UID-1"),
            new CodeBlock(300, 600, "UID-2", blockType: CodeBlockType.Print, blockName: "Print")
        };
        Project project = new Project("unsupported_workspace", blocks);

        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => filemanager.SaveFile(project));

        StringAssert.Contains(ex.Message, "Validation failed");
        StringAssert.Contains(ex.Message, "Unsupported block type");
        Assert.IsFalse(File.Exists("unsupported_workspace.bfg"));
    }

    [TestMethod]
    public void ExportPreservesVariableNamesAndTypes_WhenGeneratingCodeFromLoadedProject()
    {
        PayloadTransformer transformer = new PayloadTransformer(5);
        ProjectFileManager filemanager = new ProjectFileManager(transformer);
        List<CodeBlock> blocks = new()
        {
            new CodeBlock(
                120,
                240,
                "UID-VAR-1",
                3,
                6,
                CodeBlockType.Variable,
                "playerScore",
                VariableBlockType.Int,
                intValue: 42)
        };
        Project project = new Project("variable_metadata", blocks);
        string filepath = "variable_metadata.bfg";

        try
        {
            filemanager.SaveFile(project);
            Project loaded = filemanager.LoadFile(filepath);

            string java = JavaBlockMap.ToJava(loaded.CodeBlocks.Single());

            Assert.AreEqual("playerScore", loaded.CodeBlocks.Single().BlockName);
            Assert.AreEqual(VariableBlockType.Int, loaded.CodeBlocks.Single().VariableType);
            Assert.AreEqual("int playerScore = 0;", java);
        }
        finally
        {
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }
    }
}
