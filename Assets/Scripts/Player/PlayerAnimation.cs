using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _playerAnim;

    public string[] idleDirections = { "Idle N", "Idle NW", "Idle W", "Idle SW", "Idle S", "Idle SE", "Idle E", "Idle NE" };
    public string[] walkDirections = { "Walk N", "Walk NW", "Walk W", "Walk SW", "Walk S", "Walk SE", "Walk E", "Walk NE" };
    public string[] runDirections = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };
    public string[] attckDirections = { "Attack N", "Attack NW", "Attack W", "Attack SW", "Attack S", "Attack SE", "Attack E", "Attack NE" };
    public string[] dieDirections = { "Die N", "Die NW", "Die W", "Die SW", "Die S", "Die SE", "Die E", "Die NE" };
    string[] directionArray;

    private PlayerController _playerController;
    int lastDirection;

    private void Awake()
    {
        _playerAnim = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        _playerController.OnLookEvent += SetDirection;
    }

    public void SetDirection(Vector2 direction)
    {
        directionArray = null;

        if(direction.magnitude < 0.01)
        {
            directionArray = idleDirections;
        }
        else
        {
            directionArray = walkDirections;
            lastDirection = DirectionToIndex(direction);
        }

        _playerAnim.Play(directionArray[lastDirection]);
    }

    public void SetRunDirection(Vector2 direction)
    {
        directionArray = null;
    }

    private int DirectionToIndex(Vector2 direction)
    {
        Vector2 norDir = direction.normalized;
        float step = 360 / 8;
        float offset = step / 2;

        float angle = Vector2.SignedAngle(Vector2.up, norDir);

        angle += offset;

        if(angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;
        return Mathf.FloorToInt(stepCount);
    }
}
