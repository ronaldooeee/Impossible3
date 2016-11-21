﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthSystem : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;

	public Slider healthSlider;
	public Image fillImage;
	public Color fullHealth = Color.green;
	public Color noHealth = Color.red;

	private void OnEnable ()
	{
		currentHealth = startingHealth;

		SetHealthUI ();
	}

	public bool takeDamageAndDie (int amount)
	{
		currentHealth -= amount;

		SetHealthUI ();

		if (currentHealth <= 0) 
		{
            return true;
		}

        return false;
	}

	private void SetHealthUI()
	{
		healthSlider.value = currentHealth;

		fillImage.color = Color.Lerp (noHealth, fullHealth, currentHealth / startingHealth);
	}
}
