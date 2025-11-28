using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    //Stuff neded for movement
    Vector2 objectPosition;
    Vector2 direction = Vector3.zero;
    Vector2 velocity = Vector3.zero;

    Rigidbody2D rb;

    //public Vector3 Direction { get { return direction; } }

    [SerializeField]
    float speed = 40.0f;

    [SerializeField]
    float dSpeed = 50.0f;


    Transform playerPos;

    //stuff needed for health
    [SerializeField]
    int health = 1;

    //public int Health { get { return health; } }

    [SerializeField]
    BoxCollider2D eCollider;

    //List<BoxCollider2D> damage;

    [SerializeField]
    GameManager manager;


    //TODO: Implement states
    public enum eState
    {
        stoping, moving, shooting, reloading
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerPos = GameObject.Find("player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        SetLookDirection();

        // if the enemy is within a certain distance and isnt out of bullets and has a line of sight with the player start shooting
        //if ()
        //{

        //}

    }

    void SetLookDirection()
    {
        //source: https://www.youtube.com/watch?v=149teLQMmOQ
        Vector2 dir = playerPos.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
