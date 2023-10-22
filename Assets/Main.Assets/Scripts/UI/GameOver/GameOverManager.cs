using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameOverManager : MonoBehaviour
{
    private FadeManager fadeSystem=new FadeManager();

    public Image fadeImage;

    public GameObject idolImage;

    public GameObject audienceImage;

    public GameObject playerImage;

    public GameObject returnButton;

    public GameObject backButton;

    public GameObject[] _selectButton = new GameObject[2];

    public float fadeSpeed = 1f;

    private bool _isButton = false;

    private GameObject _button;

    // Start is called before the first frame update
    void Start()
    {
        returnButton.SetActive(false);
        backButton.SetActive(false);

        idolImage.SetActive(false);
        audienceImage.SetActive(false);
        playerImage.SetActive(false);

        foreach (GameObject selectImage in _selectButton)
        {
            selectImage.SetActive(false);
        }

        EventSystem.current.SetSelectedGameObject(returnButton);
    }

    // Update is called once per frame
    void Update()
    {
        _button = EventSystem.current.currentSelectedGameObject;

        if (FadeVariables.FadeIn)
        {
            idolImage.SetActive(true);
            audienceImage.SetActive(true);
            playerImage.SetActive(true);

            _isButton = true;
        }
        else if(!FadeVariables.FadeIn)
        {
            fadeSystem.FadeIn(fadeImage, fadeSpeed);
        }

        if (_isButton)
        {
            returnButton.SetActive(true);
            backButton.SetActive(true);
        }

        if (_button == returnButton && _isButton)
        {
            _selectButton[0].SetActive(false);
            _selectButton[1].SetActive(true);
        }
        if (_button == backButton && _isButton)
        {
            _selectButton[0].SetActive(true);
            _selectButton[1].SetActive(false);
        }
    }
}
