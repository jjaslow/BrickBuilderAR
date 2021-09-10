using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public enum Size
    {
        Small,
        Medium,
        Large
    }

    public List<Material> materials = new List<Material>();

    public Size size;
    public Color color;
    public int leftPos;
    public int rightPos;

    public void SetBrickDetails()
    {
        SetColor();
        SetSize();
    }

    void SetSize()
    {
        int i = Random.Range(0, Size.GetNames(typeof(Size)).Length);

        float xval = transform.localScale.x * (1 + i);
        transform.localScale = new Vector3(xval, transform.localScale.y, transform.localScale.z);

        size = (Size)i;
    }

    void SetColor()
    {
        int i = Random.Range(0, materials.Count);
        color = materials[i].color;

        GetComponent<Renderer>().material.color = color;
    }

    void SetLeftPos()
    {

    }

    void SetRightPos()
    {

    }



}


