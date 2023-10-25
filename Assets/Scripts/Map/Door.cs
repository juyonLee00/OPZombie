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
    private int health = 100; // ������ �ʱ� ü��
    private bool isZombieColliding = false;
    private float damageCooldown = 1.0f; // ���ظ� ������ �ֱ�
    private float lastDamageTime;
    public Vector2 Position { get; set; } // �� ���� ��ġ (���� grid ��ǥ)
    public Direction Direction { get; set; } // �� ���� ��� ������ ���� �ִ���

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
            Debug.Log("player collison");

            Vector2 nextRoomPosition = GetNextRoomPosition();

            if (mapGenerator.IsInside(nextRoomPosition) &&
               mapGenerator.mapGrid[(int)nextRoomPosition.x, (int)nextRoomPosition.y] != -1)
            {
                // IgnoreCollision ��� �ش� �� ������Ʈ�� �Ͻ������� �ٸ� ���̾�(��: "PassableDoor")�� �̵�
                gameObject.layer = LayerMask.NameToLayer("PassableDoor");
                Debug.Log("Player can pass through the door.");
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
            // Player�� ���� ����� �Ŀ��� ���� ���̾�� ���ƿ�
            gameObject.layer = LayerMask.NameToLayer("Door");
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Zombie")) // ���� ���̾ ��
        {
            // ������� �浹 ����
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
        // ��� �ı�
        Destroy(gameObject);
    }
}
