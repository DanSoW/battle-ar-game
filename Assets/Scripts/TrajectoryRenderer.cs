using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    [Header("Put your catapult")]
    [SerializeField]
    private GameObject _catapult;


    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        _catapult = GameObject.Find("Catapult(Clone)");
    }

    public void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        Vector3[] points = new Vector3[100];

        _lineRenderer.positionCount = points.Length;

        for(int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f;
            points[i] = origin + speed * time + Physics.gravity * time * time / 2;

            if(points[i].y < _catapult.gameObject.transform.position.y - 1)
            {
                _lineRenderer.positionCount = i;
                break;
            }

            _lineRenderer.SetPositions(points);
        }
    }
}
