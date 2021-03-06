﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenTransition : MonoBehaviour
{
    //マウスカーソルを表示
    void Start()
    {
        Cursor.visible = true;
    }
    
    void Update()
    {
        //タイトルだけスペースキーで遷移
        if (Input.GetKey(KeyCode.Space))
        {
            GameObject.Find("FadeManager").GetComponent<Fade>().TransitionScene("Select");
        }
    }

    //ボタンをクリックしたら・・・
    //ゲーム画面へ
    public void Select_Game()
    {

        GameObject.Find("FadeManager").GetComponent<Fade>().TransitionScene("GameMain");
    }

    //チュートリアル画面へ
    public void Select_Tutorial()
    {

        GameObject.Find("FadeManager").GetComponent<Fade>().TransitionScene("Tutorial");
    }

    //ルール画面へ
    public void Select_Rule()
    {
        GameObject.Find("FadeManager").GetComponent<Fade>().TransitionScene("Rule");
    }

    //操作方法画面へ
    public void Select_Operation()
    {
        GameObject.Find("FadeManager").GetComponent<Fade>().TransitionScene("Operation");
    }

    //セレクト画面へ
    public void Operation_Select()
    {
        GameObject.Find("FadeManager").GetComponent<Fade>().TransitionScene("Select");
    }

    //セレクト画面へ
    public void Rule_Select()
    {
        GameObject.Find("FadeManager").GetComponent<Fade>().TransitionScene("Select");
    }

    //タイトル画面へ
    public void Clear_Title()
    {
        GameObject.Find("FadeManager").GetComponent<Fade>().TransitionScene("Title");
    }

    //タイトル画面へ
    public void Over_Title()
    {
        GameObject.Find("FadeManager").GetComponent<Fade>().TransitionScene("Title");
    }

}
