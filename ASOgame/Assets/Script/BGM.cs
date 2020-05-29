using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM : MonoBehaviour
{
    void Start()
    {
       DontDestroyOnLoad(this);    //遷移しても流し続ける
       SceneManager.sceneLoaded += BGM_Destroyer;

    }



    void Update()
    {
       
    }



    //BGMオブジェクトの破棄
    void BGM_Destroyer(Scene scene,LoadSceneMode mode)
    {
        if(scene.name=="GameMain")
        {
            SceneManager.MoveGameObjectToScene(gameObject,SceneManager.GetActiveScene());
            //Destroy(this.gameObject);
        }
    }
}