using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    private bool canAtk = true;

    private GameObject player;

    [SerializeField]
    private BoxCollider2D attackHurtbox;

    [SerializeField]
    private Transform attackHurtboxPos;


    [Header("Light Attack")]
    //Light Attack hurt box
    [SerializeField]
    private float lightHurtboxWidth;

    [SerializeField]
    private float lightHurtboxHeight;

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

    //delay between attacks
    [SerializeField]
    private float heavyAtkDelay = 0.4f;
    private float heavyAtkStart;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lightAtkStart = lightAtkDelay;
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
    }

    /// <summary>
    ///  Enables and resizes the hurtbox based on the attack the player is doing
    ///  Checks for any enemies overlapping the hurtbox, then calls the appropriate attack functions when an enemy is hit
    /// </summary>
    /// <param name="attackType"></param>
    public void Attack(string attackType)
    {
        if (canAtk)
        {


            //TODO: Fix the collider position/size so that attacks cannot be activated from behind




            //Setting up the hurtbox
            attackHurtbox.enabled = true;
            attackHurtbox.transform.rotation = gameObject.transform.rotation;

            List<Collider2D> enemiesHit = new List<Collider2D>();

            //Changing the size of the hurtbox based on the attack
            switch (attackType)
            {
                case "Light":
                    attackHurtbox.size = new Vector2(lightHurtboxWidth, lightHurtboxHeight);
                    print("Light attack");

                    break;

                case "Heavy":
                    attackHurtbox.size = new Vector2(heavyHurtboxWidth, heavyHurtboxHeight);
                    print("Heavy attack");
                    break;
            }

            //Checking if the attack has hit an enemy, if so call that enemy's hit function
            attackHurtbox.Overlap(enemiesHit);


            //TODO: Remember to leave room for the animation to finish before dealing damage


            foreach (Collider2D enemyCol in enemiesHit)
            {
                if (enemyCol.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    enemyCol.GetComponentInParent<EnemyScript>().HitByAttack(attackType, player.GetComponent<MovementController>().LookDirection);
                }
            }

            attackHurtbox.enabled = false;
            canAtk = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackHurtboxPos.position, new Vector2(lightHurtboxWidth, lightHurtboxHeight));
        Gizmos.color = Color.darkRed;
        Gizmos.DrawWireCube(attackHurtboxPos.position, new Vector2(heavyHurtboxWidth, heavyHurtboxHeight));
    }
}
