using UnityEngine;
using System.Collections;

public class TestItem : Item {
	public TestItem(ItemManager manager)
	{
		controller = manager;
	}

	// Use this for initialization
	public void Start (){

		if (onClick == null) {
			onClick = new Nothing ();		
		}
	}
}