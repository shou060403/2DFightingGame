using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboScript : MonoBehaviour {

    [SerializeField]
    GameObject character;
    HPDirectorScript script;

	// Use this for initialization
	void Start ()
    {
        script = character.GetComponent<HPDirectorScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
