using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 状態一覧
/// </summary>
public enum SceneState
{
    Title,
    Select,
    Play
}

public class gManager : MonoBehaviour
{
    // インスタンス
    public static gManager instance;
    // 現在のシーン
    private SceneState currentState;

    // 操作キャラクターのサイズ
    [SerializeField]
    private static int playerValue = 0;
    private int pValue;

    // 選ばれたキャラクター
    [SerializeField]
    private static GameObject[] character;

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
            case SceneState.Select:
                SelectAction();
                break;
            case SceneState.Play:
                PlayAction();
                break;
        }
    }

    /// <summary>
    /// タイトル画面時の処理
    /// </summary>
    void TitleAction()
    {
        SceneManager.LoadScene("Title");
    }

    /// <summary>
    /// セレクト画面時の処理
    /// </summary>
    void SelectAction()
    {
        SceneManager.LoadScene("Select");
    }

    /// <summary>
    /// プレイ画面時の処理
    /// </summary>
    void PlayAction()
    {
        SceneManager.LoadScene("Play");
    }

    public void SetPlayerValue(int value)
    {
        playerValue = value;

        character = new GameObject[playerValue];
    }

    public void SetPlayCharacter(GameObject chara)
    {
        character[pValue] = chara;
        character[pValue].GetComponent<PlayerStates>().PlayerID = pValue + 1;
        pValue++;
    }

    public GameObject GetPlayCharacter(int charaValue)
    {
        return character[charaValue];
    }

    public int GetPlayerValue()
    {
        return playerValue;
    }


}