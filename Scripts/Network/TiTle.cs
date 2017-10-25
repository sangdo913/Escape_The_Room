using UnityEngine;
using System.Collections;
using System.Net;
using System.Threading;

public class TiTle : MonoBehaviour
{
    private Client m_client;
    private Network m_network;
    
    private bool isServer;

	private string serverAddress = GlobalParam.serverAddress;

    GameObject Controller;

	//private string server = NetConfig.

    Setting setting = Setting.GetInstance();

    int serverNode = 0;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch (setting.state)
        {
            case State.ConnectServer:

                createClient();
                createNetwork();
                createCharController();
                createInputController();
                createUIController();
                startClient();

                setting.state = State.GameStart;
                break;
        }
    }
    
    private void startClient()
    { 
        m_client.SetControllers();
        m_client.ConnectServer(serverAddress);
		m_client.ResisterEventHandler ();
    }

    void OnGUI()
    {
        float px = GlobalParam.px;
        float py = GlobalParam.py;

		Rect position = new Rect (px * 0.3f, py * 0.2f, px * 0.4f, py * 0.1f);

        switch (setting.state)
        {
		case State.Wait:
			bool yes;
			bool no;
            position.y += py * 0.2f; 

			position.y += py * 0.2f;
            yes = GUI.Button(position, "접속");
                if (yes)
                {
				GameObject obj = GameObject.Find ("BackGround");
				Destroy (obj);
                    isServer = true;
                    setting.state = State.ConnectServer;
                }
                break;

		case State.Disconnect:
			GUI.TextArea (position, "서버가 끊겼습니다!!");
			break;
        }
    }


    //캐릭터 컨트롤러 생성 : 자신의 캐릭터 의외의 캐릭터를 조종합니다.
    void createCharController()
    {
        Controller = GameObject.Find("GameController");
        if (Controller == null)
        {
            Controller = new GameObject("GameController");            
        }

        if (Controller.GetComponent<CharacterController>() == null)
        {
            CharacterController controll = Controller.AddComponent<CharacterController>();
        }

    }

    private void createNetwork()
    {
        GameObject obj;

        if (isServer)
        {
            obj = GameObject.Find("GameController");

            if (obj == null)
            {
                obj = new GameObject("GameController");
            }
            m_network = obj.AddComponent<Network>();
            m_client.SetNetwork(m_network);
        }
    }

    void createUIController()
    {
        Controller = GameObject.Find("GameController");
        if (Controller == null)
        {
            Controller = new GameObject("GameController");
        }

        if (Controller.GetComponent<UIController>() == null)
        {
            Controller.AddComponent<UIController>();
        }
    }


    void createInputController()
    {
        //인풋 컨트롤러 생성 : 입력을 어떻게 처리할지에 대한 스크립트를 생성합니다.
        Controller = GameObject.Find("GameController");
        if (Controller == null)
        {
            Controller = new GameObject("GameController");           
        }

        if (Controller.GetComponent<InputController>() == null)
        {
            Controller.AddComponent<InputController>();
        }
    }

    //채팅 매니저를 만듭니다.
    void createChatManager()
    {
        Controller = GameObject.Find("GameController");
        if (Controller == null)
        {
            Controller = new GameObject("GameController");
        }

        if (Controller.GetComponent<ChattingManager>() == null)
        {
            Controller.AddComponent<ChattingManager>();
        }

    }

    //클라리언트를 생성합니다.
    void createClient()
    {
        GameObject client = GameObject.Find("GameController");

        if(client == null)
        {
            client = new GameObject("GameController");
        }

        if(client.GetComponent<Client>() == null)
        {
            m_client = client.AddComponent<Client>();
        }
    }
}
