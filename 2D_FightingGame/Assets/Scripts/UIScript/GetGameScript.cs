using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetGameScript : MonoBehaviour {
    public const int NONE = 0;
    public const int PLAYER1_WIN = 1;
    public const int PLAYER2_WIN = 2;

    [SerializeField]
    private Image gameFub;

    Vector3 fubTranse1 = new Vector3(-140, 110, 0);
    Vector3 fubTranse2 = new Vector3(140, 110, 0);

    //ラウンド数
    [SerializeField]
    private int gameNum;
    private Image[] game_P1;
    private Image[] game_P2;

    //画像
    [SerializeField]
    private Sprite winImage;

    public static int wins1 = 0, wins2 = 0;
    [SerializeField]
    int interval = 30;

    TextGenerator textScript;

    // Use this for initialization
    void Start ()
    {
        game_P1 = new Image[gameNum];
        game_P2 = new Image[gameNum];

        textScript = GameObject.Find("TextFactory").GetComponent<TextGenerator>();

        //ラウンド数設定
        SetGame(game_P1, fubTranse1, interval);
        SetGame(game_P2, fubTranse2, -interval);
    }

    // Update is called once per frame
    void Update ()
    {
        //Debug.Log(textScript.PauseFlag());
        GetGame(game_P1, wins1);
        GetGame(game_P2, wins2);
        //勝利ラウンド取得
        switch (textScript.WinChar)
        {
            case 1:
                wins2++;
                GetGame(game_P2, wins2);
                textScript.WinChar = 0;
                break;
            case 2:
                wins1++;
                GetGame(game_P1, wins1);
                textScript.WinChar = 0;
                break;
            default:
                break;
        }
    }

    //ラウンド数設定関数
    void SetGame(Image[] games,Vector3 initPos, int interval)
    {
        RectTransform content = GameObject.Find("Canvas/Content").GetComponent<RectTransform>();

        if (gameFub != null)
        {
            for (int i = 0; i < gameNum; i++)
            {
                games[i] = (Image)Instantiate(gameFub, initPos - new Vector3(i * interval, 0, 0), gameFub.transform.rotation);
                games[i].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                games[i].transform.SetParent(content, false);
            }
        }
    }

    //勝利ラウンド取得関数
    public void GetGame(Image[] games,int wins)
    {
        for (int i = 0; i < wins; i++)
        {
            if (wins <= gameNum)
            {
                games[i].sprite = winImage;
            }
        }
    }

    public void ResetGame(Sprite image)
    {
        for (int i = 0; i < gameNum; i++)
        {
            game_P1[i].sprite = image;
            game_P2[i].sprite = image;
        }
    }
    public int GetPlayerWin()
    {
        int[] wins = { wins1, wins2 };
        int playerWin = NONE;
        if (wins[0] >= 2)
        {
            playerWin = PLAYER1_WIN;
        }
        if (wins[1] >= 2)
        {
            playerWin = PLAYER2_WIN;
        }
        return playerWin;
    }
}
