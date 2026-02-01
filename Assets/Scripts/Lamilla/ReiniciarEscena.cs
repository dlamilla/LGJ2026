using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReiniciarEscena : MonoBehaviour
{
    public Player player;
    bool unaVez;
    public void Reiniciar()
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if(player.isDead && !unaVez)
        {
            StartCoroutine(Cor());
        }
    }

    IEnumerator Cor()
    {
        unaVez = true;
        yield return new WaitForSeconds(1.5f);
        Reiniciar();
    }
}
