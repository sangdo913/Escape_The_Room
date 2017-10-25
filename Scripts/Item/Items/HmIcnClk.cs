using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;


public class HmIcnClk : OnClick {
	Hammer ham;
	ItemManager manager;
	GameObject obj;
	string t1;
	int t2;
	bool t3;



	public HmIcnClk(Hammer controller)
	{
		this.ham = controller;
	}

	void Start(){
		obj = ham.bold;

	}
	public void OnClick(GameController controller)
	{
		Debug.Log ("클ㅣㄱ");
		//controller.gameObject.GetComponent<Scroll> ().bold.SetActive (false);
		ham.hmisclicked = !ham.hmisclicked;

		ham.mytext.GetComponent<Text> ().text = "" + ham.hmisclicked;
		ham.text2.GetComponent<Text> ().text = "False";
		ham.text3.GetComponent<Text> ().text = "False";
		UI_Info info;
		info.local = new Vector3 (0, 0, 0);


		/*if (ham.isclicked) {
			ham.bold.SetActive (true);
		} else
			ham.bold.SetActive (false);
		*/
	}


}

public class ScIcnClk : OnClick {
	Scroll scrl;
	GameObject obj;
	public ScIcnClk(Scroll controller)
	{
		this.scrl = controller;
	}
	void Start(){
		obj = scrl.bold;

	}

	public void OnClick(GameController controller)
	{
		Debug.Log ("두루마리 클ㅣㄱ");




		scrl.scisclicked = !scrl.scisclicked;

		scrl.mytext.GetComponent<Text> ().text = "" + scrl.scisclicked;
		scrl.text2.GetComponent<Text> ().text = "False";
		scrl.text3.GetComponent<Text> ().text = "False";


		/*if (scrl.isclicked) {
			scrl.bold.SetActive (true);
		} else
			scrl.bold.SetActive (false);
	*/
	}


}

public class PpIcnClk : OnClick {
	Paper pp;
	GameObject obj;

	public PpIcnClk(Paper controller)
	{
		this.pp = controller;
	}
	void Start(){
		obj = pp.bold;
	}

	public void OnClick(GameController controller)
	{
		pp.isclicked = !pp.isclicked;

		pp.mytext.GetComponent<Text> ().text = "" + pp.isclicked;
		pp.text2.GetComponent<Text> ().text = "False";
		pp.text3.GetComponent<Text> ().text = "False";

	/*
		if (pp.isclicked) {
			pp.bold.SetActive (true);
		} else
			pp.bold.SetActive (false);
	*/
	}



}