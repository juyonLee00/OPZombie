using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public int health = 100; // ü��
    public int attackDamage = 5; // ���ݷ�
    

    // �ٸ� ��ũ��Ʈ�� ��ȣ �ۿ��ϰų� �̺�Ʈ�� ó���ϴ� �޼������ �߰��� �� �ֽ��ϴ�.

    // ��: �ǰ� ó��
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // ��: ��� ó��
    private void Die()
    {
        // ���� ������� �� ����Ǵ� ������ �߰��� �� �ֽ��ϴ�.
        Destroy(gameObject);
    }
}
