using UnityEngine;
using System.Collections;

public class follow : MonoBehaviour {
	private Vector3 offset;
	public Transform player;

	// Use this for initialization
	void Start () {
		offset=player.position-transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, player.position - offset, Time.deltaTime * 5);
		Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);

		Quaternion target = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3f);
		transform.rotation.Set(target.x,target.y,target.z,target.w);

		if(Input.GetKey(KeyCode.R))
			transform.Rotate(new Vector3(1,0,0));
		if(Input.GetKey(KeyCode.Q))
			transform.Rotate(new Vector3(-1,0,0));
	}
}
