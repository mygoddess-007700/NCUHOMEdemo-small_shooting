using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
//        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
