using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class BarController : MonoBehaviour
{
    [Header("Barra Roja")]
    public int maxRed = 5;
    public int currentRed = 5;

    public GameObject[] redImageHearth;
    public Sprite greenImageHearth;

    public Sprite hearthRed;
    public Sprite hearthWhite;

    [Header("Barra Verde")]
    public int maxGreen = 6;
    public int currentGreen = 6;

    public Image greenImage;
    public Sprite[] greenSprites;

    public bool bChangeColor;
    public bool bRestore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentGreen = maxGreen;
        UpdateGreenBar();

        currentRed = maxRed;
        UpdateRedBar();

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(ChangeValueGreenFull()); 
        }

        if(Input.GetKeyDown(KeyCode.H))
        {
            AddGreen(1);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            RemoveRed(1);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            AddRed(1);
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            ChangeColor();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            ChangeColorBack();
        }
    }

    public void AddGreen(int amount)
    {
        if (bRestore) return;
        currentGreen = Mathf.Clamp(currentGreen + amount, 0, maxGreen);
        UpdateGreenBar();
    }

    public void RemoveGreen(int amount)
    {
        currentGreen = Mathf.Clamp(currentGreen - amount, 0, maxGreen);
        UpdateGreenBar();
    }

    public void AddRed(int amount)
    {
        if (currentRed + amount <= maxRed)
        {
            if (bChangeColor)
            {
                redImageHearth[currentRed].GetComponent<Image>().sprite = greenImageHearth;
                currentRed += amount;
            }
            else
            {
                redImageHearth[currentRed].GetComponent<Image>().sprite = hearthRed;
                currentRed += amount;
            }
                
        }
    }

    public void ChangeColor()
    {
        bChangeColor = true;
        for(int i = 0; i < maxRed; i++)
        {
            if(i < currentRed)
            {
                redImageHearth[i].GetComponent<Image>().sprite = greenImageHearth;
            }
        }
    }

    public void ChangeColorBack()
    {
        bChangeColor = false;
        for (int i = 0; i < maxRed; i++)
        {
            if (i < currentRed)
            {
                redImageHearth[i].GetComponent<Image>().sprite = hearthRed;
            }
        }
    }

    public void RemoveRed(int amount)
    {
        if(currentRed - amount >= 0)
        {
            currentRed -= amount;
            redImageHearth[currentRed].GetComponent<Image>().sprite = hearthWhite;
        }
    }

    private IEnumerator ChangeValueGreenFull()
    {
        bRestore = true;
        /*float t = 2f;*/
        while (currentGreen >= 1)
        {
            /*t -= Time.deltaTime;
            if(t < 1f)
            {

            }*/
            yield return new WaitForSecondsRealtime(2f);
            currentGreen--;
            UpdateGreenBar();
        }
        bRestore = false;

    }

    public void UpdateRedBar()
    {

        for(int i = 0; i < maxRed; i++)
        {
            redImageHearth[i].GetComponent<Image>().sprite = hearthRed;
        }
    }

    public void UpdateGreenBar()
    {
        greenImage.sprite = greenSprites[currentGreen];
    }
}
