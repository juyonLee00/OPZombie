using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // 좀비 프리팹
    public Transform spawnPoint; // 좀비가 소환될 위치
    public float spawnInterval = 20.0f; // 소환 간격 (20초)

    private void Start()
    {
        // 초기에 시작하고 나서 주기적으로 SpawnZombie 함수를 호출
        StartCoroutine(SpawnZombie());
    }

    private IEnumerator SpawnZombie()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // 좀비를 소환할 위치에 좀비 프리팹을 생성
            Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
