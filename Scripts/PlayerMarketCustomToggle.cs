using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class PlayerMarketCustomToggle : MonoBehaviour, IPointerClickHandler
{
    public Sprite[] markerSprites;
    public DialogLabel dialog;

    private Image _image;
    private int _currentIndex;
    private bool _changed;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _currentIndex++;
        if (_currentIndex >= markerSprites.Length) _currentIndex = 0;
        _changed = true;
        UpdateSprites();
    }

    public void UpdateToogles(int index)
    {
        _currentIndex = index;
        if (_currentIndex >= markerSprites.Length) _currentIndex = 0;
        UpdateSprites();
    }

    private void UpdateSprites()
    {
        if (_currentIndex >= markerSprites.Length) _currentIndex = 0;
        _image.sprite = markerSprites[_currentIndex];
        if (_changed)
        {
            for (int i = 0; i < JournalText.Journal.dialogToDisplay.Length; i++)
            {
                if (JournalText.Journal.dialogToDisplay[i].playerMarker == this)
                {
                    JournalText.Journal.UpdateTogggleMarker(i, _currentIndex);
                    _changed = false;
                }
            }
        }
    }
}
