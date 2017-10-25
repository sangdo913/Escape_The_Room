using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ChattingManager : GameController {
    private ChattingMessage[] messages;
	private GameObject chatWindows;
	public GameObject chat;

    public override void SetClient(Client client)
    {
        base.SetClient(client);

		Command Chat_AddMessage = new Chat_AddMessage (this);

		m_client.SetCommand(CommandType.Chat_AddMessage, Chat_AddMessage);
        m_client.RegisterReceiveNotification(PacketId.ChatMessage, OnReceiveMessage);
    }

    // Use this for initialization
    void Start () {
        messages = new ChattingMessage[4];

		chat = Resources.Load<GameObject> ("Prefabs/UI/Chatting/Chat");

		chatWindows = new  GameObject("Chatting_Windows");
		chatWindows.AddComponent<RectTransform> ();
		chatWindows.transform.parent = m_client.GetTransform (CommandType.UI_GetRoot);
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0 ; i <4;  i++)
        {
			if (messages [i] != null) {
				messages[i].SetPosition(GetPosition((CharacterInfo)i));

				if (messages [i].isDestroy()) {
					Debug.Log ("delete");
					messages [i].Destroy ();
					messages [i] = null;
				}

				else {
					messages [i].TimeCounter ();
			} 
			}
        }	
	}

	/*
    void OnGUI()
    {
        // 표현되어야할 채팅메시지가 있다면 화면에 표시해줍니다.
        if (messages != null)
        {
            foreach (ChattingMessage chat in messages)
            {
                if (chat != null)
                {
                    
                }
            }
        }
    }
    */

    //채팅 메시지 목록을 추가합니다.
    public void AddMessage(string message, CharacterInfo info)
	{

		//메세지 재구성
		string Message = "[" + info + "]\n" + message;

		if ((int)info < 4) {

			//전 메세지 삭제
			if (messages [(int)info] != null) {
				messages [(int)info].Destroy ();
			}


			//채팅창을 만들고 위치를 조정합니다.
			GameObject ChatWindow = Instantiate (this.chat, chatWindows.transform) as GameObject;
			RectTransform ChatRect = ChatWindow.GetComponent<RectTransform> ();

			ChatRect.position = GetPosition(info);

			Text text = ChatRect.GetComponentInChildren<Text> ();
			text.text = Message;

			//말풍선 관리 배열에 넣어줍니다.
			ChattingMessage chat = new ChattingMessage (ChatWindow);
			int character = (int)info;
			messages [(int)info] = chat;

			//자기가 친 채팅의 경우 서버로 전송
			if (Setting.GetInstance ().GetCharacter () == info)
				SendMessage (message, info);
		}
	}

	private Vector3 GetPosition(CharacterInfo info)
	{

		//캐릭터의 주소를 받아옵니다.
		RectTransform rectTransform = m_client.GetRect (CommandType.Char_GetRect, info);
		Vector3 position = Camera.main.WorldToScreenPoint (rectTransform.position);
		return position;
	}

    public void SendMessage(string msg, CharacterInfo info)
    {
        //메시지를 서버로 보냅니다.
        ChatMessage chatMessage = new ChatMessage();
        chatMessage.Char = info;
        chatMessage.message = msg;

        m_client.SendReliable(0, new ChatPacket(chatMessage));

    }

    //서버에서 데이터를 받았을 시 그 채팅 내용을 채팅목록에 추가.
    public void OnReceiveMessage(int i, byte[] data)
    {
        ChatPacket packet = new ChatPacket(data);
        ChatMessage chat = packet.GetPacket();

        string message = chat.message;
        CharacterInfo info = chat.Char;

		m_client.execute(CommandType.UI_Chat_Write,  message, info);
    }
}

//채팅메시지의 정보를 담고있는 클래스입니다.
public class ChattingMessage
{
    private float timer = 8.0f;
	private GameObject Chat;
	/*
    MessageInfo Info;
    public struct MessageInfo
    {
        public string message;
        public CharacterInfo Char;
    }
*/

    public ChattingMessage(string message, CharacterInfo character)
    {
        //Info.message = message;
        //Info.Char = character;

    }

	public ChattingMessage(GameObject Chat)
	{
		this.Chat = Chat;
	}

    public ChattingMessage()
    {
    }

	/*
    public MessageInfo GetMessageInfo()
    {
        return Info;
    }
    */

    public void TimeCounter()
    {
        timer -= Time.deltaTime;
    }

    public bool isDestroy()
    {
        return timer < 0;
    }

	public void Destroy()
	{
		GameObject.Destroy (Chat);
		Debug.Log ("호출);");
	}

	public void SetPosition(Vector3 position)
	{
		RectTransform rect = Chat.GetComponent<RectTransform> ();
		if(Vector3.Distance(rect.position, position) > 10)
			rect.position = position;
	}
}