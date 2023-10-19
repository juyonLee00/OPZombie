using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bed : MonoBehaviour
{
    //[SerializeField]
    //GameObject player;

    [SerializeField]
    GameObject sleepIcon;

    [SerializeField]
    TextMeshProUGUI FatigueTxt;

    //private PlayerStatsHandler _playerStatsHandler;

    public int test = 0;
    float time;

    /*private void Awake()
    {
        _playerStatsHandler = player.GetComponent<PlayerStatsHandler>();
    }
    */

    private void Start()
    {
        ChangePlayerStat(true);
        StartCoroutine("OnSleep");
        Invoke("CoroutineStop", 11);
        ChangePlayerStat(true);
    }
    IEnumerator OnSleep()
    {
        while (true)
        {
            ShowSleepStat(false);
            Debug.Log(test);
            test += 6;
            /*
             테스트 위해 넣어둠. 삭제 예정
             _playerStatsHandler.currentStats.fatigability += 6.0;
             */
            ShowSleepStat(true);
            yield return new WaitForSeconds(1);
        }
    }

    void CoroutineStop() //시작 후 3초뒤에 호출 될 함수
    {
        Debug.Log("코루틴 종료");
        StopCoroutine("OnSleep");
    }

    void ChangePlayerStat(bool isPlayerSleep)
    {
        //플레이어 sleep 상태로 바꾸기
        //_playerStatsHandler.currentStats.isSleep = isPlayerSleep;

        //플레이어 오브젝트 비활성화
        //player.SetActive(isPlayerSleep);
        //ShowSleepStat(isPlayerSleep);
    }

    void ShowSleepStat(bool isShow)
    {
        sleepIcon.SetActive(isShow);
        FatigueTxt.gameObject.SetActive(isShow);
    }

    
}
