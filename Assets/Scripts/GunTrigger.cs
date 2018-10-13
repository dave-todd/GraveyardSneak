using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTrigger : MonoBehaviour
{

    public event Action OnStartDrop;
    public event Action OnEndDrop;

    public PlayerControl player;
    public GameObject gunGraphic;
    public SaveData masterSaveData;
    public GameObject gunBlock;
    public float blockMoveSpeed = 1;

    private const float UnavailableHeight = -2f;
    private const float AvailableHeight = -8f;
    private bool available = false;
    private bool blockMoving = false;

    public void Start()
    {
        if (masterSaveData.hasGun) { MakeGunInvisible(); }
        else { MakeGunVisible();}
        if (masterSaveData.gunAvailable) { MakeGunAvailable(); }
        else { MakeGunUnavailable(); }
    }
        
    public void MakeGunVisible()
    {
        gunGraphic.SetActive(true);
    }

    public void MakeGunInvisible()
    {
        gunGraphic.SetActive(false);
    }

    private void ChangeGameObjectY(GameObject go, float y)
    {
        Vector3 newPosition = go.transform.position;
        newPosition.y = y;
        go.transform.position = newPosition;
    }

    public void MakeGunAvailable()
    {
        ChangeGameObjectY(gunBlock, AvailableHeight);
        available = true;
    }

    public void MakeGunUnavailable()
    {
        ChangeGameObjectY(gunBlock, UnavailableHeight);
        available = false;
    }

    public void DropGunBlock()
    {
        if (available) { return; }
        if (OnStartDrop != null) { OnStartDrop(); }
        masterSaveData.gunAvailable = true;
        blockMoving = true;
        GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        if (blockMoving)
        {
            gunBlock.transform.position += gunBlock.transform.up * blockMoveSpeed * Time.deltaTime * -1; 
            blockMoving = (gunBlock.transform.position.y > AvailableHeight);
            if (!blockMoving)
            {
                MakeGunAvailable();
                if (OnEndDrop != null) { OnEndDrop(); }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && available ) { PickUpGun(); }
    }

    private void PickUpGun()
    {
        gunGraphic.SetActive(false);
        player.ActivateGun();
    }

}
