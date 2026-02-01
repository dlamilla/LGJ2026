using UnityEngine;

public class ControlDoor : MonoBehaviour
{
    public GameObject door;
    public GameObject door1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            door.SetActive(false);
            door1.SetActive(true);
        }
    }
}
