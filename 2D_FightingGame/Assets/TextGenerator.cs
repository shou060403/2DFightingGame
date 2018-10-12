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

    public const int GAME_START = 0;
    public const int GAME_PLAY = 1;
    public const int GAME_END = 2;

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
    GameObject playerObj1;
    // シーン上のプレイヤー2
    [SerializeField]
    GameObject playerObj2;
    [SerializeField]
    GameObject timerObj;
    //=========================================

    [SerializeField]
    GameObject gameDirecter;
    [SerializeField]
    GameObject fadePanel;

    GameObject gamebase;
    HPDirectorScript hp1;
    HPDirectorScript hp2;
    PlayerController pause;
    TimerScript timer;
    GetGameScript getGame;
    GameDirector directer;
    FadeScript fade;
    // ラウンド数
    public static int gameCount = 1;
    // text表示用
    float time;
    // text非表示用
    float winTime;
    // 基盤となる時間
    float baseTime;
    // ゲームが始まっているか
    bool startFlag;
    // ゲームが終わっているか
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

    //勝利プレイヤー判定用
    int winChar;
    float test;
    bool gameSet;
    bool flag;
    // Use this for initialization
    void Start()
    {
        time = 0;
        winTime = 0;
        baseTime = 0;
        gameSet = true;
        flag = true;
        pause = playerObj1.GetComponent<PlayerController>();
        hp1 = playerObj1.GetComponent<HPDirectorScript>();
        hp2 = playerObj2.GetComponent<HPDirectorScript>();
        timer = timerObj.GetComponent<TimerScript>();
        getGame = gameDirecter.GetComponent<GetGameScript>();
        directer = gameDirecter.GetComponent<GameDirector>();
        fade = fadePanel.GetComponent<FadeScript>();
        winChar = 0;
        test = 60.0f;
    }

    // Update is called once per frame
    void Update()
    {
        fade.FadeInFlag();

        // ゲーム開始前(ラウンド開始時)
        if (directer.GetGameState() == GAME_START)
        {
            NowRound(gameCount);
        }
        // プレイ中(制限時間)
        if (directer.GetGameState() == GAME_PLAY)
        {
            // 制限時間をうごかす
            timer.SwithGameTimer();
            // プレイヤー1が勝ったら
            if (directer.GameSet() == 1)
            {
                setRound(PLAYER1, hp1.GetNowHP());
                winChar = 2;
                baseTime = timer.GetTimer();
                directer.GameState(GAME_END);
            }
            // プレイヤー2が勝ったら
            if (directer.GameSet() == 2)
            {
                setRound(PLAYER2, hp2.GetNowHP());
                winChar = 1;
                baseTime = timer.GetTimer();
                directer.GameState(GAME_END);
            }
        }
        // ゲーム終了時(決着が着いた時)
        if (directer.GetGameState() == GAME_END)
        {
            // 時間経過でテキストを非表示に
            if (timer.GetTimer() - baseTime >= 2.0f)
            {
                // Textを非表示に     -> 今後演出を追加する予定
                canvasText.GetComponent<Text>().text = "";
                pause.enabled = true;                AddGameCount(gameCount);

                gameSet = false;
            }
            // いずれかのプレイヤーが勝利した時
            WinPlayer();
        }
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
        // KOのテキストを表示
        KoText(hp, gameText);
        // 制限時間を停止
        timer.SwithGameTimer();
    }
    /// <summary>
    /// 現在のラウンド表記
    /// </summary>
    /// <param name="count"></param>
    public void NowRound(int count)
    {
        RoundText(count, gameText);
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
            // プレイヤーを操作可能状態に
            pause.enabled = true;

            //Text非表示
            canvasText.SetActive(false);
            directer.GameState(GAME_PLAY);
        }
    }
    /// <summary>
    /// KOされた時 or KOした時にText表示
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="text"></param>
    public void KoText(int hp, string[] text)
    {
        // プレイヤーの動きを止める
        pause.enabled = false;

        //HPがMAXの時に倒すor倒されると"PerfectK.O."
        if (hp >= PLAYER_HP)
        {
            canvasText.GetComponent<Text>().text = text[6];
            canvasText.SetActive(true);
        }
        //どちらかのHPが0になった時に"K.O."
        if (hp <= 0)
        {
            canvasText.GetComponent<Text>().text = text[5];
            canvasText.SetActive(true);
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
    public int WinChar
    {
        set { winChar = value; }
        get { return winChar; }
    }

    public void WinPlayer()
    {
        int playerWin = getGame.GetPlayerWin();
        // プレイヤー1が勝った時
        if (playerWin == 1)
        {
            // 勝者の名前を表示する
            if (timer.GetTimer() - baseTime >= 3.0f)
            {
                IssueText(true, gameText);
            }
        }
        // プレイヤー2が勝った時
        if (playerWin == 2)
        {
            // 勝者の名前を表示する
            if (timer.GetTimer() - baseTime >= 3.0f)
            {
                IssueText(true, gameText);
            }
        }
    }
    public bool GetGameSet()
    {
        return gameSet;
    }
    void AddGameCount(int cnt)
    {
        if(flag)
        {
            switch (cnt)
            {
                case 1:
                    gameCount = 2;
                    flag = false;
                    break;
                case 2:
                    gameCount = 3;
                    flag = false;
                    break;
                default:
                    break;
            }
        }
    }
}