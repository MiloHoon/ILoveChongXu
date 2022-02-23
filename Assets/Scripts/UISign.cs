using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISign : MonoBehaviour
{
    public GameObject uiObject;

    // Start is called before the first frame update
    void Start()
    {
        uiObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            uiObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        uiObject.SetActive(false);
    }
}
