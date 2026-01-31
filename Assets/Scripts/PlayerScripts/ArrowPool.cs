using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    [Header("Pooling")]
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private List<GameObject> listBullet;

    private static ArrowPool instance;

    public static ArrowPool Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AddBullet(poolSize);
    }

    private void AddBullet(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject goBullet = Instantiate(prefabBullet);
            goBullet.SetActive(false);
            listBullet.Add(goBullet);
            goBullet.transform.parent = transform;
        }
    }

    public GameObject GetBullet()
    {
        for (int i = 0; i < listBullet.Count; i++)
        {
            if (!listBullet[i].activeSelf)
            {
                listBullet[i].SetActive(true);
                return listBullet[i];
            }
        }

        AddBullet(1);
        listBullet[listBullet.Count - 1].SetActive(true);
        return listBullet[listBullet.Count - 1];
    }
}
