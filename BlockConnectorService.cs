using System;
using System.Collections.Generic;

namespace COMP_3951_BlockForge_TechPro
{
    /// <summary>
    /// Applies the v1 parent/child connector rules for vertical block chains.
    /// </summary>
    public class BlockConnectorService
    {
        private readonly int _cellWidth;
        private readonly int _cellHeight;

        public BlockConnectorService(int cellWidth = 140, int cellHeight = 72)
        {
            _cellWidth = cellWidth;
            _cellHeight = cellHeight;
        }

        public bool CanConnect(CodeBlock parent, CodeBlock child)
        {
            if (parent.Uid == child.Uid)
            {
                return false;
            }

            if (!IsFlowBlock(parent) || !IsFlowBlock(child))
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(parent.ChildBlockUid))
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(child.ParentBlockUid))
            {
                return false;
            }

            return true;
        }

        public void Connect(CodeBlock parent, CodeBlock child)
        {
            if (!CanConnect(parent, child))
            {
                throw new InvalidOperationException("Blocks cannot be connected under the current connector rules.");
            }

            parent.ChildBlockUid = child.Uid;
            child.ParentBlockUid = parent.Uid;
            AlignChildToParent(parent, child);
        }

        public void Disconnect(CodeBlock block, IDictionary<string, CodeBlock> blocks)
        {
            if (!string.IsNullOrWhiteSpace(block.ParentBlockUid) && blocks.TryGetValue(block.ParentBlockUid, out CodeBlock? parent))
            {
                parent.ChildBlockUid = null;
            }

            if (!string.IsNullOrWhiteSpace(block.ChildBlockUid) && blocks.TryGetValue(block.ChildBlockUid, out CodeBlock? child))
            {
                child.ParentBlockUid = null;
            }

            block.ParentBlockUid = null;
            block.ChildBlockUid = null;
        }

        public void MoveChain(CodeBlock root, IDictionary<string, CodeBlock> blocks, int newGridColumn, int newGridRow)
        {
            root.UpdateGridPosition(newGridColumn, newGridRow);
            root.UpdatePosition(newGridColumn * _cellWidth, newGridRow * _cellHeight);

            CodeBlock current = root;
            int nextRow = newGridRow + 1;

            while (!string.IsNullOrWhiteSpace(current.ChildBlockUid) && blocks.TryGetValue(current.ChildBlockUid, out CodeBlock? child))
            {
                child.UpdateGridPosition(newGridColumn, nextRow);
                child.UpdatePosition(newGridColumn * _cellWidth, nextRow * _cellHeight);
                current = child;
                nextRow++;
            }
        }

        public void DeleteBlock(CodeBlock block, IDictionary<string, CodeBlock> blocks)
        {
            string? parentUid = block.ParentBlockUid;
            string? childUid = block.ChildBlockUid;

            Disconnect(block, blocks);
            blocks.Remove(block.Uid);

            if (!string.IsNullOrWhiteSpace(parentUid) && blocks.TryGetValue(parentUid, out CodeBlock? parent))
            {
                parent.ChildBlockUid = null;
            }

            if (!string.IsNullOrWhiteSpace(childUid) && blocks.TryGetValue(childUid, out CodeBlock? child))
            {
                child.ParentBlockUid = null;
            }
        }

        public List<CodeBlock> GetChainFrom(CodeBlock root, IDictionary<string, CodeBlock> blocks)
        {
            List<CodeBlock> chain = new() { root };
            CodeBlock current = root;

            while (!string.IsNullOrWhiteSpace(current.ChildBlockUid) && blocks.TryGetValue(current.ChildBlockUid, out CodeBlock? child))
            {
                chain.Add(child);
                current = child;
            }

            return chain;
        }

        private void AlignChildToParent(CodeBlock parent, CodeBlock child)
        {
            int childColumn = parent.GridColumn;
            int childRow = parent.GridRow + 1;
            child.UpdateGridPosition(childColumn, childRow);
            child.UpdatePosition(childColumn * _cellWidth, childRow * _cellHeight);
        }

        private static bool IsFlowBlock(CodeBlock block)
        {
            return block.BlockType switch
            {
                CodeBlockType.Run => true,
                CodeBlockType.Print => true,
                CodeBlockType.If => true,
                CodeBlockType.While => true,
                CodeBlockType.Variable => true,
                _ => false
            };
        }
    }
}
