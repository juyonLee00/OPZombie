using UnityEngine;
using System.Collections;

public class ZombieScript : MonoBehaviour
{
    public int health = 100; // 체력
    public int attackDamage = 1; // 공격력
    private bool isPaused = false; // 좀비가 멈췄는지 여부
    public float pauseDuration = 1.0f; // 멈출 시간(초)

    // 다른 스크립트와 상호 작용하거나 이벤트를 처리하는 메서드들을 추가할 수 있습니다.

    // 예: 피격 처리
    public void TakeDamage(int damage)
    {
        if (!isPaused)
            
        {
            Debug.Log("맞았다.");
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(PauseZombieForSeconds(pauseDuration));
            }
        }
    }

    // 예: 사망 처리
    private void Die()
    {
        // 좀비가 사망했을 때 실행되는 로직을 추가할 수 있습니다.
        Destroy(gameObject);
    }

    // 좀비를 일정 시간 동안 멈추게 하는 코루틴
    private IEnumerator PauseZombieForSeconds(float seconds)
    {
        isPaused = true; // 좀비를 멈추게 설정
        yield return new WaitForSeconds(seconds);
        isPaused = false; // 멈췄던 좀비가 다시 이동
    }
}
