using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;
using System.Net;

// 싱글톤 패턴
public class Setting
{
    private Setting() {
    }
    private CharacterInfo my_character;
    private State m_state;
    private static Setting m_setting = null;
    private static System.Object SyncObject = new object();

    //싱글톤 캐릭터 반환
    public static Setting GetInstance()
    {
        if (m_setting == null)
        {
            lock(SyncObject)
            {
                if(m_setting == null)
                    m_setting = new Setting();
            }
        }

        return m_setting;
    }

    //각종 정보
    private static float jumpPower = 7;
    private static float moveSpeed = 5.0f;
    private static string hostname;
    private static IPEndPoint m_server;
    

    //상태를 변경하는 함수.
    public State state
    {
        get
        {
            return m_state;
        }

        set
        {
            m_state = value;
        }
    }

   public static IPEndPoint GetEP()
    {
        return m_server;
    }

    public static void SetEP(IPEndPoint ep)
    {
        m_server = ep;
    }
    public static float SetJumpPower()
    {
        return jumpPower;
    }

    public static float SetmoveSpeed()
    {
        return moveSpeed;
    }

    public static void SetHostName(string name)
    {
        hostname = name;
    }

    public static string GethostName()
    {
        return hostname;
    }

    //public Network GetNetwork()
    //{
    //    return m_network;
    //}

    //public void SetNetwork(Network network)
    //{
    //    m_network = network;
    //}

    //자신의 캐릭터가 무엇인지 설정합니다.
    public void SetCharacter(CharacterInfo character)
    {
        my_character = character;
    }

    public CharacterInfo GetCharacter()
    {
        return my_character;
    }

	static public float Width_Ratio()
	{
		return (float)Screen.width/1024.0f ;
	}

	static public float Height_Ratio()
	{
		return  (float)Screen.height / 768.0f;
	}
	static public float General_Ratio()
	{
		return Screen.height > Screen.width ? (float)Screen.width / 1024.0f : (float)Screen.height / 768.0f;
	}

}
	