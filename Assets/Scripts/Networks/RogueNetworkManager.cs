using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class RogueNetworkManager : NetworkManager
{
    [SerializeField] private GameObject unitSpawnerPrefab = null;
    [SerializeField] private GameObject gameManager ; 


    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);
        gameManager.GetComponent<GameManager>().AddPlayer(conn.identity.gameObject, "Player " + numPlayers);

       // print("a player was added " + numPlayers); 



    }

}
