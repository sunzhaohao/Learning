using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenProcess : MonoBehaviour {

    [SerializeField]
    private InputField account;
    [SerializeField]
    private InputField password;

    // Use this for initialization
    void Start() {

    }

    public void loginOnClick()
    {
        if (account.text == string.Empty || password.text == string.Empty)
            return;
        if (account.text == "sunzhao" &&  password.text=="sunzhao")
            Debug.Log("login in");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
