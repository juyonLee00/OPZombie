using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private GameObject _menuCam;
    [SerializeField] private GameObject _mainCam;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _introCanvas;

    public void Awake()
    {
        //Cursor.lockState = CursorLockMode.None;
        _player.SetActive(false);
    }

    public void GameStart()
    {
        _menuCam.SetActive(false);
        _mainCam.SetActive(true);
        _player.SetActive(true);
        _introCanvas.SetActive(false);
        //Cursor.lockState = CursorLockMode.Locked;
    }
}
