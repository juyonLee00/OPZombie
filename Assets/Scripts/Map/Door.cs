using UnityEngine;

public class Door : MonoBehaviour
{
    private Collider2D doorCollider;
    private Room room;
    private int health = 100; // 도어의 초기 체력
    private bool isZombieColliding = false;
    private float damageCooldown = 1.0f; // 피해를 입히는 주기
    private float lastDamageTime;

    void Start()
    {
        doorCollider = GetComponent<Collider2D>();
    }

    // 해당 문이 속한 방을 설정합니다.
    public void SetRoom(Room room)
    {
        this.room = room;
    }

    void Update()
    {
        if (isZombieColliding)
        {
            // damageCooldown 이상 시간이 지났는지 확인
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                // 초당 5씩 체력을 감소
                health -= 5;
                lastDamageTime = Time.time;
                Debug.Log("도어 체력: " + health);

                if (health <= 0)
                {
                    // 체력이 0 이하로 떨어지면 도어를 파괴
                    DestroyDoor();
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 이 문이 연결된 방향에 다른 방이 있는지 확인합니다.
            int direction = GetDirection(collision.transform.position);
            if (room.Doors[direction] != null)
            {
                Physics2D.IgnoreCollision(collision.collider, doorCollider);
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Zombie")) // 좀비 레이어를 비교
        {
            // 좀비와의 충돌 감지
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
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Zombie")) // 좀비 레이어를 비교
        {
            // 좀비와의 충돌 해제
            isZombieColliding = false;
        }
    }

    // 충돌 위치로부터 이 문의 위치까지의 벡터를 사용하여 충돌 오브젝트가 어느 쪽에서 접근했는지 결정합니다.
    int GetDirection(Vector3 collisionPosition)
    {
        Vector3 dirVector = collisionPosition - transform.position;
        float angle = Mathf.Atan2(dirVector.y, dirVector.x) * Mathf.Rad2Deg;

        if (angle > -45 && angle <= 45)
            return 0; // 오른쪽
        else if (angle > 45 && angle <= 135)
            return 1; // 위쪽
        else if ((angle > 135 && angle <= 180) || (angle >= -180 && angle < -135))
            return 2; // 왼쪽
        else
            return 3; // 아래쪽
    }

    void DestroyDoor()
    {
        // 도어를 파괴
        Destroy(gameObject);
    }
}
