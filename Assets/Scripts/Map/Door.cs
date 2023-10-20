using UnityEngine;

public enum Direction
{
    North,
    East,
    South,
    West
}
public class Door : MonoBehaviour
{
    private Collider2D doorCollider;
    private int health = 100; // 도어의 초기 체력
    private bool isZombieColliding = false;
    private float damageCooldown = 1.0f; // 피해를 입히는 주기
    private float lastDamageTime;
    public Vector2 Position { get; set; } // 이 문의 위치 (방의 grid 좌표)
    public Direction Direction { get; set; } // 이 문이 어느 방향을 보고 있는지

    private MapGenerator mapGenerator;

    void Start()
    {
        doorCollider = GetComponent<Collider2D>();
        mapGenerator = FindObjectOfType<MapGenerator>();
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
            Debug.Log("player collison");

            Vector2 nextRoomPosition = GetNextRoomPosition();

            if (mapGenerator.IsInside(nextRoomPosition) &&
               mapGenerator.mapGrid[(int)nextRoomPosition.x, (int)nextRoomPosition.y] != -1)
            {
                // IgnoreCollision 대신 해당 문 오브젝트를 일시적으로 다른 레이어(예: "PassableDoor")로 이동
                gameObject.layer = LayerMask.NameToLayer("PassableDoor");
                Debug.Log("Player can pass through the door.");
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
            // Player가 문을 통과한 후에는 원래 레이어로 돌아옴
            gameObject.layer = LayerMask.NameToLayer("Door");
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Zombie")) // 좀비 레이어를 비교
        {
            // 좀비와의 충돌 해제
            isZombieColliding = false;
        }
    }

    Vector2 GetNextRoomPosition()
    {
        switch (Direction)
        {
            case Direction.North:
                return Position + Vector2.up;
            case Direction.East:
                return Position + Vector2.right;
            case Direction.South:
                return Position + Vector2.down;
            case Direction.West:
                return Position + Vector2.left;
            default:
                return Position;
        }
    }

    void DestroyDoor()
    {
        // 도어를 파괴
        Destroy(gameObject);
    }
}
