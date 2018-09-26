using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextGenerator : MonoBehaviour
{
    // プレイヤーの体力
    public const int PLAYER_HP = 10000;
    
    public const int PLAYER1 = 0;
    public const int PLAYER2 = 1;

    // 制限時間
    public const float MAX_TIMER = 99;
    // Textを表示するcanvas
    [SerializeField]
    GameObject canvas;
    // canvasに表示するText
    [SerializeField]
    GameObject canvasText;
    //=========================================
    //  今後動的に適応できるようにする
    //=========================================
    // シーン上のプレイヤー1
    [SerializeField]
    GameObject player1;
    // シーン上のプレイヤー2
    [SerializeField]
    GameObject player2;
    [SerializeField]
    GameObject timerObj;
    //=========================================

    [SerializeField]
    GameObject gameDirecter;
    [SerializeField]
    Sprite spr;

    GameObject gamebase;
    HPDirectorScript hp1;
    HPDirectorScript hp2;
    PlayerController pause;
    TimerScript timer;
    GetGameScript gameReset;
    // ラウンド数
    int gameCount;
    // 勝利数
    int[] wins = { 0, 0 };
    // text表示用
    float time;
    // text非表示用
    float exitTime;
    // ゲームが始まっているか
    bool startFlag;
    // ゲームが終わっているか
    bool endFlag;
    bool pauseFlag;
    // ゲーム内のテキスト
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

    // Use this for initialization
    void Start()
    {
        time = 0;
        exitTime = 0;
        gameCount = 1;
        startFlag = true;
        endFlag = true;
        pauseFlag = true;

        pause=player1.GetComponent<PlayerController>();
        hp1 = player1.GetComponent<HPDirectorScript>();
        hp2 = player2.GetComponent<HPDirectorScript>();
        timer = timerObj.GetComponent<TimerScript>();
        gameReset = gameDirecter.GetComponent<GetGameScript>();
        Debug.Log(pause);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.GetTimer() == MAX_TIMER)
            startFlag = true;
        // ラウンド開始時一度だけ
        if (startFlag)
        {
            NowRound(gameCount);
        }
        // ポーズ中であれば
        if(pauseFlag)
        {
            // プレイヤー2の体力が0になったら
            if (hp2.GetNowHP() <= 0)
            {
                setRound(PLAYER1, hp1.GetNowHP());
                exitTime += Time.deltaTime;
            }
            // プレイヤー1の体力が0になったら
            if (hp1.GetNowHP() <= 0)
            {
                setRound(PLAYER2, hp2.GetNowHP());
                exitTime += Time.deltaTime;
            }
            // 数秒置いてから文字をフェードアウト
            if (exitTime >= 2.0f)
            {
                // Textを非表示に     -> 今後演出を追加する予定
                canvasText.GetComponent<Text>().text = "";
                //プレイヤーの動きを再開させる
                //pause.enabled = !pause.enabled;
                pauseFlag = false;
            }
        }
        //今後実装
        //IssueText(true, gameText);

    }
    //========================================================================================
    //                                 関数
    //========================================================================================

    /// <summary>
    /// ラウンド終了時の処理
    /// </summary>
    /// <param name="num"></param>
    /// <param name="hp"></param>
    void setRound(int num,int hp)
    {
        // 決着が着いた時
        if(endFlag)
        {
            wins[num]++;
            endFlag = false;
        }
        KoText(hp, gameText);
    }
    /// <summary>
    /// 勝利数を取得
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public int GetWinner(int num)
    {
        return wins[num];
    }
    /// <summary>
    /// 現在のラウンド表記
    /// </summary>
    /// <param name="count"></param>
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
    /// <summary>
    /// ラウンド開始時のText表示
    /// </summary>
    /// <param name="num"></param>
    /// <param name="text"></param>
    public void RoundText(int num, string[] text)
    {
        time += Time.deltaTime;
        //"Round"Text表示
        canvasText.GetComponent<Text>().text = text[num];
        if (time >= 1.5f)
        {
            //"Fight"Text表示
            canvasText.GetComponent<Text>().text = text[4];
        }
        if (time >= 3.0f)
        {
            // プレイヤーの動きを止める
            pause.enabled = !pause.enabled;
            Debug.Log(pause);

            //Text非表示
            canvasText.SetActive(false);
            startFlag = false;
        }
    }
    /// <summary>
    /// KOされた時orKOした時にText表示
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="text"></param>
    public void KoText(int hp, string[] text)
    {
        // プレイヤーの動きを止める
        pause.enabled = !pause.enabled;

        //HPがMAXの時に倒すor倒されると"PerfectK.O."
        if (hp >= PLAYER_HP)
        {
            canvasText.GetComponent<Text>().text = text[6];
            canvasText.SetActive(true);
            startFlag = true;
        }
        //どちらかのHPが0になった時に"K.O."
        if (hp <= 0)
        {
            canvasText.GetComponent<Text>().text = text[5];
            startFlag = true;
        }
    }
    /// <summary>
    /// 勝者の名前を表示
    /// </summary>
    /// <param name="num"></param>
    /// <param name="text"></param>
    public void IssueText(bool num,string[] text)
    {
        // プレイヤー1 or プレイヤー2の名前を表示
        if(num)
        {
            canvasText.GetComponent<Text>().text = text[7];
        }
        if (!num)
            canvasText.GetComponent<Text>().text = text[8];
    }
    /// <summary>
    /// ゲームのリセット
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public void ResetGame(bool start,bool end)
    {
        // ゲームが始まっていて、決着が着いていたら
        if (start && end)
        {
            gameReset.ResetGame(spr);
            timer.ResetTimer();
            hp1.Initialise();
            hp2.Initialise();
        }
    }
}