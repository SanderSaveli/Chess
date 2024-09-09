using OFG.ChessPeak;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]

public class ScrollSelector : MonoBehaviour
{
    [SerializeField] private RectTransform selectPosition;
    [SerializeField] private RectTransform content;
    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private float scrollDeadZone = 0.1f;
    [SerializeField][Min(0)] private int startSelectedIndex = 0;

    private int _selectedIndex = -1;
    private ScrollRect _scrollRect;
    private List<ScrollElement> _elements;
    private Vector3 _targetPosition;

    private bool _isNeedToWatchTargetElement = false;

    private void OnEnable()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _scrollRect.onValueChanged.AddListener(OnScroll);
        FillAllElements();
    }

    private void OnDisable()
    {
        _scrollRect.onValueChanged.RemoveListener(OnScroll);
    }
    void Start()
    {
        _targetPosition = content.position;
        StartCoroutine(WaitAndSelect());
    }

    private IEnumerator WaitAndSelect()
    {
        yield return new WaitForSeconds(0.01f);
        SelectElement(startSelectedIndex);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) || Input.mouseScrollDelta.y != 0)
        {
            _isNeedToWatchTargetElement = true;
        }
        else
        {
            _isNeedToWatchTargetElement = false;
            ScrollToElement();
        }
    }

    private void FillAllElements()
    {
        _elements = new List<ScrollElement>();
        for (int i = 0; i < content.childCount; i++)
        {
            ScrollElement element = content.GetChild(i).GetComponent<ScrollElement>();
            element.Ini(i);
            _elements.Add(element);
            element.OnClicked += SelectElement;
        }
    } 

    void OnScroll(Vector2 scrollPosition)
    {
        if(!_isNeedToWatchTargetElement)
        {
            return;
        }
        float minDistanceToCenter = float.MaxValue;
        int minElementIndex = 0;
        for (int i = 0; i < _elements.Count; i++)
        {
            RectTransform element = _elements[i].rectTransform;

            float distanceToCenter = Mathf.Abs(element.position.y - selectPosition.position.y);
            if(minDistanceToCenter > distanceToCenter)
            {
                minDistanceToCenter = distanceToCenter;
                minElementIndex = i;
            }
        }

        SelectElement(minElementIndex);
    }

    private void SelectElement(int index)
    {
        if (_selectedIndex == index)
        {
            return;
        }
        DeselectElement(_selectedIndex);
        ScrollElement targetElement = _elements[index];
        _targetPosition = selectPosition.position + (content.position - targetElement.rectTransform.position);
        Debug.Log(content.position + " " + selectPosition.position);
        targetElement.Select();
        _selectedIndex = index;
    }

    private void DeselectElement(int index)
    {
        if(index >= 0)
        {
            _elements[index].Deselect();
        }
    }

    public void ScrollToElement()
    {
        if(Vector3.Magnitude(content.position - _targetPosition) > scrollDeadZone) 
        {
            content.position = Vector3.Lerp(content.position, _targetPosition, smoothTime* Time.deltaTime);
        }
    }
}
