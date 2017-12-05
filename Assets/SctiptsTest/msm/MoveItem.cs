using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class MoveItem : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    public int id;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData event_data)
    {

        Debug.LogFormat("{0}_{1}", "OnPointerEnter", gameObject.name);
        if (MoveMamager.press)
        {
            MoveMamager.Instance.SelectItem(this);
        }
    }

    public void OnPointerExit(PointerEventData event_data)
    {
        //Debug.LogFormat("{0}_{1}", "OnPointerExit", gameObject.name);
    }

    public void OnPointerClick(PointerEventData event_data)
    {
        //Debug.LogFormat("{0}_{1}", "OnPointerClick", gameObject.name);
    }

    public void OnPointerDown(PointerEventData event_data)
    {
        Debug.LogFormat("{0}_{1}", "OnPointerDown", gameObject.name);
        MoveMamager.press = true;
        MoveMamager.Instance.SelectItem(this);
    }

    public void OnPointerUp(PointerEventData event_data)
    {
        MoveMamager.press = false;
        Debug.LogFormat("{0}_{1}", "OnPointerUp", gameObject.name);
    }
}
