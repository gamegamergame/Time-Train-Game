using UnityEngine;

public class NormalEnemy : EnemyScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void HitByLightAttack()
    {
        print("Light HIT!");
        health--;

        //TODO: Add code to freeze the enemy momentarily, play particle effect, flash the sprite, or anything else needed



    }

    public override void HitByHeavyAttack()
    {
        print("Heavy HIT!");
        health--;

        //TODO: add force to make the enemy fall backwards
        //enemyRB.AddForce();
        //enemyRB.Slide(-enemyRB.linearVelocity * heavyAtkForce, heavyAtkDownTime, new Rigidbody2D.SlideMovement());

        //TODO: Add code to freeze the enemy momentarily, play particle effect, flash the sprite, or anything else needed

    }
}
