using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class PlayerPhysics : NetworkBehaviour
{

    public  Rigidbody2D rb;


    [SyncVar]//all the essental varibles of a rigidbody
    public Vector3 Velocity;
    [SyncVar]
    public float Rotation;
    [SyncVar]
    public Vector3 Position;
    [SyncVar] 
    public float AngularVelocity; 
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(isServer)
        {
            Position = rb.position;
            Rotation = rb.rotation;
            Velocity = rb.velocity;
            AngularVelocity = rb.angularVelocity;

        }

        if(isClient)
        {
            rb.velocity = Velocity;

        }
        
    }

    [Command]//function that runs on server when called by a client
    public void CmdResetPose()
    {
        rb.position = new Vector3(0, 1, 0);
        rb.velocity = new Vector3();
    }
    public void ApplyForce(Vector3 force, ForceMode FMode)//apply force on the client-side to reduce the appearance of lag and then apply it on the server-side
    {
        rb.AddForce(force);
        CmdApplyForce(force, FMode);

    }

    public void ApplyVelocity(Vector2 velocity)
    {
        rb.velocity = velocity; 

    }
    [Command]//function that runs on server when called by a client
    public void CmdApplyForce(Vector3 force, ForceMode FMode)
    {
        rb.AddForce(force);//apply the force on the server side
    }
}
