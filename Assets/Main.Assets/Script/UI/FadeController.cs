using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

// �쐬�ҁF�n����
// ���C����ʂ̃t�F�[�h�C��

public class FadeController : MonoBehaviour
{
    // �s�v�Ȃ̂ŏ����܂����Bby�R����
    //AudioSource _audioSource;
    //[Tooltip("�����Ƀu�U�[��������")]
    //[SerializeField] AudioClip buzzerClip;
    [Tooltip("������Ȃ�")]
    [SerializeField] UITimer _timer;
    [Tooltip("������Ȃ�")]
    [SerializeField] AroundGuardsmanController _controller;
    [SerializeField] AudioManager _audio;

    // �t�F�[�h�C���ɂ����鎞�ԁi�b�j���ύX��
    [Tooltip("�t�F�[�h�C���ɂ����鎞��")]
    [SerializeField] const float _fadeTime = 1.0f;

    // ���[�v�񐔁i0�̓G���[�j���ύX��
    [Tooltip("���[�v�񐔁A���������Ɗ��炩�ɂȂ�")]
    [SerializeField] const int _loopCount = 60;

    [SerializeField] TextMeshProUGUI _countdownText;
    [SerializeField] Image _countdownImage;
    [SerializeField] Image _fadePanel;

    public NavMeshAgent guardsman;

    float _countdown = 4f;
    int _count;

    // Start is called before the first frame update
    void Start()
    {
        guardsman = guardsman.GetComponent<NavMeshAgent>();

        //UITimer,AroundGuardsmanController���ꎞ��~����
        _timer.enabled = false;
        _controller.enabled = false;
        _fadePanel.enabled = true;
        guardsman.enabled = false;


        // �s�v�Ȃ̂ŏ����܂����Bby�R����
        //_audioSource = GetComponent<AudioSource>();

        //�t�F�[�h�C���R���[�`���X�^�[�g
        StartCoroutine("Color_FadeIn");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Color_FadeIn()
    {
        //���y��炷
        // SE�̃u�U�[�����Đ����܂��Bby�R����
        //audioSource.PlayOneShot(buzzerClip);
        _audio.PlaySESound(SEData.SE.Buzzer);

        //�I���܂őҋ@
        // �Ȃ�����Ă��邩�`�F�b�N����֐����ĂсA�Ȃ�����I������炱�̊֐��́ufalse�v�̒l�����̂ł��̏������ɂ��Ă��܂��Bby �R����
        yield return new WaitWhile(() => (!_audio.CheckPlaySound(_audio.seAudioSource)));

        // ��ʂ��t�F�[�h�C��������R�[���`��

        // �F��ς���Q�[���I�u�W�F�N�g����Image�R���|�[�l���g���擾
        Image fade = GetComponent<Image>();

        // �t�F�[�h���̐F��ݒ�i���j���ύX��
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (255.0f / 255.0f));

        // �E�F�C�g���ԎZ�o
        float wait_time = _fadeTime / _loopCount;

        // �F�̊Ԋu���Z�o
        float alpha_interval = 255.0f / _loopCount;

        // �F�����X�ɕς��郋�[�v
        for (float alpha = 255.0f; alpha >= 0.0f; alpha -= alpha_interval)
        {
            // �҂�����
            yield return new WaitForSeconds(wait_time);

            // Alpha�l��������������
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }

        _countdownText.gameObject.SetActive(true);
        _countdownImage.gameObject.SetActive(true);

        while (_countdown > 0)
        {
            _countdown -= Time.deltaTime;
            _countdownImage.fillAmount = _countdown % 1.0f;
            _count = (int)_countdown;
            _countdownText.text = _count.ToString();

            if (_countdown <= 0)
            {
                //UITimer,AroundGuardsmanController���Đ�����
                _timer.enabled = true;
                _controller.enabled = true;
                guardsman.enabled = true;

                _countdownText.gameObject.SetActive(false);
                _countdownImage.gameObject.SetActive(false);

                // BGM���Đ����� by�R����
                _audio.PlayBGMSound(BGMData.BGM.Main);

                yield break;
            }
            yield return null;
        }

    }
}
