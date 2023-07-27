using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

//textと空のオブジェクトを使ってJoyConの入力（Vector2）を確認する
//作成者：梅森茉優
public class CameraTest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text;
    public void OnMove(InputValue inputValue)
    {
        var vec = inputValue.Get<Vector2>();
        Text.text = $"Move:({vec.x:f2}, {vec.y:f2})\n{Text.text}";
    }
}
