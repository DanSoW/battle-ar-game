using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotation : MonoBehaviour
{
    private Button _button;
    private ProgramManager _programManager;
    
    void Start()
    {
        _programManager = FindObjectOfType<ProgramManager>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(RotationFunction);
    }

    void RotationFunction()
    {
        if (_programManager.rotation)
        {
            _programManager.rotation = false;
            GetComponent<Image>().color = Color.red;
        }
        else
        {
            _programManager.rotation = true;
            GetComponent<Image>().color = Color.green;
        }
    }
}
