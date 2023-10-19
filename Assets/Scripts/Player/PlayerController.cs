using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action KeyDownEvent;
    public event Action KeyUpEvent;
    public event Action KeyPressEvent;
    public event Action OnAttackEvent;
    public event Action EquipEvent;
    public event Action UnEquipEvent;
    public event Action OnRunnigEvent;
    public event Action OnWalkEvent;
    public event Action OnSleepEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action<Vector2> OnMoveEvent;
    //TODO Ctrl 키 눌렀을 때 조준모드 추가
    protected bool IsAttacking { get; set; }
    private float _timeSinceLastAttack = float.MaxValue;

    protected virtual void Update()
    {
        HandleAttackDelay();
    }
    private void HandleAttackDelay()
    {
        if (_timeSinceLastAttack <= 0.2f)    // TODO
        {
            _timeSinceLastAttack += Time.deltaTime;
        }

        if (IsAttacking && _timeSinceLastAttack > 0.2f)
        {
            _timeSinceLastAttack = 0;
            CallAttack();
        }
    }
    public void CallKeyDown()   //key down call event
    {
        KeyDownEvent?.Invoke();
    }

    public void CallKeyUp()     //key up call event
    {
        KeyUpEvent?.Invoke();
    }

    public void CallKeyPress()      //key press call event
    {
        KeyPressEvent?.Invoke();
    }

    public void CallMove(Vector2 direction)      //on move event
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallAttack()    //on attack event
    {
        OnAttackEvent?.Invoke();
    }

    public void CallRunnig()
    {
        OnRunnigEvent?.Invoke();
    }

    public void CallWalk()
    {
        OnWalkEvent?.Invoke();
    }

    public void CallEquip()     //equip event
    {
        EquipEvent?.Invoke();
    }

    public void CallUnEquip()   //unequip event
    {
        UnEquipEvent?.Invoke();
    }

    public void CallLook(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }

    public void CallSleep()     // 자는 경우에 시간 흐르면서 좀비가 공격하면 깬다
    {
        OnSleepEvent?.Invoke();
    }
    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        //canLook = !toggle;
    }
}
