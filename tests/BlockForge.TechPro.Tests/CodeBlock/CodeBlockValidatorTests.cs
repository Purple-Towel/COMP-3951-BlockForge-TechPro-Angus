using COMP_3951_BlockForge_TechPro;


/// <summary>
/// BlockForge CodeBlockValidatorTests 
/// Author: Angus Grewal
/// Date: Apr 14 2026
/// Source: Self-written, with AI coaching. All code submitted is human written, based on ChatGPT guidance.
/// </summary>
namespace BlockForge.TechPro.CodeBlockTests;
[TestClass]
public sealed class CodeBlockValidatorTests
{
    [TestMethod]
    public void Validate_CatastrophicPath_Fails()
    {
        var blocksEmpty = new List<CodeBlock>();
        var errorsEmpty = CodeBlockValidator.Validate(blocksEmpty);
        var blocksAll = new List<CodeBlock>
        {
           new CodeBlock(0, 0, "u1", 0, CodeBlockType.Unknown),
           new CodeBlock(0, 0, "", 1, CodeBlockType.Start),
           new CodeBlock(0, 0, "", 1, CodeBlockType.Start),
           new CodeBlock(0, 0, "u1", 0, CodeBlockType.Then),
           new CodeBlock(0, 0, "u1", 0, CodeBlockType.If),
           new CodeBlock(0, 0, "u1", 0, CodeBlockType.Else),
           new CodeBlock(0, 0, "u1", 0, CodeBlockType.End),
           new CodeBlock(0, 0, "u1", 0, CodeBlockType.End),
           new CodeBlock(0, 0, "u1", 0, CodeBlockType.Unknown)
        };
        var errorsAll = CodeBlockValidator.Validate(blocksAll);

        Assert.Contains("Need at least 1 block.", errorsEmpty);
        Assert.Contains("First block must be Start block.", errorsAll);
        Assert.Contains("Last block must be End block.", errorsAll);
        Assert.Contains("Only one Start block in a project.", errorsAll);
        Assert.Contains("Only one End block in a project.", errorsAll);
        Assert.Contains("End block cannot have blocks after it.", errorsAll);
        Assert.Contains("An If block must be followed by a Then block.", errorsAll);
        Assert.Contains("A Then block must follow an If block.", errorsAll);
        Assert.Contains("An Else block must follow a Then block.", errorsAll);
        Assert.Contains("Not all blocks have a valid UID.", errorsAll);
        Assert.Contains("Block UIDs are not unique.", errorsAll);
        Assert.Contains("Block Sequence numbers are not unique.", errorsAll);
        Assert.Contains("A block requires additional text.", errorsAll);
        Assert.Contains("An Unknown block type is present.", errorsAll);
    }


    [TestMethod]
    public void Validate_HappyPath_Succeeds()
    {
        var block1 = new CodeBlock(150, 300, "UID-1", 0, CodeBlockType.Start);
        var block2 = new CodeBlock(300, 600, "UID-2", 1, CodeBlockType.End);

        var blocks = new List<CodeBlock> { block1, block2 };

        var errors = CodeBlockValidator.Validate(blocks);

        Assert.IsEmpty(errors);
    }
}
