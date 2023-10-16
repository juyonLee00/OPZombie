using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    enum ZombieTye
    {
        ZombieRunners,
        ZombieChubby
    }

    private Vector3 noiseLocation;
    [SerializeField] private float zombieSpeed;
    [SerializeField] private float zombieHealth;
    [SerializeField] private float zombieATK;
    [SerializeField] private float detectionRange;
    // Start is called before the first frame update

    public void NoiseDetection()
    {
        
    }
}
