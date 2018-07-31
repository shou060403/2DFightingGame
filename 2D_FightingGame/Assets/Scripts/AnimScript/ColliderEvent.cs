using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEvent : MonoBehaviour {

    //--------------------------------------
    //あたり判定設定
    //--------------------------------------

    //くらい判定
    [SerializeField]
    private List<GameObject> HitCollider;
    //攻撃判定
    [SerializeField]
    private List<GameObject> AtkCollider;

    //あたり判定の数
    int HCnum;
    int ACnum;

    // Use this for initialization
    void Start ()
    {
        //あたり判定の数
        HCnum = HitCollider.Count;
        ACnum = AtkCollider.Count;
	}
	
    //---------------------
    //立ち状態あたり判定
    //---------------------
    void StandCollider()
    {
        //くらい判定
        HitColliderActive();

        //立ち状態くらい判定
        HitCollider[0].SetActive(true);
        HitCollider[1].SetActive(true);

        //攻撃判定
        AtkColliderActive();
    }

    //-------------------------
    //しゃがみ状態あたり判定
    //-------------------------
    void CrouchCollider()
    {
        //くらい判定
        HitColliderActive();

        //しゃがみ状態くらい判定
        HitCollider[2].SetActive(true);
        HitCollider[3].SetActive(true);

        //攻撃判定
        AtkColliderActive();
    }

    //--------------------------------
    //しゃがみパンチ状態あたり判定1
    //--------------------------------
    void CrouchPunch_C1()
    {
        //くらい判定
        HitColliderActive();

        //しゃがみパンチくらい判定
        HitCollider[4].SetActive(true);
        HitCollider[5].SetActive(true);

        //攻撃判定
        AtkColliderActive();
    }

    //--------------------------------
    //しゃがみパンチ状態あたり判定2
    //--------------------------------
    void CrouchPunch_C2()
    {
        //くらい判定
        HitColliderActive();

        //しゃがみパンチくらい判定
        HitCollider[6].SetActive(true);
        HitCollider[7].SetActive(true);
        HitCollider[8].SetActive(true);

        //攻撃判定
        AtkColliderActive();
    }

    //--------------------------------
    //しゃがみパンチ状態あたり判定3
    //--------------------------------
    void CrouchPunch_C3()
    {
        //くらい判定
        HitColliderActive();

        //しゃがみパンチくらい判定
        HitCollider[6].SetActive(true);
        HitCollider[7].SetActive(true);
        HitCollider[9].SetActive(true);

        //攻撃判定
        AtkColliderActive();

        //しゃがみパンチ攻撃判定
        AtkCollider[0].SetActive(true);
    }

    //--------------------------------
    //しゃがみパンチ状態あたり判定4
    //--------------------------------
    void CrouchPunch_C4()
    {        
        //くらい判定
        HitColliderActive();

        //しゃがみパンチくらい判定
        HitCollider[6].SetActive(true);
        HitCollider[7].SetActive(true);
        HitCollider[10].SetActive(true);

        //攻撃判定
        AtkColliderActive();

        //攻撃判定
        AtkCollider[1].SetActive(true);
    }

    //--------------------------------
    //しゃがみパンチ状態あたり判定5
    //--------------------------------
    void CrouchPunch_C5()
    {
        //くらい判定
        HitColliderActive();

        //しゃがみパンチくらい判定
        HitCollider[6].SetActive(true);
        HitCollider[7].SetActive(true);

        //攻撃判定
        AtkColliderActive();
    }

    //--------------------------------
    //しゃがみキック状態あたり判定1
    //--------------------------------
    void CrouchKick_C1()
    {
        //くらい判定
        HitColliderActive();

        //しゃがみキックくらい判定
        HitCollider[11].SetActive(true);
        HitCollider[12].SetActive(true);

        //攻撃判定
        AtkColliderActive();
    }

    //--------------------------------
    //しゃがみキック状態あたり判定2
    //--------------------------------
    void CrouchKick_C2()
    {
        //くらい判定
        HitColliderActive();

        //しゃがみキックくらい判定
        HitCollider[13].SetActive(true);
        HitCollider[14].SetActive(true);

        //攻撃判定
        AtkColliderActive();
    }

    //--------------------------------
    //しゃがみキック状態あたり判定3
    //--------------------------------
    void CrouchKick_C3()
    {
        //くらい判定
        HitColliderActive();

        //しゃがみキックくらい判定
        HitCollider[15].SetActive(true);
        HitCollider[16].SetActive(true);
        HitCollider[17].SetActive(true);

        //攻撃判定
        AtkColliderActive();

        //攻撃判定
        AtkCollider[2].SetActive(true);
    }

    //--------------------------------
    //しゃがみキック状態あたり判定4
    //--------------------------------
    void CrouchKick_C4()
    {
        //くらい判定
        HitColliderActive();

        //しゃがみキックくらい判定
        HitCollider[18].SetActive(true);
        HitCollider[19].SetActive(true);
        HitCollider[20].SetActive(true);

        //攻撃判定
        AtkColliderActive();

        //攻撃判定
        AtkCollider[3].SetActive(true);
    }

    //--------------------------------
    //しゃがみキック状態あたり判定5
    //--------------------------------
    void CrouchKick_C5()
    {
        //くらい判定
        HitColliderActive();

        //しゃがみキックくらい判定
        HitCollider[21].SetActive(true);
        HitCollider[22].SetActive(true);
        HitCollider[23].SetActive(true);

        //攻撃判定
        AtkColliderActive();
    }

    //--------------------------------
    //前移動状態あたり判定
    //--------------------------------
    void MoveFrontCollider()
    {
        //くらい判定
        HitColliderActive();

        //前移動くらい判定
        HitCollider[24].SetActive(true);
        HitCollider[25].SetActive(true);

        //攻撃判定
        AtkColliderActive();
    }

    //--------------------------------
    //後移動状態あたり判定
    //--------------------------------
    void MoveBackCollider()
    {
        //くらい判定
        HitColliderActive();

       //前移動くらい判定
        HitCollider[26].SetActive(true);
        HitCollider[27].SetActive(true);

        //攻撃判定
        AtkColliderActive();
    }

    //--------------------------------
    //パンチ状態あたり判定1
    //--------------------------------
    void PunchCollider1()
    {
        //くらい判定
        HitColliderActive();

        //前移動くらい判定
        HitCollider[28].SetActive(true);
        HitCollider[29].SetActive(true);

        //攻撃判定
        AtkColliderActive();
    }

    //--------------------------------
    //パンチ状態あたり判定2
    //--------------------------------
    void PunchCollider2()
    {
        //くらい判定
        HitColliderActive();

        //前移動くらい判定
        HitCollider[30].SetActive(true);
        HitCollider[31].SetActive(true);

        //攻撃判定
        AtkColliderActive();
    }

    //--------------------------------
    //パンチ状態あたり判定3
    //--------------------------------
    void PunchCollider3()
    {
        //くらい判定
        HitColliderActive();

        //前移動くらい判定
        HitCollider[32].SetActive(true);
        HitCollider[33].SetActive(true);

        //攻撃判定
        AtkColliderActive();

        //攻撃判定
        AtkCollider[4].SetActive(true);
    }

    //--------------------------------
    //パンチ状態あたり判定4
    //--------------------------------
    void PunchCollider4()
    {
        //くらい判定
        HitColliderActive();

        //前移動くらい判定
        HitCollider[34].SetActive(true);
        HitCollider[35].SetActive(true);

        //攻撃判定
        AtkColliderActive();

        //攻撃判定
        AtkCollider[5].SetActive(true);
    }

    //--------------------------------
    //パンチ状態あたり判定5
    //--------------------------------
    void PunchCollider5()
    {
        //くらい判定
        HitColliderActive();

        //前移動くらい判定
        HitCollider[36].SetActive(true);
        HitCollider[37].SetActive(true);

        //攻撃判定
        AtkColliderActive();

        //攻撃判定
        AtkCollider[6].SetActive(true);
    }

    //--------------------------------
    //パンチ状態あたり判定6
    //--------------------------------
    void PunchCollider6()
    {
        //くらい判定
        HitColliderActive();

        //前移動くらい判定
        HitCollider[38].SetActive(true);
        HitCollider[39].SetActive(true);

        //攻撃判定
        AtkColliderActive();
    }

    //--------------------------------
    //パンチ状態あたり判定7
    //--------------------------------
    void PunchCollider7()
    {
        //くらい判定
        HitColliderActive();

        //前移動くらい判定
        HitCollider[40].SetActive(true);
        HitCollider[41].SetActive(true);

        //攻撃判定
        AtkColliderActive();
    }

    //--------------------------------
    //パンチ状態あたり判定8
    //--------------------------------
    void PunchCollider8()
    {
        //くらい判定
        HitColliderActive();

        //前移動くらい判定
        HitCollider[42].SetActive(true);
        HitCollider[43].SetActive(true);

        //攻撃判定
        AtkColliderActive();
    }

    //--------------------------------
    //キック状態あたり判定1
    //--------------------------------
    void KickCollider1()
    {
        //くらい判定
        HitColliderActive();

        //キック状態くらい判定
        HitCollider[44].SetActive(true);
        HitCollider[45].SetActive(true);
        HitCollider[46].SetActive(true);

        //攻撃判定
        AtkColliderActive();
    }

    //--------------------------------
    //キック状態あたり判定2
    //--------------------------------
    void KickCollider2()
    {
        //くらい判定
        HitColliderActive();

        //キック状態くらい判定
        HitCollider[47].SetActive(true);
        HitCollider[48].SetActive(true);
        HitCollider[49].SetActive(true);

        //攻撃判定
        AtkColliderActive();
    }

    //--------------------------------
    //キック状態あたり判定3
    //--------------------------------
    void KickCollider3()
    {
        //くらい判定
        HitColliderActive();

        //キック状態くらい判定
        HitCollider[50].SetActive(true);
        HitCollider[51].SetActive(true);
        HitCollider[52].SetActive(true);

        //攻撃判定
        AtkColliderActive();
    }

    //--------------------------------
    //キック状態あたり判定4
    //--------------------------------
    void KickCollider4()
    {
        //くらい判定
        HitColliderActive();

        //キック状態くらい判定
        HitCollider[53].SetActive(true);
        HitCollider[54].SetActive(true);

        //攻撃判定
        AtkColliderActive();
    }

    //--------------------------------
    //キック状態あたり判定5
    //--------------------------------
    void KickCollider5()
    {
        //くらい判定
        HitColliderActive();

        //キック状態くらい判定
        HitCollider[55].SetActive(true);
        HitCollider[56].SetActive(true);
        HitCollider[57].SetActive(true);

        //攻撃判定
        AtkColliderActive();

        //攻撃判定
        AtkCollider[7].SetActive(true);
    }

    //--------------------------------
    //キック状態あたり判定6
    //--------------------------------
    void KickCollider6()
    {
        //くらい判定
        HitColliderActive();

        //キック状態くらい判定
        HitCollider[58].SetActive(true);
        HitCollider[59].SetActive(true);
        HitCollider[60].SetActive(true);

        //攻撃判定
        AtkColliderActive();
    }

    //--------------------------------
    //キック状態あたり判定7
    //--------------------------------
    void KickCollider7()
    {
        //くらい判定
        HitColliderActive();

        //キック状態くらい判定
        HitCollider[61].SetActive(true);
        HitCollider[62].SetActive(true);

        //攻撃判定
        AtkColliderActive();
    }

    //--------------------------------
    //ジャンプキック状態あたり判定1
    //--------------------------------
    void JumpKickCollider1()
    {
        //くらい判定
        HitColliderActive();

        //攻撃判定
        AtkColliderActive();

        //攻撃判定
        AtkCollider[8].SetActive(true);
        AtkCollider[9].SetActive(true);
    }

    //--------------------------------
    //ジャンプキック状態あたり判定2
    //--------------------------------
    void JumpKickCollider2()
    {
        //くらい判定
        HitColliderActive();

        //攻撃判定
        AtkColliderActive();

        //攻撃判定
        AtkCollider[10].SetActive(true);
        AtkCollider[11].SetActive(true);
    }

    //--------------------------------
    //ジャンプキック状態あたり判定3
    //--------------------------------
    void JumpKickCollider3()
    {
        //くらい判定
        HitColliderActive();

        //攻撃判定
        AtkColliderActive();

        //攻撃判定
        AtkCollider[12].SetActive(true);
        AtkCollider[13].SetActive(true);
    }

    //--------------------------------
    //ジャンプキック状態
    //--------------------------------
    void JumpKickColliderNone()
    {
        //くらい判定
        HitColliderActive();

        //攻撃判定
        AtkColliderActive();
    }


    void JumpCollider()
    {

    }



    //くらい判定管理関数
    private void HitColliderActive()
    {
        for (int i = 0; i < HCnum; i++)
        {
            HitCollider[i].SetActive(false);
        }
    }

    //あたり判定管理関数
    private void AtkColliderActive()
    {
        for (int i = 0; i < ACnum; i++)
        {
            AtkCollider[i].SetActive(false);
        }
    }
}
