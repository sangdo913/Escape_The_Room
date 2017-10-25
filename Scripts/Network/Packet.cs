using System.Collections;
using System.IO;

//
// 매칭 매킷 정의.
//

//연결 정의


public class ConnectPacket : IPacket<Connect>
{

    //저장되어있는 패킷
    Connect m_packet;

    //시리얼라이저 생성
    class ConnectSerializer : Serializer
    {

        public bool Serialize(Connect packet)
        {
            bool ret = true;
            int character = (int) packet.character;
            ret &= Serialize(character);

            return ret;
        }

        public bool Deserialize(ref Connect element)
        {
            if(GetDataSize() == 0)
            {
                return false;
            }

            bool ret = true;
            int character = 0;
            ret &= Deserialize(ref character);
            element.character = (CharacterInfo) character;

            return ret;
        }
    }

    public PacketId GetPacketId()
    {
        return PacketId.Connect;
    }

    public Connect GetPacket()
    {
        return m_packet;
    }

    public byte[] GetData()
    {
        ConnectSerializer serializer = new ConnectSerializer();

        serializer.Serialize(m_packet);
        return serializer.GetSerializedData();
    }

    // 패킷 데이터를 시리얼라이즈하기 위한 생성자.
    public ConnectPacket(Connect data)
    {
        m_packet = data;
    }

    // 바이너리 데이터를 패킷 데이터로 디시리얼라이즈하는 생성자.
    public ConnectPacket(byte[] data)
    {
        ConnectSerializer serializer = new ConnectSerializer();

        serializer.SetDeserializedData(data);
        serializer.Deserialize(ref m_packet);
    }
}



public class BreakTilePacket : IPacket<BreakTile>
{

	//저장되어있는 패킷
	BreakTile m_packet;

	//시리얼라이저 생성
	class BreakTileSerializer : Serializer
	{

		public bool Serialize(BreakTile packet)
		{
			bool ret = true;
			bool act = packet.Act;
			ret &= Serialize(act);

			return ret;
		}

		public bool Deserialize(ref BreakTile element)
		{
			if(GetDataSize() == 0)
			{
				return false;
			}

			bool ret = true;
			bool act = false;
			ret &= Deserialize(ref act);
			element.Act = act;

			return ret;
		}
	}

	public PacketId GetPacketId()
	{
		return PacketId.BreakTile;
	}

	public BreakTile GetPacket()
	{
		return m_packet;
	}

	public byte[] GetData()
	{
		BreakTileSerializer serializer = new BreakTileSerializer();

		serializer.Serialize(m_packet);
		return serializer.GetSerializedData();
	}

	// 패킷 데이터를 시리얼라이즈하기 위한 생성자.
	public BreakTilePacket(BreakTile data)
	{
		m_packet = data;
	}

	// 바이너리 데이터를 패킷 데이터로 디시리얼라이즈하는 생성자.
	public BreakTilePacket(byte[] data)
	{
		BreakTileSerializer serializer = new BreakTileSerializer();

		serializer.SetDeserializedData(data);
		serializer.Deserialize(ref m_packet);
	}
}

// 매칭 요청 패킷 정의.
public class MatchingRequestPacket : IPacket<MatchingRequest>
{
	class MatchingRequestSerializer : Serializer
	{
        
		public bool Serialize(MatchingRequest packet)
		{
			bool ret = true;

			ret &= Serialize(packet.version);
			int request = (int)packet.request;
			ret &= Serialize(request);
			ret &= Serialize(packet.roomId);
			ret &= Serialize(packet.name, MatchingRequest.roomNameLength);
			ret &= Serialize(packet.level);
			
			return ret;
		}
		
		//
		public bool Deserialize(ref MatchingRequest element)
		{
			if (GetDataSize() == 0) {
				// 데이터가 설정되어 있지 않습니다.
				return false;
			}

			bool ret = true;

			ret &= Deserialize(ref element.version);

			int request = 0;
			ret &= Deserialize(ref request);
			element.request = (MatchingRequestId) request;
			
			ret &= Deserialize(ref element.roomId);
			ret &= Deserialize(ref element.name, MatchingRequest.roomNameLength);
			ret &= Deserialize(ref element.level);
			
			return ret;
		}
	}
	
	// 패킷 데이터의 실체.
	MatchingRequest	m_packet;
	
	
	// 패킷 데이터를 시리얼라이즈하기 위한 생성자.
	public MatchingRequestPacket(MatchingRequest data)
	{
		m_packet = data;
	}
	
	// 바이너리 데이터를 패킷 데이터로 디시리얼라이즈하는 생성자.
	public MatchingRequestPacket(byte[] data)
	{
		MatchingRequestSerializer serializer = new MatchingRequestSerializer();
		
		serializer.SetDeserializedData(data);
		serializer.Deserialize(ref m_packet);
	}
	
	public PacketId	GetPacketId()
	{
		return PacketId.MatchingRequest;
	}
	
	public MatchingRequest	GetPacket()
	{
		return m_packet;
	}
	
	
	public byte[] GetData()
	{
		MatchingRequestSerializer serializer = new MatchingRequestSerializer();
		
		serializer.Serialize(m_packet);
		
		return serializer.GetSerializedData();
	}
}

// 매칭 요청 패킷 정의.
public class MatchingResponsePacket : IPacket<MatchingResponse>
{
	class MatchingResponseSerializer : Serializer
	{
		//
		public bool Serialize(MatchingResponse packet)
		{
			bool ret = true;

			int result = (int)packet.result;
			ret &= Serialize(result);
			
			int request = (int)packet.request;
			ret &= Serialize(request);

			ret &= Serialize(packet.roomId);
			ret &= Serialize(packet.name, MatchingResponse.roomNameLength);
			ret &= Serialize(packet.members);
			
			return ret;
		}
		
		//
		public bool Deserialize(ref MatchingResponse element)
		{
			if (GetDataSize() == 0) {
				// 데이터가 설정되어 있지 않습니다.
				return false;
			}
		
			bool ret = true;

			int result = 0;
			ret &= Deserialize(ref result);
			element.result = (MatchingResult) result;
			
			int request = 0;
			ret &= Deserialize(ref request);
			element.request = (MatchingRequestId) request;
			
			ret &= Deserialize(ref element.roomId);
			ret &= Deserialize(ref element.name, MatchingResponse.roomNameLength);
			ret &= Deserialize(ref element.members);
			
			return ret;
		}
	}
	
	// 패킷 데이터의 실체.
	MatchingResponse	m_packet;
	
	
	// 패킷 데이터를 시리얼라이즈하기 위한 생성자.
	public MatchingResponsePacket(MatchingResponse data)
	{
		m_packet = data;
	}
	
	// 바이너리 데이터를 패킷 데이터에 디시리얼라이즈하기 위한 생성자.
	public MatchingResponsePacket(byte[] data)
	{
		MatchingResponseSerializer serializer = new MatchingResponseSerializer();
		
		serializer.SetDeserializedData(data);
		serializer.Deserialize(ref m_packet);
	}
	
	public PacketId	GetPacketId()
	{
		return PacketId.MatchingResponse;
	}
	
	public MatchingResponse	GetPacket()
	{
		return m_packet;
	}
	
	
	public byte[] GetData()
	{
		MatchingResponseSerializer serializer = new MatchingResponseSerializer();
		
		serializer.Serialize(m_packet);
		
		return serializer.GetSerializedData();
	}
}





//
//
// 게임용 패킷 데이터 정의.
//
//


// 캐릭터 좌표 패킷 정의.
public class CharacterDataPacket : IPacket<CharacterData>
{
	class CharactorDataSerializer : Serializer
	{
		//
		public bool Serialize(CharacterData packet)
		{
		
			bool ret = true;

			ret &= Serialize(packet.characterId, CharacterData.characterNameLength);
            ret &= Serialize(packet.CharacterInfo);
            ret &= Serialize(packet.isJumpEnd);
            ret &= Serialize(packet.isJump);
			ret &= Serialize(packet.position.x);
			ret &= Serialize(packet.position.y);
            ret &= Serialize(packet.position.z);
		
			
			return ret;
		}
		
		//
		public bool Deserialize(ref CharacterData element)
		{
			if (GetDataSize() == 0) {
				// 데이터가 설정되어 있지 않습니다.
				return false;
			}

			bool ret = true;

			ret &= Deserialize(ref element.characterId, CharacterData.characterNameLength);
            ret &= Deserialize(ref element.CharacterInfo);
            ret &= Deserialize(ref element.isJumpEnd);
            ret &= Deserialize(ref element.isJump);

			// position
			ret &= Deserialize(ref element.position.x);
			ret &= Deserialize(ref element.position.y);
            ret &= Deserialize(ref element.position.z);			
			
			return ret;
		}
	}
	
	// 패킷 데이터의 실체.
	CharacterData		m_packet;
	
	public CharacterDataPacket(CharacterData data)
	{
		m_packet = data;
	}
	
	public CharacterDataPacket(byte[] data)
	{
		CharactorDataSerializer serializer = new CharactorDataSerializer();
		
		serializer.SetDeserializedData(data);
		serializer.Deserialize(ref m_packet);
	}
	
	public PacketId	GetPacketId()
	{
		return PacketId.CharacterData;
	}
	
	public CharacterData	GetPacket()
	{
		return m_packet;
	}
	
	
	public byte[] GetData()
	{
		CharactorDataSerializer serializer = new CharactorDataSerializer();
		
		serializer.Serialize(m_packet);
		
		return serializer.GetSerializedData();
	}
}


// 아이템 패킷 정의.
public class ItemPacket : IPacket<ItemData>
{
	class ItemSerializer : Serializer
	{
		//
		public bool Serialize(ItemData packet)
		{
			bool ret = true;

			ret &= Serialize(packet.itemId, ItemData.itemNameLength);
			
			return ret;
		}
		
		//
		public bool Deserialize(ref ItemData element)
		{
			if (GetDataSize() == 0) {
				// 데이터가 설정되어 있지 않습니다.
				return false;
			}

			bool ret = true;
			
			ret &= Deserialize(ref element.itemId, ItemData.itemNameLength);

			return ret;
		}
	}
	
	// 패킷 데이터의 실체.
	ItemData	m_packet;
	
	
	// 패킷 데이터를 시리얼라이즈하기 위한 생성자.
	public ItemPacket(ItemData data)
	{
		m_packet = data;
	}
	
	// 바이너리 데이터를 패킷 데이터로 디시리얼라이즈하기 위한 생성자.
	public ItemPacket(byte[] data)
	{
		ItemSerializer serializer = new ItemSerializer();
		
		serializer.SetDeserializedData(data);
		serializer.Deserialize(ref m_packet);
	}
	
	public PacketId	GetPacketId()
	{
		return PacketId.ItemData;
	}
	
	// 게임에서 사용할 패킷 데이터를 취득.
	public ItemData	GetPacket()
	{
		return m_packet;
	}
	
	// 송신용 byte[] 형 데이터 취득.
	public byte[] GetData()
	{
		ItemSerializer serializer = new ItemSerializer();
		
		serializer.Serialize(m_packet);
		
		return serializer.GetSerializedData();
	}
}

// 채팅 패킷 정의.
public class ChatPacket : IPacket<ChatMessage>
{
    class ChatSerializer : Serializer
    {
        //
        public bool Serialize(ChatMessage packet)
        {
            bool ret = true;

            //ret &= Serialize(packet.characterId, ChatMessage.characterNameLength);
            ret &= Serialize((int)packet.Char);
            ret &= Serialize(packet.message, ChatMessage.messageLength);

            return ret;
        }

        //
        public bool Deserialize(ref ChatMessage element)
        {
            if (GetDataSize() == 0)
            {
                // 데이터가 설정되어 있지 않습니다.
                return false;
            }

            bool ret = true;

            //ret &= Deserialize(ref element.characterId, ChatMessage.characterNameLength);
            int Char = 0;
            ret &= Deserialize(ref Char);
            element.Char = (CharacterInfo)Char;

            ret &= Deserialize(ref element.message, ChatMessage.messageLength);

            return ret;
        }
    }

    // 패킷 데이터의 실체.
    ChatMessage m_packet;


    // 패킷 데이터를 시리얼라이즈하기 위한 생성자.
    public ChatPacket(ChatMessage data)
    {
        m_packet = data;
    }

    // 바이너리 데이터를 패킷 데이터로 디시리얼라이즈하기 위한 생성자.
    public ChatPacket(byte[] data)
    {
        ChatSerializer serializer = new ChatSerializer();

        serializer.SetDeserializedData(data);
        serializer.Deserialize(ref m_packet);
    }


    public PacketId GetPacketId()
    {
        return PacketId.ChatMessage;
    }

    public ChatMessage GetPacket()
    {
        return m_packet;
    }


    public byte[] GetData()
    {
        ChatSerializer serializer = new ChatSerializer();

        serializer.Serialize(m_packet);

        return serializer.GetSerializedData();
    }
}

//퀴즈 클리어 정보
public class QuizClearPacket : IPacket<QuizClear>
{
	QuizClear m_packet;

	class QuizClearSerializer : Serializer
	{
		public bool Serialize(QuizClear packet)
		{
			bool ret = true;
			string quizName = packet.QuizName;

			ret &= Serialize (packet.length);
			ret &= Serialize (quizName, packet.length);

			return ret;
		}

		public bool Deserialize(ref QuizClear quiz)
		{
			if(GetDataSize() == 0)
				return false;
			bool ret = true;
			string quizName = "";
			int length = 0;
			ret &= Deserialize(ref length); 
			ret &= Deserialize (ref quizName, length);

			quiz.QuizName = quizName;

			return ret;
		}
	}

	public QuizClearPacket(QuizClear quiz)
	{
		m_packet = quiz;
	}

	public QuizClearPacket(byte[] data)
	{
		QuizClearSerializer serializer = new QuizClearSerializer ();

		serializer.SetDeserializedData (data);
		serializer.Deserialize (ref m_packet);
	}

	public PacketId GetPacketId()
	{
		return PacketId.QuizClear;
	}

	public QuizClear GetPacket()
	{
		return m_packet;
	}

	public byte[] GetData()
	{
		QuizClearSerializer serializer = new QuizClearSerializer();

		serializer.Serialize(m_packet);
		return serializer.GetSerializedData();
	}
}
