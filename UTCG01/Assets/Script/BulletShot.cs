using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShot : MonoBehaviour {

    [Header("弾丸")]
    public GameObject bulletPrefab;

    [Header("銃口")]
    public Transform muzzle;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        BulletCreate();

    }

    /// <summary>
    /// 弾丸を生成する
    /// </summary>
    void BulletCreate()
    {
        //指定したボタンが押された瞬間
        if (Input.GetButtonDown("Fire1"))
        {
            //弾丸を生成する
            Instantiate(bulletPrefab, muzzle.position,muzzle.rotation);
        }
    }
}
