using System.Drawing;
using COMP_3951_BlockForge_TechPro;

namespace BlockForge.TechPro.Tests.BlockConnectors;

[TestClass]
public sealed class BlockConnectorControlTests
{
    [TestMethod]
    public void ConnectorControl_ConstructsWithExpectedDefaultSize()
    {
        using BlockConnectorControl connector = new();

        Assert.AreEqual(new Size(18, 12), connector.Size);
        Assert.AreEqual(Color.DarkOrange, connector.ConnectorBodyPanel.BackColor);
    }
}
