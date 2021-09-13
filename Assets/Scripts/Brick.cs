using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Brick : MonoBehaviour
{
    public Size size;
    public Color color;
    public int level;
    public Vector3 finalPosition;

    float timeToMove = .5f;
    bool foundFirstHit = false;
    Rigidbody rb;
    PlaceOnPlane1 placeOnPlane;
    private IEnumerator coroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        placeOnPlane = FindObjectOfType<PlaceOnPlane1>();
    }


    public void SetColor(Color c)
    {
        GetComponent<Renderer>().material.color = c;

        color = c;
    }

    public void SetSize(int i)
    {
        float xval = transform.localScale.x * (1 + i);
        transform.localScale = new Vector3(xval, transform.localScale.y, transform.localScale.z);

        size = (Size)i;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (foundFirstHit)
            return;

        foundFirstHit = true;

        Debug.Log("collided with " + collision.transform.name);

        //center brick
        if (BrickManager.Instance.placedBricks.Count == 1)
        {
            LockPosition();
            ReEnableTouch();
            return;
        }

        Vector3 intermediatePosition = new Vector3(finalPosition.x, finalPosition.y, transform.position.z);

        coroutine = SetPosition(transform.position, intermediatePosition, finalPosition);
        StartCoroutine(coroutine);
    }



    IEnumerator SetPosition(Vector3 startPos, Vector3 endPos1, Vector3 endPos2)
    {
        float timeElapsed = 0;

        while (timeElapsed < timeToMove)
        {
            transform.position = Vector3.Lerp(startPos, endPos1, timeElapsed / timeToMove);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        timeElapsed = 0;

        while (timeElapsed < timeToMove)
        {
            transform.position = Vector3.Lerp(endPos1, endPos2, timeElapsed / timeToMove);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos2;
        LockPosition();
        ReEnableTouch();
    }



    public void LockPosition()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.isKinematic = true;
        gameObject.isStatic = true;
    }

    private void ReEnableTouch()
    {
        placeOnPlane.ReEnableTouch();
    }

}

public enum Size
{
    Small,
    Medium,
    Large
}
