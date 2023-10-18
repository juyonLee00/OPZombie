using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PopUp : MonoBehaviour
{

    public virtual void ClosePopupUI()      // 임의로 일시 정지 효과
    {
        GameManager.UI.ClosePopupUI(this);
        Time.timeScale = 1.0f;
    }

    public virtual void HidePopupUI()
    {
        GameManager.UI.HidePopupUI(this);
    }
}
