using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   //NavMeshagentを使うために記述する

public class Enemy : MonoBehaviour
{
    public GameObject target;                       //敵がついてくるターゲット
    public Vector3[] wayPoints = new Vector3[3];    //徘徊するポイントの座標を代入するVector3型の変数を配列で作る
    private int currentRoot;                        //現在目指すポイントを代入する変数
    private int Mode;                               //敵の行動パターンを分けるための変数
    public Transform player;                        //プレイヤーの位置を取得するためのTransform型の変数
	public Transform enemypos;                      //敵の位置を取得するためのTransform型の変数
	private NavMeshAgent agent;                     //NavMeshAgentの情報を取得するためのNavmeshagent型の変数
    private Animator anim;                          //アニメーション


    void Start()
	{
		agent = GetComponent<NavMeshAgent>();//NavMeshAgentの情報をagentに代入
        anim = GetComponent<Animator>();
    }

	void Update()
	{
		Vector3 pos = wayPoints[currentRoot];//Vector3型のposに現在の目的地の座標を代入
		float distance = Vector3.Distance(enemypos.position, player.transform.position);//敵とプレイヤーの距離を求める

        //もしプレイヤーと敵の距離が5以上なら
        if (distance > 30)
		{
			Mode = 0;   //Modeを0にする
		}

        //もしプレイヤーと敵の距離が5以下なら
        if (distance < 30)
		{
			Mode = 1;   //Modeを1にする
		}

        //Modeの切り替え
        switch (Mode)
		{
            //Mode0の場合
            case 0:

                //もし敵の位置と現在の目的地との距離が1以下なら
                if (Vector3.Distance(transform.position, pos) < 1f)
				{
					currentRoot += 1;//currentRootを+1する
                    //もしcurrentRootがwayPointsの要素数-1より大きいなら
                    if (currentRoot > wayPoints.Length - 1)
					{
						currentRoot = 0;//currentRootを0にする
					}
				}
                //NavMeshAgentの情報を取得し目的地をposにする
                GetComponent<NavMeshAgent>().SetDestination(pos);
                //待機
                anim.SetBool("Run Forward", false);

                break;

            //Mode1の場合
            case 1:
                //プレイヤーに向かって進む		
                agent.destination = player.transform.position;
                //走るアニメーション
                anim.SetBool("Run Forward", true);
                break;
		}
	}
}

/*
void Start()
{

}


void Update()
{
	transform.LookAt(target.transform); //ターゲットの方を向く
	transform.position += transform.forward * 10f * Time.deltaTime;//向いてる方向に進む
}
}*/
