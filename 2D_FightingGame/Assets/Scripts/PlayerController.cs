using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	//移動スピード
	public float speed = 0.1f;
    //重力
    public float gravity = 0.1f;
    //状態
    public string state = "Stand";
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

    new BoxCollider2D collider;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

	// Update is called once per frame
	void Update()
	{
		// 右・左
		float x = Input.GetAxisRaw("Horizontal");

		// 上・下
		float y = Input.GetAxisRaw("Vertical");

        //動作
        switch(state)
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
                if (Input.GetKeyDown(KeyCode.Z) && !animator.GetBool("Punch") && !animator.GetBool("Kick")) state = "FCPunch";
                if (Input.GetKeyDown(KeyCode.X) && !animator.GetBool("Punch") && !animator.GetBool("Kick")) state = "FCKick";

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
                if (Input.GetKeyDown(KeyCode.Z) && !animator.GetBool("Punch") && !animator.GetBool("Kick")) state = "FCKick";
                if (Input.GetKeyDown(KeyCode.X) && !animator.GetBool("Punch") && !animator.GetBool("Kick")) state = "FCKick";

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
        }

        //しゃがんだ時にcollider編集
        if (state == "Crouch")
        {
            collider.offset = new Vector2(0, 0.18f);
            collider.size = new Vector2(0.38f, 0.45f);
        }
        else
        {
            collider.offset = new Vector2(0, 0.4f);
            collider.size = new Vector2(0.38f, 0.9f);
        }

        //ジャンプしているときに地面に触っておらず一定時間経過していたら終了
        bool jumpEnd = gameObject.transform.position.y <= -4.2f && state == "Jump" && jumpCount > jumpTime;
        if (jumpEnd)
        {
            jumpCount = 0;
            state = "Stand";
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
            if (Input.GetKeyDown(KeyCode.Z) && !animator.GetBool("Punch") && !animator.GetBool("Kick"))
            {
                //animator.SetBool("Punch", true);
                animator.SetBool("Kick", true);
            }
            //ジャンプキック
            if (Input.GetKeyDown(KeyCode.X) && !animator.GetBool("Punch") && !animator.GetBool("Kick"))
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
    }

}
