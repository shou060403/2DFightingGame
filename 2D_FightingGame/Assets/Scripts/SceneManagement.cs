using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

    // タイトルシーン
    string titleScene = "TitleScene";
    // プレイシーン
    string playScene = "PlayScene";
    // キャラクターセレクトシーン
    string characterSelectScene = "CharacterSelectScene";
    // プレイメニューシーン
    string playMenuScene = "PlayMenuScene";
    // リザルトシーン
    string resultScene = "ResultScene";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            SceneChange(SceneName("result"));
	}

    ////----------------------------------------------------------------------
    ////! @brief シーン遷移
    ////!
    ////! @param[in] シーン名
    ////!
    ////! @return なし
    ////----------------------------------------------------------------------
    public void SceneChange(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    ////----------------------------------------------------------------------
    ////! @brief シーン名取得
    ////!
    ////! @param[in] シーン名称
    ////!
    ////!            "title"  --- タイトルシーン
    ////!            "menu"   --- プレイメニューシーン
    ////!            "select" --- キャラクターセレクトシーン
    ////!            "play"   --- プレイシーン
    ////!            "result" --- リザルトシーン
    ////!
    ////! @return なし
    ////----------------------------------------------------------------------
    public string SceneName(string name)
    {
        string sceneName = "";
        switch(name)
        {
            case "title":
                sceneName = titleScene;
                break;
            case "menu":
                sceneName = playMenuScene;
                break;
            case "select":
                sceneName = characterSelectScene;
                break;
            case "play":
                sceneName = playScene;
                break;
            case "result":
                sceneName = resultScene;
                break;
        }

        return sceneName;
    }
}
