using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    public float health = 100.0f;
    public float hunger = 100.0f;
    public float thirsty = 100.0f;
    public float strength = 10.0f;
    public float moveSpeed = 5.0f;
    public float fatigability = 100.0f;
    public float attackSpeed = 1.0f;
    public float zombieInfect = 100.0f;
    public bool isZombie = false;
    public bool isRunning = false;
    public bool isSleep = false;
    public float strengthExperience = 0.0f;
    public float runnigSpeedExperience = 0.0f;
    //공격 딜레이 ,최대 속도 등 결정
}
