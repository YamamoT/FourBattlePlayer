using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneState
{
    Title,
    Play,
    Result,
    Config
}

public class gManager : MonoBehaviour
{
    // 
    public static gManager instance;
    // 現在のシーン
    private SceneState currentState;

    // Use this for initialization
    void Start()
    {
        instance = this;
        // シーンを跨いでもこのスクリプトがアタッチされているオブジェクトを破棄しない
        DontDestroyOnLoad(this.gameObject);
        // 最初のシーンをタイトルに設定
        currentState = SceneState.Title;
    }

    /// <summary>
    /// シーン設定
    /// </summary>
    /// <param name="state"></param>
    public void SetCurrentState(SceneState state)
    {
        currentState = state;
        OnSceneChanged(currentState);
    }

    /// <summary>
    /// シーン切り替え時の処理
    /// </summary>
    /// <param name="state"></param>
    void OnSceneChanged(SceneState state)
    {
        switch (state)
        {
            case SceneState.Title:
                TitleAction();
                break;
            case SceneState.Play:
                PlayAction();
                break;
            case SceneState.Result:
                ResultAction();
                break;
            case SceneState.Config:
                ConfigAction();
                break;
        }
    }

    /// <summary>
    /// タイトル時の処理
    /// </summary>
    void TitleAction()
    {
        SceneManager.LoadScene("TestTitleScene");
    }

    /// <summary>
    /// プレイ時の処理
    /// </summary>
    void PlayAction()
    {
        SceneManager.LoadScene("TestPlayScene");
    }

    /// <summary>
    /// リザルト時の処理
    /// </summary>
    void ResultAction()
    {
        SceneManager.LoadScene("TestResultScene");
    }

    /// <summary>
    /// コンフィグ時の処理
    /// </summary>
    void ConfigAction()
    {
        SceneManager.LoadScene("TestConfigScene");
    }
}