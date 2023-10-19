using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStarGrid : MonoBehaviour
{
    [SerializeField] private Tilemap walkableMap;
    [Header("���� �׸��带 ǥ��")][SerializeField] bool ShowTestGrid;
    [Header("�밢�� Ž��")][SerializeField] bool Diagonal;

    private AStarNode[,] grid; // [y,x] �׸���

    public AStarPathfind pathfinder;

    // for TEST
    private AStarNode startNode;
    private AStarNode endNode;

    private void Start()
    {
        // �ʱ�ȭ
        CreateGrid();
        pathfinder = new AStarPathfind(this);

    }

    private void Update()
    {
        ////TEST
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    startNode = GetNodeFromWorld(worldPos);
        //}
        //if (Input.GetMouseButtonDown(1))
        //{
        //    Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    endNode = GetNodeFromWorld(worldPos);
        //}

        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    //TestPathfind();
        //}

    }

    private void CreateGrid()
    {
        walkableMap.CompressBounds();
        BoundsInt bounds = walkableMap.cellBounds;
        grid = new AStarNode[bounds.size.y, bounds.size.x];
        for (int y = bounds.yMin, i = 0; i < bounds.size.y; y++, i++)
        {
            for (int x = bounds.xMin, j = 0; j < bounds.size.x; x++, j++)
            {
                AStarNode node = new AStarNode();
                node.yIndex = i;
                node.xIndex = j;
                node.gCost = int.MaxValue;
                node.parent = null;

                //Ÿ���� �߾� ��ǥ�� ����
                node.yPos = walkableMap.CellToWorld(new Vector3Int(x, y)).y;//+0.25f
                node.xPos = walkableMap.CellToWorld(new Vector3Int(x, y)).x;//+0.5f
                // walkable Tilemap�� Ÿ���� ������ �̵� ������ ���, Ÿ���� ������ �̵� �Ұ����� ����̴�.
                if (walkableMap.HasTile(new Vector3Int(x, y, 0)))
                {
                    node.isWalkable = true;
                    grid[i, j] = node;
                }
                else
                {
                    node.isWalkable = false;
                    grid[i, j] = node;
                }
            }
        }
    }

    public void ResetNode()
    {
        foreach (AStarNode node in grid)
        {
            node.Reset();
        }
    }

    public AStarNode GetNodeFromWorld(Vector3 worldPosition)
    {
        // ���� ��ǥ�� �ش� ��ǥ�� AStarNode �ν��Ͻ��� ��´�.
        Vector3Int cellPos = walkableMap.WorldToCell(worldPosition);
        int y = cellPos.y + Mathf.Abs(walkableMap.cellBounds.yMin);
        int x = cellPos.x + Mathf.Abs(walkableMap.cellBounds.xMin);

        AStarNode node = grid[y, x];
        return node;
    }
    public List<AStarNode> GetNeighborNodes(AStarNode node, bool diagonal = false)
    {
        List<AStarNode> neighbors = new List<AStarNode>();
        int height = grid.GetUpperBound(0);
        int width = grid.GetUpperBound(1);

        int y = node.yIndex;
        int x = node.xIndex;
        // ����
        if (y < height)
            neighbors.Add(grid[y + 1, x]);
        if (y > 0)
            neighbors.Add(grid[y - 1, x]);
        // �¿�
        if (x < width)
            neighbors.Add(grid[y, x + 1]);
        if (x > 0)
            neighbors.Add(grid[y, x - 1]);


        return neighbors;
    }

    private void OnDrawGizmos()
    {
        // �׸��尡 �� �����Ǿ����� Ȯ���غ��� ���ؼ� �����Ϳ� �׷�����.
        if (grid != null && ShowTestGrid)
        {
            foreach (var node in grid)
            {
                Gizmos.color = Color.red;
                Vector3Int cellPos = walkableMap.WorldToCell(new Vector3(node.xPos, node.yPos));
                Vector3 drawPos = walkableMap.GetCellCenterWorld(cellPos);
                drawPos -= walkableMap.cellGap / 2;
                Vector3 drawSize = walkableMap.cellSize;
                Gizmos.DrawWireCube(drawPos, drawSize);
            }
        }
    }




}
