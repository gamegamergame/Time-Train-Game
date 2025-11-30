using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    protected GameManager manager;

    protected GameObject player;
    protected Transform playerPos;

    /*//Enemy Movement
    Vector2 objectPosition;
    Vector2 direction = Vector3.zero;
    Vector2 velocity = Vector3.zero;
    //public Vector3 Direction { get { return direction; } }*/

    [SerializeField]
    protected int health;
    public int Health { get; set; }


    [SerializeField]
    protected float speed;
    public float Speed { get; set; }


    [Header("Attack Behavior")]
    [SerializeField]
    protected float lightAtkForce;
    [SerializeField]
    protected float heavyAtkForce;



    protected Rigidbody2D enemyRB;
    protected BoxCollider2D enemyCollider;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("player");
        playerPos = player.transform;

        manager = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();

        enemyCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*void SetLookDirection()
    {
        //source: https://www.youtube.com/watch?v=149teLQMmOQ
        Vector2 dir = playerPos.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }*/

    abstract public void HitByAttack(string attackType, Vector2 playerDir);
}
