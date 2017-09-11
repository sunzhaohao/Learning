using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenProcess : MonoBehaviour {

    [SerializeField]
    private InputField account;
    [SerializeField]
    private InputField password;

    [SerializeField]
    private Button loginBtn;
    // Use this for initialization

    [SerializeField]
    private GameObject registerPanel;

    [SerializeField]
    private InputField regAccount;
    [SerializeField]
    private InputField regPassword;
    [SerializeField]
    private InputField repeatPass;

    void Start() {
        registerPanel.active = false;
    }

    // login  in 
    public void loginOnClick()
    {
        if (account.text == string.Empty || password.text == string.Empty)
            return;
        if (account.text == "sunzhao" &&  password.text=="sunzhao")
            Debug.Log("login in");
        // loginBtn.enabled = false;
        loginBtn.interactable = false;
    }
	
    //show  register account  panel
    public void registerOnClick()
    {
        registerPanel.active = true;
    }
    // close register panel
    public void closeRegisterPanelOnClick()
    {
        registerPanel.SetActive(false);
    }
    // register account
    public void registerAccOnClick()
    {
        if(regAccount.text==string.Empty || regPassword.text == string.Empty)
        {
            Debug.Log("can not be null ！");
            return;
        }
        if (regPassword.text != repeatPass.text)
        {
            Debug.Log("two passwords are different!");
            return;
        }
        Debug.Log("registed successfully");
        registerPanel.active = false;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
