using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private CharacterController characterController;    //CharacterController型の変数
    private Vector3 Velocity;                           //キャラクターコントローラーを動かすためのVector3型の変数
    public float JumpPowewr;                            //ジャンプ
    public Transform verRot;                            //縦の視移c動の変数（カメラに合わせる）
    public Transform horRot;                            //横の視点移動の変数（プレイヤーに合わせる）
    public float MoveSpeed;                             //移動速度
    private Animator anim;                              //アニメーション
    private Ray ray;                                    //Ray型の変数
    public GameObject Explosion;                        //敵を倒したときに出現させる爆発
    private int distance = 10000;                       //Rayを飛ばす距離
    private RaycastHit hit;                             //Rayが何かに当たった時の情報
    public GameObject Muzzle;                           //Rayを発射する場所
    private GameObject[] enemyObjects;                  //敵の数を取得するための配列
    public GameObject Clear;                            //クリア時に出現する文字


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        Clear.SetActive(false); //クリアの文字を非表示
    }


    void Update()
    {
        float X_Rotation = Input.GetAxis("Mouse X");    //X_RotationにマウスのX軸の動きを代入する
        float Y_Rotation = Input.GetAxis("Mouse Y");    //Y_RotationにマウスのY軸の動きを代入する
        horRot.transform.Rotate(new Vector3(0, X_Rotation * 2, 0)); //プレイヤーのY軸の回転をX_Rotationで合わせる
        verRot.transform.Rotate(-Y_Rotation * 2, 0, 0);    //カメラのX軸の回転をY_Rotationに合わせる



        //Wキーがおされたら 
        if (Input.GetKey(KeyCode.W))
        {
            //前方にMoveSpeed＊Time.deltaTimeだけ動かす
            characterController.Move(this.gameObject.transform.forward * MoveSpeed * Time.deltaTime);
            //走るアニメーション
            anim.SetBool("Run", true);
        }
        //ボタンを離したら
        else if(Input.GetKeyUp(KeyCode.W))
        {
            //待機アニメーション
            anim.SetBool("Run", false);
        }


        //Sキーがおされたら
        if (Input.GetKey(KeyCode.S))
        {
            //後方にMoveSpeed＊Time.deltaTimeだけ動かす
            characterController.Move(this.gameObject.transform.forward * -1f * MoveSpeed * Time.deltaTime);
            anim.SetBool("Run", true);
        }
        else if(Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("Run", false);
        }


        //Aキーがおされたら 
        if (Input.GetKey(KeyCode.A))
        {
            //左にMoveSpeed＊Time.deltaTimeだけ動かす
            characterController.Move(this.gameObject.transform.right * -1 * MoveSpeed * Time.deltaTime);
            anim.SetBool("Run", true);
        }
        else if(Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("Run", false);
        }


        //Dキーがおされたら 
        if (Input.GetKey(KeyCode.D))
        {
            //右にMoveSpeed＊Time.deltaTimeだけ動かす
            characterController.Move(this.gameObject.transform.right * MoveSpeed * Time.deltaTime);
            anim.SetBool("Run", true);
        }
        else if(Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("Run", false);
        }



        characterController.Move(Velocity);                 //キャラクターコントローラーをVeloctiyだけ動かし続ける
        Velocity.y += Physics.gravity.y * Time.deltaTime;   //Velocityのy軸を重力*Time.deltaTime分だけ動かす


        //キャラクターが地面に接触しているとき
        if (characterController.isGrounded)  
        {
            //スペースキーが押されたら
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Velocity.y を JumpPowewrにする
                Velocity.y = JumpPowewr;    
            }
        }

        //マウスが左クリックされたら
        if (Input.GetMouseButton(0))
        {
            //RayをMuzzleの場所から前方に飛ばす
            Ray ray = new Ray(Muzzle.transform.position, Muzzle.transform.forward);

            //Rayを赤色で表示させる
            Debug.DrawRay(ray.origin, ray.direction, Color.red);    

            //Rayがdistanceの範囲内で何かに当たった時に
            if (Physics.Raycast(ray,out hit,distance))   
            {
                //もし当たった物のタグがEnemyだったら
                if (hit.collider.tag == "Enemy") 
                {
                    //当たった物を消去
                    Destroy(hit.collider.gameObject);

                    //Rayが当たった場所に爆発を生成する
                    Instantiate(Explosion.gameObject, hit.collider.gameObject.transform.position, gameObject.transform.rotation);
                }
            }
        }

        //enemyObjectsにEnemyのタグがついているオブジェクトを代入する
        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        //もしenemyObjectsの数が0なら（敵が全滅したら）
        if (enemyObjects.Length == 0)
        {
            //クリアを表示
            Clear.SetActive(true);
        }
    }
}
