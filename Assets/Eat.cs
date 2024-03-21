using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : MonoBehaviour
{
    public ScoreManager myScoreManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Node")
        {
            Destroy(collision.gameObject);
            myScoreManager.AddScore(collision.gameObject.GetComponent<Node>().ObjectScore);
        }
        if(collision.gameObject.tag == "Teleporter")
        {
            Teleport(collision.gameObject.GetComponent<Teleporter>().position);
        }
    }
    public void Teleport(Vector2 _position)
    {
        transform.position= _position;
    }
}
