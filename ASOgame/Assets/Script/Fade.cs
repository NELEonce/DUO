using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 


public class Fade : MonoBehaviour
{
    bool fadeIn;    // フェードインフラグ
    bool fadeOut;   // フェードアウトフラグ

    float alpha;        // 現在のアルファ値(不透明度)
    float feedSpeed;    // フェードの進行スピード

    string loadScene;   // シーン切り替えを行う時の行先シーン名

    // 起動時に１回だけ呼び出されるメソッド
    void Start()
    {
        // 初期化処理
        fadeIn = true;      // シーン開始時はフェードインフラグをオン
        alpha = 1.0f;       // 〃フェード画像の不透明度を1に
        fadeOut = false;    // フェードアウトフラグをオフ
        loadScene = "";

        feedSpeed = 1.2f;   // フェード進行スピード
    }

    // 毎フレーム呼び出されるメソッド
    void Update()
    {
        // フェードイン処理
        if (fadeIn)
        {
            // 時間経過でアルファ値を減少
            alpha -= feedSpeed * Time.deltaTime;
            // フェードイン終了処理
            if (alpha < 0.0f)
            { // アルファ値が0.0より小さければ
                alpha = 0.0f;
                fadeIn = false; // フラグをオフ
            }
            // アルファ値を画像に適用
            GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
        }

        // フェードアウト処理
        if (fadeOut)
        {
            // 時間経過でアルファ値を増加
            alpha += feedSpeed * Time.deltaTime;
            // フェードアウト終了処理
            if (alpha > 1.0f)
            { // アルファ値が1.0より大きければ
                alpha = 1.0f;
                fadeOut = false; // フラグをオフ
                                 // シーンの切り替えを実行
                SceneManager.LoadScene(loadScene);
            }
            // アルファ値を画像に適用
            GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
        }
    }

    // フェード処理と共にシーン切り替えを行う公開メソッド
    // 引数sceneName : 行先シーン名
    public void TransitionScene(string sceneName)
    {
        // 既にフェードインかフェードアウト中なら処理しない
        if (fadeIn || fadeOut)
            return;

        // フェードアウト開始処理
        fadeOut = true;         // フェードアウトフラグをオン
        loadScene = sceneName;  // 行先シーン名を記憶
        alpha = 0.0f;
    }
}