using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamepadInput;

public class PlayerController : MonoBehaviour
{
    //移動スピード
    public float speed = 0.1f;
    //重力
    public float gravity = 0.1f;
    //状態
    public string state = "Stand";
    //必殺技の状態
    public string specialState = "";
    //着地硬直フレーム
    public int landingRecovery = 3;
    //ジャンプ硬直
    public int jumpingRecovery = 3;
    //ヒットストップ
    public int hitStop = 4;
    //コマンド入力の猶予
    public int commandCount = 10;
    //硬直のフレームカウンタ
    int recoveryCounter = 0;
    //フリーズする状態
    bool freeze = false;
    //硬直の状態
    string recoveryState = "JumpEnd";
    //アニメーター
    Animator animator;
    //現在の重力
    float nowGravity;
    //縦方向の速度
    float ySpeed = 0;
    //最終的な移動距離
    Vector3 finalMove = new Vector3(0, 0, 0);
    //ジャンプをしてから地面につかない間
    int jumpTime = 10;
    int jumpCount = 0;
    //1Pか2Pか
    public int controller = 1;

    //ダメージ
    public int damage = 0;
    //各ダメージまとめ
    public int[] attackDamage = { 600, 900, 700, 800 }; 

    enum attack
    {
        punch,
        kick,
        crouchKick,
        jumpKick
    };

    //体力
    public int hp = 10000;

    //向いている方向
    int direction = 0;
    //相手
    GameObject enemy;
    //方向キー
    int inputDKey;
    int inputDKeyOld;

    //入力履歴,textはそれを表示する
    public Text text;
    List<string> history = new List<string>();

    //new BoxCollider2D collider;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        //collider = GetComponent<BoxCollider2D>();
        //コマンドのリセット
        history.Clear();
        for (int i = 0; i < commandCount; i++)
        {
            history.Add("");
        }

        inputDKey = 5;
        inputDKeyOld = inputDKey;
        //オブジェクト名から相手を決める
        if(controller == 1) enemy = GameObject.Find("player2");
        else if(controller == 2) enemy = GameObject.Find("player1");

        if(enemy == null)
        {
            enemy = GameObject.Find("Main Camera");
        }

    }

    // Update is called once per frame
    void Update()
    {
        //向きを決める
        if (enemy.transform.position.x >= transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        text.text = "";

        //キーの定義
        float x = 0;
        float y = 0;
        //x = 0;
        //y = 0;
        bool punchKey = false;
        bool kickKey = false;

        //フリーズしている状態だったらキー動作をしない
        if (!freeze)
        {
            //1P
            if(controller == 1)
            {
                // 右・左
                x = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).x;
                x += GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.One).x * 1000;

                // 上・下
                y = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One).y;
                y += GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.One).y * 1000;

                //パンチ
                punchKey = GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.One);
                //キック
                kickKey  = GamePad.GetButtonDown(GamePad.Button.X, GamePad.Index.One);
            }
            //2P
            else if (controller == 2)
            {
                // 右・左
                x = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.Two).x;
                x += GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.Two).x * 1000;

                // 上・下
                y = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.Two).y;
                y += GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.Two).y * 1000;

                //パンチ
                punchKey = GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.Two);
                //キック
                kickKey = GamePad.GetButtonDown(GamePad.Button.X, GamePad.Index.Two);
            }

            //向いている向きで方向キーを変える(左右反転)
            if (direction == 1)
            {
                if (x < -0.4f && y < -0.5f) inputDKey = 1;
                if (x < 0.4f && x > -0.4f && y < -0.5f) inputDKey = 2;
                if (x > 0.4f && y < -0.5f) inputDKey = 3;
                if (y < 0.4f && y > -0.5f && x < -0.4f) inputDKey = 4;
                if (x < 0.4f && x > -0.4f && y < 0.4f && y > -0.5f) inputDKey = 5;
                if (y < 0.4f && y > -0.5f && x > 0.4f) inputDKey = 6;
                if (x < -0.4f && y > 0.4f) inputDKey = 7;
                if (x < 0.4f && x > -0.4f && y > 0.4f) inputDKey = 8;
                if (x > 0.4f && y > 0.4f) inputDKey = 9;
            }
            else if (direction == -1)
            {
                if (x < -0.4f && y < -0.5f) inputDKey = 3;
                if (x < 0.4f && x > -0.4f && y < -0.5f) inputDKey = 2;
                if (x > 0.4f && y < -0.5f) inputDKey = 1;
                if (y < 0.4f && y > -0.5f && x < -0.4f) inputDKey = 6;
                if (x < 0.4f && x > -0.4f && y < 0.4f && y > -0.5f) inputDKey = 5;
                if (y < 0.4f && y > -0.5f && x > 0.4f) inputDKey = 4;
                if (x < -0.4f && y > 0.4f) inputDKey = 9;
                if (x < 0.4f && x > -0.4f && y > 0.4f) inputDKey = 8;
                if (x > 0.4f && y > 0.4f) inputDKey = 7;
            }


        }
        else
        {
            //硬直
            recoveryCounter++;
            int recovery = 0;
            switch (recoveryState)
            {
                case "JumpEnd":
                    recovery = landingRecovery;
                    break;
            }
            if (recoveryCounter > recovery)
            {
                freeze = false;
                recoveryCounter = 0;
            }
        }

        //動作
        switch (state)
        {
            case "Stand":
                transform.localScale = new Vector3(direction * 5, 5, 5);
                nowGravity = 0;
                damage = 0;
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, -4.2f, gameObject.transform.position.z);
                //ジャンプ、しゃがみ
                if (inputDKey >= 7) state = "Jump";
                if (inputDKey <= 3) state = "Crouch";
                //パンチ、キックの入力
                if (punchKey && !animator.GetBool("Punch") && !animator.GetBool("Kick")) state = "FPunch";
                if (kickKey && !animator.GetBool("Punch") && !animator.GetBool("Kick")) state = "FKick";

                int move = 0;
                if (direction == 1)
                {
                    if (inputDKey == 6 || inputDKey == 9) move = 1;
                    if (inputDKey == 4 || inputDKey == 7) move = -1;
                }
                else if (direction == -1)
                {
                    if (inputDKey == 6 || inputDKey == 9) move = -1;
                    if (inputDKey == 4 || inputDKey == 7) move = 1;
                }


                // 移動する向きを求める
                finalMove = new Vector3(move, 0, 0).normalized * speed;

                animator.SetInteger("Move", move);
                animator.SetBool("Crouch", false);
                animator.SetBool("Punch", false);
                animator.SetBool("Kick", false);
                break;
            case "Crouch":
                animator.SetBool("Crouch", true);
                finalMove = new Vector3(0, 0, 0);
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, -4.2f, gameObject.transform.position.z);
                //キック
                //if (Input.GetKeyDown(KeyCode.Z) && !animator.GetBool("Punch") && !animator.GetBool("Kick")) state = "FCPunch";
                if (punchKey && !animator.GetBool("Punch") && !animator.GetBool("Kick")) state = "CKick";
                if (kickKey && !animator.GetBool("Punch") && !animator.GetBool("Kick")) state = "CKick";

                if (inputDKey >= 4) state = "Stand";
                break;
            case "Jump":
                jumpCount++;
                break;
            //立ちパンチ
            case "FPunch":
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && animator.GetBool("Punch") && !animator.GetBool("Kick"))
                {
                    animator.SetBool("Punch", false);
                    state = "Stand";
                }

                animator.SetBool("Punch", true);
                finalMove = new Vector3(0, 0, 0);
                damage = attackDamage[(int)attack.punch];
                break;
            //しゃがみパンチ
            case "CPunch":
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && animator.GetBool("Punch") && !animator.GetBool("Kick"))
                {
                    animator.SetBool("Punch", false);
                    state = "Stand";
                }

                animator.SetBool("Punch", true);
                finalMove = new Vector3(0, 0, 0);
                damage = attackDamage[(int)attack.punch];
                break;
            //立ちキック
            case "FKick":
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && animator.GetBool("Kick") && !animator.GetBool("Punch"))
                {
                    animator.SetBool("Kick", false);
                    state = "Stand";
                }

                damage = attackDamage[(int)attack.kick];
                animator.SetBool("Kick", true);
                finalMove = new Vector3(0, 0, 0);
                break;
            //しゃがみキック
            case "CKick":
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && animator.GetBool("Kick") && !animator.GetBool("Punch"))
                {
                    animator.SetBool("Kick", false);
                    state = "Stand";
                }

                damage = attackDamage[(int)attack.crouchKick];
                animator.SetBool("Kick", true);
                finalMove = new Vector3(0, 0, 0);
                break;
            //必殺
            case "Special":
                switch (specialState)
                {
                    //必殺
                    case "Hadoken":
                        Debug.Log("波動拳");
                        state = "Stand";
                        break;
                    //必殺
                    case "Syoryuken":
                        Debug.Log("昇龍拳");
                        state = "Stand";
                        break;
                }
                break;
        }

        ////しゃがんだ時にcollider編集
        //if (state == "Crouch")
        //{
        //    collider.offset = new Vector2(0, 0.18f);
        //    collider.size = new Vector2(0.38f, 0.45f);
        //}
        //else
        //{
        //    collider.offset = new Vector2(0, 0.4f);
        //    collider.size = new Vector2(0.38f, 0.9f);
        //}

        //ジャンプしているときに地面に触っておらず一定時間経過していたら終了
        bool jumpEnd = gameObject.transform.position.y <= -4.2f && state == "Jump" && jumpCount > jumpTime;
        if (jumpEnd)
        {
            jumpCount = 0;
            state = "Stand";
            freeze = true;
            recoveryState = "JumpEnd";
        }
        //ジャンプしているときに重力をかける
        bool jumping = gameObject.transform.position.y >= -4.3f && state == "Jump";
        if (jumping) nowGravity -= gravity;

        //ジャンプしたときのアニメ、ジャンプする動作
        if (state == "Jump")
        {
            animator.SetBool("Jump", true);
            ySpeed = 0.3f;

            //ジャンプキック
            if (punchKey && !animator.GetBool("Punch") && !animator.GetBool("Kick"))
            {
                //animator.SetBool("Punch", true);
                animator.SetBool("Kick", true);
                damage = 50;
            }
            //ジャンプキック
            if (kickKey && !animator.GetBool("Punch") && !animator.GetBool("Kick"))
            {
                animator.SetBool("Kick", true);
                damage = attackDamage[(int)attack.jumpKick];
            }
        }
        else
        {
            //ジャンプ終わり
            animator.SetBool("Jump", false);
            ySpeed = 0;
        }

        finalMove.y = ySpeed + nowGravity;
        // 移動する向きとスピードを代入する
        Vector3 finalPos = finalMove + gameObject.transform.position;
        gameObject.transform.position = finalPos;

        //入力
        switch (inputDKey)
        {
            case 1:
                history.RemoveAt(0);
                history.Add("↙");
                break;
            case 2:
                history.RemoveAt(0);
                history.Add("↓");
                break;
            case 3:
                history.RemoveAt(0);
                history.Add("↘");
                break;
            case 4:
                history.RemoveAt(0);
                history.Add("←");
                break;
            case 5:
                history.RemoveAt(0);
                history.Add("N");
                break;
            case 6:
                history.RemoveAt(0);
                history.Add("→");
                break;
            case 7:
                history.RemoveAt(0);
                history.Add("↖");
                break;
            case 8:
                history.RemoveAt(0);
                history.Add("↑");
                break;
            case 9:
                history.RemoveAt(0);
                history.Add("↗");
                break;
        }

        if (punchKey)
        {
            history.RemoveAt(0);
            history.Add("P");
        }
        if (kickKey)
        {
            history.RemoveAt(0);
            history.Add("K");
        }

        for (int i = 0; i < commandCount; i++)
        {
            text.text += history[i];
        }

        SpecialMove();

        inputDKeyOld = inputDKey;

    }

    /// <summary>
    /// 必殺技コマンドの処理
    /// </summary>
    private void SpecialMove()
    {

        if (state != "Special")
        {
            string[] syoryu = new string[4];
            syoryu[0] = "→";
            syoryu[1] = "↓";
            syoryu[2] = "↘";
            syoryu[3] = "P";

            int syoryuCount = 0;

            for (int i = 0; i < commandCount; i++)
            {
                if (syoryu[syoryuCount] == history[i])
                {
                    syoryuCount++;
                    if (syoryuCount > 3)
                    {
                        specialState = "Syoryuken";
                        state = "Special";
                        history.Clear();
                        for (int j = 0; j < commandCount; j++)
                        {
                            history.Add("");
                        }
                        break;
                    }
                }
            }

            string[] hadoken = new string[4];
            hadoken[0] = "↓";
            hadoken[1] = "↘";
            hadoken[2] = "→";
            hadoken[3] = "P";

            int hadokenCount = 0;

            for (int i = 0; i < commandCount; i++)
            {
                if (hadoken[hadokenCount] == history[i])
                {
                    hadokenCount++;
                    if (hadokenCount > 3)
                    {
                        specialState = "Hadoken";
                        state = "Special";
                        history.Clear();
                        for (int j = 0; j < commandCount; j++)
                        {
                            history.Add("");
                        }
                        break;
                    }
                }
            }
        }
    }

}
