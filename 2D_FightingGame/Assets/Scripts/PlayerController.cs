using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

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

	public Text text;
	public List <string> history = new List<string>();
	int historyCount = 0;

	//new BoxCollider2D collider;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		//collider = GetComponent<BoxCollider2D>();
		history.Clear();
		for (int i = 0; i < 10; i++)
		{
			history.Add("");
		}
	}

	// Update is called once per frame
	void Update()
	{
<<<<<<< HEAD

		text.text = "";
=======
        //キーの定義
        float x = 0;
        float y = 0;
        bool punchKey = false;
        bool kickKey = false;
        float punchAxis = 0.0f;
        float kickAxis = 0.0f;

        //フリーズしている状態だったらキー動作をしない
        if (!freeze)
        {
            // 右・左
            x = Input.GetAxisRaw("Horizontal");
            // 上・下
            y = Input.GetAxisRaw("Vertical");
            //パンチ
            punchKey = Input.GetKeyDown(KeyCode.Z);
            //キック
            kickKey = Input.GetKeyDown(KeyCode.X);
            //パンチ
            punchAxis = Input.GetAxis("Punch");
            //キック
            kickAxis = Input.GetAxis("Kick");

        }
        else
        {
            //硬直
            recoveryCounter++;
            int recovery = 0;
            switch(recoveryState)
            {
                case "JumpEnd":
                    recovery = landingRecovery;
                    break;
            }
            if(recoveryCounter > recovery)
            {
                freeze = false;
                recoveryCounter = 0;
            }
        }

        //動作
        switch (state)
        {
            case "Stand":
                // 移動する向きを求める
                finalMove = new Vector3(x, 0, 0).normalized * speed;
                nowGravity = 0;
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, -4.2f, gameObject.transform.position.z);
                //ジャンプ、しゃがみ
                if (y > 0) state = "Jump";
                if (y < 0) state = "Crouch";
                //パンチ、キックの入力
                if (punchKey || punchAxis!=0 && !animator.GetBool("Punch") && !animator.GetBool("Kick")) state = "FCPunch";
                if (kickKey || kickAxis!=0 && !animator.GetBool("Punch") && !animator.GetBool("Kick")) state = "FCKick";

                int move = 0;
                if (x > 0) move = 1;
                if (x < 0) move = -1;

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
                if (punchKey || punchAxis!=0 && !animator.GetBool("Punch") && !animator.GetBool("Kick")) state = "FCKick";
                if (kickKey || kickAxis!=0 && !animator.GetBool("Punch") && !animator.GetBool("Kick")) state = "FCKick";

                if (y > -0.5f) state = "Stand";                
                break;
            case "Jump":
                jumpCount++;
                break;
            //立ち、しゃがみパンチ
            case "FCPunch":
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && animator.GetBool("Punch") && !animator.GetBool("Kick"))
                {
                    animator.SetBool("Punch", false);
                    state = "Stand";
                }
>>>>>>> 585e342f452ca0b66b42608afe31891f82c5f15a

		//キーの定義
		float x = 0;
		float y = 0;
		bool punchKey = false;
		bool kickKey = false;

		//フリーズしている状態だったらキー動作をしない
		if (!freeze)
		{
			// 右・左
			x = Input.GetAxisRaw("Horizontal");
			// 上・下
			y = Input.GetAxisRaw("Vertical");
			//パンチ
			punchKey = Input.GetKeyDown(KeyCode.Z);
			//キック
			kickKey = Input.GetKeyDown(KeyCode.X);
		}
		else
		{
			//硬直
			recoveryCounter++;
			int recovery = 0;
			switch(recoveryState)
			{
				case "JumpEnd":
					recovery = landingRecovery;
					break;
			}
			if(recoveryCounter > recovery)
			{
				freeze = false;
				recoveryCounter = 0;
			}
		}

		//動作
		switch (state)
		{
			case "Stand":
				// 移動する向きを求める
				finalMove = new Vector3(x, 0, 0).normalized * speed;
				nowGravity = 0;
				gameObject.transform.position = new Vector3(gameObject.transform.position.x, -4.2f, gameObject.transform.position.z);
				//ジャンプ、しゃがみ
				if (y > 0) state = "Jump";
				if (y < 0) state = "Crouch";
				//パンチ、キックの入力
				if (punchKey && !animator.GetBool("Punch") && !animator.GetBool("Kick")) state = "FCPunch";
				if (kickKey && !animator.GetBool("Punch") && !animator.GetBool("Kick")) state = "FCKick";

				int move = 0;
				if (x > 0) move = 1;
				if (x < 0) move = -1;

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
				if (punchKey && !animator.GetBool("Punch") && !animator.GetBool("Kick")) state = "FCKick";
				if (kickKey && !animator.GetBool("Punch") && !animator.GetBool("Kick")) state = "FCKick";

				if (y > -0.5f) state = "Stand";                
				break;
			case "Jump":
				jumpCount++;
				break;
			//立ち、しゃがみパンチ
			case "FCPunch":
				if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && animator.GetBool("Punch") && !animator.GetBool("Kick"))
				{
					animator.SetBool("Punch", false);
					state = "Stand";
				}

				animator.SetBool("Punch", true);
				finalMove = new Vector3(0, 0, 0);
				break;
			//立ち、しゃがみキック
			case "FCKick":
				if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && animator.GetBool("Kick") && !animator.GetBool("Punch"))
				{
					animator.SetBool("Kick", false);
					state = "Stand";
				}

				animator.SetBool("Kick", true);
				finalMove = new Vector3(0, 0, 0);
				break;
            //必殺
            case "Special":
                switch(specialState)
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

<<<<<<< HEAD
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
			}
			//ジャンプキック
			if (kickKey && !animator.GetBool("Punch") && !animator.GetBool("Kick"))
			{
				animator.SetBool("Kick", true);
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

		//text.text = "左右:" + x + " 上下:" + y + " パンチ:" + punchKey + " キック:"+ kickKey;

		//入力履歴
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			history.RemoveAt(0);
			history.Add("→");
			//if (historyCount >= 30) history.RemoveAt(0);
			//historyCount++;
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			history.RemoveAt(0);
			history.Add("←");
			//if (historyCount >= 30) history.RemoveAt(0);
			//historyCount++;
		}
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			history.RemoveAt(0);
			history.Add("↑");
			//if (historyCount >= 30) history.RemoveAt(0);
			//historyCount++;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			history.RemoveAt(0);
			history.Add("↓");
			//if (historyCount >= 30) history.RemoveAt(0);
			//historyCount++;
		}
		if (punchKey)
		{
			history.RemoveAt(0);
			history.Add("PU");
			//if (historyCount >= 30) history.RemoveAt(0);
			//historyCount++;
		}
		if (kickKey)
		{
			history.RemoveAt(0);
			history.Add("KI");
			//if (historyCount >= 30) history.RemoveAt(0);
			//historyCount++;
		}
		if (Input.GetKeyDown(KeyCode.Q))
		{
			string test ="";
			for (int i = 0; i < 30; i++)
			{
				test += history[i];
			}
			Debug.Log(test);
			//if (historyCount >= 30) history.RemoveAt(0);
			//historyCount++;
		}

		for (int i=0;i<10;i++)
		{
			text.text += history[i];
		}

        if (history[6] == "→" && history[7] == "↓" && history[8] == "→" && history[9] == "PU" && state != "Special")
        {
            specialState = "Syoryuken";
            state = "Special";
            history.Clear();
            for (int i = 0; i < 10; i++)
=======
            //ジャンプキック
            if (punchKey || punchAxis!=0 && !animator.GetBool("Punch") && !animator.GetBool("Kick"))
            {
                //animator.SetBool("Punch", true);
                animator.SetBool("Kick", true);
            }
            //ジャンプキック
            if (kickKey || kickAxis!=0 && !animator.GetBool("Punch") && !animator.GetBool("Kick"))
>>>>>>> 585e342f452ca0b66b42608afe31891f82c5f15a
            {
                history.Add("");
            }
        }

        if (history[7] == "↓" && history[8] == "→" && history[9] == "PU" && state != "Special")
        {
            specialState = "Hadoken";
            state = "Special";
            history.Clear();
            for (int i = 0; i < 10; i++)
            {
                history.Add("");
            }
        }

	}

}
