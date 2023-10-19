using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : Equip
{
    public float attackRate;
    private bool attacking;
    public float attackDistance;
    private GameObject _player;
    private PlayerController _playerController;
    private PlayerStatsHandler _playerStatsHandler;

    [SerializeField]
    private float _needFatigability;

    [Header("Resource Gathering")]
    public bool doesGatherResources;

    [Header("Combat")]
    public bool doesDealDamage;
    public int damage;

    private Animator animator;
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
        animator = GetComponent<Animator>();
        _player = GameObject.FindWithTag("Player");
        _playerController = _player.GetComponent<PlayerController>();
        _playerStatsHandler = _player.GetComponent<PlayerStatsHandler>();
    }

    private void Start()
    {
        _playerController.OnAttackEvent += OnAttackInput;
    }

    public override void OnAttackInput()
    {
        if (!attacking)
        {
            attacking = true;
            animator.SetTrigger("Attack");
            _playerStatsHandler.currentStats.fatigability -= _needFatigability;
            Invoke("OnCanAttack", attackRate);
        }
    }

    void OnCanAttack()
    {
        attacking = false;
    }
}