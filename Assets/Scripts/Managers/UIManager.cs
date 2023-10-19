using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Diagnostics;

public class UIManager : MonoBehaviour
{
    UI_Stack<UI_PopUp> popupStack = new UI_Stack<UI_PopUp>(3); //임의로 3크기

    int order = 10;

    public void Init()
    {
    }
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("PopUp_UI");
            if (root == null)
                root = new GameObject { name = "PopUp_UI" };

            return root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = order;
            order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }


    public T ShowPopupUI<T>(string name = null, Transform parent = null) where T : UI_PopUp
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = GameManager.Resource.Instantiate($"UI/PopUp/{name}");
        T popup = ComponentEx.GetOrAddComponent<T>(go);
        popupStack.Push(popup);

        if (parent != null)
            go.transform.SetParent(parent);
        else
            go.transform.SetParent(Root.transform);

        go.transform.localScale = Vector3.one;

        return popup;
    }


    public T PeekPopupUI<T>() where T : UI_PopUp
    {
        if (popupStack.IsEmpty())
            return null;

        return popupStack.Peek() as T;
    }

    public void ClosePopupUI(UI_PopUp popup)
    {
        if (popupStack.IsEmpty())
            return;

        if (popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (popupStack.IsEmpty())
            return;

        UI_PopUp popup = popupStack.Pop();
        GameManager.Resource.Destroy(popup.gameObject);
        popup = null;
        order--;
    }

    public void HidePopupUI(UI_PopUp popup)
    {
        popup.gameObject.SetActive(false);
    }

    public void CloseAllPopupUI()
    {
        while (!popupStack.IsEmpty())
            ClosePopupUI();
    }

    public void Clear()
    {
        CloseAllPopupUI();
    }
    //마우스 오버 시 텍스트 출력
}
