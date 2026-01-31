using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
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
        while (currentGreen >= 1)
        {
            Image img = greenImage;
            Sprite normalSprite = greenSprites[currentGreen];
            Sprite warningSprite = greenSprites[currentGreen - 1];

            img.DOKill();
            DG.Tweening.Sequence seq = DOTween.Sequence();
            seq.AppendCallback(() => img.sprite = warningSprite);
            seq.AppendInterval(0.08f);
            seq.AppendCallback(() => img.sprite = normalSprite);
            seq.AppendInterval(0.08f);

            seq.SetLoops(4);
            yield return seq.WaitForCompletion();
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
