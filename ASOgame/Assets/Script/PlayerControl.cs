using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private CharacterController characterController;    //CharacterController型の変数
    private Vector3 Velocity;   //キャラクターコントローラーを動かすためのVector3型の変数
    public float JumpPowewr;    //ジャンプ
    public Transform verRot;    //縦の視点移動の変数（カメラに合わせる）
    public Transform horRot;    //横の視点移動の変数（プレイヤーに合わせる）
    public float MoveSpeed;     //移動速度



    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }


    void Update()
    {
        float X_Rotation = Input.GetAxis("Mouse X");    //X_RotationにマウスのX軸の動きを代入する
        float Y_Rotation = Input.GetAxis("Mouse Y");    //Y_RotationにマウスのY軸の動きを代入する
        horRot.transform.Rotate(new Vector3(0, X_Rotation * 2, 0)); //プレイヤーのY軸の回転をX_Rotationで合わせる
        verRot.transform.Rotate(-Y_Rotation * 2, 0, 0);    //カメラのX軸の回転をY_Rotationに合わせる


        /*
        if(Input.GetKey(KeyCode.W)) //Wキーがおされたら
        {
            characterController.Move(this.gameObject.transform.forward * MoveSpeed * Time.deltaTime);   //前にMoveSpeed * Time.deltaTimeだけ動かす
        }

        if (Input.GetKey(KeyCode.W)) //Sキーがおされたら
        {
            characterController.Move(this.gameObject.transform.forward * -1f * MoveSpeed * Time.deltaTime); //後ろにMoveSpeed * Time.deltaTimeだけ動かす
        }

        if (Input.GetKey(KeyCode.W)) //Aキーがおされたら
        {
            characterController.Move(this.gameObject.transform.right * -1 * MoveSpeed * Time.deltaTime);   //左ににMoveSpeed * Time.deltaTimeだけ動かす
        }

        if (Input.GetKey(KeyCode.W)) //Dキーがおされたら
        {
            characterController.Move(this.gameObject.transform.right * MoveSpeed * Time.deltaTime);   //右にMoveSpeed * Time.deltaTimeだけ動かす
        }
        */
        

        if (Input.GetKey(KeyCode.W))//Wキーがおされたら 
        {
            //前方にMoveSpeed＊Time.deltaTimeだけ動かす
            characterController.Move(this.gameObject.transform.forward * MoveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))//①Sキーがおされたら
        {
            //後方にMoveSpeed＊Time.deltaTimeだけ動かす
            characterController.Move(this.gameObject.transform.forward * -1f * MoveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))//①Aキーがおされたら 
        {
            //左にMoveSpeed＊Time.deltaTimeだけ動かす
            characterController.Move(this.gameObject.transform.right * -1 * MoveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))//①Dキーがおされたら 
        {
            //右にMoveSpeed＊Time.deltaTimeだけ動かす
            characterController.Move(this.gameObject.transform.right * MoveSpeed * Time.deltaTime);
        }


        characterController.Move(Velocity);//キャラクターコントローラーをVeloctiyだけ動かし続ける
        Velocity.y += Physics.gravity.y * Time.deltaTime;   //Velocityのy軸を重力*Time.deltaTime分だけ動かす

        if(characterController.isGrounded)  //キャラクターが地面に接触しているとき
        {
            if(Input.GetKeyDown(KeyCode.Space)) //スペースキーが押されたら
            {
                Velocity.y = JumpPowewr;    //Velocity.y を JumpPowewrにする
            }
        }
    }
}
