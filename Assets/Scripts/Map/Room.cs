using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomConfig config; // �� ���� ����

    private void Start()
    {
        if (config != null)
        {
            // ���� ���� ����
            GameObject monster = config.SpawnMonster();
            if (monster != null)
            {
                // TODO: ���� ��ġ ���� �� Ȱ��ȭ ����
            }

            // ������ ���� ����
            GameObject item = config.SpawnItem();
            if (item != null)
            {
                // TODO: ������ ��ġ ���� �� Ȱ��ȭ ����
            }
        }
    }
}
