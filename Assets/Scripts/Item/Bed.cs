using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bed : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject sleepIcon;

    [SerializeField]
    public TextMeshProUGUI fatigueText;

    private PlayerStatsHandler _playerStatsHandler;

    public SpriteRenderer playerRenderer;

    private void Awake()
    {
        _playerStatsHandler = player.GetComponent<PlayerStatsHandler>();
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            ChangePlayerStat(true);
            StartCoroutine("OnSleep");
            Invoke("CoroutineStop", 11);
            ChangePlayerStat(false);
        }
    }

    IEnumerator OnSleep()
    {
        while (true)
        {
            ShowSleepStat(false);
            yield return new WaitForSeconds(0.5f);
             _playerStatsHandler.currentStats.fatigability += (float)6.0;
            ShowSleepStat(true);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void CoroutineStop() //시작 후 3초뒤에 호출 될 함수
    {
        Debug.Log("코루틴 종료");
        StopCoroutine("OnSleep");
        ChangePlayerStat(false);
        ShowSleepStat(false);
    }

    void ChangePlayerStat(bool isPlayerSleep)
    {
        //플레이어 sleep 상태로 바꾸기
        _playerStatsHandler.currentStats.isSleep = isPlayerSleep;

        //플레이어 오브젝트 비활성화
        playerRenderer = player.GetComponent<SpriteRenderer>();
        playerRenderer.enabled = isPlayerSleep;
    }

    void ShowSleepStat(bool isShow)
    {
        sleepIcon.SetActive(isShow);
        //fatigueText.gameObject.SetActive(isShow);
    }

    
}
