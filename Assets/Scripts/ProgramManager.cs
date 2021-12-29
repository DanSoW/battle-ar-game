using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ProgramManager : MonoBehaviour
{
    [Header("Put your plane marker here")]
    [SerializeField]
    private GameObject _planeMarkerPrefab;

    [Header("Put your camera")]
    [SerializeField]
    private Camera _camera;

    [Header("Put object to spawn")]
    public GameObject objectToSpawn;

    [Header("Put ScrollView here")]
    public GameObject scrollView;

    [Header("Put your end text")]
    [SerializeField]
    private GameObject _endText;

    private ARRaycastManager _raycastManager;
    private Vector2 _touchPosition;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit> ();

    public bool chooseObject = false;
    private GameObject _selectedObject;

    [Header("Put your maket shell")]
    [SerializeField]
    private GameObject _maketShell;

    public bool rotation;
    private Quaternion _yRotation;
    public bool recharging;
    public int strikes;

    void Start()
    {
        // Поиск объекта по типу
        _raycastManager = FindObjectOfType<ARRaycastManager>();

        // Изменение состояния видимости Plane Marker
        _planeMarkerPrefab.SetActive(false);
        scrollView.SetActive(false);
        _endText.SetActive(false);
    }

    void Update()
    {
        if (chooseObject)
        {
            ShowMarkerAndSetObject();
        }

        MoveObjectAndRotation();

        if(strikes > 2)
        {
            _endText.SetActive(true);
        }

        if (recharging)
        {
            _maketShell.SetActive(false);
        }
        else
        {
            _maketShell.SetActive(true);
        }
    }

    void ShowMarkerAndSetObject()
    {
        // Информация о пересечении лучей с обнаруженными плоскостями
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        _raycastManager.Raycast(
            new Vector2(Screen.width / 2, Screen.height / 2),
            hits,
            TrackableType.Planes
        );

        // Установка маркера
        if (hits.Count > 0)
        {
            // Изменение позиции Plane Marker
            _planeMarkerPrefab.transform.position = hits[0].pose.position;
            _planeMarkerPrefab.SetActive(true);
        }

        // Установка объекта
        if ((Input.touchCount > 0) && (Input.touches[0].phase == TouchPhase.Began))
        {
            Instantiate(objectToSpawn, hits[0].pose.position, objectToSpawn.transform.rotation);
            _maketShell = GameObject.Find("MaketShell");
            chooseObject = false;
            _planeMarkerPrefab.SetActive(false);
        }
    }

    void MoveObjectAndRotation()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            _touchPosition = touch.position;

            if(touch.phase == TouchPhase.Moved)
            {
                Ray ray = _camera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;

                if (Physics.Raycast(ray, out hitObject))
                {
                    if (hitObject.collider.CompareTag("UnSelected"))
                    {
                        hitObject.collider.gameObject.tag = "Selected";
                    }
                }
            }

            _selectedObject = GameObject.FindWithTag("Selected");

            if ((touch.phase == TouchPhase.Moved)
                && (Input.touchCount == 1))
            {
                if (rotation)
                {
                    _yRotation = Quaternion.Euler(0f, -touch.deltaPosition.x * 0.1f, 0f);
                    _selectedObject.transform.rotation = _yRotation * _selectedObject.transform.rotation;
                }
                else
                {
                    _raycastManager.Raycast(_touchPosition, _hits, TrackableType.Planes);
                    _selectedObject.transform.position = _hits[0].pose.position;
                }
            }

            if(Input.touchCount == 2)
            {
                Touch touch1 = Input.touches[0];
                Touch touch2 = Input.touches[1];

                if((touch1.phase == TouchPhase.Moved)
                    || (touch2.phase == TouchPhase.Moved))
                {
                    float distBetweenTouches = Vector2.Distance(touch1.position, touch2.position);
                    float prevDistBetweenTouches = Vector2.Distance(
                        (touch1.position - touch1.deltaPosition),
                        (touch2.position - touch2.deltaPosition));

                    float delta = distBetweenTouches - prevDistBetweenTouches;

                    if(Mathf.Abs(delta) > 0)
                    {
                        delta *= 0.1f;
                    }
                    else
                    {
                        distBetweenTouches = delta = 0;
                    }

                    _yRotation = Quaternion.Euler(0f, -touch1.deltaPosition.x * delta, 0f);
                    _selectedObject.transform.rotation = _yRotation * _selectedObject.transform.rotation;
                }

                if(touch.phase == TouchPhase.Ended)
                {

                }
            }

            if(touch.phase == TouchPhase.Ended)
            {
                if (_selectedObject.CompareTag("Selected"))
                {
                    _selectedObject.tag = "UnSelected";
                }
            }
        }
    }
}
