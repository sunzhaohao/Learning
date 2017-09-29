using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //当子弹与物体碰撞时
    void OnCollisionEnter(Collision other)
    {
        //如果碰撞的物体的Tag 是Player
        if (other.collider.tag == "user")
        {
            //调用碰撞物体的TakeDamage，传递一个参数是10
            other.collider.SendMessage("TakeDamage", 10);
            Destroy(gameObject);  //销毁子弹
        }
    }
}
