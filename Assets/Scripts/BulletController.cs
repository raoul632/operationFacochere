using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class BulletController : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float moveSpeed;
    private Rigidbody2D rb;
    [SerializeField] GameObject impactEffect; 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);
        rb.velocity = transform.right * moveSpeed; 
    }

    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
        Destroy(gameObject); 
    }

    
  
   
}
