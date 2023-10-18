using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameClearManager : MonoBehaviour
{
    public FadeManager backFade=new FadeManager();

    private FadeManager chekiFade=new FadeManager();

    public TranstionScenes transSystem;

    public Image backImage;

    public Image fadeImage;

    public Image chekiImage;

    public RectTransform[] idolImage = new RectTransform[2];

    public Vector2[] endPosition = new Vector2[2];

    private Vector2[] _startPosition = new Vector2[2];

    private float[] _distance = new float[2];

    private float _time;

    public float _moveSpeed;

    public GameObject playerImage;

    public float countDownTime;

    public TextMeshProUGUI countText;

    private int _uiCount;

    public float backSpeed;

    public float chekiSpeed;

    public GameObject lastText;

    public float changeSpeed;


    // Start is called before the first frame update
    void Start()
    {
        Initi_UI();

        FadeVariables.Initi_Fade();
    }

    // Update is called once per frame
    void Update()
    {
        direction_UI();
    }

    private void Initi_UI()
    {
        for (int i = 0; i < idolImage.Length; i++)
        {
            _startPosition[i] = idolImage[i].anchoredPosition;
            _distance[i] = Vector2.Distance(_startPosition[i], endPosition[i]);
        }

        _uiCount = 0;
    }

    private void direction_UI()
    {
        switch (_uiCount)
        {
            case 0:
                backFade.FadeOut(backImage, backImage.color.b, backSpeed,true);
                if (FadeVariables.FadeOut)
                {
                    FadeVariables.FadeOut = false;
                    _uiCount++;
                }
                break;
            case 1:               
                if (!chekiImage.gameObject.activeSelf)
                {
                    chekiImage.gameObject.SetActive(true);
                }
                chekiFade.FadeOut(  chekiImage, chekiImage.color.a, chekiSpeed);
                if (FadeVariables.FadeOut)
                {
                    FadeVariables.FadeOut = false;
                    _time = Time.time;
                    _uiCount++;
                }
                break;
            case 2:               
                Move_IdolImage();
                break;
            case 3:
                playerImage.SetActive(true);
                _uiCount++;
                break;
            case 4:
                CountDown_Text();
                break;
            case 5:
                StartCoroutine(ShotPhoto());
                break;
            case 6:
                backFade.FadeOut(fadeImage, fadeImage.color.a, backSpeed);
                if (FadeVariables.FadeOut)
                {
                    FadeVariables.FadeOut = false;
                    transSystem.Trans_Scene(0);
                }
                break;
            default:
                break;
        }
    }

    private void Move_IdolImage()
    {
        float _positionValue;

        for (int i = 0; i < idolImage.Length; i++)
        {
            // 初期位置と移動先の距離の割合を計算する処理
            // 「(Time.time - time) / _distance」は距離の長さを100として見て時間経過で距離の長さを割ることで２点の移動距離を指定する値を求める。
            _positionValue = ((Time.time - _time) / _distance[i]) * _moveSpeed;

            // カメラの位置を動かす処理
            idolImage[i].anchoredPosition = Vector2.Lerp(_startPosition[i], endPosition[i], _positionValue);

            // カメラの位置が指定した位置に来た場合
            if ((idolImage[0].anchoredPosition == endPosition[0]) && (idolImage[1].anchoredPosition == endPosition[1]))
            {
                _uiCount++;
            }
        }
    }

    private void CountDown_Text()
    {
        int _countDownText;

        countDownTime -= Time.deltaTime;
        
        _countDownText = (int)countDownTime;

        countText.text = (_countDownText+1).ToString();

        if ((int)countDownTime < 0)
        {
            countText.enabled = false;
            _uiCount++;
        }
    }

    private IEnumerator ShotPhoto()
    {
        yield return new WaitForSeconds(changeSpeed);

        if (_uiCount == 5)
        {
            _uiCount++;
        }
        

        yield return null;
    }
}
