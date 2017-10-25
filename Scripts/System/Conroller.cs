using UnityEngine;
using System.Collections;

public abstract class GameController : MonoBehaviour {
    protected Client m_client;

    public virtual void SetClient(Client client)
    {
        m_client = client;
    }
}
