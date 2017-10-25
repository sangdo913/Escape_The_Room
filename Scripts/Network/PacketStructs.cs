using UnityEngine;
using System;
using System.Collections;
using System.Net;




public enum TransportRequest
{
	Connect = 0,

	Disconnect,

	UserData,

}

public enum State
{
    Wait = 0,
    ConnectServer,
    GameStart,
	Disconnect
}

public enum PacketId
{
	// 매칭용 패킷.
    Connect         = 1,
	MatchingRequest,		// 매칭 요청 패킷.
	MatchingResponse, 			// 매칭 응답 패킷.

	// 게임용 패킷.
	CharacterData,				// 캐릭터 좌표 정보.
	ItemData,					// 아이템 취득/폐기 정보.
	PrizeResult,				// 보너스 획득 결과.
	ChatMessage,				// 채팅 메시지.
	QuizClear,					//퀴즈 클리어 정보
	BreakTile,					//타일 부수기
	Max,
}

public struct BreakTile
{
	public bool Act;
}

public struct QuizClear
{
	public int 		length;
	public string 	QuizName;
}
	
public enum CharacterInfo
{
    mummy = 0,
    rabbit,
    cat,
    Haruko,
	System = 10
}

public struct Connect
{
    public CharacterInfo character;
}


public enum MatchingRequestId
{
	CreateRoom = 0,
	JoinRoom,
	StartSession,
	SearchRoom,

	Max,
}

public enum MatchingResult 
{
	Success = 0,


	RoomIsFull,
	MemberIsFull,
	RoomIsGone,

}

public struct PacketHeader
{
	// 패킷 종별.
	public PacketId 	packetId;
}

//
// マッチングリクエスト.
//
public struct MatchingRequest
{
	public int					version;	// 패킷ID.
	public MatchingRequestId	request;	// 요청 내용.
	public int 					roomId;		// 요청 방 ID.
	public string				name;		// 생성할 방이름.
	public int					level;		// 레벨 지정.
	
	public const int roomNameLength = 32;	// 방 이름 길이.
}

//
// 매칭 응답.
//
public struct MatchingResponse
{
	// 요청 결과.
	public MatchingResult		result;
	
	// 요청 내용.
	public MatchingRequestId	request;

	// 응답 방ID.
	public int 					roomId;

	// 
	public string			 	name;

	// 참가인원.
	public int					members;

	// 방 이름 길이.
	public const int roomNameLength = 32;
}



//
// 방 정보.
//
public struct RoomInfo
{
	// 방ID.
	public int 					roomId;
	
	// 방 이름.
	public string				name;

	//
	public int					members;

	// 방 이름 길이.
	public const int roomNameLength = 32;
}

//
// 방 검색 결과.
//
public struct SearchRoomResponse
{
	// 검색한 방 수.
	public int			roomNum;

	// 방 정보.
	public RoomInfo[]	rooms;
}

//
// 접속할 곳의 정보.
//
public struct EndPointData
{
	public string		ipAddress;
	
	public int 			port;

	// IP 주소의 길이.
	public const int ipAddressLength = 32;
}

//
// 세션 정보.
//
public struct SessionData
{
	public MatchingResult 	result;

	public int				playerId;		// 동일 단말에서 동작시킬 때 사용합니다.

	public int				members;

	public EndPointData[]	endPoints;
}


//
//
// 게임용 패킷 데이터 정의.
//
//


//
// 게임 전 동기화할 정보.
//
public struct CharEquipment
{
	public int			globalId;	// 캐릭터의 글로벌 ID.
	public string		itemId;		// 선택한 무기 정보.

	public const int 	itemNameLength = 32;	// 무기 이름 길이.
}

//
// 전원의 동기화 정보.
//
public struct GameSyncInfo
{
	public int				seed;		// 동기화할 난수의 시드.
	public CharEquipment[]	items;		// 동기화할 장비 정보.
}


//
// 아이템 취득 정보.
//
public struct ItemData
{
	public string 		itemId;		// 아이템 식별자.
	public const int 	itemNameLength = 32;		// 아이템 이름 길이.
}


//
// 캐릭터 좌표 정보.
//
public struct CharacterCoord
{
	public float	x;		// 캐릭터의 x좌표.
	public float	z;		// 캐릭터의 z좌표.
	
	public CharacterCoord(float x, float z)
	{
		this.x = x;
		this.z = z;
	}
	public Vector3	ToVector3()
	{
		return(new Vector3(this.x, 0.0f, this.z));
	}
	public static CharacterCoord	FromVector3(Vector3 v)
	{
		return(new CharacterCoord(v.x, v.z));
	}
	
	public static CharacterCoord	Lerp(CharacterCoord c0, CharacterCoord c1, float rate)
	{
		CharacterCoord	c = new CharacterCoord();
		
		c.x = Mathf.Lerp(c0.x, c1.x, rate);
		c.z = Mathf.Lerp(c0.z, c1.z, rate);
		
		return(c);
	}
}

//
// 캐릭터의 이동 정보.
//
public struct CharacterData
{
	public string 			characterId;	// 캐릭터 ID.
    public int              CharacterInfo;  // 캐릭터 정보.
    public bool             isJumpEnd;      // 점프가 끝났을때면 다른것을 실행합니다.
    public bool             isJump;         // 점프 정보
    public Vector3	        position;	    // 좌표 데이터.

	public const int 		characterNameLength = 64;	// 캐릭터 ID의 길이.
}

//
// 캐릭터의 공격 정보.
//
public struct AttackData
{
	public string		characterId;		// 캐릭터 ID.
	
	public const int 	characterNameLength = 64;	// 캐릭터 ID의 길이.
}

//
// 몬스터의 리스폰 정보.
//
public struct MonsterData
{
	public string		lairId;			// 몬스터 이름.
	
	public const int 	monsterNameLength = 64;	// 몬스터 이름 길이.
}


//
// 대미지 양 정보.
//
public struct DamageData
{
	public string 			target;			// 공격된 캐릭터 ID.
	public int	 			attacker;		// 공격한 어카운트 ID.
	public float			damage;			// 대미지 양.

	public const int 		characterNameLength = 64;	// 캐릭터 ID의 길이.
}

//
// 캐릭터 HP 정보.
//
public struct HpData
{
	public string 			characterId;	// 캐릭터 ID.
	public float			hp;				// HP.
	
	public const int 		characterNameLength = 64;	// 캐릭터 ID의 길이.
}

//
// 도넛에 들어간 상태의 정보.
//
public struct CharDoorState
{
	public int			globalId;		// 글로벌 ID.
	public string		keyId;			// 열쇄 ID.
	public bool			isInTrigger;	// 도넛 위에 있는 상태.
	public bool			hasKey;			// 열쇠를 가지고 있는가.

	public const int 	keyNameLength = 64;	// 열쇠 ID 길이.
}


//
// 방 이동 통지.
//
public struct MovingRoom
{
	public string		keyId;				// 열쇠 ID.

	public const int 	keyNameLength = 32;	// 열쇠 이름 길이.
}

//
// 아이템 사용 정보.
//
public struct ItemUseData
{
	public int		itemFavor;	// 아이템의 효과.
	public string	targetId;	// 효과를 줄 캐릭터 ID.
	public string	userId;		// 아이템을 사용하는 캐릭터 ID.

	public int		itemCategory;	// 아이템 효과의 종류.

	public const int characterNameLength = 64;	// 캐릭터 ID의 길이.
}

//
// 소환수 출현 정보.
//
public struct SummonData
{
	public string		summon;					// 소환수 정보.

	public const int 	summonNameLength = 32;	// 소환수 이름 길이.
}


//
// 보스 공격 정보.
//

// 직접 공격.
public struct BossDirectAttack
{
	public string		target;		// 타깃 캐릭터.
	public float		power;		// 공격력.

	public const int 	characterNameLength = 64;	// 캐릭터 ID 길이.
}

// 범위 공격.
public struct BossRangeAttack
{
	public float	power;		// 공격력.
	public float	range;		// 범위.
}


//
//보너스 케이크 정보.
//
public struct PrizeData
{
	public string		characterId;	// 캐릭터 ID.
	public int			cakeNum;		// 케이크 수.

	public const int 	characterNameLength = 64;	// 캐릭터 ID 길이.
}

//
// 케이크 결과 정보.
//
public struct PrizeResultData
{
	public int 		cakeDataNum;	// 케이크 수.
	public int []	cakeNum;		// 먹은 케이크 수 .
}


//
// 채팅 메시지
//
public struct ChatMessage
{
    //public string		characterId; // 캐릭터 ID.
    public CharacterInfo    Char;       //캐릭터 종류
	public string		    message;	 // 채팅 메시지.

	public const int	    messageLength = 50;
}

