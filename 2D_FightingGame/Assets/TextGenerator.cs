using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextGenerator : MonoBehaviour
{
    public const int PLAYER_HP = 100;

    [SerializeField]
    GameObject canvas;

    [SerializeField]
    GameObject windowText;

    [SerializeField]
    GameObject player;
    PauseScript pause;
    bool pauseFlag;
    string[] gameText =
    {
            "None",
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
    bool startFlag;
    int gameCount;

    // Use this for initialization
    void Start()
    {
        time = 0;
        gameCount = 1;
        startFlag = true;
        pause=player.GetComponent<PauseScript>();
        pause.pausing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(startFlag)
        {
            NowRound(gameCount);
        }
        //IssueText(true, gameText);
        //KoText(PLAYER_HP-1, gameText);
    }
    public void NowRound(int count)
    {
        switch(count)
        {
            case 1:
                RoundText(count, gameText);
                break;
            case 2:
                RoundText(count, gameText);
                break;
            case 3:
                RoundText(count, gameText);
                break;
        }
    }
    public void RoundText(int num, string[] text)
    {
        time += Time.deltaTime;
        //"Round2"Text表示
        windowText.GetComponent<Text>().text = text[num];
        if (time >= 1.5f)
        {
            //"Fight"Text表示
            windowText.GetComponent<Text>().text = text[4];
        }
        if (time >= 3.0f)
        {
            pause.pausing = false;
            startFlag = false;

            //Text非表示
            windowText.SetActive(false);
        }
    }
    public void KoText(int hp, string[] text)
    {
        pause.pausing = true;
        //HPがMAXの時に倒すor倒されると"PerfectK.O."
        if (hp >= PLAYER_HP)
        {
            windowText.GetComponent<Text>().text = text[6];
        }
        //どちらかのHPが0になった時に"K.O."
        else
        {
            windowText.GetComponent<Text>().text = text[5];
        }
    }
    public void IssueText(bool num,string[] text)
    {
        if(num)
        {
            windowText.GetComponent<Text>().text = text[7];
        }
        if (!num)
            windowText.GetComponent<Text>().text = text[8];
    }
}