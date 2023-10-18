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
    //������ 70�̻��� �� ����Ȯ���� ġ�� -> ���ǻ���(0.1�ʸ��� ���ŵǴ� healthSystem)  -> �Ϸ簡 �����ų� ���߶�
    private bool CheckPlayerZombieInfect()  
    {
        if (false)  // TODO  ���񿡰� �ǰ� �� Ȯ�� ����
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
            if (CheckPlayerZombieInfect())      // TODO ���� �� ���� ���� ȿ�� ����
            {
                _playerStatsHandler.currentStats.zombieInfect -= _updatePlayerStats.zombieInfect * MINUS_STAT;
            }
            _playerStatsHandler.currentStats.thirsty -= _updatePlayerStats.thirsty * MINUS_STAT;
            _playerStatsHandler.currentStats.hunger -= _updatePlayerStats.hunger * MINUS_STAT;
            if (_playerStatsHandler.currentStats.isRunning)
                _playerStatsHandler.currentStats.fatigability -= _updatePlayerStats.fatigability * MINUS_STAT * 1.2f;   //�� �� �Ƿε� ���� �ӽ÷�1.2��
            else
                _playerStatsHandler.currentStats.fatigability -= _updatePlayerStats.fatigability * MINUS_STAT;          
            if (_playerStatsHandler.currentStats.isSleep)
                _playerStatsHandler.currentStats.fatigability += _updatePlayerStats.fatigability * MINUS_STAT * 3.0f;   //�� �� �Ƿε� ȸ��

            _playerStatsHandler.LimitAllStats();

            if (PlayerIsDead())
            {
                //�׾��� �� �۵�
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

}
