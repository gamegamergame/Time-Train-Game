using UnityEngine;

public class MovementController : MonoBehaviour
{

    //Player Movement
    //Vector3 objectPosition;
    Vector2 lookDirection = Vector3.zero;
    Vector3 moveDirection = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    public Vector2 LookDirection {  get { return lookDirection; } }

    Rigidbody2D rb;

    [SerializeField]
    float speed;

    [SerializeField]
    Transform cursorPos;

    [SerializeField]
    GameManager manager;


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

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + moveDirection * 2);
    }
}
