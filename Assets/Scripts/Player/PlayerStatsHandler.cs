using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsHandler : MonoBehaviour
{
    private const int MAX_LEVEL = 10;

    [SerializeField]
    private PlayerStats _basePlayerStats;
    public PlayerStats currentStats { get; private set; }

    public List<PlayerStats> playerStatsModifire = new List<PlayerStats>();


    private void Awake()
    {
        UpdateStatsModifire();
    }

    public void AddStatModifier(PlayerStats statModifier)
    {
        playerStatsModifire.Add(statModifier);
        UpdateStatsModifire();
    }

    public void RemoveStatModifier(PlayerStats statModifier)
    {
        playerStatsModifire.Remove(statModifier);
        UpdateStatsModifire();
    }

    public void UpdateStatsModifire()
    {
        currentStats = new PlayerStats();
        UpdateStats((a, b) => a + b, _basePlayerStats);

        foreach (PlayerStats modifire in playerStatsModifire)
        {
            UpdateStats((a, b) => a + b, modifire);
        }

        LimitAllStats();
    }

    private void UpdateStats(Func<float, float, float> operation, PlayerStats modifire)
    {
        currentStats.health = (int)(operation(currentStats.health, modifire.health));
        currentStats.hunger = (int)(operation(currentStats.hunger, modifire.hunger));
        currentStats.thirsty = (int)(operation(currentStats.thirsty, modifire.thirsty));
        currentStats.fatigability = (int)(operation(currentStats.fatigability, modifire.fatigability));
        currentStats.strengthExperience = (int)(operation(currentStats.strengthExperience, modifire.strengthExperience));
        currentStats.runnigSpeedExperience = (int)(operation(currentStats.runnigSpeedExperience, modifire.runnigSpeedExperience));
    }

    public void LimitAllStats()
    {
        LimitStats(ref currentStats.health, 100.0f);
        LimitStats(ref currentStats.strength, 10.0f);
        LimitStats(ref currentStats.thirsty, 100.0f);
        LimitStats(ref currentStats.fatigability, 100.0f);
        LimitStats(ref currentStats.attackSpeed, 5.0f);
        LimitStats(ref currentStats.moveSpeed, 5.0f);
    }

    private void LimitStats(ref float stat, float minVal)
    {
        stat = Mathf.Min(stat, minVal);
    }
}
