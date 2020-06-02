using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    // メンバ変数宣言
    public float BestTime_01;   // ステージ最速クリアタイム
    public float ClearTime_01;  // ステージクリアタイム

    // 起動時に１回だけ呼び出されるメソッド
    void Start()
    {
        // オブジェクトに「シーン切り替え時に破棄されない」設定を付与
        DontDestroyOnLoad(gameObject);

        // 初期化処理
        BestTime_01 = 999.99f;
        ClearTime_01 = 999.99f;
    }
}