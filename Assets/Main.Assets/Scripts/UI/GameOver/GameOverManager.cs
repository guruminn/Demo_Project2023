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

    public float fadeSpeed = 1f;

    private bool _isButton = false;

    // Start is called before the first frame update
    void Start()
    {
        returnButton.SetActive(false);
        backButton.SetActive(false);

        idolImage.SetActive(false);
        audienceImage.SetActive(false);
        playerImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (FadeVariables.FadeIn)
        {
            idolImage.SetActive(true);
            audienceImage.SetActive(true);
            playerImage.SetActive(true);

            _isButton = true;
        }
        else
            fadeSystem.FadeIn(fadeImage, fadeImage.color.a, fadeSpeed);

        if (_isButton)
        {
            returnButton.SetActive(true);
            backButton.SetActive(true);
        }
    }
}
