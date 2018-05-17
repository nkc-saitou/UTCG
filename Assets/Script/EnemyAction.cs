using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAction : MonoBehaviour {

    [Header("弾丸")]
    public GameObject bulletPrefab;

    [Header("銃口")]
    public Transform muzzle;

    [Header("射程範囲")]
    public int shotRange;

    [Header("弾の発射間隔")]
    public float shotInterval;

    float shotTimer; //インターバルを確かめる

    GameObject player; //プレイヤーオブジェクトの取得する

    NavMeshAgent agent; //NavMeshAgentコンポーネントを取得する

    Vector3 playerPos; //プレイヤーの位置を取得する

    // Use this for initialization
    void Start () {

        agent = GetComponent<NavMeshAgent>();

        //値の初期化
        shotTimer = shotInterval;

        //playerタグが付いたオブジェクトを検索する
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {

        PlayerState();

    }

    /// <summary>
    /// プレイヤーの次の行動を決める
    /// </summary>
    void PlayerState()
    {
        //プレイヤーオブジェクトがScene上にいなかった場合は処理をしない
        if (player == null) return;

        //プレイヤーの位置を取得
        playerPos = player.transform.position;

        //プレイヤーが攻撃できる位置にいたら
        if (IsAttack() == true)
        {
            Shot();
            Rotation();
        }
        else // 攻撃できる位置にいなかったら
        {
            Move();
        }
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    void Move()
    {
        //NavMeshAgentが止めてあった場合は動かす
        if (agent.isStopped == true) agent.isStopped = false;

        //目的地に向かう
        agent.SetDestination(playerPos);
    }

    /// <summary>
    /// プレイヤーの方向へ回転させる
    /// </summary>
    void Rotation()
    {
        //方向ベクトルを求める
        Vector3 dir = playerPos - transform.position;

        //滑らかにプレイヤーの方向を向かせる
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
    }

    /// <summary>
    /// 弾を打つ
    /// </summary>
    void Shot()
    {
        //NavMeshAgentが動いていた場合は止める
        if (agent.isStopped == false) agent.isStopped = true;

        shotTimer -= Time.deltaTime;

        //shotIntervalが終わったら
        if (shotTimer <= 0.0f)
        {
            //弾丸を生成する
            Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);

            shotTimer = shotInterval;
        }
    }

    /// <summary>
    /// 攻撃できるかどうかを判断する
    /// </summary>
    /// <returns></returns>
    bool IsAttack()
    {
        //自分とプレイヤーの距離を取得
        float distance = Vector3.Distance(transform.position, playerPos);

        NavMeshHit hit;

        //自分と相手との距離が射程範囲内　かつ　自分と相手との間に障害物がない場合にture
        if (distance <= shotRange && !agent.Raycast(playerPos,out hit)) return true;
        else return false;
    }
}
