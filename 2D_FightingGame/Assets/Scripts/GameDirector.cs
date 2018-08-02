using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour {

    [SerializeField]
    GameObject player1;
    [SerializeField]
    GameObject player2;

    HPDirectorScript HP1, HP2;
    Vector3 initPos1, initPos2;

    ComboScript comboScript;
    GetGameScript gameScript;

    [SerializeField]
    Sprite image;

	// Use this for initialization
	void Start ()
    {
        HP1 = player1.GetComponent<HPDirectorScript>();
        HP2 = player2.GetComponent<HPDirectorScript>();

        initPos1 = player1.transform.position;
        initPos2 = player2.transform.position;

        comboScript = GetComponent<ComboScript>();
        gameScript = GetComponent<GetGameScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //情報をリセット
		if(Input.GetKey(KeyCode.R))
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


}
