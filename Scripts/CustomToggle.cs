using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class CustomToggle : MonoBehaviour, IPointerClickHandler
{
    public bool active;
    public bool? option = null;
    public Sprite trueSprite, falseSprite, nullSprite;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!active) return;

        if (option == null)
            UpdateToggles(true);
        else if (option == true)
            UpdateToggles(false);
        else if (option == false)
            UpdateToggles(null);
    }

    public void UpdateToggles(bool? option)
    {
        this.option = option;
        UpdateSprites();        
    }

    private void UpdateSprites()
    {
        if (option == null)
            _image.sprite = trueSprite;
        else if (option == true)
            _image.sprite = falseSprite;
        else if (option == false)
            _image.sprite = nullSprite;
    }
}
