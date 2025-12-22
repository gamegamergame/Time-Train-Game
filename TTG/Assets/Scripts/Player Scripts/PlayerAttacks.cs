using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    private bool canAtk = true;

    private GameObject player;


    [SerializeField]
    private BoxCollider2D lightAttackHurtbox;

    [SerializeField]
    private BoxCollider2D heavyAttackHurtbox;


    //Throwable item attacks
    private bool isHoldingItem = false;

    public bool IsHoldingItem {  get { return isHoldingItem; } set { isHoldingItem = value; } }

    private ThrowableItem item;

    public ThrowableItem ThrowableItem { get { return item; } set { item = value; } }


    [Header("Light Attack")]
    //Light Attack hurt box
    [SerializeField]
    private float lightHurtboxWidth;

    [SerializeField]
    private float lightHurtboxHeight;

    [SerializeField]
    private Vector2 lightHurtboxOffset;

    //delay between attacks
    [SerializeField]
    private float lightAtkDelay = 0.25f;
    private float lightAtkStart;

    [Space]

    [Header("Heavy Attack")]

    //Heavy Attack hurt box
    [SerializeField]
    private float heavyHurtboxWidth;

    [SerializeField]
    private float heavyHurtboxHeight;

    [SerializeField]
    private Vector2 heavyHurtboxOffset;

    //delay between attacks
    [SerializeField]
    private float heavyAtkDelay = 0.4f;
    private float heavyAtkStart;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lightAtkStart = lightAtkDelay;
        heavyAtkStart = heavyAtkDelay;

        lightAttackHurtbox.offset = lightHurtboxOffset;
        heavyAttackHurtbox.offset = heavyHurtboxOffset;

        player = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Checking if the player can attack by seeing if both attack delays are finished
        if (lightAtkStart <= 0)
        {
            canAtk = true;
            lightAtkStart = lightAtkDelay;
        }
        if (heavyAtkStart <= 0)
        {
            canAtk = true;
            heavyAtkStart = heavyAtkDelay;
        }

        if (!canAtk)
        {
            lightAtkStart -= Time.deltaTime;
            heavyAtkStart -= Time.deltaTime;
        }


        //making sure to update the position of the throwable item as it follows the player
        if (isHoldingItem)
        {
            item.gameObject.transform.position = gameObject.transform.position;
            item.gameObject.transform.rotation = gameObject.transform.rotation;
        }
    }

    /// <summary>
    ///  Enables and resizes the hurtbox based on the attack the player is doing
    ///  Checks for any enemies overlapping the hurtbox, then calls the appropriate attack functions when an enemy is hit
    ///  If the player is holding a throwable item and attacks this will call the Throw function
    /// </summary>
    /// <param name="attackType"></param>
    public void Attack(string attackType)
    {
        if (canAtk)
        {
            if (isHoldingItem)
            {
                ThrowItem(attackType);
                return;
            }





            //Setting up the hurtbox
            lightAttackHurtbox.enabled = true;
            lightAttackHurtbox.transform.rotation = gameObject.transform.rotation;
            
            heavyAttackHurtbox.enabled = true;
            heavyAttackHurtbox.transform.rotation = gameObject.transform.rotation;


            List<Collider2D> enemiesHit = new List<Collider2D>();

            //Changing the size of the hurtbox based on the attack
            switch (attackType)
            {
                case "Light":
                    lightAttackHurtbox.size = new Vector2(lightHurtboxWidth, lightHurtboxHeight);
                    print("Light attack");

                    //Checking if the attack has hit an enemy, if so call that enemy's hit function
                    lightAttackHurtbox.Overlap(enemiesHit);

                    break;

                case "Heavy":
                    heavyAttackHurtbox.size = new Vector2(heavyHurtboxWidth, heavyHurtboxHeight);
                    print("Heavy attack");

                    //Checking if the attack has hit an enemy, if so call that enemy's hit function
                    heavyAttackHurtbox.Overlap(enemiesHit);
                    break;
            }




            //TODO: Remember to leave room for the animation to finish before dealing damage


            foreach (Collider2D enemyCol in enemiesHit)
            {
                if (enemyCol.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    enemyCol.GetComponentInParent<EnemyScript>().HitByAttack(attackType, player.GetComponent<MovementController>().LookDirection);
                }
            }

            enemiesHit.Clear();
            lightAttackHurtbox.enabled = false;
            heavyAttackHurtbox.enabled = false;
            canAtk = false;
        }
    }

    /// <summary>
    /// The function for the throwable item attacks
    /// </summary>
    /// <param name="attackType"></param>
    public void ThrowItem(string attackType)
    {
        switch (attackType)
        {
            case "Light":
                print("Light Throw");
                item.Throw();
                break;

            case "Heavy":
                print("Heavy Throw");
                item.Throw();
                break;
        }
        isHoldingItem = false;
    }


    private void OnDrawGizmosSelected()
    {
        //Light Attack Hurtbox
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(
            lightAttackHurtbox.transform.position.x + lightHurtboxOffset.x, 
            lightAttackHurtbox.transform.position.y + lightHurtboxOffset.y, 
            lightAttackHurtbox.transform.position.z), 
            
            new Vector2(lightHurtboxWidth, lightHurtboxHeight));

        //Heavy Attack Hurtbox
        Gizmos.color = Color.darkRed;
        Gizmos.DrawWireCube(new Vector3(
            heavyAttackHurtbox.transform.position.x + heavyHurtboxOffset.x,
            heavyAttackHurtbox.transform.position.y + heavyHurtboxOffset.y,
            heavyAttackHurtbox.transform.position.z),

            new Vector2(heavyHurtboxWidth, heavyHurtboxHeight));

        //due to how the DrawWireCube function works the attack previews do not rotate with the player
    }
}
