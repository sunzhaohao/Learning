using UnityEngine;
using System.Collections;

public class ZombieMotion : MonoBehaviour {

	private Animator animator;
	private string walk="Base Layer.walk";
	// Use this for initialization
	void Start () {
		animator=GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if(animator){
			 
			if(Input.GetKeyDown(KeyCode.T))
			{
				Debug.Log("turn back");
				animator.SetTrigger("turn");   // turn back
			}
			
			if(Input.GetKeyDown(KeyCode.J))
				animator.SetTrigger("attack"); // attack 
			
			if(Input.GetKeyDown(KeyCode.G))
				animator.SetBool("death",true); // dead
			if(Input.GetKeyDown(KeyCode.F))
				animator.SetBool("death",false);
		}
		 

		if(Input.GetKeyDown(KeyCode.A))
			transform.Rotate(0,-5,0);  // turn left
		if(Input.GetKeyDown(KeyCode.D))
			transform.Rotate(0,5,0);   // turn right
		
	}
}
