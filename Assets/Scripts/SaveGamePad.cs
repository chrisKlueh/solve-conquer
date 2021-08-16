using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGamePad : MonoBehaviour
{
    public Player player;
    public Companion companion;
    public GameObject canvas;

    void Start()
    {
        canvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && SaveSystem.gameLoaded == false)
        {
            SaveSystem.SaveGame(player, companion, this);
            canvas.SetActive(true);
            StartCoroutine("WaitForSec");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        SaveSystem.gameLoaded = false;
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
        canvas.SetActive(false);
    }

}
