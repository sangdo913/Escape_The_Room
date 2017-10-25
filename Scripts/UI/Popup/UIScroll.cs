using UnityEngine;
using System.Collections;

public class UIScroll : UI {
	AudioSource Sound;
	public override void ButtonX ()
	{
		GameObject obj = GameObject.Find ("scroll");
		obj.GetComponent<Scroll> ().scisclicked = false;
		Destroy (gameObject);
	}

	void Awake()
	{
		Sound = gameObject.GetComponent<AudioSource> ();
		Sound.PlayOneShot (Sound.clip);
	}
}
