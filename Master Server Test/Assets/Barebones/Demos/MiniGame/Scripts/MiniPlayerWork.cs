using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MiniPlayerWork : NetworkBehaviour
{

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
	        tran.position=new Vector3(tran.position.x,tran.position.y+10,tran.position.z);
	    }
	}
}
