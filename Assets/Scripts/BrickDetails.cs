using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickDetails : MonoBehaviour
{

    public List<Material> materials = new List<Material>();

    [SerializeField] Image colorButtonImage;
    [SerializeField] Text sizeButtonText;

    int currentColor = 0;
    Size brickSize;


    private void Start()
    {
        colorButtonImage.color = materials[currentColor].color;

        brickSize = Size.Small;
        sizeButtonText.text = "Set Size:\nSmall";
    }



    public void SetSize()
    {
        int currentSize =(int)brickSize;

        currentSize++;
        if (currentSize == Size.GetNames(typeof(Size)).Length)
            currentSize = 0;

        brickSize = (Size)currentSize;

        sizeButtonText.text = "Set Size:\n" + brickSize.ToString();
    }

    public void SetColor()
    {
        currentColor++;
        if (currentColor == materials.Count)
            currentColor = 0;

        colorButtonImage.color = materials[currentColor].color;
    }


    public Size GetSize()
    {
        return brickSize;
    }

    public Color GetColor()
    {
        return materials[currentColor].color;
    }

}
