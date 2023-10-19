using UnityEngine;

public class TestWeapon : MonoBehaviour
{
    public int damage = 10; // 무기의 공격력

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Zombie"))
        {
            ZombieScript zombie = collision.gameObject.GetComponent<ZombieScript>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }
        }
    }
}
