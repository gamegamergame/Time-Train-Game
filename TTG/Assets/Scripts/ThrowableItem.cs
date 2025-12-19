using UnityEngine;

public class ThrowableItem : MonoBehaviour
{
    [SerializeField]
    private float throwForce;

    [SerializeField]
    private float throwSpin;

    private Rigidbody2D rb;

    private CircleCollider2D circleCollider;

    private bool isThrown = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Throw()
    {
        gameObject.transform.parent = null;

        rb.AddForce(transform.up * throwForce, ForceMode2D.Impulse);
        rb.AddTorque(throwSpin);

        isThrown = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isThrown)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                collision.gameObject.GetComponent<EnemyScript>().HitByAttack("Light Throw", Vector2.zero);



                Destroy(gameObject);
            }
            //TODO: should play an animation/particle effects when destroyed
        }
    }
}