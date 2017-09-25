using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;

public class NewBehaviourScript : MonoBehaviour {

    public GameObject obj;

	// Use this for initialization
	void Start () {
		obj.OnTriggerEnterAsObservable()
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
