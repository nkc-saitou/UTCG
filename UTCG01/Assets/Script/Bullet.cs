using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [Header("弾丸を飛ばす力")]
    public float bulletPower;

    Rigidbody rg;

	// Use this for initialization
	void Start () {
        //自分についているRigidbodyコンポーネントを取得してくる
        rg = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        //前方向にbulletPower分の力で飛ばす
        rg.AddForce(transform.forward * bulletPower);

        //生成されてから３秒で消す
        Destroy(gameObject, 3.0f);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag != "Bullet")
        {
            //bulletを消す
            Destroy(gameObject);
        }
    }
}
