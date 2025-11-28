using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI ammoText;
    public Image bulletTimeBar;

    [SerializeField]
    MovementController pScript;

    [SerializeField]
    GameManager manager;

    // Update is called once per frame
    void Update()
    {
        TextUpdate();
    }



    //updates UI text to display 
    public void TextUpdate()
    {
         healthText.text = "HP: " + pScript.Health.ToString();
         ammoText.text = "Ammo: " + manager.PlayerBullets.ToString();

        //ADD UI FOR BULLET TIME COOLDOWN BAR
    }
}
