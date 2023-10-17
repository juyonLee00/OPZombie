using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Zombie : MonoBehaviour
{
    enum ZombieTye
    {
        Zombie,
        ZombieRunners,
        ZombieChubby
    }

    public enum AIState
    {
        idle,
        Wandering,
        Attacking

    }

    [Header("AI")]
    private AIState aiState;
    public float detectDistance;
    public float safeDistance;

    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;



    [Header("Stats")]
    [SerializeField] private float zombieSpeed;
    [SerializeField] private float zombieHealth;
    [SerializeField] private float zombieATK;
    [SerializeField] private float noisedetectionRange;

    [SerializeField] private LayerMask soundLayer;
    private Vector2 noiseLocation;
    private Transform zombieTransform;

    public float fieldofVIew = 120f;

    private NavMeshAgent agent;
    private SkinnedMeshRenderer[] meshRenderers;

    private Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //animator = GetComponentInChildren<Animator>();

        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        zombieTransform = transform;

        SetState(AIState.Wandering);
    }

    private void SetState(AIState Wandering)
    {
        throw new NotImplementedException();
    }
    public void NoiseDetection()
    {
        Collider[] colliders = Physics.OverlapSphere(zombieTransform.position, noisedetectionRange, soundLayer);
        
        foreach (Collider collider in colliders)
        {
            //감지된 오브젝트에 대한 처리( 좀비 목표 위치 설정)
            //좀비를 소음위치로 이동시키는 로직 추가
            Vector2 noiseLocation = collider.transform.position;
            MoveToNoiseLocation(noiseLocation);
        }

    }

    private void MoveToNoiseLocation(Vector2 noiseLocation)
    {
        //이동 로직
        //NavMesh
    }
}
