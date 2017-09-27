using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    //当子弹与物体碰撞时
    void OnCollisionEnter(Collision other)
    {
        //如果碰撞的物体的Tag 是Player
        if (other.collider.tag == "Player")
        {
            //调用碰撞物体的TakeDamage，传递一个参数是10
            other.collider.SendMessage("TakeDamage", 10);
            Destroy(gameObject);  //销毁子弹
        }
    }
}
