using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class PathFinding : MonoBehaviour {

    Grid grid;
    public Transform seeker, target;
    PathRequestManager requestManager;


    void Awake()
    {
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<Grid>();       
    }

    void Update()
    {
        if(seeker&&target)
        FindPath(seeker.position, target.position);
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Stopwatch sw = new Stopwatch();

        sw.Start();

        Vector3[] wayPoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        if (startNode.walkable && targetNode.walkable)
        {
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closeSet = new HashSet<Node>();

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirstItem();
                closeSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    sw.Stop();
                    print("Path Found: " + sw.ElapsedMilliseconds + "ms");
                    pathSuccess = true;
                    break;
                }
                foreach (Node neighbour in grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.walkable || closeSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                        else
                            openSet.UpdateItem(neighbour);
                    }
                }
            }
        }
        yield return null;
        if (pathSuccess)
        {
            wayPoints = Retracepath(startNode, targetNode);
        }
        if (wayPoints != null) { requestManager.FinishedProcessingPath(wayPoints, pathSuccess); }
       

    }

    Vector3[] Retracepath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        grid.path = path;
        Vector3[] wayPoints = SimplifyPath(path);
        Array.Reverse(wayPoints);  
        return wayPoints;
    }

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> wayPoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for(int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if(directionNew != directionOld)
            {
                wayPoints.Add(path[i].worldPosition);
            }
            directionOld = directionNew;
        }
        return wayPoints.ToArray();
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        else return 14 * dstX + 10 * (dstY - dstX);
    }
}
