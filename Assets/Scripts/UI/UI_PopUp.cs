using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PopUp : MonoBehaviour
{

    public virtual void ClosePopupUI()      // ���Ƿ� �Ͻ� ���� ȿ��
    {
        GameManager.UI.ClosePopupUI(this);
        Time.timeScale = 1.0f;
    }

    public virtual void HidePopupUI()
    {
        GameManager.UI.HidePopupUI(this);
    }
}
