using UnityEngine;
using System.Collections;



public class CharacterController : GameController {
    public int characterNum = 4;

    GameObject obj;
    CharacterInfo my_character;
    InputInfo info = new InputInfo();

    GameObject[] characters;
    public CharacterBase[] ControlScripts;

    bool Counterflag = false;
    float counter;

    public override void  SetClient(Client client)
    {
        m_client = client;

        //델리게이트들을 등록해줍니다.
		m_client.SetCommand(CommandType.Char_GetInfo, new Char_GetInfo(this));
		m_client.SetCommand(CommandType.Char_GetRect, new Char_GetRect(this));
		m_client.SetCommand(CommandType.Char_Jump, new Char_Jump(this));
		m_client.SetCommand(CommandType.Char_Move, new Char_Move(this));
		m_client.SetCommand(CommandType.Char_CameraSetting, new Char_CameraSetting(this));
		m_client.SetCommand(CommandType.Char_GetPosition, new Char_GetPosition(this));

        m_client.RegisterReceiveNotification(PacketId.CharacterData, setCharInfo);
        m_client.RegisterReceiveNotification(PacketId.Connect, OnReceiveConnect);
    }

    void Awake()
    {
        //캐릭터를 만듭니다.
        newCharacter();
    }

	// Use this for initialization
	void Start ()
    {
    }

    // Update is called once per frame
    void Update()
    {
		if(ControlScripts[(int)my_character].IsJumpEnd() && !Counterflag)
        {
            Counterflag = true;
            counter = 2.0f;
        }

        if( Counterflag && counter < 0)
        {
            //점프가 끝났으면 착지한 곳의 정보를 보냅니다.
            CharacterData data = new CharacterData();

            data.characterId = "" + my_character;
            data.CharacterInfo = (int)my_character;
            data.isJumpEnd = true;
            data.isJump = false;
            data.position = GetPosition((int)my_character);
            Debug.Log("보냈습니다! " + data.position);

            m_client.SendReliable<CharacterData>(0, new CharacterDataPacket(data));

            ControlScripts[(int)my_character].ClearJump();

            Counterflag = false;
        }

        if(Counterflag) counter -= Time.deltaTime;
    }

    //점프 함수입니다.
    public void Jump()
    {
        ControlScripts[(int)Setting.GetInstance().GetCharacter()].jump();
        sendCharData();
    }


    //이동 함수입니다.
    public void Move()
    {
        //커서가 UI라면 이동하지 않습니다.
        Vector3 destination = new Vector3(info.mouse.position.x, 0, info.mouse.position.y);

        ControlScripts[(int)Setting.GetInstance().GetCharacter()].SetDestination(destination);
        sendCharData();
    }
    
    void newCharacter()
    {
        characters = new GameObject[characterNum];
        ControlScripts = new CharacterBase[characterNum];

        //캐릭터들 정보 받아오기
        characters[0] = Resources.Load<GameObject>("Prefabs/Characters/Mummy_char");
        characters[1] = Resources.Load<GameObject>("Prefabs/Characters/rabbit");
        characters[2] = Resources.Load<GameObject>("Prefabs/Characters/kitten");
        characters[3] = Resources.Load<GameObject>("Prefabs/Characters/Haruko");

        //캐릭터들의 스크립트 정보 받아오고 캐릭터 생성
        Vector3 position = new Vector3(0, 3, 0);

        for (int i = 0; i < 4; i++)
        {
            //캐릭터 생성 후 스크립트 받아오기
            characters[i] = Instantiate(characters[i]);
            ControlScripts[i] = characters[i].GetComponent<CharacterBase>();

            position.x += 3;
        }
    }

    //캐릭터 데이터를 서버로 전송합니다.
    void sendCharData()
    {
        CharacterData data = new CharacterData();

        data.characterId = "" +my_character;     
        data.CharacterInfo = (int)my_character;
        data.isJumpEnd = false;
        data.isJump = info.jump;
        data.position.x = info.mouse.position.x;
        data.position.z = info.mouse.position.y;
        data.position.y = 0;

        m_client.SendReliable<CharacterData>(0, new CharacterDataPacket(data));
    }



    void setCharInfo(int node, byte[] data)
    {
        CharacterDataPacket positionPacket = new CharacterDataPacket(data);

        CharacterData CharacterInfo = positionPacket.GetPacket();

        Vector3 position = CharacterInfo.position;

        if(CharacterInfo.isJumpEnd)
        {
            ControlScripts[CharacterInfo.CharacterInfo].SetPosition(position);
        }
        else if (CharacterInfo.isJump)
        {
            ControlScripts[CharacterInfo.CharacterInfo].jump();
        }

        else
        {
            ControlScripts[CharacterInfo.CharacterInfo].SetDestination(position);
        }
      
    }

    public void CameraSetting(CharacterInfo info)
    {
        //카메라 세팅
        MainCamera cam = Camera.main.gameObject.AddComponent<MainCamera>();
        cam.SetCharacter(characters[(int)info]);
        Debug.Log("설정된 캐릭터 : " + Setting.GetInstance().GetCharacter());
    }

    //Input 정보를 가지고 옵니다.
    public void GetInput(InputInfo info)
    {
        this.info = info;
    }

    public RectTransform GetRect(CharacterInfo info)
    {
        return ControlScripts[(int)info].GetRect(); 
    }

    //캐릭터의 위치를 반환합니다.
    public Vector3 GetPosition(int i)
    {
        return characters[i].transform.position;
    }

    void OnReceiveConnect(int node, byte[] data)
    {

        ConnectPacket packet = new ConnectPacket(data);

        Connect connect = packet.GetPacket();

        my_character = connect.character;
        Setting.GetInstance().SetCharacter(connect.character);

        CameraSetting(connect.character);

        Debug.Log("받았습니다!! 받은 값 : " + Setting.GetInstance().GetCharacter() + my_character);
    }

}


