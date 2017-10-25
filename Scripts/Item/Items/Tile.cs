using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
public class Tile : MonoBehaviour { 

	GameObject Player;
	GameObject Paper;
	GameObject hammertxt;
	AudioSource Sound;
	Client m_client;

	public bool isCollision;
	bool isHammer;

	void OnTriggerEnter(Collider other){

		if (other.tag.Equals("Character")){
			Player = other.gameObject;
			isCollision = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag.Equals("Character")) {
			Player = null;
			isCollision = false;
		}
	}

	void Start(){
		try{
			Paper = GameObject.Find ("Paper");
			hammertxt = GameObject.Find("hammer_txt");
			GameObject obj = GameObject.Find("BreakTile");
			Sound = obj.GetComponent<AudioSource> ();
			m_client = null;
		}
		catch(NullReferenceException){
		}

		//Paper.gameObject.transform.position = new Vector3 (20f, 0, 0);
	}

	void Update(){
	 	if (m_client == null) {
			GameObject obj = GameObject.Find ("GameController");
			if (obj != null) {
				m_client = obj.GetComponent<Client> ();
				m_client.RegisterReceiveNotification (PacketId.BreakTile, OnReceiveBreakTile);
			}
			
		}

			try{
			isHammer = System.Convert.ToBoolean (hammertxt.GetComponent<Text> ().text);
		}
		catch(FormatException){
		}

		if (Input.GetButtonDown ("Fire1") && isCollision && isHammer) {
			BreakTile tile;
			tile.Act = true;
			Sound.PlayOneShot (Sound.clip);
			BreakTilePacket packet = new BreakTilePacket (tile);
			m_client.SendReliable (0, packet);
			Paper.gameObject.transform.position = new Vector3 (-4.2072f,-3.3f,3.33278f);
			Destroy (gameObject);


		}
	}

	public void OnReceiveBreakTile(int node, byte[] data)
	{
		BreakTilePacket packet = new BreakTilePacket (data);
		BreakTile tile = packet.GetPacket ();
		bool act = tile.Act;

		if (act) {
			Sound.PlayOneShot (Sound.clip);
			Paper.gameObject.transform.position = new Vector3 (-4.2072f,-3.3f,3.33278f);
			Destroy (gameObject);
		}
	}

}



