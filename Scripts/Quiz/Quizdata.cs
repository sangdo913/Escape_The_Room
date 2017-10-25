using UnityEngine;
using System.Collections;

//퀴즈 관리 클래스입니다.
public abstract class Quiz : MonoBehaviour
{
	protected QuizManager m_quiz;

	//protected QuizFactory factory;
	protected OnClick onClick;

	protected bool isStarted = false;
	protected bool isClear = false;
	protected GameObject obj;

	public abstract string GetName ();

	void Start()
	{
	}

	public void Update()
	{
		enabled = isStarted ||  m_quiz == null;

		if (m_quiz == null) {
			GameObject obj;
			if(obj = GameObject.Find ("GameController")){
				this.m_quiz = obj.GetComponent<QuizManager> ();
				Debug.Log (m_quiz);
				m_quiz.ResisterQuiz (GetName(), this);
				Debug.Log (m_quiz);
			}
		}

		if (isStarted) {
			SolveQuiz ();
		}

		//퀴즈가 다 풀어지면 EndQuiz를 호출합니다.
		if (isClear) {
			endQuiz ();
		}
	}

	//퀴즈를 풀시에 일어나는 함수들입니다.
	public abstract bool SolveQuiz();
	//퀴즈를 클리어 할 시에 일어나는 함수입니다.
	public abstract void ClearQuiz();
	//퀴즈를 만들 때 준비할 것입니다.
	public abstract void prepareQuiz ();

	public void endQuiz()
	{
		enabled = false;
		m_quiz.DeleteQuiz (this);
		Destroy (this);
	}

	public void OnClick()
	{
		onClick.OnClick (m_quiz);
		isStarted = true;
	}

	public void startQuiz()
	{
		isStarted = true;
		enabled = true;
	}

	public void StopQuiz()
	{
		isStarted = false;
		enabled = false;
	}

	public abstract void ResisterObject (UI obj);

	public abstract bool IsUIOpen(UI obj);
}