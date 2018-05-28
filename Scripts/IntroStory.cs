using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroStory : MonoBehaviour
{
    public DialogueNode node;
    public Text text;

    [SerializeField]
    private float fadeTimer = 2;
    private bool _fading = false;
    private Image _image;


    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    void Start()
    {
        Invoke("StartIntro", 1);
    }

    private void StartIntro()
    {
        DialogueSystem.currentNode = node;
        DialogueSystem.isTalking = true;

        DialogueSystem.dialog.textBoard = text;
        DialogueSystem.dialog.currentTextBackground = text.gameObject;

        DialogueSystem.dialog.StartDialog();
        DialogueSystem.textIndex = 0;
    }

    private void FadeOut()
    {
        _fading = true;
        text.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_fading)
        {
            fadeTimer -= Time.deltaTime;
            Color color = _image.color;
            color.a = fadeTimer / 2;
            _image.color = color;

            if(_image.color.a <= 0)
            {
                gameObject.SetActive(false);
                DialogueSystem.isTalking = false;
            }
        }

        if (!_fading && DialogueSystem.textIndex >= node.npcText.Length)
            FadeOut();
    }
}
