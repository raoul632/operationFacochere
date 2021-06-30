using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class GameManager : NetworkBehaviour
{
    [SerializeField] GameObject infoplayerprefab; 
    Dictionary<GameObject,GameObject> players;
    [Server]
    private void Awake()
    {
        players = new Dictionary<GameObject, GameObject>(); 
    }
    /// <summary>
    /// add a player, and add information to this player 
    /// </summary>
    /// <param name="gameObject"></param>
    /// 
    [Server]
    public void AddPlayer(GameObject gameObject, string name)
    {
        print("add player"); 
        GameObject uiinfo = Instantiate(infoplayerprefab, gameObject.transform.position, Quaternion.identity);
       
        uiinfo.GetComponent<UIinfoPlayer>().SetName(name);
        NetworkServer.Spawn(uiinfo, connectionToClient);

        players.Add(gameObject,uiinfo ); 
    }
    // Start is called before the first frame update
    void Start()
    {
        players = new Dictionary<GameObject, GameObject>(); 
    }

    // Update is called once per frame
    [ServerCallback]
    void Update()
    {

        //information follow the player 
        foreach (KeyValuePair<GameObject, GameObject> entry in players)
        {
            if(entry.Value != null && entry.Key != null )
            entry.Value.transform.position = entry.Key.transform.position; 
            
        }



    }
}
