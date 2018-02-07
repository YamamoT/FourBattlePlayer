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

    // キャラクター決定人数
    [SerializeField]
    private int playerIsDecideNum = 0;

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
        //Debug.Log(playerValue);
	}


    ///// <summary>
    ///// UnityChanをエントリー
    ///// </summary>
    //public void EntryUnityChan()
    //{
    //    entryCharacter[playerValue - 1] = unityChan;
    //}

    ///// <summary>
    ///// スパイをエントリー
    ///// </summary>
    //public void EntrySpy()
    //{
    //    entryCharacter[playerValue -1] = spy;
    //}

    ///// <summary>
    /////  モンスターをエントリー
    ///// </summary>
    //public void EntryMonster()
    //{
    //    entryCharacter[playerValue -1] = monster;
    //}

    ///// <summary>
    ///// ロボットをエントリー
    ///// </summary>
    //public void EntryRobot()
    //{
    //    entryCharacter[playerValue -1] = robot;
    //}

    // プレイヤーの参加
    public void PlayerJoined()
    {
        playerValue++;
    }

    // キャラクターの決定
    public void CharacterIsDecide()
    {
        playerIsDecideNum++;
    }

    public void CharacterCancel()
    {
        playerIsDecideNum--;
    }

    /// <summary>
    /// キャラクターのエントリー
    /// </summary>
    /// <param name="num">何番目にエントリーさせるか</param>
    /// <param name="CharaNum">キャラの番号</param>
    public void EntryCharacter(int num, int CharaNum)
    {
        switch(CharaNum)
        {
            case 1:
                entryCharacter[num - 1] = unityChan;
                break;
            case 2:
                entryCharacter[num - 1] = spy;
                break;
            case 3:
                entryCharacter[num - 1] = monster;
                break;
            case 4:
                entryCharacter[num - 1] = robot;
                break;
        }
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
        if (playerValue >= 2 && playerIsDecideNum >= 2)
        {
            Debug.Log("入れる数 : " + (playerValue));
            gManager.instance.SetPlayerValue(playerValue - 1);

            for (int i = 0; i < playerValue - 1; i++)
            {
                Debug.Log(i);
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
