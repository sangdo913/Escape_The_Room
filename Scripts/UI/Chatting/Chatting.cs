using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Queue
{
    private int start = 0;
    private int count = 0;
    private int length = 0;
    private string[] queue;

    public Queue(int num)
    {
        queue = new string[num];
    }

    public void EnQueue(string str)
    {
        queue[count % 100] = str;
        count++;
        length++;
        if (count > 100) count %= 100;
        if (length > 100)
        {
            length = 100;
            start++;
        }
    }

    public string DeQueue()
    {
        if (length > 0)
        {
            count--;
            length--;
            return queue[count];
        }

        else return null;
    }

    public int Length()
    {
        return length;
    }

    //큐에서 빼내지 않고 읽기만 합니다.
    public void ReadAll(out string str)
    {
        int flag = start;
        string result = "";

        for (int i = 0; i < length; i++)
        {
            result += queue[flag % 100] + "\n";
            flag++;
        }

        str = result;
    }
}



public class Chatting : UI {
	public override void ButtonX ()
	{
	}

    string ChattingLog = "";
    string message= "";

    bool chatAble = true;
    bool isLogActive = false;

    float ratioX, ratioY;
    float x = 1024.0f;
    float y = 768.0f;

    Queue chatLog;

    float fontSize = 23;

    InputField field;
    RectTransform UIRect;
    RectTransform log_rect;

    public GameObject LogWindow;
	public GameObject Chat;

    GameObject openedLogWindow;

    void Awake()
    {
        field = GetComponentInChildren<InputField>();
        chatLog = new Queue(100);

        log_rect = LogWindow.GetComponent<RectTransform>();
    }
    // Use this for initialization
    void Start ()
    {
    }
		

    public override void SetUIController(UIController ui)
    {
        base.SetUIController(ui);

        // 부모 등록
        UIRect = transform.parent.GetComponent<RectTransform>();
        transform.parent = UIRect.transform;

        // 커맨드 등록
		Command ChatActive = new UI_Chat_Active(this);
		m_UI.SetCommand (CommandType.UI_Chat_Activate, ChatActive);
		m_UI.SetCommand(CommandType.UI_Chat_Write, new UI_Chat_Write (this));
    }

    // Update is called once per frame
    void Update () {
        // 채팅창의 크기를 조절합니다.
        ratioX = ((float)Screen.width) / x;
        ratioY = ((float)Screen.height) / y;
        transform.localScale = new Vector3(ratioX, ratioY, 1);
        
        if (!field.isFocused)
        {
            chatAble = true;
        }

        if(isLogActive == true && openedLogWindow == null)
        {
            isLogActive = false;
			m_UI.StartInput ();
        }

        //로그창에 대화 내용을 전달합니다.
        if (isLogActive)
        {
            if(openedLogWindow != null)
            openedLogWindow.GetComponent<ChattingLog>().Display(ChattingLog);

        }

		if (isLogActive) {
			m_UI.StopInput ();
		}
    }

    public void Activate()
    {
        if (!field.isFocused && chatAble)
        {
            field.ActivateInputField();
        }

        else field.DeactivateInputField();
    }

    public void OnEnter()
    {
        //인풋 필드 초기화
        message = field.text;

        WriteMessage(message, Setting.GetInstance().GetCharacter());
    }

    //채팅 메시지를 로그에 기록합니다.
    public void WriteMessage(string message, CharacterInfo info)
    {
        //채팅이 빈 공간이 아닐 경우
        if (!message.Equals(""))
        {
            string Message = "[" + info + "] : " + message;
            chatLog.EnQueue(Message);
            //채팅 내용을 ChattingManager에 전달합니다.
            m_UI.SendMessage(message, info);
        }

        message = "";
        field.text = "";

        chatLog.ReadAll(out ChattingLog);

        chatAble = false;
    }

    //로그창을 만듭니다.
    public void CreateLog()
    {
        if (!isLogActive)
        {
            //여기서 m_client에게 chatLog 요구하는 함수 필요. 그렇담 요구하면 거기서 string return하자.
            openedLogWindow = Instantiate(LogWindow);
            openedLogWindow.transform.parent = transform.parent;
            openedLogWindow.GetComponent<ChattingLog>().SetChatting(this);

            //위치를 조정합니다.
            RectTransform rect = openedLogWindow.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(0, 0);

            //rect.sizeDelta = new Vector2(800 * ratioX, 600 * ratioY);          

            isLogActive = true;
        }
    }

    public void OnMouseEnter()
    {
        m_UI.OnMouseEnter();
    }

    public void OnPointerExit()
    {
        m_UI.MouseOut();
    }
}
