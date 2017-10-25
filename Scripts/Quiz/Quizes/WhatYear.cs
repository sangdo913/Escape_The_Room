using UnityEngine;
using System.Collections;


//Quiz 1.
public class WhatYear : Quiz{
	public GameObject door;
	AudioSource[] Sound;
	private KeyPad pad;

	public AudioClip DoorSound;
	public AudioClip ClearSound;
	private string password;

	public override string GetName()
	{
		return "WhatYear";
	}

	public WhatYear(QuizManager manager)
	{
		this.m_quiz = manager;
	}

	void Awake()
	{
		onClick = new PopUpWhatYear ();
		password = "1934";

		Sound = gameObject.GetComponents<AudioSource> ();
	}

	public override bool SolveQuiz()
	{		
		if (IsEqual (pad.ReturnPassword ())) {
			m_quiz.SendQuizResult (GetName ());
			ClearQuiz ();
		}
		return isClear;
	}

	//퀴즈를 클리어 하고 일어나는 함수입니다.
	public override void ClearQuiz()
	{
		UI_Info info = new UI_Info ();
		info.name = "Clear Image";
		info.Object = Resources.Load<GameObject> ("Prefabs/UI/PopUp/Clear");
		info.local = new Vector3 (0, 0, 0);
		isClear = true;
		Destroy (door);

		Sound [0].PlayOneShot (Sound[0].clip);
		Sound [1].PlayOneShot (Sound[1].clip);
		m_quiz.CreateUI (CommandType.UI_Create, info);
		if(pad != null) pad.ButtonX ();
	}

	public override void prepareQuiz()
	{
		Debug.Log ("WhatYear Quiz ready");
	}

	public override void ResisterObject(UI obj)
	{
		this.pad = obj as KeyPad;
	}

	public bool IsEqual(string password)
	{
		return this.password.Equals (password);
	}

	public override bool IsUIOpen(UI obj)
	{
		return obj == pad;
	}
}

public interface QuizFactory
{
	Quiz create (QuizManager manager, string name);
}