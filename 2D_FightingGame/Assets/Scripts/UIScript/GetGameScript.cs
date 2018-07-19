using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetGameScript : MonoBehaviour {

    [SerializeField]
    private Image gameFub;

    Vector3 fubTranse = new Vector3(-75, 55, 0);

    //ラウンド数
    [SerializeField]
    private int gameNum;
    private Image[] game;

    //画像
    [SerializeField]
    private Sprite winImage;

    int wins = 0;

	// Use this for initialization
	void Start ()
    {
        RectTransform content = GameObject.Find("Canvas/Content").GetComponent<RectTransform>();
        game = new Image[gameNum];

        if (gameFub != null)
        {
            for (int i = 0; i < gameNum; i++)
            {
                game[i] = (Image)Instantiate(gameFub, fubTranse - new Vector3(i * 20, 0, 0), gameFub.transform.rotation);
                game[i].transform.SetParent(content, false);
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            wins++;
        }

        for (int i = 0; i < wins; i++)
        {
            if (wins <= gameNum)
            {
                game[i].sprite = winImage;
            }
        }
    }
}
