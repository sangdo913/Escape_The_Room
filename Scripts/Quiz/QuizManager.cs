using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuizManager : GameController {
	private GameObject popup;
	private GameObject pad;
	private Dictionary<string,Quiz> quizes = new Dictionary<string, Quiz>();

	//현재 실행되고있는 퀴즈
	private Quiz executing = null;

	void Awake()
	{

		//create Game Object
		popup = Resources.Load<GameObject>("Prefabs/UI/PopUp/popup");
		pad = Resources.Load<GameObject> ("Prefabs/UI/PopUp/Pad");
		//stop quiz manager
	}


	// Use this for initialization
	void Start () {
		//Add Command to Client
		m_client.SetCommand(CommandType.Quiz_Create, new Quiz_Create(this));
		m_client.SetCommand (CommandType.Quiz_StopQuiz, new Quiz_StopQuiz (this));

		m_client.RegisterReceiveNotification (PacketId.QuizClear, OnReceiveQuizClear);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void DeleteQuiz(Quiz quiz)
	{
		executing = null;
		string name = quiz.GetName ();
		quizes.Remove (name);
	}

	public void DeleteQuiz(string name)
	{
		executing = null;
		quizes.Remove (name);
		Debug.Log ("Delete Quiz");
	}

	public UI CreateUI(CommandType type, UI_Info info)
	{
		return m_client.GetObject(type, info);
	}

	public void StartQuiz(string name)
	{
		Quiz quiz = quizes [name];
		executing = quiz;
		quiz.startQuiz ();
		enabled = true;

		m_client.StopInput ();
	}

	public void ResisterQuiz(string name, Quiz quiz)
	{
		quizes.Add (name, quiz);
	}

	public void ResisterObject (string name, UI obj)
	{
		quizes [name].ResisterObject (obj);
	}

	public void StopQuiz(string name)
	{
		Quiz quiz = quizes [name];
		quiz.StopQuiz ();
		executing = null;

		m_client.startInput ();
	}

	public void CreateQuiz (string name)
	{
	}

	public void ClearQuiz(string name)
	{
		Debug.Log (name);
		Quiz quiz = quizes [name];

		quiz.ClearQuiz ();
		quiz.endQuiz ();
		if (quiz == executing) {
			executing = null;
		}

		//서버에 플래그 보내기
	}

	public void SendQuizResult(string name)
	{
		QuizClear clear = new QuizClear();
		clear.QuizName = name;
		clear.length = name.Length;
		QuizClearPacket packet = new QuizClearPacket (clear);


		m_client.SendReliable(0, packet);

		QuizClear quizclear = packet.GetPacket ();



		Debug.Log ("send suceess");
	}

	public void OnReceiveQuizClear(int node, byte[] data)
	{
		Debug.Log ("received quiz Clear");
		QuizClearPacket packet = new QuizClearPacket (data);

		QuizClear clear = packet.GetPacket ();
		string name = clear.QuizName;

		ClearQuiz (name);
	}
}

/*
//퀴즈를 만드는 클래스 입니다.
public class SimpleQuizFactory : QuizFactory
{
	public Quiz create(QuizManager manager, string name)
	{
		return new WhatYear ();
	}
}

*/