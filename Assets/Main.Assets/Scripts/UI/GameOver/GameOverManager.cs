using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    private FadeManager fadeSystem;

    public Image fadeImage;

    public float fadeSpeed = 0.1f;

    private int _uiCount;

    // Start is called before the first frame update
    void Start()
    {
        fadeSystem.FadeIn(fadeImage, fadeImage.color.a);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
