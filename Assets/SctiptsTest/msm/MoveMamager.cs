using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MoveMamager : MonoBehaviour
{
    public static MoveMamager Instance = null;
    public static bool press = false;
    public List<MoveItem> _select_item;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectItem(MoveItem item)
    {
        if (_select_item.Count > 2)
        {
            Debug.Log("ERROR MORE CONUT");
            return;
        }

        Debug.Log("AddItem" + item.gameObject.name);
        if (_select_item.Contains(item)) return;

        _select_item.Add(item);
        if (_select_item.Count >= 2)
        {
            Vector3 first = _select_item[0].transform.localPosition;
            Vector3 sencond = _select_item[1].transform.localPosition;
            _select_item[0].transform.DOLocalMove(sencond, 0.2f);
            _select_item[1].transform.DOLocalMove(first, 0.2f);
            Clear();
        }
    }

    public void Clear()
    {
        _select_item.Clear();
        press = false;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
