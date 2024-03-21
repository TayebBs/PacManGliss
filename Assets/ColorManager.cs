using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    public GameObject[] gameObjects = null;
    public Sprite[] blackGameObjects = null;
   // public Sprite[] whiteGameObjects = null;
    public Sprite[] blueGameObjects = null;
    public Sprite[] yellowGameObjects = null;
    public GameObject level1;
    public GameObject level2;
    public String color;
    private void Start()
    {
       SelectRandomLevel();
       ChooseColor(color);
    }
    public void SetColor(string _color)
    {
        color = _color;
    }
    public void ChooseColor(string color)
    {
        switch (color)
        {
            case "Black":
                SetSprites(blackGameObjects);
                break;
            case "Blue":
                SetSprites(blueGameObjects);
                break;
            case "Yellow":
                SetSprites(yellowGameObjects);
                break;
            default:
                Debug.LogWarning("Unknown color: " + color);
                break;
        }
    }

    private void SetSprites(Sprite[] sprites)
    {
        if (gameObjects.Length != sprites.Length)
        {
            Debug.LogError("Number of GameObjects and Sprites do not match!");
            return;
        }

        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].GetComponent<SpriteRenderer>().sprite = sprites[i];
        }
    }

    public void SelectRandomLevel()
    {
        int random = UnityEngine.Random.Range(0, 2);
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
