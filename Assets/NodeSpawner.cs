using System.Collections.Generic;
using UnityEngine;

public class NodeSpawner : MonoBehaviour
{
    public GameObject nodePrefab; // Prefab for the node GameObject
    public LayerMask wallLayer;   // Layer mask for walls

    public float nodeSpacing = 1f; // Spacing between nodes
    public float wallDistanceThreshold = 0.1f; // Threshold distance to consider a wall

    public int gridSizeX = 10; // Number of nodes along the X-axis
    public int gridSizeY = 10; // Number of nodes along the Y-axis

    public Vector2 spawnOffset = new Vector2(1f, 1f); // Offset for spawn position

    void Awake()
    {
        SpawnNodes();
    }

    void SpawnNodes()
    {
        // Array to store all spawned nodes
        Node[,] nodes = new Node[gridSizeX, gridSizeY];

        // Loop through grid to spawn nodes
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 spawnPosition = new Vector2(x * nodeSpacing, y * nodeSpacing) + spawnOffset;
                if (!IsNearWall(spawnPosition))
                {
                    GameObject newNodeObj = Instantiate(nodePrefab, spawnPosition, Quaternion.identity);
                    Node newNode = newNodeObj.GetComponent<Node>();
                    newNode.gridX = x;
                    newNode.gridY = y;

                    nodes[x, y] = newNode;
                }
            }
        }

        // Assign neighbors for each node
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Node node = nodes[x, y];
                if (node != null)
                {
                    // Check neighboring nodes
                    if (x > 0)
                        node.left = nodes[x - 1, y];
                    if (x < gridSizeX - 1)
                        node.right = nodes[x + 1, y];
                    if (y > 0)
                        node.down = nodes[x, y - 1];
                    if (y < gridSizeY - 1)
                        node.up = nodes[x, y + 1];

                    // Populate neighbors list
                    node.neighbors = new List<Node>();
                    if (node.up != null) node.neighbors.Add(node.up.GetComponent<Node>());
                    if (node.down != null) node.neighbors.Add(node.down.GetComponent<Node>());
                    if (node.left != null) node.neighbors.Add(node.left.GetComponent<Node>());
                    if (node.right != null) node.neighbors.Add(node.right.GetComponent<Node>());
                }
            }
        }
    }

    bool IsNearWall(Vector2 position)
    {
        // Check if there's a wall nearby using overlap circle
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, wallDistanceThreshold, wallLayer);
        return colliders.Length > 0;
    }
}
