using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float detectionDistance = 5f;
     // 플레이어 레이어

    private Transform player; // 플레이어의 Transform
    private AStarGrid aStarGrid;
    private List<AStarNode> currentPath;
    public float moveSpeed = 0.5f;
    private bool isChasingPlayer = false;
    private GameObject[] spriteObjects; // 좀비 스프라이트 게임 오브젝트 배열
    private int currentSpriteIndex = 0; // 현재 스프라이트 인덱스

    private void Start()
    {
        aStarGrid = FindObjectOfType<AStarGrid>();
        player = Player.Instance.transform; // 플레이어를 탐지 (Player 클래스를 사용하려면 Player 스크립트가 필요합니다.)
        currentPath = new List<AStarNode>();

        // 좀비 스프라이트 게임 오브젝트를 가져오고 초기화
        spriteObjects = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            spriteObjects[i] = transform.Find("Character_Zombie3_" + i).gameObject;
            spriteObjects[i].SetActive(false);
        }

        // 초기 스프라이트 활성화
        spriteObjects[currentSpriteIndex].SetActive(true);
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionDistance)
            {
                AStarNode startNode = aStarGrid.GetNodeFromWorld(transform.position);
                AStarNode endNode = aStarGrid.GetNodeFromWorld(player.position);

                // 경로 찾기
                currentPath = aStarGrid.pathfinder.CreatePath(startNode, endNode);

                if (currentPath != null && currentPath.Count > 1)
                {
                    isChasingPlayer = true;

                    // 계산된 이동 방향을 기반으로 스프라이트 변경
                    Vector3 moveDirection = (new Vector3(currentPath[1].xPos, currentPath[1].yPos, transform.position.z) - transform.position).normalized;

                    // 방향에 따라 스프라이트 인덱스 설정
                    if (moveDirection.x > 0 && moveDirection.y > 0)
                    {
                        // 우상 대각선
                        currentSpriteIndex = 1;
                    }
                    else if (moveDirection.x < 0 && moveDirection.y > 0)
                    {
                        // 좌상 대각선
                        currentSpriteIndex = 2;
                    }
                    else if (moveDirection.x < 0 && moveDirection.y < 0)
                    {
                        // 좌하 대각선
                        currentSpriteIndex = 3;
                    }
                    else
                    {
                        // 그 외의 경우 (우하 대각선)
                        currentSpriteIndex = 0;
                    }
                }
                else
                {
                    isChasingPlayer = false;
                }
            }
            else
            {
                isChasingPlayer = false;
            }
        }

        // 방향에 따른 스프라이트 설정
        for (int i = 0; i < 4; i++)
        {
            if (i == currentSpriteIndex)
            {
                spriteObjects[i].SetActive(true);
            }
            else
            {
                spriteObjects[i].SetActive(false);
            }
        }

        if (isChasingPlayer)
        {
            MoveZombieAlongPath();
        }
    }

    private void MoveZombieAlongPath()
    {
        if (currentPath != null && currentPath.Count > 1)
        {
            // 다음 노드 방향으로 이동
            Vector3 nextPosition = new Vector3(currentPath[1].xPos, currentPath[1].yPos, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);

            // 현재 위치가 목표에 충분히 가까우면 다음 노드로 이동
            if (Vector3.Distance(transform.position, nextPosition) < 0.1f)
            {
                currentPath.RemoveAt(0);
            }
        }
        else
        {
            isChasingPlayer = false;
        }
    }
}
