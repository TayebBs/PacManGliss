using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyBehaviourManager : MonoBehaviour
{
    public NavMeshAgent[] myEnemies;
    public float timeBetweenEnemies;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivateEnemiesSequentially());
    }

    IEnumerator ActivateEnemiesSequentially()
    {
        // Loop through each enemy in the array
        for (int i = 0; i < myEnemies.Length; i++)
        {
            // Activate the enemy
            myEnemies[i].enabled = true;

            // Wait for a specified time before activating the next enemy
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }
}
