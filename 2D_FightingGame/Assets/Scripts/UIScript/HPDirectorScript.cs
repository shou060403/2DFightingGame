using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPDirectorScript : MonoBehaviour {

    [SerializeField]
    Image hpImage;
    [SerializeField]
    Image dmageImage;

    private int maxHP;
    private int nowHP;
    private int moveHP;


	// Use this for initialization
	void Start ()
    {
        maxHP = 1000;
        moveHP = 1000;
        nowHP = maxHP;
	}

    void hitDmage(int dmage)
    {
        nowHP = nowHP - dmage;
        nowHP = Mathf.Clamp(nowHP, 0, maxHP);
        float HPgage = (float)nowHP / maxHP;

        hpImage.transform.localScale = new Vector3(HPgage, 1, 1);
    }

    void moveDmage()
    {
        if(nowHP<moveHP)
        {
            moveHP -= Mathf.FloorToInt(maxHP * Time.deltaTime * 0.2f);
            moveHP = Mathf.Clamp(moveHP, 0, maxHP);
            float dmageGage = (float)moveHP / maxHP;

            dmageImage.transform.localScale = new Vector3(dmageGage, 1, 1);
        }
    }

	// Update is called once per frame
	void Update ()
    {


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
}
