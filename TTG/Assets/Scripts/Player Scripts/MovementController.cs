using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementController : MonoBehaviour
{

    //Player Movement
    Vector3 objectPosition;
    Vector3 direction = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    Rigidbody2D rb;

    //public Vector3 Direction { get { return direction; } }

    [SerializeField]
    float speed;

    //[SerializeField]
    //float dSpeed;


    //stuff needed for health
    [SerializeField]
    int health = 3;

    public int Health {  get { return health; } }

    //[SerializeField]
    BoxCollider2D pCollider;

    //List<BoxCollider2D> damage;

    [SerializeField]
    Transform cursorPos;

    [SerializeField]
    GameManager manager;

    //int bulletsInGun = 6;

    //TODO: Implement states
    public enum pState
    {
        moving, attacking, dodging
    }

    pState playerState;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        pCollider = GetComponent<BoxCollider2D>();
        //objectPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //movement code


        //objectPosition += velocity;

        SetLookDirection();


        //health stuff

        //goes through the list of enemy bullets and their colliders to check if any have hit the player
        //foreach (GameObject bullet in manager.EnemyBulletsList)
        //{
            //if (pCollider.IsTouching(bullet.GetComponent<BoxCollider2D>()) || 
                //bullet.GetComponent<BoxCollider2D>().IsTouching(pCollider)) 
            //{ health--; }
        //}
        
        //if you are dead than take you to game over screen
        if (health <= 0)
        {
            //game over scene
            Debug.Log("You are dead");
        }


        //transform.position = objectPosition;
    }

    //movement code
    private void FixedUpdate()
    {
        rb.AddForce(velocity);
    }

    //rotates player based on the cursors position
    void SetLookDirection()
    {
        //source: https://www.youtube.com/watch?v=149teLQMmOQ
        Vector2 dir = cursorPos.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    // makes the player move a certain direction using WASD
    public void SetMoveDirection(Vector2 newDirection)
    {
        //direction = newDirection.normalized;

        if (direction != Vector3.zero)
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
        //velocity = newDirection * speed * Time.deltaTime;
        velocity = newDirection * speed;
    }

    //activates the dodge which will move you quickly while making you harder to hit
    public void Dodge()
    {
        //reminder to fix dodging and make it more smooth and an actual dodge
        //objectPosition += direction * dSpeed * Time.deltaTime;

        //playerState = pState.dodging;

        //pCollider.size = pCollider.size / 2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Player loses health when hit by a bullet and bullet despawns
        if (pCollider.IsTouchingLayers(LayerMask.GetMask("Bullets")))
        {
            health--;
            Destroy(collision.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + direction * 2);
    }
}
