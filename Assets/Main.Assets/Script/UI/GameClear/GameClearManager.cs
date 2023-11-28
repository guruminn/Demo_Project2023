using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

//  ? ?F R    
//  Q [   N   A ??   \ [ X R [ h

public class GameClearManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    ///  uFadeManager v   Q  
    /// </summary>
    private FadeManager _fadeManager;

    /// <summary>
    ///  uTranstionScenes v   Q  
    /// </summary>
    private TranstionScenes _transSystem;

    /// <summary>
    ///  w i ?t F [ h A E g ?? 
    /// </summary>
    [SerializeField]
    private FadeManager.FadeSetting _backGroundFadeOut;

    /// <summary>
    ///  ` F L ?t F [ h A E g ?? 
    /// </summary>
    [SerializeField]
    private FadeManager.FadeSetting _chekiFadeOut;

    /// <summary>
    ///   ?I     ?t F [ h A E g ?? 
    /// </summary>
    [SerializeField]
    private FadeManager.FadeSetting _endFadeOut;

    /// <summary>
    ///  A C h   ??  ?     ? 
    /// </summary>
    [SerializeField]
    private RectTransform[] _idolImage = new RectTransform[2];

    /// <summary>
    ///  A C h   ? ?    ?u  ?     ? 
    /// </summary>
    private Vector2[] _startPosition = new Vector2[2];

    /// <summary>
    ///  A C h   ? ??    ?     ? 
    /// </summary>
    [SerializeField]
    private Vector2[] _endPosition = new Vector2[2];

    /// <summary>
    ///  A C h   ? ?    ?u ??   ?     ?     ? 
    /// </summary>
    private float[] _distance = new float[2];

    /// <summary>
    ///    ? ?     ? 
    /// </summary>
    private float _time;

    /// <summary>
    ///  A C h   ? ??         ?     ? 
    /// </summary>
    [SerializeField, Range(0f, 100f)]
    private float _moveSpeed;

    /// <summary>
    ///  v   C   [ ??   èÔ    ? 
    /// </summary>
    [SerializeField]
    private GameObject _playerImage;

    /// <summary>
    ///  J E   g _ E   ?  ? ?     ? 
    /// </summary>
    [SerializeField, Range(0, 10f)]
    private float _countDownTime;

    /// <summary>
    ///  J E   g _ E    \      e L X g I u W F N g   èÔ    ? 
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _countText;

    /// <summary>
    ///  J E   g _ E   ?  ? int ^ ??     ? 
    /// </summary>
    private int _uiCount;

    /// <summary>
    ///  I ??\      e L X g ?I u W F N g   èÔ    ? 
    /// </summary>
    [SerializeField]
    private GameObject _lastText;

    /// <summary>
    ///  ` F L   B      ??@   ? ?     ? 
    /// </summary>
    [SerializeField, Range(0f, 10f)]
    private float _changeSpeed;

    #endregion ---Fields---

    #region ---Methods---

    // Start is called before the first frame update
    void Start()
    {
        Initi_UI();
    }

    // Update is called once per frame
    void Update()
    {
        direction_UI();
    }

    /// <summary>
    ///      ?? 
    /// </summary>
    private void Initi_UI()
    {
        //  A C h   ??u        
        for (int i = 0; i < _idolImage.Length; i++)
        {
            _startPosition[i] = _idolImage[i].anchoredPosition;
            _distance[i] = Vector2.Distance(_startPosition[i], _endPosition[i]);
        }

        //  J E   g _ E          
        _uiCount = 0;
    }

    /// <summary>
    /// UI ?  o ? 
    /// </summary>
    private void direction_UI()
    {
        switch (_uiCount)
        {
            //  w i ?   t F [ h    
            case 0:
                _fadeManager.FadeOut(_backGroundFadeOut);
                if (FadeManager.fadeOut)
                {
                    FadeManager.fadeOut = false;
                    _uiCount++;
                }
                break;

            //  ` F L   t F [ h    
            case 1:
                _fadeManager.FadeOut(_chekiFadeOut);
                if (FadeManager.fadeOut)
                {
                    FadeManager.fadeOut = false;
                    _time = Time.time;
                    _uiCount++;
                }
                break;

            //  A C h   ?     ?X   C h    
            case 2:
                Move_IdolImage();
                break;

            //  v   C   [ ?  \      
            case 3:
                _playerImage.SetActive(true);
                _uiCount++;
                break;

            //  J E   g _ E    \      
            case 4:
                CountDown_Text();
                break;

            //  ` F L   B e    
            case 5:
                StartCoroutine(ShotPhoto());
                break;

            //   ?I     ?t F [ h    
            case 6:
                _fadeManager.FadeOut(_endFadeOut);
                if (FadeManager.fadeOut)
                {
                    FadeManager.fadeOut = false;
                    _transSystem.Trans_Scene(0);
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    ///  A C h   ? ??    ? 
    /// </summary>
    private void Move_IdolImage()
    {
        float _positionValue;

        for (int i = 0; i < _idolImage.Length; i++)
        {
            //      ?u ??   ?    ?      v Z   ËY  
            //  u(Time.time - time) / _distance v ?    ?     100 ?  ?  ?  ?o ??    ?        Á∑ ??Q _ ??        w ?  l     ? B
            _positionValue = ((Time.time - _time) / _distance[i]) * _moveSpeed;

            //  A C h   ? ??u ??       
            _idolImage[i].anchoredPosition = Vector2.Lerp(_startPosition[i], _endPosition[i], _positionValue);

            //  A C h   ? ??u   w ?   ?u ?    ?
            if ((_idolImage[0].anchoredPosition == _endPosition[0]) && (_idolImage[1].anchoredPosition == _endPosition[1]))
            {
                _lastText.SetActive(false);
                _uiCount++;
            }
        }
    }

    /// <summary>
    ///  J E   g _ E       o    ? 
    /// </summary>
    private void CountDown_Text()
    {
        //    ? ?     ? 
        int _countDownText;

        //    ? ?     
        _countDownTime -= Time.deltaTime;

        //      t   ?  ???  ??     
        _countDownText = (int)_countDownTime;

        //  J E   g _ E   ?  ? \  
        _countText.text = (_countDownText + 1).ToString();

        //  J E   g _ E   ?  ?  O  ?     ?    ?
        if ((int)_countDownTime < 0)
        {
            _countText.enabled = false;
            _uiCount++;
        }
    }

    /// <summary>
    ///  ` F L   B ÁÈ o ?? 
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShotPhoto()
    {
        // SE   ?  ?  ?      ?
        if (AudioManager.audioManager.CheckPlaySound(AudioManager.audioManager.seAudioSource))
        {
            AudioManager.audioManager.Play_SESound(SESoundData.SE.Shutters);
        }

        //  ?     
        yield return new WaitForSeconds(_changeSpeed);

        //  e L X g  \      
        _lastText.SetActive(true);

        // UI ?J E   g  i ? 
        if (_uiCount == 5)
        {
            _uiCount++;
        }

        //  I  
        yield return null;
    }

    #endregion ---Methods---
}