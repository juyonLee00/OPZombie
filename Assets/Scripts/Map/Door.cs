using UnityEngine;

public class Door : MonoBehaviour
{
    private Collider doorCollider;
    private Room room;

    void Start()
    {
        doorCollider = GetComponent<Collider>();
    }

    // 해당 문이 속한 방을 설정합니다.
    public void SetRoom(Room room)
    {
        this.room = room;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 이 문이 연결된 방향에 다른 방이 있는지 확인합니다.
            int direction = GetDirection(collision.transform.position);
            if (room.Doors[direction] != null)
            {
                Physics.IgnoreCollision(collision.collider, doorCollider);
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics.IgnoreCollision(collision.collider, doorCollider, false);
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
}
