using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChattingLog : UI {
    Text text;
    Chatting m_chatting;
    Scrollbar scroll;

	public override void ButtonX ()
	{
	}

    void Awake()
    {
        text = gameObject.GetComponentInChildren<Text>();
        scroll = gameObject.GetComponentInChildren<Scrollbar>();
        
    }
	// Use this for initialization
	void Start () {
        scroll.value = 1;
	}

    public void OnClick()
    {
        Destroy(gameObject);
    }

    public void Display(string Log)
    {
        text.text = Log;
    }

    public void SetChatting(Chatting chatting)
    {
        m_chatting = chatting;
    }

	public void OnMouseEnter()
	{
		m_chatting.OnMouseEnter();
	}

	public void PointerExit()
	{
		m_chatting.OnPointerExit();
	}

}
