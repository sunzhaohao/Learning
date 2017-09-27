using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MiniPlayerWork : NetworkBehaviour
{

    public GameObject fireBallPref;
    public Transform tran;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
        // ignore other player's input
	    if (!isLocalPlayer)
	        return;
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
	        CmdFire();
	    }
	}

    [Command]
    void CmdFire()
    {
        Vector3 pos = tran.position;
        pos.y += 2;
        GameObject bullet=Instantiate(fireBallPref,pos,Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody>().velocity = tran.forward * 2;
        Destroy(bullet,2);
        NetworkServer.Spawn(bullet);
    }
}
