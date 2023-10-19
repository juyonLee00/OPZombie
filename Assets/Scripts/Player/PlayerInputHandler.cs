using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : PlayerController
{
    PlayerStatsHandler _playerStatsHandler;
    PlayerAnimation _playerAnim;
    private Camera _camera;
    private Vector3 _cameraPosition;
    private bool _isLookMode = false;

    private void Start()
    {
        _camera = Camera.main;
        _playerAnim = GetComponent<PlayerAnimation>();
        Cursor.lockState = CursorLockMode.Locked;
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

        if (Input.GetMouseButton(1))
        {
            _isLookMode = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            _isLookMode = false;
        }
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveDirect = value.Get<Vector2>().normalized;
        Debug.Log(moveDirect);
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
        Debug.Log(newAim);
        if (_isLookMode)
        {

            Debug.Log(newAim);
            newAim = newAim - new Vector2(transform.position.x, transform.position.y);
            _playerAnim.SetDirection(newAim);
            //Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
            //newAim = (worldPos - (Vector2)transform.position).normalized;
            CallLook(newAim);
        }

    }



    public void ToggleCursor(bool toggle)
    {
        //Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
