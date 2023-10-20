using UnityEngine;
using UnityEngine.SceneManagement;

public class Stair : MonoBehaviour
{
    public bool isUpStair;

    private MapGenerator mapGenerator;

    private void Start()
    {
        mapGenerator = FindObjectOfType<MapGenerator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어 접촉");
            int currentFloorIndex = mapGenerator.floors.IndexOf(transform.parent.gameObject);

            if (isUpStair && currentFloorIndex != 0)
            {
                // 올라가는 계단이며 현재 층이 맨 위층(인덱스 0)이 아닐 경우 위로 한 층 이동
                mapGenerator.ActivateFloor(currentFloorIndex - 1);
                transform.parent.gameObject.SetActive(false);
            }
            else if (!isUpStair)
            {
                if (currentFloorIndex != mapGenerator.floors.Count - 1)
                {
                    // 내려가는 계단이며 현재 층이 맨 아래층(마지막 인덱스)이 아닐 경우 아래로 한 층 이동
                    mapGenerator.ActivateFloor(currentFloorIndex + 1);
                    transform.parent.gameObject.SetActive(false);
                }
                else
                {
                    // 맨 마지막 인덱스에서 내려가는 계단에 접촉하면 GameClear 씬 로드
                    SceneManager.LoadScene("GameEnd");
                }
            }
        }
    }
}
