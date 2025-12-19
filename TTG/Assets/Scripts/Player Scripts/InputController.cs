using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.LowLevel;

public class InputController : MonoBehaviour
{
    [SerializeField]
    PlayerManager playerManager;

    [SerializeField]
    MovementController movementController;

    [SerializeField]
    PlayerAttacks pAtkScript;

    [SerializeField]
    GameManager manager;

    public void OnMove(InputAction.CallbackContext context)
    {
        movementController.SetMoveDirection(context.ReadValue<Vector2>());
    }
    public void OnLightAtk(InputAction.CallbackContext context)
    {
        //reminder to code switching number when you switch weapons
        //if (manager)
        //number represents what weapon you are holding and boolean tells method if the player is the one firing
        //manager.SpawnBullet(true, 1);

        pAtkScript.Attack("Light");

    }
    public void OnHeavyAtk(InputAction.CallbackContext context)
    {
        pAtkScript.Attack("Heavy");
    }
    public void OnBT(InputAction.CallbackContext context)
    {
        manager.BulletTime();
    }

    public void OnPickup(InputAction.CallbackContext context)
    {
        playerManager.TryPickupDropItem();
    }
}
