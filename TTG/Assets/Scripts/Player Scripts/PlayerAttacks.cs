using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    private bool canAtk = true;


    private BoxCollider2D attackHurtBox;

    [SerializeField]
    Transform attackHurtboxPos;


    [Header("Light Attack")]
    //Light Attack hurt box
    [SerializeField]
    float lightHurtboxWidth;

    [SerializeField]
    float lightHurtboxHeight;

    //delay between attacks
    [SerializeField]
    float lightAtkDelay = 0.25f;
    private float lightAtkStart;

    [Space]

    [Header("Heavy Attack")]

    //Heavy Attack hurt box
    [SerializeField]
    float heavyHurtboxWidth;

    [SerializeField]
    float heavyHurtboxHeight;

    //delay between attacks
    [SerializeField]
    float heavyAtkDelay = 0.4f;

    private float heavyAtkStart;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lightAtkStart = lightAtkDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (lightAtkStart <= 0)
        {
            canAtk = true;
            lightAtkStart = lightAtkDelay;
        }
        if (!canAtk)
        {
            lightAtkStart -= Time.deltaTime;
        }
    }

    /// <summary>
    /// A light attack is a simple punch that does damage to an enemy
    /// </summary>
    public void Attack(string attackType)
    {
        if (canAtk)
        {
            //attackHurtBox = Instantiate(gameObject.AddComponent<BoxCollider2D>(), attackHurtboxPos);
            attackHurtBox = gameObject.AddComponent<BoxCollider2D>();


            //attackHurtBox.IsTouchingLayers(LayerMask.NameToLayer("Enemy"));

            Collider2D[] enemiesHit = new Collider2D[10];

            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.layerMask = LayerMask.NameToLayer("Enemy");

            if (attackType == "Light")
            {
                attackHurtBox.size = new Vector2(lightHurtboxWidth, lightHurtboxHeight);

                attackHurtBox.Overlap(contactFilter, enemiesHit);

                foreach (Collider2D enemyCol in enemiesHit)
                {
                    if (enemyCol != null)
                    {
                        enemyCol.GetComponentInParent<EnemyScript>().HitByLightAttack();
                    }
                }
            }
            else if (attackType == "Heavy")
            {
                attackHurtBox.size = new Vector2(heavyHurtboxWidth, heavyHurtboxHeight);

                attackHurtBox.Overlap(contactFilter, enemiesHit);

                foreach (Collider2D enemyCol in enemiesHit)
                {
                    if (enemyCol != null)
                    {
                        //enemyCol.GetComponentInParent<EnemyScript>().HitByLightAttack();
                    }
                }
            }


            Destroy(attackHurtBox);
            print("light attack");
            canAtk = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackHurtboxPos.position, new Vector2(lightHurtboxWidth, lightHurtboxHeight));
        Gizmos.color = Color.darkRed;
        //Gizmos.DrawWireCube(attackHurtboxPos.position, new Vector2(lightHurtboxWidth, lightHurtboxHeight));
    }
}
