using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
    private GameObject  _character;
    private Vector3     cameraPosition;

	// Use this for initialization
	void Start () { 
	}
	
	// Update is called once per frame
	void Update () {
        cameraPosition = _character.transform.position;
		cameraPosition = new Vector3(cameraPosition.x, _character.transform.position.y + 6, cameraPosition.z-6);
        gameObject.transform.position = cameraPosition;
	}

    //자신의 캐릭터를 받습니다.
    public void SetCharacter(GameObject character)
    {
        _character = character;
    }
}
