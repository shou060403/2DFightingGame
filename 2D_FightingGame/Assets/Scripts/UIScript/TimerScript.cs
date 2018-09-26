using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{

    private float timer = 99;
    private Text text;

	// Use this for initialization
	void Start ()
    {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(timer>=0)
        {
            timer -= Time.deltaTime;
        }
        text.text = timer.ToString("F0");
	}
    public float GetTimer()
    {
        return timer;
    }
    public void ResetTimer()
    {
        timer = 99;
    }
}
