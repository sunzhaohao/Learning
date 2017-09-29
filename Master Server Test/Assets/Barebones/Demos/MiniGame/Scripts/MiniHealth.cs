using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MiniHealth : NetworkBehaviour
{
    public int health = 30;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int damage)
    {
        if (!isServer)   //如果不是服务端
        {
            return;
        }
        health -= damage;
        if (health <= 0)
        {
            Vector3 position = gameObject.GetComponent<Rigidbody>().position;
            //position.y += 10;
            gameObject.GetComponent<Rigidbody>().position = position;
            health = 30;
        }
             
    }
}
