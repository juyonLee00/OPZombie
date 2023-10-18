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

    private bool CheckPlayerStats()
    {
        if(_playerStatsHandler.currentStats.thirsty <= 0 || _playerStatsHandler.currentStats.hunger <= 0)
        {
            return true;
        }
        return false;
    }
    //감염도 70이상일 때 일정확률로 치유 -> 조건생각(0.1초마다 갱신되는 healthSystem)  -> 하루가 끝나거나 잠잘때
    private bool CheckPlayerZombieInfect()  
    {
        if (false)  // TODO  좀비에게 피격 시 확률 조건
        {
            _playerStatsHandler.currentStats.isZombie = true;
            return true;
        }
        else
        {
            _playerStatsHandler.currentStats.isZombie = false;
            return false;
        }
    }

    private bool PlayerIsDead()     
    {
        if (_playerStatsHandler.currentStats.health <= 0 || _playerStatsHandler.currentStats.zombieInfect <=0)
        {
            return true;
        }
        return false;
    }

    private IEnumerator CO_PlayerStatsUpdate()
    {
        while (true)
        {
            if (CheckPlayerStats()  ) 
            {
                _playerStatsHandler.currentStats.health -= _updatePlayerStats.health * MINUS_STAT;
            }
            if (CheckPlayerZombieInfect())      // TODO 감염 시 스탯 감소 효과 결정
            {
                _playerStatsHandler.currentStats.zombieInfect -= _updatePlayerStats.zombieInfect * MINUS_STAT;
            }
            _playerStatsHandler.currentStats.thirsty -= _updatePlayerStats.thirsty * MINUS_STAT;
            _playerStatsHandler.currentStats.hunger -= _updatePlayerStats.hunger * MINUS_STAT;
            if (_playerStatsHandler.currentStats.isRunning)
                _playerStatsHandler.currentStats.fatigability -= _updatePlayerStats.fatigability * MINUS_STAT * 1.2f;   //뛸 때 피로도 누적 임시로1.2배
            else
                _playerStatsHandler.currentStats.fatigability -= _updatePlayerStats.fatigability * MINUS_STAT;          
            if (_playerStatsHandler.currentStats.isSleep)
                _playerStatsHandler.currentStats.fatigability += _updatePlayerStats.fatigability * MINUS_STAT * 3.0f;   //잘 때 피로도 회복

            _playerStatsHandler.LimitAllStats();

            if (PlayerIsDead())
            {
                //죽었을 때 작동
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

}
