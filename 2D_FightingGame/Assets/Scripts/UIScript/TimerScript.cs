using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{

    private float gameTimer = 99;
    private float timer = 0;
    private Text text;

    bool stopFlag = false;
	// Use this for initialization
	void Start ()
    {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // ゲーム内の時間制限用タイマー
        timer += Time.deltaTime;

        // ゲームが始まっていたら
        if(stopFlag)
        {
            // 制限時間が0でなければ
            if (gameTimer >= 0)
            {
                // 制限時間を減少
                gameTimer -= Time.deltaTime;
            }
        }
        text.text = gameTimer.ToString("F0");
	}
    // 制限時間を取得
    public float GetGameTimer()
    {
        return gameTimer;
    }
    // 現在のゲーム時間を取得
    public float GetTimer()
    {
        return timer;
    }
    // 制限時間をリセット
    public void ResetGameTimer()
    {
        gameTimer = 99;
    }
    // 制限時間を減らすかどうかの判断
    public void SwithGameTimer()
    {
        stopFlag = !stopFlag;
    }
}
