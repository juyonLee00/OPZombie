using UnityEngine;
using System.Collections;

public class ZombieScript : MonoBehaviour
{
    public int health = 100;
    public int attackDamage = 1;
    public float pauseDuration = 1.0f; // �̵��� ���� �ð�(��)
    private ZombieController zombieController; // ZombieController ��ũ��Ʈ ����

    private void Start()
    {
        // ZombieController ��ũ��Ʈ�� ����
        zombieController = GetComponent<ZombieController>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("�¾Ҵ�.");
        if (health <= 0)
        {
            Die();
        }
        else
        {
            // ZombieController�� SlowDownZombieForSeconds �ڷ�ƾ ȣ��
            StartCoroutine(zombieController.SlowDownZombieForSeconds(pauseDuration));
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
