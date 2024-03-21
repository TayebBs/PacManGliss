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
            int _score = collision.gameObject.GetComponent<Node>().ObjectScore;
            Debug.Log("_score : " + _score);
            myScoreManager.AddScore(_score);
            if (_score > 1)
            {
                myScoreManager.incrementGlissNodes();
            }
            if (ScoreManager.TotalGlissNodes == 3)
            {
                GetComponent<PacManController>().Win();
            }
        }

        if (collision.gameObject.tag == "Teleporter")
        {
            Teleport(collision.gameObject.GetComponent<Teleporter>().position);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            GetComponent<PacManController>().Die();
        }
    }
    public void Teleport(Vector2 _position)
    {
        transform.position = _position;
    }
}
