using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public int health = 100; // 체력
    public int attackDamage = 1; // 공격력
    

    // 다른 스크립트와 상호 작용하거나 이벤트를 처리하는 메서드들을 추가할 수 있습니다.

    // 예: 피격 처리
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // 예: 사망 처리
    private void Die()
    {
        // 좀비가 사망했을 때 실행되는 로직을 추가할 수 있습니다.
        Destroy(gameObject);
    }


}
