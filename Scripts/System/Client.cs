using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;

public class Client : MonoBehaviour {
    GameController[] controllers = new GameController[(int)ControllerType.length];
    Network m_network;

    //서버 정보입니다.
    string m_hostname;
    IPEndPoint endpoint;

	/*
    //CharacterController 델리게이트들입니다.
    public delegate Vector3 Character_GetCharPosition(int i);
    public delegate RectTransform Character_GetRect(CharacterInfo info);
    public delegate void Character_GetInfo(InputInfo info);
    public delegate void Character_CameraSetting(CharacterInfo info);
    public delegate void Character_Jump();
    public delegate void Character_Move();

    //델리게이트들을 등록합니다.
    public Character_GetCharPosition GetCharacterPosition
    {
        get { return m_GetCharPosition; }
        set { m_GetCharPosition = value; }
    }

    public Character_GetRect GetRect
    {
        get { return m_GetRect; }
        set { m_GetRect = value; }
    }

    public Character_GetInfo GetInfo
    {
        get { return m_GetInfo; }
        set { m_GetInfo = value; }
    }

    public Character_CameraSetting CameraSetting
    {
        get { return m_CameraSetting; }
        set { m_CameraSetting = value; }
    }

    public Character_Jump Jump
    {
        get { return m_Jump; }
        set { m_Jump = value; }
    }

    public Character_Move Move
    {
        get { return m_Move; }
        set { m_Move = value; }
    }

    private Character_GetCharPosition m_GetCharPosition;
    private Character_GetRect m_GetRect;
    private Character_GetInfo m_GetInfo;
    private Character_CameraSetting m_CameraSetting;
    private Character_Jump m_Jump;
    private Character_Move m_Move;

    //InputController 델리게이트들입니다.
    public delegate void Input_OnMouseEnter();
    public delegate void Input_MouseOut();

    private Input_OnMouseEnter m_OnMouseEnter;
    private Input_MouseOut m_MouseOut;

    public Input_OnMouseEnter OnMouseEnter
    {
        get { return m_OnMouseEnter; }
        set { m_OnMouseEnter = value; }
    }

    public Input_MouseOut MouseOut
    {
        get { return m_MouseOut; }
        set { m_MouseOut = value; }
    }

    //UIController 델리게이트들입니다.

    public delegate void UI_ChatActivate();
    public delegate void UI_ChatWrite(string message, CharacterInfo info);

    private UI_ChatActivate m_ChatActivate;
    private UI_ChatWrite    m_ChatWrite;

    public UI_ChatActivate ChatActivate
    {
        get { return m_ChatActivate; }
        set { m_ChatActivate = value; }
    }

    public UI_ChatWrite ChatWrite
    {
        get { return m_ChatWrite; }
        set { m_ChatWrite = value; }
    }

	


    //ChatMenager 델리게이트들입니다.
    public delegate void Chat_AddMessage(string message, CharacterInfo info);

    private Chat_AddMessage m_AddMessage;

    public Chat_AddMessage AddMessage
    {
        get { return m_AddMessage; }
        set { m_AddMessage = value; }
    }
*/
	//명령어들이 들어있는 함수입니다.
	private Dictionary<int, Command> commands;

	public void SetCommand(CommandType type, Command command)
	{
		int commandType = (int) type;
		commands.Add (commandType, command);
	}

	//커맨드 실행 함수들
	public void execute(CommandType type)
	{
		int commandType = (int)type;
		commands [commandType].execute ();
	}

	public void execute(CommandType type, string str)
	{
		int commandType = (int)type;
		commands [commandType].execute (str);
	}

	public void execute(CommandType type, UI_Info info)
	{
		int commandType = (int)type;
		commands [commandType].execute (info);
	}

	public void execute(CommandType type, int i)
	{
		int commandType = (int)type;
		commands [commandType].execute (i);
	}

	public void execute(CommandType type, string message, CharacterInfo info)
	{
		commands [(int)type].execute (message, info);
	}

	public void execute(CommandType type, InputInfo info)
	{
		commands [(int)type].execute (info);
	}

	public void execute(CommandType type, CharacterInfo info)
	{
		commands [(int)type].execute (info);
	}

	public Vector3 GetVector3(CommandType type, int i)
	{
		return commands[(int)type].GetVector3(i);
	}

	public UI GetObject(CommandType type, UI_Info info)
	{
		return commands[(int)type].GetObject(info);
	}

	public RectTransform GetRect(CommandType type, CharacterInfo info)
	{
		return commands[(int)type].GetRect(info);
	}

	public Transform GetTransform(CommandType type)
	{
		return commands [(int)type].GetTransform ();
	}

    void Awake()
    {
		commands = new Dictionary<int,Command> ();
    }

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void SetControllers()
    {
        GameObject obj;
        obj = GameObject.Find("GameController");

        if (obj == null)
        {
            obj = new GameObject("GameController");            
            
        }

        if(obj.GetComponent<CharacterController>() == null)
        {
            obj.AddComponent<CharacterController>();
        }

        if(obj.GetComponent<InputController>() == null)
        {
            obj.AddComponent<InputController>();
        }

        if(obj.GetComponent<UIController>() == null)
        {
            obj.AddComponent<UIController>();
        }

        if (obj.GetComponent<ChattingManager>() == null)
        {
            obj.AddComponent<ChattingManager>();
        }

		if (obj.GetComponent<QuizManager> () == null) {
			obj.AddComponent<QuizManager> ();
		}

		if (obj.GetComponent<ItemManager> () == null) {
			obj.AddComponent<ItemManager> ();
		}

        controllers = obj.GetComponents<GameController>();

        if (controllers != null)
        {
            for (int i = 0; i < (int)ControllerType.length; i++)
            {
                controllers[i].SetClient(this);
            }
        }

    }

	public void StopInput()
	{
		(controllers[(int)ControllerType.Input] as InputController).StopInput();
	}

	public void startInput()
	{
		(controllers[(int)ControllerType.Input] as InputController).StartInput();
	}

    public void SetController(ControllerType controll, GameController controller)
    {
        int i = (int)controll;
        controllers[i] = controller;
    }

    public GameController GetController(ControllerType controll)
    {
        int i = (int)controll;
        return controllers[i];
    }

    public void SetNetwork(Network network)
    {
        m_network = network;
    }

	public void ConnectServer(string server)
    {
        //호스트 이름을 얻는다.
        m_hostname = Dns.GetHostName();


        //서버 연결
        int serverNode = -1;
        if (m_network != null)
        {
            serverNode = m_network.Connect(server, NetConfig.MATCHING_SERVER_PORT);

            //서버에 연결되었으면 서버에게 정보를 받았을 때 작동하는 함수를 등록한다.
                     

            Setting.SetEP(m_network.GetEndPoint(serverNode));
            Debug.Log("" + Setting.GetEP().Address);
        }
    }

    public void Send<T>(int node, PacketId Id, IPacket<T> packet)
    {
        m_network.Send<T>(node, Id, packet);
    }

    public void SendReliable<T>(int node, IPacket<T> packet)
    {
        m_network.SendReliable<T>(node,  packet);
    }

    public void RegisterReceiveNotification(PacketId id, Network.RecvNotifier notifier)
    {
        if (m_network == null) Debug.Log("null");

        m_network.RegisterReceiveNotification(id, notifier);
    }

	public void ResisterEventHandler()
	{
		m_network.RegisterEventHandler (EventHandler);
	}

	private void EventHandler(int node, NetEventState state)
	{
		switch (state.type) {           
		case NetEventType.Connect:
			break;

		case NetEventType.Disconnect:
			Setting setting = Setting.GetInstance ();
			setting.state = State.Disconnect;
			break;   
		}
	}
}
