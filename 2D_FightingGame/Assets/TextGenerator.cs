using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextGenerator : MonoBehaviour {
    [SerializeField]
    GameObject canvas;

    [SerializeField]
    GameObject windowText;

    string[] gameText =
    {
            "Round1",
            "Round2",
            "Final Round",
            "Fight!!",
            "K.O",
            "Perfect!\n K.O.",
            "YOU WIN",
            "YOU LOSE"
    };
    float time;
    // Use this for initialization
    void Start () {
        StartText(gameText);

    }

    // Update is called once per frame
    void Update () {
        Debug.Log(time);
    }
    public void StartText(string[] text)
    {
        time += Time.deltaTime;
        windowText.GetComponent<Text>().text = text[0];
        if (time >= 1.5f)
        {
            windowText.GetComponent<Text>().text = text[3];
        }
        if (time >= 3.0f)
        {
            windowText.SetActive(false);
        }
    }
}
