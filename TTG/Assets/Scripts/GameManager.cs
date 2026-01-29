using UnityEngine;

public class GameManager : MonoBehaviour
{
    //bullet time
    /*[SerializeField]
    float slowTimeFloat = 0.05f;

    [SerializeField]
    float BTCooldown = 2.0f;

    bool isBTActive = false;

    [SerializeField]
    float remainingBulletTime = 5.0f;
    float remainingBTMax = 5.0f;

    public float BulletTimeUI { get { return remainingBulletTime; } }*/


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //starts bullet time cooldown
        //source: https://www.youtube.com/watch?v=0VGosgaoTsw
        /*if (isBTActive)
        {
            remainingBulletTime -= Time.deltaTime/slowTimeFloat;
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
                    remainingBulletTime = remainingBTMax;
                }
            }
        }*/

    }

    //slows down the game by a certain factor while still allowing the player to aim at normal speed
    //has a cooldown
    /*public void BulletTime()
    {
        if (!isBTActive)
        {
            isBTActive = true;
            Time.timeScale = slowTimeFloat;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }*/
}