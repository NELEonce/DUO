using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRemainingMachine : MonoBehaviour
{

    public Text text_Enemy;                     //敵の残機を表示させるテキスト
    private float RemainingMachine;             //敵の残機のカウント
    float timer = 0.0f;
    float interval = 0.5f;
    GameObject[] tagObjects;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > interval)
        {
            Check("Enemy");
            timer = 0;
        }
    }

    //シーン上のEnemyタグが付いたオブジェクトを数える
    void Check(string tagname)
    {
        tagObjects = GameObject.FindGameObjectsWithTag(tagname);
        RemainingMachine = tagObjects.Length;
        text_Enemy.text = "残りの敵 : " + RemainingMachine.ToString();
        if (tagObjects.Length == 0)
        {
            text_Enemy.text = "全滅！！";
        }
    }
}
    

