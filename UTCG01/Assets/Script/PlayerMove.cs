using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("移動スピードの調整")]
    public int moveSpeed;

    Rigidbody rg;
    Plane plane;

    public Camera cameraObj;

    // Use this for initialization
    void Start()
    {
        //自分のRigidbodyを取得
        rg = GetComponent<Rigidbody>();

        //平面を生成する
        plane = new Plane();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotation();
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    void Move()
    {
        //Input値にmoveSpeedをかけた値を更新
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        //移動処理
        rg.velocity = new Vector3(moveX, rg.velocity.y, moveZ);

    }

    void Rotation()
    {
        //カメラからマウス位置へレイを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //平面の位置を更新
        plane.SetNormalAndPosition(Vector3.up, transform.position);

        //Rayが当たった位置を保存するための一時変数
        float distance = 0;

        //衝突を検出して深度から位置を割り出す 
        if (plane.Raycast(ray, out distance))
        {
            Vector3 lookPoint = ray.GetPoint(distance);
            transform.LookAt(lookPoint);
        }

    }
}