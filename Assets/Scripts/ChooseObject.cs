using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseObject : MonoBehaviour
{
    private ProgramManager _programManager;
    private Button _button;

    public GameObject chosedObject;

    void Start()
    {
        _programManager = FindObjectOfType<ProgramManager>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(ChooseObjectFunction);
    }

    void ChooseObjectFunction()
    {
        _programManager.objectToSpawn = chosedObject;
        _programManager.chooseObject = true;
        _programManager.scrollView.SetActive(false);
    }
}
