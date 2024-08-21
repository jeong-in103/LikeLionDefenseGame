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
    [SerializeField] private Node startNode;
    [SerializeField] private Node endNode;
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
                grid[row, column].gameObject.SetActive(false);
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
                node.gameObject.SetActive(active);
            }
        }
        else
        {
            foreach (var node in WalkUnableNodes)
            {
                node.gameObject.SetActive(active);
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
}
