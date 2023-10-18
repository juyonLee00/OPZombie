using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomConfig config; // 이 방의 설정

    private void Start()
    {
        if (config != null)
        {
            // 몬스터 스폰 로직
            GameObject monster = config.SpawnMonster();
            if (monster != null)
            {
                // TODO: 몬스터 위치 결정 및 활성화 로직
            }

            // 아이템 생성 로직
            GameObject item = config.SpawnItem();
            if (item != null)
            {
                // TODO: 아이템 위치 결정 및 활성화 로직
            }
        }
    }
}
