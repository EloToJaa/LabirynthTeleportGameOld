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

    public Material red;
    public Material green;
    public Material gold;

    public Renderer myLock;

    public AudioClip openClip;

    private void Start()
    {
        key = GetComponent<Animator>();
        SetMyColor();
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
            GameManager.gameManager.SetUseInfo("");
        }
    }

    private void Update()
    {
        if(iCanOpen && !locked)
        {
            GameManager.gameManager.SetUseInfo("Press E to open lock");
        }

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
            GameManager.gameManager.PlayClip(openClip);
            GameManager.gameManager.redKeyText.text = GameManager.gameManager.redKey.ToString();
            locked = true;
            return true;
        }
        if (GameManager.gameManager.greenKey > 0 && myColor == KeyColor.Green)
        {
            GameManager.gameManager.greenKey--;
            GameManager.gameManager.PlayClip(openClip);
            GameManager.gameManager.greenKeyText.text = GameManager.gameManager.greenKey.ToString();
            locked = true;
            return true;
        }
        if (GameManager.gameManager.goldKey > 0 && myColor == KeyColor.Gold)
        {
            GameManager.gameManager.goldKey--;
            GameManager.gameManager.PlayClip(openClip);
            GameManager.gameManager.goldKeyText.text = GameManager.gameManager.goldKey.ToString();
            locked = true;
            return true;
        }
        return false;
    }

    void SetMyColor()
    {
        switch (myColor)
        {
            case KeyColor.Red:
                myLock.material = red;
                break;
            case KeyColor.Green:
                myLock.material = green;
                break;
            case KeyColor.Gold:
                myLock.material = gold;
                break;
        }
    }
}
