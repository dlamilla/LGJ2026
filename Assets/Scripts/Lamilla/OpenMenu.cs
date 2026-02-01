using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    public GameObject panelOptiones;
    public int cont = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            cont++;
            if(cont == 1)
            {
                ChangeTimeON();
                panelOptiones.SetActive(true);
            }
            else if(cont == 2)
            {
                ChangeTimeOFF();
                panelOptiones.SetActive(false);
                cont = 0;
            }
            
        }
    }

    public void ChangeTimeON()
    {
        Time.timeScale = 0f;
    }

    public void ChangeTimeOFF()
    {
        Time.timeScale = 1f;
    }

    public void Test()
    {
        cont = 0;
    }

}
