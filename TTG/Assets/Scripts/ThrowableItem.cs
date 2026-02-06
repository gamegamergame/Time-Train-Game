using UnityEngine;

public class ThrowableItem : MonoBehaviour
{
    [SerializeField]
    private float throwForce;

    [SerializeField]
    private float throwSpinForce;

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

    /// <summary>
    /// Light throw 
    /// Heavy Throw that changes the force of the throw based on how long the player charges the attack
    /// </summary>
    /// <param name="power"></param>
    public void Throw(float heavyAttackPower)
    {
        gameObject.transform.parent = null;

        //If the player does a light throw the heavy attack power will be zero
        rb.AddForce(transform.up * (throwForce + heavyAttackPower), ForceMode2D.Impulse);
        rb.AddTorque(throwSpinForce);

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