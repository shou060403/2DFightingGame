using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour {
    public const int NONE = 0;
    public const int PLAYER1_WIN = 1;
    public const int PLAYER2_WIN = 2;

    [SerializeField]
    GameObject player1;
    [SerializeField]
    GameObject player2;

    HPDirectorScript HP1, HP2;
    Vector3 initPos1, initPos2;

    ComboScript comboScript;
    GetGameScript gameScript;
    TextGenerator textScript;
    SceneManagement sceneManager;
    [SerializeField]
    Sprite image;

    float timer;
    bool hp;
    // ゲームの状態(0 = ゲーム開始時 , 1 = ゲームプレイ中 , 2 = ゲーム終了)
    int gameState;
	// Use this for initialization
	void Start ()
    {
        timer = 0;
        gameState = 0;
        player1.transform.position = new Vector3(-3, -4.2f, gameObject.transform.position.z);
        player2.transform.position = new Vector3(3, -1.5f, gameObject.transform.position.z);
        textScript = GameObject.Find("TextFactory").GetComponent<TextGenerator>();
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManagement>();

        HP1 = player1.GetComponent<HPDirectorScript>();
        HP2 = player2.GetComponent<HPDirectorScript>();

        initPos1 = player1.transform.position;
        initPos2 = player2.transform.position;

        comboScript = GetComponent<ComboScript>();
        gameScript = GetComponent<GetGameScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (textScript.GetGameSet() == false && gameScript.GetPlayerWin() == 0)
        {
            timer += Time.deltaTime;
            if (timer >= 3.0f)
            {
                sceneManager.SceneChange("play");
            }
        }
        if (gameScript.GetPlayerWin() == 1 || gameScript.GetPlayerWin() == 2)
        {
            timer += Time.deltaTime;
            if (timer >= 5.0f)
            {
                sceneManager.SceneChange("title");
            }
        }
        //情報をリセット
        if (Input.GetKey(KeyCode.R))
        {
            HP1.Initialise();
            HP2.Initialise();

            HP1.HPScale = new Vector3(1, 1, 1);
            HP1.DamageScale = new Vector3(1, 1, 1);
            HP2.HPScale = new Vector3(1, 1, 1);
            HP2.DamageScale = new Vector3(1, 1, 1);

            player1.transform.position = initPos1;
            player2.transform.position = initPos2;

            comboScript.NoneCombo();
            gameScript.ResetGame(image);
        }
    }
    // 決着が着いた(どちらが勝ったか)
    public int GameSet()
    {
        int setNum = NONE;
        // Player1のHPが0以下になったら
        // Player2の勝利
        if (HP1.GetNowHP() <= 0)
        {
            setNum = PLAYER2_WIN;
        }
        // Player2のHPが0以下になったら
        // Player1の勝利
        if (HP2.GetNowHP() <= 0)
        {
            setNum = PLAYER1_WIN;
        }
        return setNum;
    }
    public void GameState(int state)
    {
        gameState = state;
    }
    public int GetGameState()
    {
        return gameState;
    }
    public void ResetState()
    {
        gameState = 0;
    }
}
