using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetGameScript : MonoBehaviour {

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

    int wins = 0;
    [SerializeField]
    int interval = 30;

	// Use this for initialization
	void Start ()
    {
        game_P1 = new Image[gameNum];
        game_P2 = new Image[gameNum];

        //ラウンド数設定
        SetGame(game_P1, fubTranse1, interval);
        SetGame(game_P2, fubTranse2, -interval);
    }

    // Update is called once per frame
    void Update ()
    {
        //勝利ラウンド取得
        if(Input.GetKeyDown(KeyCode.Q))
        {
            GetGame(game_P1);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GetGame(game_P2);
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
    void GetGame(Image[] games)
    {
        wins++;
        for (int i = 0; i < wins; i++)
        {
            if (wins <= gameNum)
            {
                games[i].sprite = winImage;
            }
        }
    }
}
