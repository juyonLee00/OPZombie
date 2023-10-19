using UnityEngine;
using System.Collections;

public class ZombieScript : MonoBehaviour
{
    public int health = 100; // ü��
    public int attackDamage = 1; // ���ݷ�
    private bool isPaused = false; // ���� ������� ����
    public float pauseDuration = 1.0f; // ���� �ð�(��)

    // �ٸ� ��ũ��Ʈ�� ��ȣ �ۿ��ϰų� �̺�Ʈ�� ó���ϴ� �޼������ �߰��� �� �ֽ��ϴ�.

    // ��: �ǰ� ó��
    public void TakeDamage(int damage)
    {
        if (!isPaused)
            
        {
            
            health -= damage;
            Debug.Log("�¾Ҵ�.");
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

    // ��: ��� ó��
    private void Die()
    {
        // ���� ������� �� ����Ǵ� ������ �߰��� �� �ֽ��ϴ�.
        Destroy(gameObject);
    }

    // ���� ���� �ð� ���� ���߰� �ϴ� �ڷ�ƾ
    private IEnumerator PauseZombieForSeconds(float seconds)
    {
        isPaused = true; // ���� ���߰� ����
        yield return new WaitForSeconds(seconds);
        isPaused = false; // ����� ���� �ٽ� �̵�
    }
}
