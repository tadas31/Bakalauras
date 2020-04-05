using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _id = _packet.ReadInt();

        Debug.Log($"Message from server {_msg}");
        Client.instance.myId = _id;

        // TODO: Send back the packet to the server.
    }
}
