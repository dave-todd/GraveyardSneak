using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBossScript : MonoBehaviour
{

    public CreatureScript creature;
    public GameObject currentHPBar;
    public GameObject maxHPBar;

    private void OnEnable()
    {
        if (creature != null) { creature.OnStartDeath += OnStartDeath; }
    }

    private void OnDisable()
    {
        if (creature != null) { creature.OnStartDeath -= OnStartDeath; }
    }

    private void OnStartDeath()
    {
        SetHPBar(creature.currentHP, creature.maxHP);
    }

    private void SetHPBar(int currentHP, int maxHP)
    {
        if (currentHP < 1) { maxHPBar.SetActive(false); }
        else if (currentHP > maxHP) { currentHP = maxHP; }

        float currentWidth = (float)currentHP / (float)maxHP;
        float offset = 1 - currentWidth;
        currentHPBar.transform.localScale = new Vector3(currentHPBar.transform.localScale.x, currentWidth, currentHPBar.transform.localScale.z);
        currentHPBar.transform.localPosition = new Vector3(currentHPBar.transform.localPosition.x, -offset, currentHPBar.transform.localPosition.z);
    }
		
}
