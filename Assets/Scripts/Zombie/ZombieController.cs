//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ZombieController : MonoBehaviour
//{
//    public float detectionDistance = 25f;

//    public LayerMask playerLayer; // 플레이어 레이어
//    private Transform player; // 플레이어의 Transform
//    private AStarGrid aStarGrid;
//    private List<AStarNode> currentPath;
//    private float moveSpeed = 0.5f;
//    private bool isChasingPlayer = false;

//    private void Start()
//    {
//        aStarGrid = FindObjectOfType<AStarGrid>();
//        player = Player.Instance.transform;  // 플레이어를 탐지
//        currentPath = new List<AStarNode>();
//    }

//    private void Update()
//    {
//        if (player != null)
//        {
//            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

//            if (distanceToPlayer <= detectionDistance)
//            {
//                AStarNode startNode = aStarGrid.GetNodeFromWorld(transform.position);
//                AStarNode endNode = aStarGrid.GetNodeFromWorld(player.position);

//                // 경로 찾기
//                currentPath = aStarGrid.pathfinder.CreatePath(startNode, endNode);

//                if (currentPath != null && currentPath.Count > 1)
//                {
//                    isChasingPlayer = true;
//                }
//                else
//                {
//                    isChasingPlayer = false;
//                }
//            }
//            else
//            {
//                isChasingPlayer = false;
//            }
//        }

//        if (isChasingPlayer)
//        {
//            MoveZombieAlongPath();
//        }
//    }

//    private void MoveZombieAlongPath()
//    {
//        if (currentPath != null && currentPath.Count > 1)
//        {
//            // 다음 노드 방향으로 이동
//            Vector3 nextPosition = new Vector3(currentPath[1].xPos, currentPath[1].yPos, transform.position.z);
//            transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);

//            // 현재 위치가 목표에 충분히 가까우면 다음 노드로 이동
//            if (Vector3.Distance(transform.position, nextPosition) < 0.2f)
//            {
//                currentPath.RemoveAt(0);

//            }
//        }
//        else
//        {
//            isChasingPlayer = false;
//        }
//    }
//}