using UnityEngine;
using System.Collections;

public class ZombieScript : MonoBehaviour
{
    public int health = 100;
    public int attackDamage = 1;
    public float pauseDuration = 1.0f; // 이동을 멈출 시간(초)
    private ZombieController zombieController; // ZombieController 스크립트 참조

    private void Start()
    {
        // ZombieController 스크립트를 참조
        zombieController = GetComponent<ZombieController>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("맞았다.");
        if (health <= 0)
        {
            Die();
        }
        else
        {
            // ZombieController의 SlowDownZombieForSeconds 코루틴 호출
            StartCoroutine(zombieController.SlowDownZombieForSeconds(pauseDuration));
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
