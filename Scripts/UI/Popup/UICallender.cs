using UnityEngine;
using System.Collections;

public class UICallender : UI {
	AudioSource Sound;
	public override void ButtonX ()
	{
		Destroy (gameObject);
		m_UI.StopQuiz ("Callender");
	}

	void Awake()
	{
		Sound = gameObject.GetComponent<AudioSource> ();
		Sound.PlayOneShot (Sound.clip);
	}
}
