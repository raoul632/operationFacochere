using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class RogueNetworkManager : NetworkManager
{
    [SerializeField] private GameObject unitSpawnerPrefab = null;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // add player at correct spawn position
     
        GameObject player = Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(conn, player);

       
    }
}
