using System.Reflection;
using System.Drawing;
using System.Windows.Forms;
using COMP_3951_BlockForge_TechPro;

namespace BlockForge.TechPro.Tests.SnapGrid;

/// <summary>
/// BlockForge SnapGridIntegrationTests
/// Author: Andre Di Lascio
/// Date: Mar 24 2026
/// Source: Written with the help of AI.
/// </summary>
[TestClass]
[DoNotParallelize]
public sealed class SnapGridIntegrationTests
{
    [TestMethod]
    public void WorkSpaceDropEvent_DropsBlock_ThroughSnapLogic()
    {
        using Form1 form = new();
        Dictionary<Panel, CodeBlock> workspaceBlocks = GetPrivateField<Dictionary<Panel, CodeBlock>>(form, "_workspaceBlocks");
        Panel block = new()
        {
            Size = new Size(140, 45),
            Tag = "Print"
        };
        SnappedPlacement placement = new(new Point(280, 216), new GridPosition(2, 3));

        InvokePrivateInstance(form, "RegisterWorkspaceBlock", new object?[] { block, placement });

        Assert.AreEqual(1, workspaceBlocks.Count);
        CodeBlock stored = workspaceBlocks[block];
        Assert.AreEqual(280d, stored.PosX);
        Assert.AreEqual(216d, stored.PosY);
        Assert.AreEqual(2, stored.GridColumn);
        Assert.AreEqual(3, stored.GridRow);
        Assert.AreEqual(CodeBlockType.Print, stored.BlockType);
        Assert.AreEqual("Print", stored.BlockName);
    }

    [TestMethod]
    public void WorkspaceMoveEvent_ReleasesBlock_UsingSnappedPosition()
    {
        using Form1 form = new();
        Dictionary<Panel, CodeBlock> workspaceBlocks = GetPrivateField<Dictionary<Panel, CodeBlock>>(form, "_workspaceBlocks");
        Panel block = new()
        {
            Size = new Size(140, 45),
            Location = new Point(0, 0),
            Tag = "Print"
        };
        workspaceBlocks[block] = new CodeBlock(0, 0, "uid-print", 0, 0, CodeBlockType.Print, "Print");
        SnappedPlacement updatedPlacement = new(new Point(560, 144), new GridPosition(4, 2));

        InvokePrivateInstance(form, "UpdateStoredBlockPosition", new object?[] { block, updatedPlacement });

        CodeBlock stored = workspaceBlocks[block];
        Assert.AreEqual(560d, stored.PosX);
        Assert.AreEqual(144d, stored.PosY);
        Assert.AreEqual(4, stored.GridColumn);
        Assert.AreEqual(2, stored.GridRow);
    }

    [TestMethod]
    public void SaveWorkspace_UsesSnappedPositionsRatherThanRawCoordinates()
    {
        using Form1 form = new();
        Dictionary<Panel, CodeBlock> workspaceBlocks = GetPrivateField<Dictionary<Panel, CodeBlock>>(form, "_workspaceBlocks");
        Panel block = new()
        {
            Size = new Size(140, 45),
            Location = new Point(577, 157),
            Tag = "Print"
        };
        workspaceBlocks[block] = new CodeBlock(560, 144, "uid-print", 4, 2, CodeBlockType.Print, "Print");

        Project project = (Project)InvokePrivateInstance(form, "CreateWorkspaceProjectSnapshot", new object?[] { "snap-save-test" })!;
        CodeBlock saved = project.CodeBlocks.Single();

        Assert.AreEqual(560d, saved.PosX);
        Assert.AreEqual(144d, saved.PosY);
        Assert.AreEqual(4, saved.GridColumn);
        Assert.AreEqual(2, saved.GridRow);
    }

    [TestMethod]
    public void VariableWorkspaceBlock_UsesSameSnappedStorageFlow()
    {
        using Form1 form = new();
        Dictionary<Panel, CodeBlock> workspaceBlocks = GetPrivateField<Dictionary<Panel, CodeBlock>>(form, "_workspaceBlocks");
        Panel block = new()
        {
            Size = new Size(140, 45),
            Tag = VariableBlock.CreateBool("enabled", true)
        };
        SnappedPlacement placement = new(new Point(420, 360), new GridPosition(3, 5));

        InvokePrivateInstance(form, "RegisterWorkspaceBlock", new object?[] { block, placement });

        CodeBlock stored = workspaceBlocks[block];
        Assert.AreEqual(CodeBlockType.Variable, stored.BlockType);
        Assert.AreEqual("enabled", stored.BlockName);
        Assert.AreEqual(VariableBlockType.Bool, stored.VariableType);
        Assert.AreEqual(true, stored.BoolValue);
        Assert.AreEqual(420d, stored.PosX);
        Assert.AreEqual(360d, stored.PosY);
        Assert.AreEqual(3, stored.GridColumn);
        Assert.AreEqual(5, stored.GridRow);
    }

    [TestMethod]
    public void OccupiedCellMove_IsRefused_AndPriorValidPositionIsRestored()
    {
        using Form1 form = new();
        Dictionary<Panel, CodeBlock> workspaceBlocks = GetPrivateField<Dictionary<Panel, CodeBlock>>(form, "_workspaceBlocks");
        Panel occupiedBlock = new()
        {
            Size = new Size(140, 45),
            Location = new Point(560, 144),
            Tag = "Print"
        };
        Panel movingBlock = new()
        {
            Size = new Size(140, 45),
            Location = new Point(0, 0),
            Tag = "Run"
        };

        workspaceBlocks[occupiedBlock] = new CodeBlock(560, 144, "uid-occupied", 4, 2, CodeBlockType.Print, "Print");
        workspaceBlocks[movingBlock] = new CodeBlock(0, 0, "uid-moving", 0, 0, CodeBlockType.Run, "Run");
        SnappedPlacement conflictingPlacement = new(new Point(560, 144), new GridPosition(4, 2));

        bool occupied = (bool)InvokePrivateInstance(form, "IsGridCellOccupied", new object?[] { conflictingPlacement.GridPosition, movingBlock })!;
        Assert.IsTrue(occupied);

        movingBlock.Location = conflictingPlacement.Location;
        InvokePrivateInstance(form, "RestoreStoredBlockPosition", new object?[] { movingBlock });

        Assert.AreEqual(new Point(0, 0), movingBlock.Location);
        CodeBlock stored = workspaceBlocks[movingBlock];
        Assert.AreEqual(0d, stored.PosX);
        Assert.AreEqual(0d, stored.PosY);
        Assert.AreEqual(0, stored.GridColumn);
        Assert.AreEqual(0, stored.GridRow);
    }

    private static object? InvokePrivateInstance(object instance, string methodName, object?[]? args)
    {
        MethodInfo? method = instance.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.IsNotNull(method, $"Unable to find {instance.GetType().Name}.{methodName}");
        return method.Invoke(instance, args);
    }

    private static T GetPrivateField<T>(object instance, string fieldName) where T : class
    {
        FieldInfo? field = instance.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.IsNotNull(field, $"Unable to find field {fieldName}");
        object? value = field.GetValue(instance);
        Assert.IsNotNull(value, $"Field {fieldName} was null");
        return (T)value;
    }
}

