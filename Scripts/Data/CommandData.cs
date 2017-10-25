using UnityEngine;
using System.Collections;

public enum CommandType
{

	//CharacterController 함수입니다.
	Char_GetPosition = 0,
	Char_GetRect,
	Char_GetInfo,
	Char_CameraSetting,
	Char_Jump,
	Char_Move,

	//inputController 함수입니다.
	Input_OnMouseEnter,
	Input_MouseOut,

	//UIController 함수입니다.
	UI_Create,
	UI_Delete,
	UI_GetRoot,
	//UI의 Chat 
	UI_Chat_Activate,
	UI_Chat_Write,

	//Chatting Manager 함수입니다.
	Chat_AddMessage,

	//Quiz의 함수
	Quiz_Create,
	Quiz_StopQuiz,

	length
}

public abstract class Command
{
	public virtual void execute()
	{
		throw new UnityException("정의되지 않았습니다.");
	}

	public virtual void execute(int i)
	{
		throw new UnityException("정의되지 않았습니다.");
	}

	public virtual void execute(string str)
	{
		throw new UnityException ("정의되지 않았습니다.");
	}

	public virtual void execute(InputInfo info)
	{
		throw new UnityException("정의되지 않았습니다.");
	}

	public virtual void execute(CharacterInfo info)
	{
		throw new UnityException("정의되지 않았습니다.");
	}

	public virtual void execute(UI_Info info)
	{
		throw new UnityException("Not defined");
	}
		
	public virtual void execute(string message, CharacterInfo info)
	{
		throw new UnityException("정의되지 않았습니다.");
	}

	public virtual Transform GetTransform()
	{
		throw new UnityException("정의되지 않았습니다.");
	}

    public virtual UI GetObject(UI_Info info)
    {
        throw new UnityException("정의되지 않았습니다.");
    }
    public virtual Vector3 GetVector3(int i)
    {
        throw new UnityException("정의되지 않았습니다.");
    }

    public virtual RectTransform GetRect(CharacterInfo info)
    {
        throw new UnityException("정의되지 않았습니다.");
    }
}



//CharacterController 함수들입니다.

public class Char_GetPosition : Command{
	private CharacterController controller;

	public Char_GetPosition(CharacterController controller)
	{
		this.controller = controller;
	}

	public override Vector3 GetVector3 (int i)
	{
		return controller.GetPosition (i);
	}
}

public class Char_GetRect : Command{
	private CharacterController controller;

	public Char_GetRect(CharacterController controller)
	{
		this.controller = controller;
	}

	public override RectTransform GetRect (CharacterInfo info)
	{
		return controller.GetRect (info);
	}
}

/*
    //CharacterController 델리게이트들입니다.
public delegate Vector3 Character_GetCharPosition(int i);
public delegate RectTransform Character_GetRect(CharacterInfo info);
public delegate void Character_GetInfo(InputInfo info);
public delegate void Character_CameraSetting(CharacterInfo info);
public delegate void Character_Jump();
public delegate void Character_Move();


public delegate void Input_OnMouseEnter();
public delegate void Input_MouseOut();

//UIController 델리게이트들입니다.

public delegate void UI_ChatActivate();
public delegate void UI_ChatWrite(string message, CharacterInfo info);

//ChatMenager 델리게이트들입니다.
public delegate void Chat_AddMessage(string message, CharacterInfo info);

*/

//CharacterController 함수들입니다.



public class Char_GetInfo : Command{
	private CharacterController controller;

	public Char_GetInfo (CharacterController controller)
	{
		this.controller = controller;
	}

	public override void execute (InputInfo info)
	{
		controller.GetInput (info);
	}
}

public class Char_CameraSetting : Command{
	private CharacterController controller;

	public Char_CameraSetting(CharacterController controller)
	{
		this.controller = controller;
	}

	public override void execute (CharacterInfo info)
	{
		controller.CameraSetting (info);
	}
}

public class Char_Jump : Command{
	private CharacterController controller;

	public Char_Jump (CharacterController controller)
	{
		this.controller = controller;
	}

	public override void execute ()
	{
		controller.Jump ();
	}
}

public class Char_Move : Command{
	private CharacterController controller;

	public Char_Move (CharacterController controller)
	{
		this.controller = controller;
	}

	public override void execute ()
	{
		controller.Move ();
	}
}


//InputCountroller 델리게이트들입니다.
public class Input_OnMouseEnter : Command{
	private InputController controller;

	public Input_OnMouseEnter (InputController controller)
	{
		this.controller = controller;
	}

	public override void execute ()
	{
		controller.OnMouseEnter ();
	}
}

public class Input_MouseOut : Command{
	private InputController controller;

	public Input_MouseOut (InputController controller)
	{
		this.controller = controller;
	}

	public override void execute ()
	{
		controller.MouseOut ();
	}
}
//UIController 함수입니다.

public class UI_GetRoot : Command{
	private UIController controller;

	public UI_GetRoot(UIController controller)
	{
		this.controller = controller;
	}

	public override Transform GetTransform()
	{
		return controller.GetUI();
	}
}

public class UI_Create : Command{
	private UIController controller;

	public UI_Create(UIController controller)
	{
		this.controller = controller;
	}

	public override UI GetObject(UI_Info info)
	{
		return controller.CreateUI (info);
	}
}

public class UI_Delete : Command{
	private UIController controller;

	public UI_Delete(UIController controller)
	{
		this.controller = controller;
	}

	public override void execute(string str)
	{
		controller.DeleteUI (str);
	}
}

public class UI_Chat_Write : Command{
	private Chatting controller;

	public UI_Chat_Write (Chatting controller)
	{
		this.controller = controller;
	}

	public override void execute (string message, CharacterInfo info)
	{
		controller.WriteMessage (message, info);
	}
}

public class UI_Chat_Active : Command{
	private Chatting controller;

	public UI_Chat_Active (Chatting controller)
	{
		this.controller = controller;
	}

	public override void execute ()
	{
		controller.Activate ();
	}
}

//ChatManager 함수들입니다.
public class Chat_AddMessage : Command{
	private ChattingManager controller;

	public Chat_AddMessage(ChattingManager controller)
	{
		this.controller = controller;
	}

	public override void execute (string message, CharacterInfo info)
	{
		controller.AddMessage (message, info);
	}
}

//Quiz Manager 입니다.
public class Quiz_Create : Command
{
	private QuizManager manager;

	public Quiz_Create(QuizManager manager)
	{
		this.manager = manager;
	}

	public override void execute(string name)
	{
		manager.CreateQuiz (name);
	}
}

public class Quiz_StopQuiz : Command{	
	private QuizManager manager;

	public Quiz_StopQuiz(QuizManager manager)
	{
		this.manager = manager;
	}

	public override void execute(string name)
	{
		manager.StopQuiz (name);
	}
}


