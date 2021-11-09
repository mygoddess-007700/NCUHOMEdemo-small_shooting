using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnButton : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        SceneManager.LoadScene("SceneBegin");
    }
}
