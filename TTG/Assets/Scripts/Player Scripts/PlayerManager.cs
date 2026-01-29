using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //player scripts
    private MovementController movementController;

    private PlayerAttacks playerAttacks;


    //Health Points
    //TODO: Implement Taking Damage
    [SerializeField]
    int health = 3;
    public int Health { get { return health; } }
    
    private BoxCollider2D playerCollider;


    //Bullet time
    [Header("Bullet Time")]

    [SerializeField]
    float totalBulletTime = 5.0f;
    float remainingBulletTime;

    [SerializeField]
    float BTMultiplier = 0.5f;

    [SerializeField]
    float BTCooldown = 2.0f;

    bool isBTActive = false;

    public float BulletTimeUI { get { return remainingBulletTime; } }


    //Pickup and Drop Throwable Items
    private CircleCollider2D pickupCollider;

    private bool canPickupDrop = true;

    private float pickupDropStart;

    [Space]
    [SerializeField]
    private float pickupDropCooldown = 0.25f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        pickupCollider = GetComponentInChildren<CircleCollider2D>();
        
        movementController = GetComponent<MovementController>();
        playerAttacks = GetComponent<PlayerAttacks>();

        remainingBulletTime = totalBulletTime;

        pickupDropStart = pickupDropCooldown;
    }


    // Update is called once per frame
    void Update()
    {
        //Health reaches zero the player is dead
        if (health <= 0)
        {
            //game over scene
            Debug.Log("You are dead");
        }

        //Pickup/Drop Cooldown so you can't just spam pickup
        if (pickupDropStart <= 0)
        {
            canPickupDrop = true;
            pickupDropStart = pickupDropCooldown;
        }

        if (!canPickupDrop)
        {
            pickupDropStart -= Time.deltaTime;
        }


        //starts bullet time cooldown
        //source: https://www.youtube.com/watch?v=0VGosgaoTsw
        if (isBTActive)
        {
            remainingBulletTime -= Time.deltaTime / BTMultiplier;
            remainingBulletTime = Mathf.Clamp(remainingBulletTime, 0f, 999f);

            //gradually returns the timescale back to normal
            if (remainingBulletTime == 0)
            {
                Time.timeScale += (1f / BTCooldown) * Time.unscaledDeltaTime;
                Time.timeScale = Mathf.Clamp01(Time.timeScale);

                //when time scale is back to normal return fixed delta time back to normal and turn off BT
                if (Time.timeScale == 1)
                {
                    Time.fixedDeltaTime = 0.02f;
                    isBTActive = false;
                    remainingBulletTime = totalBulletTime;
                }
            }
        }
    }


    //slows down the game by a certain factor while still allowing the player to aim at normal speed
    //has a cooldown
    public void BulletTime()
    {
        if (!isBTActive)
        {
            isBTActive = true;
            Time.timeScale = BTMultiplier;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }


    //When the player picks up an item the item is attached to the player
    //When the player drops an item they are holding it falls back on the floor and can be picked up again
    public void TryPickupDropItem()
    {
        if (canPickupDrop)
        {
            if (!playerAttacks.IsHoldingItem)
            {
                //looks for any items that are on the Throwable Item layer 
                List<Collider2D> throwableItemCols = new List<Collider2D> { };
                pickupCollider.Overlap(throwableItemCols);

                if (throwableItemCols.Count > 0)
                {
                    //sends a reference to the throwable item to the player attack script so it knows what specific item is being thrown
                    playerAttacks.ThrowableItem = throwableItemCols[0].GetComponent<ThrowableItem>();


                    //TODO: Put an outline over the sprite to indicate that this is the object that is being picked up


                    //always picks up the first collider added to the list
                    throwableItemCols[0].transform.SetParent(gameObject.transform, true);
                    throwableItemCols[0].transform.position = gameObject.transform.position;
                    throwableItemCols[0].transform.rotation = gameObject.transform.rotation;


                    canPickupDrop = false;
                    playerAttacks.IsHoldingItem = true;
                }
            }
            else
            {
                //if the player has picked up an item then it deparents the items transform, thus dropping it on the ground
                if (playerAttacks.ThrowableItem != null)
                {
                    playerAttacks.ThrowableItem.transform.parent = null;
                }
                canPickupDrop = false;
                playerAttacks.IsHoldingItem = false;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //TODO: Implement Taking Damage
    }
}