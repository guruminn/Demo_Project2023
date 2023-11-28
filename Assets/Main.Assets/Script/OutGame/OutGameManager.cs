using Cinemachine;
using Mocopi.Receiver;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//  ゲームがゲームオーバー・クリアになった時の処理
//  作成者：山﨑晶

public class OutGameManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    ///  Q [   I [ o [ ?    ?     ? 
    /// </summary>
    public static bool gameOver;

    /// <summary>
    ///  Q [   N   A ?    ?     ? 
    /// </summary>
    public static bool gameClear;

    /// <summary>
    ///  uFadeSystem v ?C   X ^   X ?? 
    /// </summary>
    private FadeManager _fadeSystem;

    /// <summary>
    ///  Q [   N   A E I [ o [  ??        ?t F [ h A E g  ? 
    /// </summary>
    private FadeManager.FadeSetting _blackFadeOut;

    /// <summary>
    ///   ?I     ?t F [ h A E g ?? 
    /// </summary>
    private FadeManager.FadeSetting _endFadeOut;

    /// <summary>
    ///  uTranstionScene v ?C   X ^   X ?? 
    /// </summary>
    private TranstionScenes _transSystem;

    /// <summary>
    ///  Q [   I [ o [ A Q [   N   A  \      text   擾
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _logoText;

    /// <summary>
    ///  Q [   I [ o [ ?  ?\       ?    ?     ? 
    /// </summary>
    [SerializeField]
    private string _overText;

    /// <summary>
    ///  Q [   N   A ?  ?\       ?    ?     ? 
    /// </summary>
    [SerializeField]
    private string _clearText;

    [Space(10)]

    /// <summary>
    ///  v   C   [ ?R   | [ l   g   Q ?  ? ?A I u W F N g   擾
    /// </summary>
    [SerializeField]
    private GameObject _playerObj;

    //  v   C   [ ??  ?J     ??    ?   script   Q ?  ? ?A I u W F N g   擾
    [SerializeField] private GameObject[] playerDontMove = new GameObject[2];
    /// <summary>
    ///  J     ?R   | [ l   g   Q ?  ? ?A I u W F N g   擾
    /// </summary>
    [SerializeField]
    private GameObject _cameraObj;

    /// <summary>
    ///  x     ?R   | [ l   g   Q ?  ? ?A I u W F N g   擾
    /// </summary>
    [SerializeField]
    private GameObject _guardObj;

    [Space(10)]

    [SerializeField, Range(0f, 10f)]
    private int _waitTime = 3;

    #endregion ---Fields---

    #region ---Methods---

    private void Start()
    {

    }

    private void Update()
    {
        //  Q [   I [ o [ ?  ?   
        if (gameOver)
        {
            AudioManager.audioManager.Stop_Sound(AudioManager.audioManager.bgmAudioSource);
            Direction_UI(_overText, 4);
        }

        //  Q [   N   A ?  ?   
        if (gameClear)
        {
            AudioManager.audioManager.Stop_Sound(AudioManager.audioManager.bgmAudioSource);
            Direction_UI(_clearText, 3);
        }
    }

    /// <summary>
    /// UI ?  o ?         ? 
    /// </summary>
    /// <param name="textWord">  \      e L X g </param>
    /// <param name="sceneNumber">  J ?      V [   ??  </param>
    /// <returns>  ?      </returns>
    private IEnumerator Direction_UI(string textWord, int sceneNumber)
    {
        //  v   C   [ A x     ?      ~ ? 
        DontMove_AntherScript();

        //  t F [ h A E g ?  o   ?яo  
        _fadeSystem.FadeOut(_blackFadeOut);

        //  t F [ h A E g   I      ?
        if (FadeManager.fadeOut)
        {
            //  w ?   e L X g  \  
            _logoText.text = textWord;

            // uFadeOut v ?   ??  ?   
            FadeManager.fadeOut = false;

            //  w ?   b    ? 
            yield return new WaitForSeconds(_waitTime);

            // uFadeOut v   ?яo   B
            _fadeSystem.FadeOut(_endFadeOut);

            //   ?  ?  ?    ?
            if (FadeManager.fadeOut)
            {
                //  w ?   ?  ?V [   ?J ?   
                _transSystem.Trans_Scene(sceneNumber);
            }
        }
    }

    /// <summary>
    ///  v   C   [ ??  ?J     ?    A x     ??    ~ ?鏈   ?? 
    /// </summary>
    private void DontMove_AntherScript()
    {
        //  v   C   [ ??   script ??  ?   
        _playerObj.GetComponent<MocopiAvatar>().enabled = false;

        //  v   C   [ ?R   g   [   [ ??  p  script ??  ?   
        _playerObj.GetComponent<PlayerController>().enabled = false;

        //  v   C   [  Mocopi ??  p X N   v g ??  ?   
        _playerObj.GetComponent<PlayerWalkManager>().enabled = false;

        //  v   C   [ ?J      script ??  ?   
        _cameraObj.GetComponent<CinemachineBrain>().enabled = false;

        //  x     ??   script ??  ?   
        _guardObj.GetComponent<AroundGuardsmanController>().enabled = false;

        //  x     ??   NavMeshAgent ??  ?   
        _guardObj.GetComponent<NavMeshAgent>().enabled = false;

        //  x     ??   Animator ??  ?   
        _guardObj.GetComponent<Animator>().enabled = false;
    }

    #endregion ---Methods---
}