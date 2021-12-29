using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddObject : MonoBehaviour
{
    private Button _button;
    private ProgramManager _programManager;

    void Start()
    {
        _programManager = FindObjectOfType<ProgramManager>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(AddObjectFunction);
    }

    // Добавление объекта
    void AddObjectFunction()
    {
        _programManager.scrollView.SetActive(true);
    }
}
