using UnityEngine;
using System.Collections;

public class ButtonX : MonoBehaviour {
	public void close()
	{
		GameObject parent = transform.parent.gameObject;
		UI ui = parent.GetComponent<UI> ();
		ui.ButtonX ();
	}
	// Update is called once per frame
	void Update () {
	}

}
