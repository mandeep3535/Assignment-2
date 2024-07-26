using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class colorChanging : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    public int score = 0;
    public Color targetColor = new Color(0f, 0f, 1f, 1f); // Color to change targets to

    public InputHandler inputHandler;
    public DataLogger dataLogger;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void changeColor(GameObject g)
    {
        Debug.Log($"Attempting to change color of: {g.name}");
        if (g.name == "background")
        {
            dataLogger.wrong = 1;

            Debug.Log("Clicked on Background, logging as wrong");
        }
        dataLogger.WriteCSV();
        _spriteRenderer = g.GetComponent<SpriteRenderer>();
        _spriteRenderer.color = new Color(0f, 0f, 1f, 1f);
    }

    public void resetColor(GameObject g)
    {
        _spriteRenderer = g.GetComponent<SpriteRenderer>();
        _spriteRenderer.color = Color.white; // Reset to default color
    }

    public void colorNextTarget(GameObject g)
    {
        _spriteRenderer = g.GetComponent<SpriteRenderer>();
        _spriteRenderer.color = targetColor;
    }


}
