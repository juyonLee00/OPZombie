using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerStatsHandler _playerStatsHandler;

    private Vector2 _moveDirection = Vector2.zero;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerStatsHandler = GetComponent<PlayerStatsHandler>();
    }

    private void Start()
    {
        _playerController.OnMoveEvent += Move;
    }

    private void FixedUpdate()
    {
        ApplyMove(_moveDirection);
    }

    private void Move(Vector2 direction)
    {
        _moveDirection = direction;
    }

    private void ApplyMove(Vector2 direction)
    {
        if (_playerStatsHandler.currentStats.isRunning)
        {
            direction = direction * _playerStatsHandler.currentStats.moveSpeed * 1.5f;  //달릴 때 속도 임의로 1.5배
        }
        else
        {
            direction = direction * _playerStatsHandler.currentStats.moveSpeed;
        }

        _rigidbody.velocity = direction;
    }
}
