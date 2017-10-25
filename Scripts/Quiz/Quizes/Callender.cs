using UnityEngine;
using System.Collections;

public class Callender : Quiz {

	public override string GetName ()
	{
		return "Callender";
	}
	public override bool SolveQuiz(){return false;}
	//퀴즈를 클리어 할 시에 일어나는 함수입니다.
	public override void ClearQuiz(){}
	//퀴즈를 만들 때 준비할 것입니다.
	public override void prepareQuiz (){}

	public override void ResisterObject (UI obj){}

	public override bool IsUIOpen(UI obj){return false;}

	void Awake()
	{
		onClick = new PoPUpCallender ();
	}
}
