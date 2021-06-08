using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform handGun;
    
    private Camera _cam;
    private Animator _animator;
    private Vector2 moveInput ;
 


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>(); 
        _cam = Camera.main; 
    }

    // Update is called once per frame
    void Update()
    {


        moveInput.y = Input.GetAxis("Vertical") ;
        moveInput.x = Input.GetAxis("Horizontal") ;

        rb.velocity = moveInput * speed;
        print(rb.velocity);


        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = _cam.WorldToScreenPoint(transform.position);

        if (mousePosition.x < playerPosition.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            handGun.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            handGun.localScale = Vector3.one;
        }



        Vector3 direction = mousePosition - playerPosition;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        handGun.rotation = Quaternion.Euler(0, 0, angle);


        //transform.Translate(moveHorizontal, moveVertical, 0);  <-- applique le mouvement au transform pas a la physique
        if (rb.velocity != Vector2.zero)
        {
            _animator.SetBool("IsMoving", true);
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }



    }
    
}
