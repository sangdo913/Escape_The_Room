using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Hammer : Item
{
	public GameObject mytext;
	public GameObject text2;
	public GameObject text3;
	public GameObject bold;

	public bool hmisclicked;
	bool a;

	public void Awake(){
		hmisclicked = false;

		onClick = new HmIcnClk (this);

		bold.SetActive (false);
	}

	public void Update(){
		try {
			a = System.Convert.ToBoolean (mytext.GetComponent<Text> ().text);
		} catch (FormatException) {
		}

		if (hmisclicked) {
			if (a)
				bold.SetActive (true);
			else
				bold.SetActive (false);
		} else
			bold.SetActive (false);
	}

}

