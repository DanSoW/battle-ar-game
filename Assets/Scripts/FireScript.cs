using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireScript : MonoBehaviour
{
    private ProgramManager _programManager;
    private Button _button;
    private GameObject _beam;
    private Rigidbody _beamRigidbody;

    [Header("Put your force")]
    public float force = 5;

    void Start()
    {
        _programManager = FindObjectOfType<ProgramManager>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(FireFunction);
    }

    void FireFunction()
    {
        _beam = GameObject.Find("Beam");
        _beamRigidbody = _beam.GetComponent<Rigidbody>();

        if (!_programManager.recharging)
        {
            _beamRigidbody.AddForce(_beamRigidbody.transform.up * force, ForceMode.Impulse);
            _programManager.recharging = true;
        }
    }
}
