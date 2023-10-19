using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Diagnostics;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public static GameManager Instance { get { return instance; } }

    private static ResourceManager resourceManager = new ResourceManager();
    private static UIManager uiManager = new UIManager();
    //private static SoundManager soundManager = new SoundManager();

    public static ResourceManager Resource { get { Init(); return resourceManager; } }
    public static UIManager UI { get { Init(); return uiManager; } }
    //public static SoundManager Sound { get { Init(); return soundManager; } }

    private void Start()
    {
        Init();
    }

    private static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("GameManager");
            if (go == null)
                go = new GameObject { name = "GameManager" };
            
            instance = ComponentEx.GetOrAddComponent<GameManager>(go);

            DontDestroyOnLoad(go);

            resourceManager.Init();
            uiManager.Init();
            //soundManager.Init();
        }
    }
    //TODO 게임 시작, 끝 관리
}
