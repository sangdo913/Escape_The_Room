using UnityEngine;
using System.Collections;

public interface OnClick
{
	void OnClick(GameController controller);
}


public class Nothing : OnClick{
	public void OnClick(GameController clcik)
	{		
	}
}

public class PoPUpCallender : OnClick
{
	public void OnClick(GameController controller)
	{
		QuizManager manager = (controller as QuizManager);

		UI_Info info = new UI_Info ();
		info.name = "Callender";
		info.Object = Resources.Load ("Prefabs/UI/PopUp/Callender") as GameObject;

		info.local = new Vector3 (0, 0, 0);

		manager.CreateUI (CommandType.UI_Create, info);

		manager.StartQuiz ("Callender");
	}
}


public class PopUpWhatYear : OnClick{
	public void OnClick(GameController controller)
	{		
		QuizManager manager = (controller as QuizManager);

		UI_Info info = new UI_Info ();
		info.name = "WhatYear";
		info.Object = Resources.Load ("Prefabs/UI/PopUp/Pad") as GameObject;

		info.local = new Vector3 (0, 0, 0);

		Setting setting = Setting.GetInstance ();

		UI obj = manager.CreateUI (CommandType.UI_Create, info);
		manager.ResisterObject ("WhatYear", obj);
		manager.StartQuiz ("WhatYear");
	}
}
