using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowShell : MonoBehaviour
{
    private ProgramManager _programManager;
    private TrajectoryRenderer _trajectoryRenderer;
    // private FireScript _fireScript;

    [SerializeField]
    private GameObject _shellPrefab;

    private GameObject _shellObject;
    private Rigidbody _shellRigidbody;
    private Vector3 _speed;

    private GameObject _fielldObject;
    private InputField _field;
    private string _forceString;
    private int force;
    private Rigidbody _collisionRigidBody;

    public AudioClip throwSound;
    private AudioSource _catapultSound;

    void Start()
    {
        _programManager = FindObjectOfType<ProgramManager>();
        _trajectoryRenderer = FindObjectOfType<TrajectoryRenderer>();
        // _fireScript = FindObjectOfType<FireScript>();
        _fielldObject = GameObject.Find("InputField");
        _field = _fielldObject.GetComponent<InputField>();
    }

    void Update()
    {
        _forceString = _field.text;
        force = (_forceString.Length > 0) ? Int32.Parse(_forceString) : 0;

        _speed = transform.forward * 2 + transform.up * force;
        // _fireScript.force = force;

        _trajectoryRenderer.ShowTrajectory(transform.position + new Vector3(0, 0.25f, 0), _speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _shellObject = Instantiate(_shellPrefab, 
            transform.position + new Vector3(0, 0.25f, -0.05f),
            _shellPrefab.transform.rotation);

        _shellRigidbody = _shellObject.GetComponent<Rigidbody>();
        _shellRigidbody.AddForce(_speed, ForceMode.Impulse);

        _collisionRigidBody = collision.rigidbody;
        _collisionRigidBody.AddForce(_collisionRigidBody.transform.up * (-1), ForceMode.Impulse);

        _programManager.recharging = true;

        _catapultSound = GetComponent<AudioSource>();
        _catapultSound.PlayOneShot(throwSound, 1.0f);
    }
}
