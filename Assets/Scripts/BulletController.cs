using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("敵に衝突");
            Destroy(this.gameObject);
        }
        if (col.gameObject.tag == "Field")
        {
            Debug.Log("地面に衝突");
            Destroy(this.gameObject);
        }
    }
}
