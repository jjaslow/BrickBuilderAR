using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;


[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane1 : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;
    [SerializeField] BrickDetails brickDetails;

    bool touchAllowed = true;

    Camera mainCamera;
    RaycastHit hit;
    Ray ray;

    private int fingerID;
    float upPosition = 1;

    void Awake()
    {
        mainCamera = Camera.main;

#if UNITY_EDITOR
        fingerID = -1;
#else
    fingerID = 0;
#endif
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;
            touchPosition = new Vector2(mousePosition.x, mousePosition.y);
            touchAllowed = false;
            return true;
        }
#else
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            touchAllowed = false;
            return true;
        }
#endif

        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (!touchAllowed)
            return;

        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (EventSystem.current.IsPointerOverGameObject(fingerID))
        {
            //Debug.Log("Clicked on the UI");
            Invoke("ReEnableTouch", 0.5f);
            return;
        }


        ray = mainCamera.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("hit: "+ hit.collider.tag);


            Vector3 placePosition = new Vector3(hit.point.x, hit.point.y + upPosition, hit.point.z);

            GameObject newBrick = Instantiate(m_PlacedPrefab, placePosition, Quaternion.identity);

            Brick brick = newBrick.GetComponent<Brick>();
            SetBrickLevel(hit, brick);
            brick.SetColor(brickDetails.GetColor());
            brick.SetSize((int)brickDetails.GetSize());

            BrickManager.Instance.AddBrickToList(brick);
        }
    }


    void SetBrickLevel(RaycastHit hit, Brick newBrick)
    {
        if (hit.transform.CompareTag("Brick"))
            newBrick.level = hit.transform.GetComponent<Brick>().level + 1;
        else
            newBrick.level = 0;
    }


    public void ReEnableTouch()
    {
        touchAllowed = true;
    }
}
