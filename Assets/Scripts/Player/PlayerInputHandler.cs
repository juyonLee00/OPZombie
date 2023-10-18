using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : PlayerController
{
    PlayerStatsHandler _playerStatsHandler;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        _playerStatsHandler = GetComponent<PlayerStatsHandler>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("LeftDown");
            CallKeyDown();
        }
        if (Input.GetMouseButton(0))
        {
            CallKeyPress();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("LeftUP");
            CallKeyUp();
        }
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveDirect = value.Get<Vector2>().normalized;
        CallMove(moveDirect);
    }

    public void OnAttack(InputValue value)
    {
        IsAttacking = value.isPressed;
    }

    public void OnRunning(InputValue value)
    {
        _playerStatsHandler.currentStats.isRunning = true;
        CallRunnig();
    }

    public void OnWalk(InputValue value)
    {
        _playerStatsHandler.currentStats.isRunning = false;
        CallWalk();
    }

    public void Equip(InputValue value)
    {
        CallEquip();
    }

    public void UnEquip(InputValue value)
    {
        CallUnEquip();
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;
        if (newAim.magnitude >= .9f)
        {
            CallLook(newAim);
        }
    }
}
