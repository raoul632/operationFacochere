using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class Health : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SyncVar]
    private int currentHealth;

    #region Server
    public void DealDamage(int damageAmount)
    {

    }

    #endregion

    #region Client

    #endregion
}
