using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;

public class KeyPad : UI {
	string password = "";

	private Text[] texts;
	// Use this for initialization
	public void Awake()
	{
		texts = new Text[4];
		Text[] temp = gameObject.GetComponentsInChildren<Text> ();
		int count = 0;

		foreach (Text t in temp) {
			if(t.transform.parent.name.Equals("Password"))
			{
				texts [count] = t;
				count++;
			}
			if (count == 4)
				break;
		}
	}


	//닫는 창입니다.
	public override void ButtonX ()
	{
		m_UI.StopQuiz ("WhatYear");
		Destroy (gameObject);
	}

	//패스워드를 받습니다.
	public void GetPasswrod(string password)
	{
		if (this.password.Length < 4) {
			texts [this.password.Length].text = "*";
			this.password += password;
		} else {
			texts [0].text = "*";
			for (int i = 1; i < 4; i++) {
				texts [i].text = "";
			this.password = password;
			}
		}
	}

	//password를 지웁니다.
	public void Clear()
	{
		for (int i = 0; i < 4; i++) {
			texts [i].text = "";
			password = "";
		}
	}

	//패스워드를 반환합니다.
	public string ReturnPassword()
	{
		return password;
	}
}