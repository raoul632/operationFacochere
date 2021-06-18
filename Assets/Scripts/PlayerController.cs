using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class PlayerController : NetworkBehaviour
{
    [SerializeField] float speed ;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform handGun;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint; 
    
    private Camera _cam;
    private Animator _animator;
    private Vector2 moveInput ;
    private float myTime = 0.0F;





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

       if (!hasAuthority)
        {
            return;
        }


        moveInput.y = Input.GetAxis("Vertical");
        moveInput.x = Input.GetAxis("Horizontal");

        moveInput.Normalize();

        rb.velocity = moveInput * speed;
       


        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = _cam.WorldToScreenPoint(transform.localPosition);

        if (mousePosition.x < playerPosition.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            handGun.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            handGun.localScale = Vector3.one;
        }



        Vector2 offset = new Vector2(mousePosition.x - playerPosition.x, mousePosition.y - playerPosition.y);

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

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

        float nextFire = 0.5f;
        myTime = myTime + Time.deltaTime;

        if (Input.GetButton("Fire1") && myTime > nextFire)
        {
           
            CmdFire(); 
         
            nextFire = nextFire - myTime;
            myTime = 0.0f;
        }

    }

    [ServerCallback]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collision avec le joueur detecter");
        }
    }

    #region server
    [Command(requiresAuthority = false)]
    private void CmdFire()
    {

        if (!this.isLocalPlayer)
        {
            print("na pas les droits �a degage");
            return;
        }
        GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation);
        
        NetworkServer.Spawn(projectile, connectionToClient);
       // RpcSetParent(projectile, gameObject); 


    }

    [ClientRpc]
    void RpcSetParent(GameObject obj, GameObject parent) {
        if (!isLocalPlayer)
        {
            obj.transform.parent = parent.transform;
        }
    
    }
    #endregion
}
