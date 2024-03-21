using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] spawnableObjects; // Array of objects to be spawned
    public LayerMask wallLayer;   // Layer mask for walls

    public float wallDistanceThreshold = 0.1f; // Threshold distance to consider a wall

    void Start()
    {
        SpawnObjectsOnNodes();
    }

    void SpawnObjectsOnNodes()
    {
        // Find all nodes in the scene with the "Node" tag
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");

        // Loop through each node
        foreach (GameObject node in nodes)
        {
            // Calculate the spawn position based on the node's position
            Vector2 spawnPosition = (Vector2)node.transform.position;
            // Check if the spawn position is clear of walls before spawning the object
          
                // Randomly choose a spawnable object
                GameObject objectToSpawn = spawnableObjects[Random.Range(0, spawnableObjects.Length)];

                // Spawn the object
                Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
              

            }
        }
    }

   

