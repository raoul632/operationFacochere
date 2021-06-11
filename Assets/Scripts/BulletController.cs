using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float moveSpeed;
    private Rigidbody2D rb; 
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject); 
    }
}
