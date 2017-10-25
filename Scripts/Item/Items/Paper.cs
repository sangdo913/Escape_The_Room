using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Paper : Item
{
	public GameObject mytext;
	public GameObject text2;
	public GameObject text3;
	public GameObject bold;

	public GameObject UI_Paper;
	public bool isclicked;
	bool a;
	UI  isPopup;
	public void Awake(){
		isclicked = false;
		onClick = new PpIcnClk (this);
		isPopup = null;
		bold.SetActive (false);
	}

	public void Update(){
		base.Update ();
		try {
			a = System.Convert.ToBoolean (mytext.GetComponent<Text> ().text);
		} catch (FormatException) {
		}

		if (isclicked) {
			if (a) {
				bold.SetActive (true);

				if (isPopup == null) {
					UI_Info info = CreateUI ("Paper", UI_Paper);
					isPopup = controller.CreateUI (info);
				}
			} else {
				bold.SetActive (false);
				if(isPopup != null) isPopup.ButtonX ();
			}
		} else {
			bold.SetActive (false);
			if(isPopup != null) isPopup.ButtonX ();
		}
	}

}


