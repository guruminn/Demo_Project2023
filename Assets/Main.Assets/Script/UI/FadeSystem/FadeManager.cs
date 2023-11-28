using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  ? ?F R    
//  t F [ h ??   \ [ X R [ h

public class FadeManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    ///  t F [ h A E g ?    ?     ? 
    /// </summary>
    public static bool fadeOut;

    /// <summary>
    ///  t F [ h C   ?    ?     ? 
    /// </summary>
    public static bool fadeIn;

    #endregion ---Fields---

    #region ---Methods---

    /// <summary>
    ///  t F [ h A E g ?  o      ? 
    /// </summary>
    /// <param name="fade">  t F [ h  ??  \     </param>
    public void FadeOut(FadeSetting fade)
    {
        float fadeColor = fade.fadeImage.color.a;

        //  t F [ h A E g      p l    \      
        fade.fadeImage.enabled = true;

        //      x     Z   ?ƒO  
        fadeColor += fade.fadeSpeed * Time.deltaTime;

        //  t F [ h A E g      p l   ?    x  ??  
        fade.fadeImage.color = new Color(fade.fadeImage.color.r, fade.fadeImage.color.g, fade.fadeImage.color.b, fadeColor);

        //  p l   ?    x   w ?       x ?l ??      ?   
        if (fadeColor >= fade.fadeAlpha)
        {
            //  p l   ?    x   ??  
            fadeColor = fade.fadeAlpha;

            //  t F [ h A E g ?    L   ?   
            fadeOut = true;
        }
    }

    /// <summary>
    ///  t F [ h C       o    ? 
    /// </summary>
    /// <param name="fade">  t F [ h  ??  \     </param>
    public void FadeIn(FadeSetting fade)
    {
        float fadeColor = fade.fadeImage.color.a;

        //  t F [ h A E g      p l    \      
        fade.fadeImage.enabled = true;

        //      x     Z   ?     
        fadeColor -= fade.fadeSpeed * Time.deltaTime;

        //  t F [ h A E g      p l   ?    x  ??  
        fade.fadeImage.color = new Color(fade.fadeImage.color.r, fade.fadeImage.color.g, fade.fadeImage.color.b, fadeColor);

        //  p l   ?    x   w ?       x ?l ??      ?   
        if (fadeColor <= fade.fadeAlpha)
        {
            //  p l   ?    x   ??  
            fadeColor = fade.fadeAlpha;

            //  t F [ h A E g      p l     \      
            fade.fadeImage.enabled = false;

            //  t F [ h C   ?    L   ?   
            fadeIn = true;
        }
    }

    #endregion ---Methods---

    #region ---Struct---

    /// <summary>
    ///  t F [ h  ??  \    
    /// </summary>
    public struct FadeSetting
    {
        /// <summary>
        ///  t F [ h ?     
        /// </summary>
        public string fadeName;

        /// <summary>
        ///  t F [ h      ?
        /// </summary>
        public Image fadeImage;

        /// <summary>
        ///  t F [ h ?   
        /// </summary>
        [Range(0f, 10f)]
        public float fadeSpeed;

        /// <summary>
        ///  t F [ h ?    x
        /// </summary>
        [Range(0f, 1f)]
        public float fadeAlpha;
    }

    #endregion ---Struct---
}