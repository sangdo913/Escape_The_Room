using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : GameController {
	Dictionary<string,Item> items;

	void Awake() {
		items = new Dictionary<string,Item> ();
	}

	// Use this for initialization
	void Start () {
		m_client.RegisterReceiveNotification (PacketId.ItemData,OnReceiveDestroyItem);
	}
	
	// Update is called once per frame
	void Update () {
		enabled = true;	
	}

	public UI  CreateUI(UI_Info info)
	{
		return m_client.GetObject (CommandType.UI_Create, info);
	}

	//퀴즈를 만드는 함수입니다.
	public void CreateQuiz(string name)
	{
		m_client.execute (CommandType.Quiz_Create, name);
	}

	public void DeleteUI(string name)
	{
		m_client.execute (CommandType.UI_Delete, name);	
	}

	public void SetItem(string name, Item item)
	{
		items.Add (name, item);
		Debug.Log (name+ " 템 획득");
	}

	public Item GetItem(string name)
	{
		Debug.Log ("name");
		Debug.Log (items [name].transform.tag);
		return items[name];
	}

	public void RemoveDictionary(string name)
	{
		items.Remove (name);
		Debug.Log(name + " 템 사라짐");
		ItemData item;
		item.itemId = name;

		ItemPacket packet = new ItemPacket (item);

		m_client.SendReliable<ItemData> (0, packet);
		Debug.Log ("send item data");
	}

	//서버전송
	public void DestroyItem(string name)
	{
		Item item = items [name];
		Destroy (item.gameObject);
	}

	//서버에서 받았을 시
	public void OnReceiveDestroyItem(int node, byte[] data)
	{
		ItemPacket packet = new ItemPacket (data);
		ItemData item = packet.GetPacket ();

		string name = item.itemId;
		if (items.ContainsKey (name)) {
			DestroyItem (name);
		}
	}
}