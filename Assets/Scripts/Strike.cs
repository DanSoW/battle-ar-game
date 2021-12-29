using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoBehaviour
{
    private ProgramManager _programManager;
    private bool killed = false;

    void Start()
    {
        _programManager = FindObjectOfType<ProgramManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if((!killed) && (collision.gameObject.name == "Shell(Clone)"))
        {
            _programManager.strikes += 1;
            killed = true;
        }
    }
}
