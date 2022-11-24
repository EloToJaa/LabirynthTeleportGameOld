using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public Doors[] doors;
    public KeyColor myColor;
    bool iCanOpen = false;
    bool locked = false;
    Animator key;

    private void Start()
    {
        key = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            iCanOpen = true;
            Debug.Log("You can use this Lock");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            iCanOpen = false;
            Debug.Log("You can not use this Lock");
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && iCanOpen && !locked)
        {
            key.SetBool("useKey", CheckTheKey());
        }
    }

    public void UseKey()
    {
        foreach(Doors door in doors)
        {
            door.OpenClose();
        }
    }

    public bool CheckTheKey()
    {
        if(GameManager.gameManager.redKey > 0 && myColor == KeyColor.Red)
        {
            GameManager.gameManager.redKey--;
            locked = true;
            return true;
        }
        if (GameManager.gameManager.greenKey > 0 && myColor == KeyColor.Green)
        {
            GameManager.gameManager.greenKey--;
            locked = true;
            return true;
        }
        if (GameManager.gameManager.goldKey > 0 && myColor == KeyColor.Gold)
        {
            GameManager.gameManager.goldKey--;
            locked = true;
            return true;
        }
        Debug.Log("Nie mo¿esz otworzyæ drzwi");
        return false;
    }
}
