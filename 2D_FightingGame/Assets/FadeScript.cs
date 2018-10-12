using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //パネルのイメージを操作するのに必要

public class FadeScript : MonoBehaviour
{

    //透明度が変わるスピード
    float fadeSpeed = 0.02f;
    //パネルの色、不透明度
    float red, green, blue, alfa;

    //フェードアウト処理の開始、完了を管理するフラグ
    public bool isFadeOut = false;
    //フェードイン処理の開始、完了を管理するフラグ
    public bool isFadeIn = false;

    //透明度を変更するパネルのイメージ
    Image fadeImage;                

    void Start()
    {
        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alfa = fadeImage.color.a;
    }

    void Update()
    {
        if (isFadeIn)
        {
            StartFadeIn();
        }

        if (isFadeOut)
        {
            StartFadeOut();
        }
    }

    void StartFadeIn()
    {
        // 不透明度を徐々に下げる
        alfa -= fadeSpeed;
        // 変更した不透明度パネルに反映する
        SetAlpha();
        if (alfa <= 0)
        {
            // 完全に透明になったら処理を抜ける
            isFadeIn = false;
            // パネルの表示をオフにする
            fadeImage.enabled = false;
        }
    }

    void StartFadeOut()
    {
        // パネルの表示をオンにする
        fadeImage.enabled = true;
        // 不透明度を徐々にあげる
        alfa += fadeSpeed;
        // 変更した透明度をパネルに反映する
        SetAlpha();
        if (alfa >= 1)
        {
            // 完全に不透明になったら処理を抜ける
            isFadeOut = false;
        }
    }
    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }
    public void FadeInFlag()
    {
        isFadeIn = !isFadeIn;
    }
    public void FadeOutFlag()
    {
        isFadeOut = !isFadeOut;
    }
}