using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM : MonoBehaviour
{
    public AudioClip Title;
    public AudioClip GameMain;
    public AudioClip Clear;
    public AudioClip Over;
    public AudioClip Tutorial;

    AudioSource audio;

    void Start()
    {
        //遷移しても流し続ける
        DontDestroyOnLoad(this);    

        //どのシーンをロードするか
        SceneManager.sceneLoaded += BGM_SceneLoad;

        //AudioSourceを取得
        audio = GetComponent<AudioSource>();
    }

    //何のシーンをロードしたか
    void BGM_SceneLoad(Scene scene,LoadSceneMode mode)
    {
        //タイトルBGM
        if (scene.name == "Title") 
        {
            audio.clip = Title;
            audio.Play();
        }

        //ゲーム中BGM
        if (scene.name == "GameMain")
        {

            audio.clip = GameMain;
            audio.Play();

        }

        //クリアBGM
        if (scene.name == "Clear")
        {
           
            audio.clip = Clear;
            audio.Play();

        }

        //ゲームオーバーBGM
        if (scene.name == "Over")
        {
            
            audio.clip = Over;
            audio.Play();

        }

        //チュートリアルBGM
        if (scene.name == "Tutorial")
        {

            audio.clip = Tutorial;
            audio.Play();

        }
    }
}