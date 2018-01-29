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

    public void SetCurrentState(SceneState state)
    {
        currentState = state;
        OnSceneChanged(currentState);
    }

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

    void TitleAction()
    {
        SceneManager.LoadScene("TestTitleScene");
    }

    void PlayAction()
    {
        SceneManager.LoadScene("TestPlayScene");
    }

    void ResultAction()
    {
        SceneManager.LoadScene("TestResultScene");
    }

    void ConfigAction()
    {
        SceneManager.LoadScene("TestConfigScene");
    }
}