using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementController : MonoBehaviour
{

    //Player Movement
    Vector3 objectPosition;
    Vector2 lookDirection = Vector3.zero;
    Vector3 moveDirection = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    public Vector2 LookDirection {  get { return lookDirection; } }

    Rigidbody2D rb;

    //public Vector3 Direction { get { return direction; } }

    [SerializeField]
    float speed;

    //[SerializeField]
    //float dSpeed;

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
        lookDirection = cursorPos.position - transform.position;

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    // makes the player move a certain direction using WASD
    public void SetMoveDirection(Vector2 newMoveDirection)
    {
        //direction = newDirection.normalized;

        if (moveDirection != Vector3.zero)
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
        //velocity = newDirection * speed * Time.deltaTime;
        velocity = newMoveDirection * speed;
    }

    //activates the dodge which will move you quickly while making you harder to hit
    public void Dodge()
    {
        //reminder to fix dodging and make it more smooth and an actual dodge
        //objectPosition += direction * dSpeed * Time.deltaTime;

        //playerState = pState.dodging;

        //pCollider.size = pCollider.size / 2;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + moveDirection * 2);
    }
}
