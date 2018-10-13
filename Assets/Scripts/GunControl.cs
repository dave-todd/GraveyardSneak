using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{

    public event Action OnGunShoots;
    public event Action OnGunReloads;

    public Transform bulletStartPoint;
    public Transform mainCamera;
    public GameObject BulletPrefab;
    public WarningScript warningScreen;
    public float shootSpeed = 1;
    public float maxShootingRange = 100;
    public bool paused = false;

    private bool stateRecoil = false;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !stateRecoil)
        {
            Shoot();
        }
    }

    public void ActivateGun()
    {
        gameObject.SetActive(true);
    }

    public void DeactivateGun()
    {
        gameObject.SetActive(false);
    }

    private void Shoot()
    {
        if (paused) { return; }
        stateRecoil = true;
        Invoke("SetStateRecoilFalse", shootSpeed);
        GetComponent<AudioSource>().Play();
        RaycastHit hit;
        LineRenderer newBullet = Instantiate(BulletPrefab, bulletStartPoint.position, mainCamera.rotation).GetComponent<LineRenderer>();
        newBullet.SetPosition(0, bulletStartPoint.position);
        if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, maxShootingRange))
        {
            newBullet.SetPosition(1, hit.point);
            if (hit.transform.tag == "Creature")
            { // hit creature
                hit.transform.GetComponent<CreatureAnimationScript>().KillCreature();
                warningScreen.Deactivate();
            }
            else
            { // hit background
            }
        }
        else
        { // shoot into space
            newBullet.SetPosition(1, mainCamera.position + (mainCamera.forward * maxShootingRange));
        }
        if (OnGunShoots != null) { OnGunShoots(); }
    }

    private void SetStateRecoilFalse()
    {
        stateRecoil = false;
        if (OnGunReloads != null) { OnGunReloads(); }
    }

}
