using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyKeyChanged : MonoBehaviour
{
	// 更新
	void Update ()
    {
        // 何かキーを押したら次のシーン状態へ変更する
        if (Input.anyKeyDown)
            gManager.instance.SetCurrentState(SceneState.Select);
	}
}
