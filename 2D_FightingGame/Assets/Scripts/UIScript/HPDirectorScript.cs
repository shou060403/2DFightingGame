using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPDirectorScript : MonoBehaviour {

    [SerializeField]
    Image hpImage;
    [SerializeField]
    Image dmageImage;

    [SerializeField]
    private int maxHP = 10000;
    private int nowHP;
    [SerializeField]
    private int moveHP = 10000;

    [SerializeField]
    private GameObject opponent;
    PlayerController script;

    public bool hitFlag = false;

	// Use this for initialization
	void Start ()
    {
        nowHP = maxHP;
        script = opponent.GetComponent<PlayerController>();
	}

    void hitDmage(int dmage)
    {
        nowHP = nowHP - dmage;
        nowHP = Mathf.Clamp(nowHP, 0, maxHP);
        float HPgage = (float)nowHP / maxHP;

        hpImage.transform.localScale = new Vector3(HPgage, 1, 1);
    }

    private void moveDmage()
    {
        //HPが減っているなら
        if(nowHP<moveHP)
        {
            if (nowHP > 0)
            {
                //時間差で赤ゲージを減らす
                moveHP -= Mathf.FloorToInt(maxHP * Time.deltaTime * 0.2f);
            }
            else
            {
                //HPが0になったら赤ゲージを消す
                moveHP = 0;
            }
            moveHP = Mathf.Clamp(moveHP, 0, maxHP);
            float dmageGage = (float)moveHP / maxHP;

            dmageImage.transform.localScale = new Vector3(dmageGage, 1, 1);
        }
    }

    //攻撃を食らったなら
    void OnTriggerEnter2D(Collider2D collision)
    {
        //サンドバッグなら(仮※仕様によって要変更)
        if (this.gameObject.name == "player2")
        {
            if (collision.gameObject.tag == "Attack1")
            {
                hitFlag = true;
                hitDmage(script.damage);
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        hitFlag = false;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            hitDmage(100);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            hitDmage(200);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            hitDmage(300);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            hitDmage(400);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            hitDmage(500);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            hitDmage(600);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            hitDmage(700);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            hitDmage(800);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            hitDmage(900);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            hitDmage(1000);
        }

        moveDmage();
    }

    public int GetNowHP()
    {
        return nowHP;
    }

    //ゲージ取得関数
    public Vector3 HPScale
    {
        get { return hpImage.transform.localScale; }
        set { hpImage.transform.localScale = value; }
    }

    public Vector3 DamageScale
    {
        get { return dmageImage.transform.localScale; }
        set { dmageImage.transform.localScale = value; }
    }

    //初期化関数
    public void Initialise()
    {
        moveHP = 10000;
        nowHP = maxHP;
    }
}
