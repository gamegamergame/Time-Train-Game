using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //player bullet prefab and transform for the spawn point of the bullets in front of the player
    /*
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform bulletSP;


    //code for adjusting weapon fire rate
    // reminder to change this code so that it each gun has a different fire rate
    float fireRateTimer = 0;

    [SerializeField]
    float fireRateCooldown = 0.5f;

    bool canFire;


    //vars for reloading
    [SerializeField]
    float reloadCooldown = 1.0f;

    [SerializeField]
    int playerBullets = 6;

    public int PlayerBullets {  get { return playerBullets; } }

    bool isReloading = false;
    */

    //bullet time
    [SerializeField]
    float slowTimeFloat = 0.05f;

    [SerializeField]
    float BTCooldown = 2.0f;

    bool isBTActive = false;

    [SerializeField]
    float remainingBulletTime = 5.0f;
    float remainingBTMax = 5.0f;

    public float BulletTimeUI { get { return remainingBulletTime; } }

    //float tScale;

    /*
    //list of all of the player's bullets
    List<GameObject> pBulletsList = new List<GameObject>();

    //list of all of the enemy bullets
    List<GameObject> eBulletsList = new List<GameObject>();

    public List<GameObject> EnemyBulletsList { get { return eBulletsList; } }
    */


    // Start is called before the first frame update
    void Start()
    {
        //gun is loaded on spawn
        //canFire = true;

        //stores time scale value so game can know when to reset it when BT is over
        //tScale = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        //REMINDER to make this work for each weapon
        //setting fire rate for the gun using timer
        /*if (canFire == false)
        {
            fireRateTimer += Time.deltaTime;
            if (fireRateTimer >= fireRateCooldown)
            {
                canFire = true;
                fireRateTimer = 0;
            }
        }

        //if mag is empty reload automatically
        if (playerBullets <= 0) {canFire = false; StartCoroutine(Reload());}
        */

        //starts bullet time cooldown
        //source: https://www.youtube.com/watch?v=0VGosgaoTsw
        if (isBTActive)
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
        }

    }

    //spawns players bullets depending on what weapon is in hand using players rotation and position
    //REMINDER spawns enemies bullets ...
    /*
    public void SpawnBullet(bool isPlayerBullet, int weaponEquipped)
    {
        if (isPlayerBullet) 
        {
            switch (weaponEquipped) 
            {
                case 0:
                    // sword
                    //code for swinging melee weapon
                    break;

                case 1:
                    //revolver
                    if (canFire == true)
                    {
                        playerBullets--;
                        pBulletsList.Add(Instantiate(bullet, bulletSP.position, bulletSP.rotation));
                        canFire = false;
                    }
                    break;
            }
        }
        else
        {
            //eBullets.Add() blah blah blah
        }
    }

    //reloads weapon for the player
    //timer source: https://www.youtube.com/watch?v=jwEPKY9poa4
    public IEnumerator Reload()
    {
        if (isReloading != true)
        {
            canFire = false;
            playerBullets = 0;
            isReloading = true;

            //make work with bullet time
            //if (canFire == false) { }
            yield return new WaitForSeconds(reloadCooldown);

            playerBullets = 6;
            isReloading = false;
        }
    }
    */
    //slows down the game by a certain factor while still allowing the player to aim at normal speed
    //has a cooldown
    public void BulletTime()
    {
        if (!isBTActive)
        {
            isBTActive = true;
            Time.timeScale = slowTimeFloat;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }
}