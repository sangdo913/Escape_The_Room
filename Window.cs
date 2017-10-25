using UnityEngine;
using System.Collections;

public class Window : MonoBehaviour {
	public void OnClick()
	{
		GameObject.Destroy (this.gameObject);
	}
}
