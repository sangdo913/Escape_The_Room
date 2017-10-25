using UnityEngine;
using System.Collections;


//UI 정보입니다.
public struct UI_Info
{
	public string name;
	public GameObject Object;
	public Vector3 local;
} 



public abstract class UI : MonoBehaviour
{
    protected UIController m_UI;

	public abstract void ButtonX (); // 버튼 X가 눌릴 시 불려지는 함수입니다.
    public virtual void SetUIController(UIController ui)
    {
         m_UI= ui;
    }

	void Start()
	{
		GameObject obj = GameObject.Find ("GameController");
		m_UI = obj.GetComponent<UIController> ();
	}

	void Update()
	{
		transform.localScale = new Vector3 (Setting.Width_Ratio (), 1, Setting.Height_Ratio ());
	}
}

public enum UIType
{
    Chatting= 0,
    length
}

//GameController 정보입니다.
public struct Info_Controller
{
    public static int length = (int)ControllerType.length;
}

public enum ControllerType
{
    Character = 0,
    Input,
    UI,
    Chating,
	Quiz,
	Item,
    length
}
