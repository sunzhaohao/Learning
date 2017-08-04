using UnityEngine;
using System.Collections;

public class ZombieManager : MonoBehaviour {

	private Animator m_animator;
	private string attack="attack";
	private string speed="speed";

	enum ZombieState{
		Walk,
		Attack,
		Idle,
		Turn,
		None
	};

	private ZombieState z_State=ZombieState.Walk;
	// Use this for initialization
	void Start () {
		m_animator=GetComponent<Animator>();
		m_animator.SetFloat(speed,1f);
	}
	
	// Update is called once per frame
	void Update () {
		
		ZombieState state=getState();
		switch(state){
		case ZombieState.Idle:
			m_animator.SetFloat(speed,0f);
			break;
		case ZombieState.Walk:
			m_animator.SetFloat(speed,1f);
			break;
		case ZombieState.Attack:
			Debug.Log("attack");
			m_animator.SetFloat(speed,0.2f);
			m_animator.SetBool("go",false);
			m_animator.SetTrigger(attack);

			break;
		case ZombieState.Turn:
			break;

		default:
			break;
		}
		if(state==ZombieState.None)
			return ;
		z_State=state;
	}

	private ZombieState getState(){
		ZombieState state=ZombieState.None;

		if(Input.GetKeyDown(KeyCode.J))
			state=ZombieState.Attack;
		else if(Input.GetKeyDown(KeyCode.M))
			state=ZombieState.Walk;
		else if(Input.GetKeyDown(KeyCode.P))
			state=ZombieState.Idle;
		 

		return state;
	}
	void FixedUpdate(){
		/*if(Input.GetKeyDown(KeyCode.J)){
			Debug.Log("zombie attack");
			m_animator.SetTrigger(attack);
		}
		if(Input.GetKeyDown(KeyCode.S))
			m_animator.SetFloat(speed,0);
			*/
	}
}
