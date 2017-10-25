using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIController : GameController {
    private GameObject UIRoot;
    private UI[] UI;

    GameObject[] UIPrefabs = new GameObject[(int)UIType.length];
	Dictionary<string, GameObject> UIs;

    ////UI의 delegate들입니다.
    //public delegate void Chat_Activate();

	/*
    private Chat_Activate m_ChatActivate;
    //UI들의 delegate 등록

    public Chat_Activate UIChatActivate
    {
        get { return m_ChatActivate; }
        set { m_ChatActivate = value; }
    }

    public Client.UI_ChatWrite WriteMessage
    {
        get { return m_WriteMessage; }
        set { m_WriteMessage = value; }
    }

    //채팅창을 활성화시킵니다.
    public void ChatActivate(Client.UI_ChatActivate Activate)
    {
        if (m_client == null) Debug.Log("itis null");
        m_client.ChatActivate = Activate;
    }

    //채팅창을 칠 수 있도록 합니다.
    public void ChatWrite(Client.UI_ChatWrite Write)
    {
        m_client.ChatWrite = Write;
    }

    */


	//UI안의 기능들입니다.
	Dictionary<int,Command> commands;

	//UI 커맨드를 등록합니다.
	public void SetCommand(CommandType type, Command command)
	{
		commands.Add ((int)type, command);

		//클라이언트에도 등록해줍니다.
		m_client.SetCommand (type, command);
	}

	public void execute(CommandType type)
	{
		commands [(int)type].execute ();
	}

	public void execute(CommandType type, string message)
	{
		commands [(int)type].execute (message);
	}
	public void execute(CommandType type, string message, CharacterInfo info)
	{
		commands [(int)type].execute (message, info);
	}


    public override void SetClient(Client client)
    {
        base.SetClient(client);
    }

    void Awake()
    {
        UIRoot = GameObject.Find("UI");

        UI = new UI[(int)UIType.length];
    }

	// Use this for initialization
	void Start ()
	{
		commands = new Dictionary<int,Command>();
		UIs = new Dictionary<string,GameObject> ();

        // UI들을 만들어냅니다.
        for (int i = 0; i < (int)UIType.length; i++)
        {
            string Path = "Prefabs/" + "UI/" + (UIType)i + "/" + (UIType)i + "Window";

            UIPrefabs[i] = Resources.Load(Path) as GameObject;
            UIPrefabs[i] = Instantiate(UIPrefabs[i]);
            UIPrefabs[i].transform.parent = UIRoot.transform;
            UI[i] = UIPrefabs[i].GetComponent<UI>();
            UI[i].SetUIController(this);

			//클라이언트에도 명령어를 등록해 줍니다.
			m_client.SetCommand (CommandType.UI_Create, new UI_Create (this));
			m_client.SetCommand (CommandType.UI_Delete, new UI_Delete (this));
			m_client.SetCommand (CommandType.UI_GetRoot, new UI_GetRoot (this));
        }
    }
	
	// Update is called once per frame
	void Update () {	   
	}

    public UI GetUI(int i)
    {
        return UI[i]; 
    }
    //UI에서 처리하는 부분입니다.

    //Cleint에서 정보를 가져옵니다.

    //마우스가 UI에 들어갔을 경우와 나왔을 경우입니다.
    public void OnMouseEnter()
    {
		m_client.execute(CommandType.Input_OnMouseEnter);
    }
    public void MouseOut()
    {
		m_client.execute(CommandType.Input_MouseOut);
    }

	public void StopInput()
	{
		m_client.StopInput ();
	}

	public void StartInput()
	{
		m_client.startInput ();
	}

    public GameObject GetUIRoot()
    {
        return UIRoot;
    }

    public void SendMessage(string message, CharacterInfo info)
    {
		m_client.execute(CommandType.Chat_AddMessage, message, info);
    }

	public void StopQuiz(string name)
	{
		m_client.execute (CommandType.Quiz_StopQuiz, name);
	}


	//UI를 생성합니다.
	public UI CreateUI(UI_Info content)
	{
		GameObject obj = Instantiate (content.Object, UIRoot.transform) as GameObject;
		RectTransform rect = obj.GetComponent<RectTransform> ();
		rect.localPosition = content.local;

		return obj.GetComponent<UI>();
	}

	//UI를 지웁니다.
	public void DeleteUI(string name)
	{
	}

	public Transform GetUI()
	{
		return UIRoot.transform;
	}
}
