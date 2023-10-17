using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] roomPrefabs; // 방 프리팹 배열

    private int[,] mapGrid; // 맵 그리드
    public int mapWidth; // 맵 가로 크기
    public int mapHeight; // 맵 세로 크기
    public int totalFloor;

    public List<GameObject> floors = new List<GameObject>();

    void Start()
    {
        mapGrid = new int[mapWidth, mapHeight];
        GenerateFloors(totalFloor);
        ActivateFloor(0);
    }

    void GenerateFloors(int floorCount)
    {
        for (int i = 0; i < floorCount; i++)
        {
            mapGrid = new int[mapWidth, mapHeight];
            GenerateMap();
            InstantiateRooms();
        }
    }

    void GenerateMap()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (x == 0 || y == 0 || x == mapWidth - 1 || y == mapHeight - 1)
                {
                    // 경계는 벽으로 처리
                    mapGrid[x, y] = -1;
                }
                else
                {
                    // 내부는 랜덤하게 방 배치
                    int roomIndex = Random.Range(0, roomPrefabs.Length);
                    mapGrid[x, y] = roomIndex;
                }
            }
        }
    }

    void InstantiateRooms()
    {
        GameObject floor = new GameObject("Floor");
        floor.transform.position = Vector3.zero;
        floors.Add(floor);

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (mapGrid[x, y] != -1)
                {
                    Vector3 positionIsoMetric = new Vector3((x - y) * 4f, (-x - y) * 2f, y - x);
                    GameObject roomInstance = Instantiate(roomPrefabs[mapGrid[x, y]], positionIsoMetric, Quaternion.identity);
                    roomInstance.transform.parent = floor.transform;
                    UpdateSortingOrder(roomInstance);
                }
            }
        }

        floor.SetActive(false);
    }

    void ActivateFloor(int index)
    {
        for (int i = 0; i < floors.Count; i++)
        {
            if (i == index) floors[i].SetActive(true);
            else floors[i].SetActive(false);
        }
    }

    void UpdateSortingOrder(GameObject instance)
    {
        // 인스턴스 자체에 대한 Renderer 컴포넌트 찾기
        Renderer renderer = instance.GetComponent<Renderer>();

        if (renderer != null)
        {
            // 기존 sortingOrder와 Y 위치를 고려하여 새로운 sortingOrder 설정
            renderer.sortingOrder += -Mathf.RoundToInt(instance.transform.position.y * 100);
        }

        // 인스턴스의 자식들에 대한 모든 Renderer 컴포넌트 찾기
        Renderer[] childRenderers = instance.GetComponentsInChildren<Renderer>();

        foreach (Renderer childRenderer in childRenderers)
        {
            if (childRenderer != renderer)  // 부모 오브젝트를 제외하기 위한 체크
            {
                // 기존 sortingOrder와 Y 위치 및 깊이(depth)를 고려하여 새로운 sortingOrder 설정. 여기서 '10'은 임의로 선택한 값으로, 실제 프로젝트에서는 필요에 따라 조정해야 합니다.
                childRenderer.sortingOrder += -Mathf.RoundToInt(childRenderer.transform.position.y * 100 + childRenderer.transform.position.z * 10);
            }
        }
    }
}
