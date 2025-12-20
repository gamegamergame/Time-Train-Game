using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerManager : MonoBehaviour
{
    //player scripts
    private MovementController movementController;

    private PlayerAttacks playerAttacks;


    //Health Points
    [SerializeField]
    int health = 3;
    public int Health { get { return health; } }
    
    private BoxCollider2D playerCollider;


    //Pickup and Drop Throwable Items
    private CircleCollider2D pickupCollider;

    private bool canPickupDrop = true;

    private float pickupDropStart;

    [SerializeField]
    private float pickupDropDelay = 0.25f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        pickupCollider = GetComponentInChildren<CircleCollider2D>();
        
        movementController = GetComponent<MovementController>();
        playerAttacks = GetComponent<PlayerAttacks>();

        pickupDropStart = pickupDropDelay;
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

        if (pickupDropStart <= 0)
        {
            canPickupDrop = true;
            pickupDropStart = pickupDropDelay;
        }

        if (!canPickupDrop)
        {
            pickupDropStart -= Time.deltaTime;
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
        //Player loses health when hit by an enemy
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Bullets")))
        {
            health--;
            Destroy(collision.gameObject);
        }
    }
}
