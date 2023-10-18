using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    string GetInteractPrompt();
    void OnInteract();
}

public class InteractionManager : MonoBehaviour
{
    public float maxCheckDistance;
    public LayerMask layerMask;

    private GameObject curInteractGameobject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit;
        Debug.DrawRay(transform.position, transform.right, Color.black);

        if (hit = Physics2D.Raycast(this.transform.position, this.transform.forward, maxCheckDistance, layerMask))
        {
            if (hit.collider.gameObject != curInteractGameobject)
            {
                curInteractGameobject = hit.collider.gameObject;
                curInteractable = hit.collider.GetComponent<IInteractable>();
                Debug.Log(hit.collider.gameObject.name);
                SetPromptText();
            }
        }
        else
        {
            curInteractGameobject = null;
            curInteractable = null;
            //promptText.gameObject.SetActive(false);
        }
        /*
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            RaycastHit2D hit;
            Debug.DrawRay(this.transform.position, this.transform.forward, Color.black);

            if (hit = Physics2D.Raycast(this.transform.position, this.transform.forward, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameobject)
                {
                    curInteractGameobject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    Debug.Log(hit.collider.gameObject.name);
                    //SetPromptText();
                }
            }
            else
            {
                
            }
        }*/
    }

    private void SetPromptText()
    {
        Debug.Log("Get");
        /*promptText.gameObject.SetActive(true);
        promptText.text = string.Format("<b>[E]</b> {0}", curInteractable.GetInteractPrompt());*/
    }

    public void OnInteractInput(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractGameobject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}