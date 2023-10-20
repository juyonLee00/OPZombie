using UnityEngine;

public class Door : MonoBehaviour
{
    private Collider2D doorCollider;
    private Room room;
    private int health = 100; // ������ �ʱ� ü��
    private bool isZombieColliding = false;
    private float damageCooldown = 1.0f; // ���ظ� ������ �ֱ�
    private float lastDamageTime;

    void Start()
    {
        doorCollider = GetComponent<Collider2D>();
    }

    // �ش� ���� ���� ���� �����մϴ�.
    public void SetRoom(Room room)
    {
        this.room = room;
    }

    void Update()
    {
        if (isZombieColliding)
        {
            // damageCooldown �̻� �ð��� �������� Ȯ��
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                // �ʴ� 5�� ü���� ����
                health -= 5;
                lastDamageTime = Time.time;
                Debug.Log("���� ü��: " + health);

                if (health <= 0)
                {
                    // ü���� 0 ���Ϸ� �������� ��� �ı�
                    DestroyDoor();
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �� ���� ����� ���⿡ �ٸ� ���� �ִ��� Ȯ���մϴ�.
            int direction = GetDirection(collision.transform.position);
            if (room.Doors[direction] != null)
            {
                Physics2D.IgnoreCollision(collision.collider, doorCollider);
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Zombie")) // ���� ���̾ ��
        {
            // ������� �浹 ����
            isZombieColliding = true;
            lastDamageTime = Time.time;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, doorCollider, false);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Zombie")) // ���� ���̾ ��
        {
            // ������� �浹 ����
            isZombieColliding = false;
        }
    }

    // �浹 ��ġ�κ��� �� ���� ��ġ������ ���͸� ����Ͽ� �浹 ������Ʈ�� ��� �ʿ��� �����ߴ��� �����մϴ�.
    int GetDirection(Vector3 collisionPosition)
    {
        Vector3 dirVector = collisionPosition - transform.position;
        float angle = Mathf.Atan2(dirVector.y, dirVector.x) * Mathf.Rad2Deg;

        if (angle > -45 && angle <= 45)
            return 0; // ������
        else if (angle > 45 && angle <= 135)
            return 1; // ����
        else if ((angle > 135 && angle <= 180) || (angle >= -180 && angle < -135))
            return 2; // ����
        else
            return 3; // �Ʒ���
    }

    void DestroyDoor()
    {
        // ��� �ı�
        Destroy(gameObject);
    }
}
