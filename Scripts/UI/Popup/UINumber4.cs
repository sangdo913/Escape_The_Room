using UnityEngine;
using System.Collections;

public class UINumber4 : UI {
	AudioSource Sound;
	public override void ButtonX ()
	{
		GameObject obj = GameObject.Find ("paper");
		obj.GetComponent<Paper> ().isclicked = false;
		Destroy (gameObject);
	}

	void Awake()
	{
		Sound = gameObject.GetComponent<AudioSource> ();
		Sound.PlayOneShot (Sound.clip);
	}
}
