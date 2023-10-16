using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private const float MINUS_STAT = 0.1f;
    private PlayerStatsHandler _playerStatsHandler;
    [SerializeField]
    private PlayerStats _updatePlayerStats;

    private void Awake()
    {
        _playerStatsHandler = GetComponent<PlayerStatsHandler>();
    }

    private void Start()
    {
        StartCoroutine("CO_PlayerStatsUpdate");
    }

    IEnumerator CO_PlayerStatsUpdate()
    {
        _playerStatsHandler.currentStats.health -= _updatePlayerStats.health * MINUS_STAT;
        _playerStatsHandler.currentStats.thirsty -= _updatePlayerStats.thirsty * MINUS_STAT;
        _playerStatsHandler.currentStats.hunger -= _updatePlayerStats.hunger * MINUS_STAT;
        if (_playerStatsHandler.currentStats.isRunning)
            _playerStatsHandler.currentStats.fatigability -= _updatePlayerStats.fatigability *MINUS_STAT * 1.2f;   //뛸 때 피로도 누적 임시로1.2배
        else
            _playerStatsHandler.currentStats.fatigability -= _updatePlayerStats.fatigability * MINUS_STAT;
        if (_playerStatsHandler.currentStats.isSleep)
            _playerStatsHandler.currentStats.fatigability += _updatePlayerStats.fatigability * MINUS_STAT *  3.0f;   //잘 때 피로도 회복

        Debug.Log(_playerStatsHandler.currentStats.health + "  " + _playerStatsHandler.currentStats.thirsty
            + "  " + _playerStatsHandler.currentStats.hunger + "  " + _playerStatsHandler.currentStats.fatigability);
        yield return new WaitForSeconds(0.1f);
    }

}
