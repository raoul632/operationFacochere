using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10.0f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float moveVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float moveHorizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime; 
        transform.Translate(moveHorizontal, moveVertical, 0); 
        
    }
}
