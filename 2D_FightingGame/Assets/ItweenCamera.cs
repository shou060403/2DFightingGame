using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItweenCamera : MonoBehaviour
{

    public Transform Target;

    void Update()
    {
        iTween.MoveUpdate(this.gameObject, iTween.Hash(
            "position", Target.position,
            "time", 3.0f)
        );
        iTween.RotateUpdate(this.gameObject, iTween.Hash(
            "rotation", Target.rotation.eulerAngles,
            "time", 3.0f)
        );
    }
}
