using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class pickup : Item {
	bool isCollision = false;
	GameObject Player;
	public GameObject itemSlot;

	ItemManager Manager;

	void Start () {
		itemSlot.SetActive(false);
	}

	void OnTriggerEnter(Collider other){
		if (other.tag.Equals ("Character")) {
			CharacterBase Char = other.gameObject.GetComponent<CharacterBase> ();
			CharacterInfo CharInfo = (CharacterInfo)Char.GetCharacter ();

			if ((int)Setting.GetInstance ().GetCharacter () == (int)CharInfo) {
				Player = other.gameObject;
				isCollision = true;
			}
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag.Equals ("Character")) {
			CharacterBase Char = other.gameObject.GetComponent<CharacterBase> ();
			CharacterInfo CharInfo = (CharacterInfo)Char.GetCharacter ();

			if ((int)Setting.GetInstance ().GetCharacter () == (int)CharInfo) {
				Player = null;
				isCollision = false;
			}
		}
	}
		
	void Update () {
		base.Update ();

		if (Input.GetButtonDown ("Fire1") && isCollision) {
			itemSlot.SetActive (true);

			controller.RemoveDictionary (transform.tag);

			Destroy (this.gameObject);
		}
	}
}