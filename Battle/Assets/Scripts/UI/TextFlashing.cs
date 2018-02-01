using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFlashing : MonoBehaviour
{
    // テキスト
    public Text text;
    // アルファ値変更用のカラー
    float color;
    // フラグ
    bool flag;

    // 点滅する速さ
    [SerializeField,Range(0,1)]
    private float flashingTime;

    // Use this for initialization
    void Start()
    {
        color = 0;
    }
    // Update is called once per frame
    void Update()
    {
        //テキストの透明度を変更する
        text.color = new Color(0, 0, 0, color);
        if (flag)
            color -= flashingTime;
        else
            color += flashingTime;
        if (color < 0)
        {
            color = 0;
            flag = false;
        }
        else if (color > 1)
        {
            color = 1;
            flag = true;
        }
    }
}
