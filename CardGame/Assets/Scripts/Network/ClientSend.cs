using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);
            _packet.Write(SaveSystem.LoadDeck().ToString());

            SendTCPData(_packet);
        }
    }

    public static void PutCardOnTable(string _cardName)
    {
        using (Packet _packet = new Packet((int)ClientPackets.placeCardToTable))
        {
            _packet.Write(_cardName);

            SendTCPData(_packet);
        }
    }

    public static void EndTurn()
    {
        using (Packet _packet = new Packet((int)ClientPackets.endTurn))
        {
            SendTCPData(_packet);
        }
    }

    public static void Attack(string _attackFrom, string _attackTo)
    {
        using (Packet _packet = new Packet((int)ClientPackets.attack))
        {
            Debug.Log($"Attacking from {_attackFrom} to {_attackTo}.");
            _packet.Write(_attackFrom);
            _packet.Write(_attackTo);

            SendTCPData(_packet);
        }
    }
    #endregion
}
