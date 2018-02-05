using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateManager : MonoBehaviour
{
    // 最大人数
    [SerializeField]
    private int maxPlayerValue = 4;

    // 遊ぶ人数
    [SerializeField]
    private int playerValue = 0;

    // 生成可能キャラクター
    [SerializeField]
    private GameObject unityChan;
    [SerializeField]
    private GameObject spy;
    [SerializeField]
    private GameObject monster;
    [SerializeField]
    private GameObject robot; 

    // 登録用
    [SerializeField]
    private GameObject[] entryCharacter;

	// Use this for initialization
	void Start ()
    {
        entryCharacter = new GameObject[maxPlayerValue];
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(playerValue);
	}


    /// <summary>
    /// UnityChanをエントリー
    /// </summary>
    public void EntryUnityChan()
    {
        entryCharacter[playerValue - 1] = unityChan;
    }

    /// <summary>
    /// スパイをエントリー
    /// </summary>
    public void EntrySpy()
    {
        entryCharacter[playerValue -1] = spy;
    }

    /// <summary>
    ///  モンスターをエントリー
    /// </summary>
    public void EntryMonster()
    {
        entryCharacter[playerValue -1] = monster;
    }

    /// <summary>
    /// ロボットをエントリー
    /// </summary>
    public void EntryRobot()
    {
        entryCharacter[playerValue -1] = robot;
    }

    /// <summary>
    /// キャラクター決定
    /// </summary>
    public void CharacterDecision()
    {
        if(playerValue < maxPlayerValue + 1 && entryCharacter[playerValue -1])
            playerValue += 1;
    }

    public void GameStart()
    {
        if (playerValue > 2)
        {
            Debug.Log("入れる数 : " + (playerValue - 1));
            gManager.instance.SetPlayerValue(playerValue - 1);

            for (int i = 0; i < playerValue; i++)
            {
                gManager.instance.SetPlayCharacter(i, entryCharacter[i]);
            }

            // プレイシーンへ移行
            gManager.instance.SetCurrentState(SceneState.Play);
        }
        else
        {
            Debug.Log("1人では遊べません");
        }
    }
}
