using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Scroll : Item
{
	public GameObject mytext;
	public GameObject text2;
	public GameObject text3;
	public GameObject bold;
	public GameObject UI_Scroll;

	UI 	isPopup;

	public bool scisclicked;
	bool a;
	Client m_client;

	public void Awake(){
		scisclicked = false;
		isPopup = null;
		onClick = new ScIcnClk (this);

		bold.SetActive (false);
	}

	public void Update(){
		base.Update ();
		try {
			a = System.Convert.ToBoolean (mytext.GetComponent<Text> ().text);
		} catch (FormatException) {
		}

		if (scisclicked) {
			if (a) {
				bold.SetActive (true);
			
				if (isPopup == null) {
				UI_Info info = CreateUI ("Scroll", UI_Scroll);
				isPopup = controller.CreateUI (info);
				}


			}else{
				bold.SetActive (false);
				if(isPopup != null) isPopup.ButtonX ();
			} 
		}else{
			bold.SetActive (false);
			if(isPopup != null) isPopup.ButtonX ();	
		}
	}
}


