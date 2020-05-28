using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ClearTime : MonoBehaviour
{

    // メンバ変数宣言
    public Text text_BestTime_1; // ステージ最速クリアタイム表示UI

    // 起動時に１回だけ呼び出されるメソッド
    void Start()
    {
        // 最速クリアタイムをUIに表示
        Data data = GameObject.Find("DataManager").GetComponent<Data>(); // データスクリプトを取得
        text_BestTime_1.text = "ベストタイム :  " + data.BestTime_01.ToString("F1")+"秒!!";
    }
}