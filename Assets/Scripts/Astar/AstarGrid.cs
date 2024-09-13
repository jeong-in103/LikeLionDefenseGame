using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarGrid : MonoBehaviour
{
    private static AstarGrid instance;

    public static AstarGrid Instance
    {
        get => instance;
    }
    
    [Header("Grid info")]
    [SerializeField] private int xSize; // 열
    [SerializeField] private int ySize; // 행
    [SerializeField] private GameObject startNode;
    [SerializeField] private GameObject endNode;
    [SerializeField] private Node[] nodes;
    
    private Node[,] grid;

    private List<Node> WalkableNodes;
    private List<Node> WalkUnableNodes;

    private void Awake()
    {
        if(!instance)
            instance = this;
        
        nodes = GetComponentsInChildren<Node>();
        
        grid = new Node[ySize, xSize];
        for (int row = 0; row < ySize; ++row)
        {
            for (int column = 0; column < xSize; ++column)
            {
                grid[row, column] = nodes[row * 7 + column];
                grid[row, column].highlight.gameObject.SetActive(false);
                grid[row, column].range.gameObject.SetActive(false);
            }
        }

        WalkableNodes = new List<Node>();
        WalkUnableNodes = new List<Node>();
        foreach (var node in grid)
        {
            if (node.IsWalkAble == true)
            {
                WalkableNodes.Add(node);
            }

            if (node.IsWalkAble == false)
            {
                WalkUnableNodes.Add(node);
            }
        }
    }

    // grid debug
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(xSize,ySize,0));
        
        if (grid != null)
        {
            for (int row = 0; row < ySize; ++row)
            {
                for (int column = 0; column < xSize; ++column)
                {
                    Gizmos.color = (grid[row, column].IsWalkAble) ? Color.white : Color.red;
                    Gizmos.DrawCube(grid[row, column].WorldPos, Vector3.one * 0.8f);
                }
            }
        }
    }

    public void SetActiveNodesByType(AutoAttackType type, bool active)
    {
        if (type == AutoAttackType.Melee)
        {
            foreach (var node in WalkableNodes)
            {
                node.highlight.gameObject.SetActive(active);
            }
        }
        else
        {
            foreach (var node in WalkUnableNodes)
            {
                node.highlight.gameObject.SetActive(active);
            }
        }
    }

    public List<Node> GetNodesByType(AutoAttackType type)
    {
        if (type == AutoAttackType.Melee)
        {
            return WalkableNodes;
        }
        else
        {
            return WalkUnableNodes;
        }
    }

    public void SetActiveFalseAllAttackRange()
    {
        foreach (var node in grid)
        {
            node.range.SetActive(false);
        }
    }

    public List<Node> SetActiveAttackRange(Node charPos, bool[,] attackRange, ArrowDir dir)
    {
        foreach (var node in grid)
        {
            node.range.SetActive(false);
        }
        
        int charNodeX = 0;
        int charNodeY = 0;
        for (int i = 0; i < ySize; ++i)
        {
            for (int j = 0; j < xSize; ++j)
            {
                if (grid[i, j] == charPos)
                {
                    charNodeX = i;
                    charNodeY = j;
                }
            }
        }

        Node[][] allRange = new Node[3][];
        for (int index = 0; index < 3; index++)
        {
            allRange[index] = new Node[3];
        }
    
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int x = charNodeX - 1 + i; // 중심점으로부터 x 좌표
                int y = charNodeY - 1 + j; // 중심점으로부터 y 좌표
                
                if (dir == ArrowDir.Right)
                {
                    y = charNodeY + j;
                }
                else if (dir == ArrowDir.Left)
                {
                    y = charNodeY - 2 + j;
                }
                else if (dir == ArrowDir.Up)
                {
                    x = charNodeX - 2 + i;
                }
                else
                {
                    x = charNodeX + i;
                }

                // 범위를 벗어나는 경우 처리 (여기서는 -1로 표시)
                if (x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1))
                {
                    allRange[i][j] = grid[x, y];
                }
                else
                {
                    allRange[i][j] = null; // 범위를 벗어난 경우를 위한 값
                }
            }
        }

        List<Node> attackableNodse = new List<Node>();
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                if (allRange[i][j] && attackRange[i, j])
                {
                    attackableNodse.Add(allRange[i][j]);
                    allRange[i][j].range.SetActive(true);
                }
            }
        }

        return attackableNodse;
    }
}
