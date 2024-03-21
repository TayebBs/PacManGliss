using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    public GameObject[] gameObjects = null;
    public Sprite[] blackGameObjects = null;
   // public Sprite[] whiteGameObjects = null;
    public Sprite[] blueGameObjects = null;
    public GameObject level1;
    public GameObject level2;
    private void Start()
    {
       SelectRandomLevel();
    }
    public void ChooseColor(string _color)
    {
        if (_color == "Black")
        {
            foreach (GameObject go in gameObjects)
            {
                foreach(Sprite s in blackGameObjects)
                {
                    go.GetComponent<SpriteRenderer>().sprite = s;

                }
            }
        }
        if (_color == "Blue")
        {
            foreach (GameObject go in gameObjects)
            {
                foreach (Sprite s in blueGameObjects)
                {
                    go.GetComponent<SpriteRenderer>().sprite = s;

                }
            }
        }
       
    }
    public void SelectRandomLevel()
    {
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            level1.SetActive(true);
            level2.SetActive(false);
        }
        else
        {
            level2.SetActive(true);
            level1.SetActive(false);
        }
    }

}
