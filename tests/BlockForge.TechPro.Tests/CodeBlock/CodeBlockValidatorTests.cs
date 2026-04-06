using COMP_3951_BlockForge_TechPro;


/// <summary>
/// BlockForge CodeBlockValidatorTests 
/// Author: Angus Grewal
/// Date: Mar 4 2026
/// Source: Self-written, with AI coaching. All code submitted is human written, based on ChatGPT guidance.
/// </summary>
namespace BlockForge.TechPro.CodeBlockTests;
[TestClass]
public sealed class CodeBlockValidatorTests
{
    [TestMethod]
    public void Validate_DuplicateUIDCodeBlock_Fails()
    {
        var block1 = new CodeBlock(150, 300, "UID-1");
        var block2 = new CodeBlock(300, 600, "UID-1");

        var blocks = new List<CodeBlock> { block1, block2 };

        var errors = CodeBlockValidator.Validate(blocks);

        Assert.IsGreaterThan(0, errors.Count);
    }

    [TestMethod]
    public void Validate_MissingUIDCodeBlock_Fails()
    {
        var block1 = new CodeBlock(150, 300, "");
        var block2 = new CodeBlock(300, 600, "UID-1");

        var blocks = new List<CodeBlock> { block1, block2 };

        var errors = CodeBlockValidator.Validate(blocks);

        Assert.IsGreaterThan(0, errors.Count);
    }

    [TestMethod]
    public void Validate_UniqueUIDs_Passes()
    {
        var block1 = new CodeBlock(150, 300, "UID-1", blockType: CodeBlockType.Print, blockName: "Print");
        var block2 = new CodeBlock(300, 600, "UID-2", blockType: CodeBlockType.Run, blockName: "Run");

        var blocks = new List<CodeBlock> { block1, block2 };

        var errors = CodeBlockValidator.Validate(blocks);

        Assert.AreEqual(0, errors.Count);
    }

    [TestMethod]
    public void Validate_EmptyWorkspace_Fails()
    {
        var errors = CodeBlockValidator.Validate(new List<CodeBlock>());

        Assert.IsGreaterThan(0, errors.Count);
        StringAssert.Contains(errors[0], "empty");
    }

    [TestMethod]
    public void Validate_UnknownBlockType_Fails()
    {
        var block = new CodeBlock(150, 300, "UID-1");

        var errors = CodeBlockValidator.Validate(new List<CodeBlock> { block });

        Assert.IsTrue(errors.Any(error => error.Contains("Unsupported block type")));
    }

    [TestMethod]
    public void Validate_VariableBlockMissingType_Fails()
    {
        var block = new CodeBlock(150, 300, "UID-1", blockType: CodeBlockType.Variable, blockName: "score");

        var errors = CodeBlockValidator.Validate(new List<CodeBlock> { block });

        Assert.IsTrue(errors.Any(error => error.Contains("VariableType")));
    }
}
