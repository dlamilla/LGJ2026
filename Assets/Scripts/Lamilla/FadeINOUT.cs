using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class FadeINOUT : MonoBehaviour
{
    public Image dotweenFade;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dotweenFade.DOFade(0, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
