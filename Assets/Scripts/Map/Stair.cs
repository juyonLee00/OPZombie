using UnityEngine;

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
            int currentFloorIndex = mapGenerator.floors.IndexOf(transform.parent.gameObject);

            if (isUpStair && currentFloorIndex < mapGenerator.floors.Count - 1)
            {
                // 올라가는 계단이며 현재 층이 맨 위층이 아닐 경우 다음 층으로 이동
                mapGenerator.ActivateFloor(currentFloorIndex + 1);
                transform.parent.gameObject.SetActive(false);
            }
            else if (!isUpStair && currentFloorIndex > 0)
            {
                // 내려가는 계단이며 현재 층이 맨 아래층이 아닐 경우 이전 층으로 이동
                mapGenerator.ActivateFloor(currentFloorIndex - 1);
                transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
