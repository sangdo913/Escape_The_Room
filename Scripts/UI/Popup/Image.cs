using UnityEngine;
using System.Collections;

public class Image : UI {
	//닫는 창입니다.
	public override void ButtonX ()
	{
		Destroy (gameObject);
	}
}
