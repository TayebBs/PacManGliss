using UnityEngine;
using System.Collections.Generic;

public class Node : MonoBehaviour
{
    public Node up;
    public Node down;
    public Node right;
    public Node left;

    public List<Node> neighbors;
    public int gridX;
    public int gridY;
    public int gCost;
    public int hCost;
    public Node parent;

    public int fCost => gCost + hCost;
}
