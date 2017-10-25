using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour{
	protected ItemManager controller = null;
	protected OnClick onClick; 

	public void OnClick(){
		onClick.OnClick (controller);
	}


	// 만약 controller가 없다면 찾고나서 update를 false를 시켜줍니다.
	public void Update()
	{
		GameObject obj;
		if (controller == null) {
			if (obj = GameObject.Find ("GameController")) {
				controller = obj.GetComponent<ItemManager> ();
				if (controller != null && this is pickup) {
					controller.SetItem (transform.tag, this);
				}
			}
		} 
		}

	public void DestroyItem()
	{
		Destroy (gameObject);
	}
		
	//UI생성을 위한 함수
	protected UI_Info CreateUI(string name, GameObject UI)
	{
		UI_Info info;
		info.local = new Vector3 (0, 0, 0);
		info.name = name;
		info.Object = UI;

		Debug.Log ("생성완료 " + UI.name); 
		return info;
	}
}

