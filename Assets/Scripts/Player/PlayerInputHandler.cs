using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : PlayerController
{
    PlayerStatsHandler _playerStatsHandler;
    private Camera _camera;
    private Vector3 _cameraPosition;

    private void Start()
    {
        _camera = Camera.main;
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

        _cameraPosition = new Vector3(this.transform.position.x, this.transform.position.y, _camera.transform.position.z);

        _camera.transform.position = Vector3.Lerp(_cameraPosition, this.transform.position, 1.0f * Time.deltaTime);
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

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
