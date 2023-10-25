using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] roomPrefabs; // 방 프리팹 배열
    public int stairRoomIndex;

    public GameObject aStarGridPrefab; // AStarGrid 프리팹 추가

    public int[,] mapGrid; // 맵 그리드
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

            // 모든 값을 -1로 초기화합니다.
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    mapGrid[x, y] = -1;
                }
            }

            GenerateMap();
            InstantiateRooms();

            GameObject aStarGridInstance = Instantiate(aStarGridPrefab, Vector3.zero, Quaternion.identity);
            aStarGridInstance.transform.parent = floors[i].transform;

            Vector3 gridPosition = new Vector3(0.5f, -0.25f, 0f);
            aStarGridInstance.transform.position = gridPosition;
        }
    }

    void GenerateMap()
    {
        // 모든 가능한 방의 인덱스를 리스트로 만듭니다.
        List<int> availableRoomIndices = new List<int>();
        for (int i = 0; i < roomPrefabs.Length; i++)
        {
            if (i != stairRoomIndex)
            {
                availableRoomIndices.Add(i);
            }
        }

        int stairX = Random.Range(0, mapWidth);
        int stairY = Random.Range(0, mapHeight);
        mapGrid[stairX, stairY] = stairRoomIndex;

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (!(x == stairX && y == stairY) && availableRoomIndices.Count > 0) // 이미 계단이 배치된 곳과 사용 가능한 방이 없는 경우는 제외합니다.
                {
                    int randomIndexInList = Random.Range(0, availableRoomIndices.Count);
                    int roomIndexInPrefabArray = availableRoomIndices[randomIndexInList];
                    availableRoomIndices.RemoveAt(randomIndexInList); // 선택된 방 인덱스를 리스트에서 제거합니다.

                    mapGrid[x, y] = roomIndexInPrefabArray;
                }
            }
        }
    }


    void InstantiateRooms()
    {
        GameObject floor = new GameObject("Floor");
        floor.transform.position = Vector3.zero;
        floors.Add(floor);

        float roomSpacingX = 0.5f;
        float roomSpacingY = 0.25f;

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (mapGrid[x, y] != -1)
                {
                    Vector3 positionIsoMetric = new Vector3((x - y) * (4f + roomSpacingX), (-x - y) * (2f + roomSpacingY), 0);
                    GameObject roomInstance = Instantiate(roomPrefabs[mapGrid[x, y]], positionIsoMetric, Quaternion.identity);
                    roomInstance.transform.parent = floor.transform;
                    UpdateSortingOrder(roomInstance);
                }
            }
        }

        floor.SetActive(false);
    }

    public void ActivateFloor(int index)
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
            if (childRenderer != renderer && childRenderer.name != "Floor")  // 부모 오브젝트를 제외하기 위한 체크
            {
                // 기존 sortingOrder와 Y 위치 및 깊이(depth)를 고려하여 새로운 sortingOrder 설정. 여기서 '10'은 임의로 선택한 값으로, 실제 프로젝트에서는 필요에 따라 조정해야 합니다.
                childRenderer.sortingOrder += -Mathf.RoundToInt(childRenderer.transform.position.y * 100 + childRenderer.transform.position.z * 10);
            }
        }
    }

    public bool IsInside(Vector2 position)
    {
        return position.x >= 0 && position.x < mapWidth && position.y >= 0 && position.y < mapHeight;
    }
}
