using UnityEngine;

public class Door : MonoBehaviour
{
    private Collider doorCollider;
    private Room room;

    void Start()
    {
        doorCollider = GetComponent<Collider>();
    }

    // �ش� ���� ���� ���� �����մϴ�.
    public void SetRoom(Room room)
    {
        this.room = room;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �� ���� ����� ���⿡ �ٸ� ���� �ִ��� Ȯ���մϴ�.
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
}
