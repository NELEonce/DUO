using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    private CharacterController characterController;    //CharacterController型の変数
    private Vector3 Velocity;                           //キャラクターコントローラーを動かすためのVector3型の変数
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
    bool oldButton;                                     //毎フレームのボタンの押下状況
    bool newButton;                                     //現在の押下状況
    bool trgButton;                                     //前に押してなくて今押した状態
    bool visible;                                       //生存フラグ
    private GameObject muzzleFlash;                     //マズルフラッシュをするゲームオブジェクト
    public AudioClip playerShot;                        //プレイヤーが撃った時のSE
    public AudioClip enemyExplosion;                    //エネミーが死んだときのSE
    AudioSource audioSource;                            //AudioSourceを格納
    public GameObject Clear;                            //クリア時に出現する文字
    public GameObject Sighting;                         //クリア時に出現する文字


    private bool afterFinish;           // 戦闘が終了しているかのフラグ
    private float afterFinishTime;      // 戦闘終了後の経過時間(秒)
    public Text text_result;            // 戦闘結果表示UI
    public Text text_battleTime;        // 戦闘時間表示UI
    private float battleTime;           // 戦闘時間(秒)

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        visible = true;
        muzzleFlash = transform.FindChild("Bone024").FindChild("AK47").FindChild("MuzzleFlash").gameObject;
        audioSource = GetComponent<AudioSource>();
        Clear.SetActive(false);
        Sighting.SetActive(true);
        afterFinish = false;
    }


    void Update()
    {
        Cursor.visible = false; //カーソルを消す
        if (visible)
        {
            float X_Rotation = Input.GetAxis("Mouse X");                //X_RotationにマウスのX軸の動きを代入する
            float Y_Rotation = Input.GetAxis("Mouse Y");                //Y_RotationにマウスのY軸の動きを代入する
            horRot.transform.Rotate(new Vector3(0, X_Rotation * 2, 0)); //プレイヤーのY軸の回転をX_Rotationで合わせる
            verRot.transform.Rotate(-Y_Rotation * 2, 0, 0);             //カメラのX軸の回転をY_Rotationに合わせる

            //Wキーがおされたら 
            if (Input.GetKey(KeyCode.W))
            {
                //前方にMoveSpeed＊Time.deltaTimeだけ動かす
                characterController.Move(this.gameObject.transform.forward * MoveSpeed * Time.deltaTime);
                //走るアニメーション
                anim.SetBool("Move", true);
            }
            //ボタンを離したら
            else if (Input.GetKeyUp(KeyCode.W))
            {
                //待機アニメーション
                anim.SetBool("Move", false);
            }


            //Sキーがおされたら
            if (Input.GetKey(KeyCode.S))
            {
                //後方にMoveSpeed＊Time.deltaTimeだけ動かす
                characterController.Move(this.gameObject.transform.forward * -1f * MoveSpeed * Time.deltaTime);
                anim.SetBool("Move", true);
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                anim.SetBool("Move", false);
            }


            //Aキーがおされたら 
            if (Input.GetKey(KeyCode.A))
            {
                //左にMoveSpeed＊Time.deltaTimeだけ動かす
                characterController.Move(this.gameObject.transform.right * -1 * MoveSpeed * Time.deltaTime);
                anim.SetBool("Move", true);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                anim.SetBool("Move", false);
            }


            //Dキーがおされたら 
            if (Input.GetKey(KeyCode.D))
            {
                //右にMoveSpeed＊Time.deltaTimeだけ動かす
                characterController.Move(this.gameObject.transform.right * MoveSpeed * Time.deltaTime);
                anim.SetBool("Move", true);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                anim.SetBool("Move", false);
            }


            characterController.Move(Velocity);                 //キャラクターコントローラーをVeloctiyだけ動かし続ける
            Velocity.y += Physics.gravity.y * Time.deltaTime;   //Velocityのy軸を重力*Time.deltaTime分だけ動かす

            oldButton = newButton;
            trgButton = false;
            newButton = Input.GetMouseButton(0);
            trgButton = newButton & !oldButton;


            //マウスが左クリックされたら
            if (trgButton)
            {
                //RayをMuzzleの場所から前方に飛ばす
                Ray ray = new Ray(Muzzle.transform.position, Muzzle.transform.forward);

                //Rayを赤色で表示させる
                Debug.DrawRay(ray.origin, ray.direction, Color.red);

                //マズルフラッシュを表示
                muzzleFlash.SetActive(true);

                //Rayがdistanceの範囲内で何かに当たった時に
                if (Physics.Raycast(ray, out hit, distance))
                {
                    //もし当たった物のタグがEnemyだったら
                    if (hit.collider.tag == "Enemy")
                    {
                        //当たった物を消去
                        Destroy(hit.collider.gameObject);

                        //Rayが当たった場所に爆発を生成する
                        Instantiate(Explosion.gameObject, hit.collider.gameObject.transform.position, gameObject.transform.rotation);

                        //爆発SE
                        audioSource.PlayOneShot(enemyExplosion, 0.1f);
                    }
                }
                //ショットSE
                audioSource.PlayOneShot(playerShot, 0.05f);
            }
            else
            {
                muzzleFlash.SetActive(false);
            }
        }


        //enemyObjectsにEnemyのタグがついているオブジェクトを代入する
        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        //もしenemyObjectsの数が0なら（敵が全滅したら）
        if (enemyObjects.Length == 0)
        {
            //戦闘終了
            afterFinish = true;
            //クリアを表示
            Clear.SetActive(true);
            //照準を非表示
            Sighting.SetActive(false);
            //2秒後に実行
            Invoke("LoadSceneClear", 2.0f);
        }


        // 戦闘時間をカウントして表示
        if (!afterFinish)
        { // 戦闘が終了していなければ経過時間をカウント
            battleTime += Time.deltaTime;
            text_battleTime.text = "タイム : " + battleTime.ToString("F1");
        }

        // 戦闘が終了したなら
        if (afterFinish)
        {
            afterFinishTime = 0.0f;
            //撃破時間処理
            Data data = GameObject.Find("DataManager").GetComponent<Data>(); // データスクリプトを取得

            //戦闘時間がステージの最速タイムより短ければ
            if (battleTime < data.BestTime_01)
            {
                //最速タイムを更新
                data.BestTime_01 = battleTime;
            }
            else
            {
                //クリアタイムを表示
                data.ClearTime_01 = battleTime;
            }
  
        }
    }


    //敵に当たったら
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //死亡
            visible = false;
            //死ぬアニメーション
            anim.SetBool("Death", true);
            //2秒後に実行
            Invoke("GameOverScene", 1.8f);
        }
    }

    //ゲームオーバー画面
    void GameOverScene()
    {
        //ゲームオーバーを表示
        SceneManager.LoadScene("Over");
    }

    //クリアシーンに移動
    void LoadSceneClear()
    {
        SceneManager.LoadScene("Clear");
    }
}
     

