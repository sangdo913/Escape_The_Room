using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// 마우스의 위치를 x와 y로 나눈 것입니다.
public struct position
{
    public float x;
    public float y;
}

//인풋 정보입니다.
public struct MouseInfo
{
    public position position;
    public string cursor;
}

public struct InputInfo
{
    public MouseInfo   mouse;
    public string      KeyInfo;
    public bool        jump;
}


public class InputController : GameController {
    private InputInfo info;

    private float WaitCounter = 1.0f;

    bool OnUI = false;

    public override void SetClient(Client client)
    {
        m_client = client;

		m_client.SetCommand(CommandType.Input_OnMouseEnter,new Input_OnMouseEnter(this));
		m_client.SetCommand(CommandType.Input_MouseOut, new Input_MouseOut(this));
    }

    void Awake()
    {

    }
	// Use this for initialization
	void Start () {
        info = new InputInfo();
    }
	
	// Update is called once per frame
	void Update () {
        isMouse();
        isKeyBoard();


        if (WaitCounter > 0)
        {
            WaitCounter -= Time.deltaTime;
        }
    }    

    //키보드 입력에 관한 함수
    private void isKeyBoard()
    {
        isJump();

        if (Input.GetButtonDown("Submit"))
        {
			m_client.execute(CommandType.UI_Chat_Activate);
        }
    }

    private void isMouse()
    {
        //마우스가 눌렸을 시
        if (Input.GetMouseButton(0) && WaitCounter <= 0 && !OnUI)
        {
            info = GetInput();
			if (info.mouse.cursor.Equals ("Floor")) {
				m_client.execute (CommandType.Char_GetInfo, info);
				m_client.execute (CommandType.Char_Move);
			} 
            //눌리면 잠시동안 마우스입력을 안받게합니다.
            WaitCounter = 0.2f;
        }

    }

    private void isJump()
    {

        //점프 버튼이 눌렸을 시
        if (Input.GetButtonDown("Jump"))
        {
            info.mouse.position.x = 1;
            info.mouse.position.y = 1;
            info.jump = true;

            m_client.execute(CommandType.Char_GetInfo, info);
            m_client.execute(CommandType.Char_Jump);
        }
    }

    //마우스 위치를 가져오고 반환합니다.
    private InputInfo GetInput()
    {
        LayerMask layermask;

        layermask = LayerMask.GetMask("Floor");
        InputInfo result = new InputInfo();
        RaycastHit hitObj;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);



		if (Physics.Raycast(ray, out hitObj, Mathf.Infinity))
        {
            //바닥을 클릭했다면 그 주소를 가져옵니다.
			if (hitObj.transform.tag == "Floor") {
				result.mouse.position.x = hitObj.point.x;
				result.mouse.position.y = hitObj.point.z;
			} else if (hitObj.transform.tag == "Quiz") {
				Quiz click = hitObj.transform.gameObject.GetComponent<Quiz> ();
				if(click !=null) click.OnClick ();
			} else if (hitObj.transform.tag == "Item") {
				Item click = hitObj.transform.gameObject.GetComponent<Item> ();
				if(click!=null) click.OnClick ();
				}

			result.mouse.cursor = hitObj.transform.tag;
        }
        info.jump = false;

        return result;
    }

    public void OnMouseEnter()
    {
        OnUI = true;
    }

    public void MouseOut()
    {
        OnUI = false;
    }

	public void StopInput()
	{
		enabled = false;
	}

	public void StartInput()
	{
		enabled = true;
	}
}