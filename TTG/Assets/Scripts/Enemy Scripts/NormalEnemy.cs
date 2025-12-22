using UnityEngine;

public class NormalEnemy : EnemyScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //heavyAtkForce = 5f;

        enemyRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //enemyRB.AddForce(new Vector2(100,0));
    }

    public override void HitByAttack(string attackType, Vector2 playerDir)
    {
        //TODO: Add code to freeze the enemy momentarily, play particle effect, flash the sprite, or anything else needed

        switch (attackType)
        {
            case "Light":
                print("Light HIT!");
                health--;

                //TODO: This should stop the enemy for moving for something like a quarter of a second
                enemyRB.AddForce(playerDir * lightAtkForce);

                break;

            case "Heavy":
                print("Heavy HIT!");
                health--;

                enemyRB.AddForce(playerDir * heavyAtkForce);

                break;

            case "Light Throw":
                print("light throw hit");

                break;
        }
    }
}
