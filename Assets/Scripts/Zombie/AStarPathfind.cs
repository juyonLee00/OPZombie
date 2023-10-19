using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfind
{
    public AStarGrid grid;
    public AStarPathfind(AStarGrid grid)
    {
        this.grid = grid;
    }

    private float Heuristic(AStarNode a, AStarNode b, bool diagonal = false)
    {
        // ����ư �Ÿ�
        var dx = Mathf.Abs(a.xPos - b.xPos);
        var dy = Mathf.Abs(a.yPos - b.yPos);

        if (!diagonal) return 1 * (dx + dy);
        // ü����� �Ÿ�
        return Mathf.Max(Mathf.Abs(a.xPos - b.xPos), Mathf.Abs(a.yPos - b.yPos));
    }

    public List<AStarNode> CreatePath(AStarNode start, AStarNode end, bool diagonal = false)
    {
        if (start == null || end == null) return null;
        grid.ResetNode();

        List<AStarNode> openSet = new List<AStarNode>();
        List<AStarNode> closedSet = new List<AStarNode>();

        AStarNode startNode = start;
        AStarNode endNode = end;
        startNode.gCost = 0;
        startNode.hCost = Heuristic(start, end);
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            // Open Set ���� ��� �� ���� �Ÿ��� ª�� ��带 ã�´�.
            int shortest = 0;
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < openSet[shortest].fCost)
                {
                    shortest = i;
                }
            }
            AStarNode currentNode = openSet[shortest];

            // ������ ����
            if (currentNode == endNode)
            {
                
                List<AStarNode> path = new List<AStarNode>();
                path.Add(endNode);
                var tempNode = endNode;
                while (tempNode.parent != null)
                {
                    path.Add(tempNode.parent);
                    tempNode = tempNode.parent;
                }
                path.Reverse();
                return path;
            }

            // ����Ʈ�� ������Ʈ�Ѵ�.
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            // ���� ��带 �湮�Ѵ�.
            var neighbors = grid.GetNeighborNodes(currentNode, diagonal);
            for (int i = 0; i < neighbors.Count; i++)
            {
                if (closedSet.Contains(neighbors[i]) || !neighbors[i].isWalkable) continue;
                var gCost = currentNode.gCost + Heuristic(currentNode, neighbors[i], diagonal);
                if (gCost < neighbors[i].gCost)
                {
                    neighbors[i].parent = currentNode;
                    neighbors[i].gCost = gCost;
                    neighbors[i].hCost = Heuristic(neighbors[i], endNode, diagonal);
                    if (!openSet.Contains(neighbors[i]))
                        openSet.Add(neighbors[i]);
                }
            }
        }
        return null;
    }

}
