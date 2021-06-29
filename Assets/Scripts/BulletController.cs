using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class BulletController : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float moveSpeed;
    [SerializeField]  private Rigidbody2D rb;
    [SerializeField] private GameObject impactEffect;



    // Update is called once per frame
    [Server]
    void FixedUpdate()
    {
        //transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);
        rb.velocity = transform.right * moveSpeed;

    }

        [ServerCallback]
    private void OnTriggerEnter2D(Collider2D collision)
    {

        print("collide"); 
        if(collision.TryGetComponent<NetworkIdentity>(out NetworkIdentity networkIdentity))
        {
            if(networkIdentity.connectionToClient == connectionToClient) { return;  }
        }
        //sinon collision avec le parent !! va savoir 
        //Physics2D.IgnoreCollision(transform.parent.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());


        //transform.parent
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Trigger collision avec le joueur detecter");
        }
       
        Instantiate(impactEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    [Server]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("collide");
        //sinon collision avec le parent !! va savoir 
        Physics2D.IgnoreCollision(transform.parent.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());


        //transform.parent
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Trigger collision avec le joueur detecter");
        }

        Instantiate(impactEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    

    [Server]
    private void OnBecameInvisible()
    {
        NetworkServer.Destroy(gameObject); 
    }

    
  
   
}
