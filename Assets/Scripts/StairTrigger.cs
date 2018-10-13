using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairTrigger : MonoBehaviour {

    public event Action OnStartStairsRaise;
    public event Action OnEndStairsRaise;

    public SaveData masterSaveData;
    public GameObject firstStair;
    public float stairRaiseHeight = 1;
    public float stairRaiseSpeed = 0.1f;

    private bool raising = false;
    private GameObject currentStair;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !masterSaveData.stairsRaised) { RaiseStairs(); }
    }

    public void Start()
    {
        if (masterSaveData.stairsRaised) { SetStairsUp(); }
        else { SetStairsDown(); }
    }

    private void Update()
    {
        if (raising)
        {
            currentStair.transform.position += currentStair.transform.up * stairRaiseSpeed * Time.deltaTime;
            if (currentStair.transform.localPosition.y > stairRaiseHeight) 
            { // move to next stair
                
                if (currentStair.transform.childCount == 0)
                { // last stair, end process
                    raising = false;
                    GetComponent<AudioSource>().Stop();
                    if (OnEndStairsRaise != null) { OnEndStairsRaise(); }
                }
                else
                { // move to next stair
                    currentStair = currentStair.transform.GetChild(0).gameObject;
                }
            }
        }
    }

    private void ChangeGameObjectY(GameObject go, float y)
    {
        Vector3 newPosition = go.transform.position;
        newPosition.y = y;
        go.transform.position = newPosition;
    }

    private void RaiseStairs()
    {
        if (OnStartStairsRaise != null) { OnStartStairsRaise(); }
        masterSaveData.stairsRaised = true;
        GetComponent<AudioSource>().Play();
        currentStair = firstStair;
        raising = true;
    }

    private void SetStairsUp()
    {
        currentStair = firstStair;
        while (currentStair.transform.childCount > 0)
        {
            currentStair.transform.localPosition = new Vector3(currentStair.transform.localPosition.x, stairRaiseHeight, currentStair.transform.localPosition.z);
            currentStair = currentStair.transform.GetChild(0).gameObject;
        }
    }

    private void SetStairsDown()
    {
        currentStair = firstStair;
        while (currentStair.transform.childCount > 0)
        {
            currentStair.transform.localPosition = new Vector3(currentStair.transform.localPosition.x, -0.01f, currentStair.transform.localPosition.z);
            currentStair = currentStair.transform.GetChild(0).gameObject;
        }
    }

}
