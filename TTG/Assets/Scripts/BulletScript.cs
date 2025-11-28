using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    float bSpeed = 10.0f;
    //float bVelocity;

    float despawnTimer;

    [SerializeField]
    float despawnTimeCooldown = 10.0f;

    //[SerializeField]
    CapsuleCollider2D col;
    Rigidbody2D rb;
    SpriteRenderer sr;


    //num of times a bullet has bounced, used for despawning
    int numRicochet;


    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        despawnTimer = 0;
        numRicochet = 0;

        //bVelocity = bSpeed * Time.deltaTime;

    }

    // Update is called once per frame
    void Update()
    {
        //adds constant force in the direction the bullet is facing when instantiated
        //bVelocity = bSpeed * Time.deltaTime;
        //Debug.Log(bVelocity);
        //transform.Translate(0, bVelocity, 0, Space.Self);


        //despawning bullets based on timer or the amount of ricochets
        despawnTimer += Time.deltaTime;
        if (despawnTimer > despawnTimeCooldown || numRicochet > 2)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.up * bSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("test1");
        //adds a ricochet if bullet hits a wall
        if (col.IsTouchingLayers(LayerMask.GetMask("Walls")))
        {
            Ricochet(collision);
            //rb.freezeRotation = true;
        }
        //if (col.IsTouchingLayers(LayerMask.GetMask("People")))
        //{

        //}
    }

    /// <summary>
    /// takes the first contact point in the collision and reflects the bullet based on its direction
    /// </summary>
    /// <param name="collision"></param>
    void Ricochet(Collision2D collision)
    {
        //rb.MoveRotation(Quaternion.LookRotation(rb.linearVelocity));
        //rb.SetRotation(transform.rotation.z);

        //transform.Rotate(0,0,transform.rotation.z);


        //ContactPoint2D contactPoint = collision.contacts[0];

        //rb.linearVelocity = Vector2.Reflect(, contactPoint.normal);

        numRicochet++;
    }

    private void OnDrawGizmos()
    {
        //direction vector
        //Gizmos.DrawLine(transform.position, transform.position + new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z) * 2);
    } 
}