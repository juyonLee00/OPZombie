using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _playerAnim;

    public string[] idleDirections = { "Idle N", "Idle NW", "Idle W", "Idle SW", "Idle S", "Idle SE", "Idle E", "Idle NE" };
    public string[] walkDirections = { "Walk N", "Walk NW", "Walk W", "Walk SW", "Walk S", "Walk SE", "Walk E", "Walk NE" };
    public string[] runDirections = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };
    public string[] attackDirections = { "Attack N", "Attack NW", "Attack W", "Attack SW", "Attack S", "Attack SE", "Attack E", "Attack NE" };
    public string[] dieDirections = { "Die N", "Die NW", "Die W", "Die SW", "Die S", "Die SE", "Die E", "Die NE" };
    public Vector2[] normalizedVec = { new Vector2(0f, 1f), new Vector2(-1f, 1f), new Vector2(-1f, 0f), new Vector2(-1f, -1f), 
                                    new Vector2(0f, -1f), new Vector2(1f, -1f), new Vector2(1f, 0f), new Vector2(1f, 1f)};
    string[] directionArray;

    private PlayerController _playerController;
    private Transform _weaponPoint;
    int lastDirection;
    private Vector2 _direction;
    private bool _isIdle = false;

    private void Awake()
    {
        _playerAnim = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        _weaponPoint = transform.GetChild(0);
    }

    public void SetIdleDirection(Vector2 direction)
    {
        _isIdle = true;
        directionArray = null;

        directionArray = idleDirections;

        lastDirection = DirectionToIndex(direction);
        _playerAnim.Play(directionArray[lastDirection]);
        _direction = direction;
    }

    public void SetWalkDirection(Vector2 direction)
    {
        directionArray = null;

        directionArray = walkDirections;

        lastDirection = DirectionToIndex(direction);
        _playerAnim.Play(directionArray[lastDirection]);
    }

    public void SetRunDirection(Vector2 direction)
    {
        _isIdle = false;
        directionArray = null;

        directionArray = runDirections;

        lastDirection = DirectionToIndex(direction);
        _playerAnim.Play(directionArray[lastDirection]);
    }

    public void SetAttackDirection()
    {
        directionArray = null;

        directionArray = attackDirections;
        if (_isIdle)
        {
            lastDirection = DirectionToIndex(_direction);
            _playerAnim.Play(directionArray[lastDirection]);
        }
    }

    public void SetDieDirection(Vector2 direction)
    {
        _isIdle = false;
        directionArray = null;

        directionArray = dieDirections;

        lastDirection = DirectionToIndex(direction);
        _playerAnim.Play(directionArray[lastDirection]);
    }

    private int DirectionToIndex(Vector2 direction)
    {
        Vector2 norDir = direction.normalized;
        float step = 360 / 8;
        float offset = step / 2;

        float angle = Vector2.SignedAngle(Vector2.up, norDir);

        angle += offset;
        angle = angle - (angle % offset);

        if (angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;
        int count = Mathf.FloorToInt(stepCount);
        
        _weaponPoint.transform.position = (new Vector2(transform.position.x, transform.position.y) 
                                         - normalizedVec[count].normalized * 0.2f) + normalizedVec[(count + 7) / normalizedVec.Length]*0.1f;
        return count;
    }
}
