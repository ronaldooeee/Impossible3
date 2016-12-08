using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthSystem : MonoBehaviour {



	public int startingHealth;
	public int currentHealth;
	public GameObject spawn;

	public Slider healthSlider;
	public Image fillImage;
	public Color fullHealth = Color.green;
	public Color noHealth = Color.red;


	private void Start ()
	{
        startingHealth = this.GetComponentInParent<PlayerUnit>().health;

        currentHealth = (int)startingHealth;

		spawn = this.gameObject;

        SetHealthUI();
    }

	public void takeDamageAndDie (int amount)
	{
		currentHealth -= amount;
        this.GetComponentInParent<PlayerUnit>().health = currentHealth;

        SetHealthUI ();

		if (currentHealth <= 0) 
		{
			if(BoardManager.enemyUnits.Contains(spawn)) {
				BoardManager.enemyUnits.Remove(spawn);
			}
			if(BoardManager.playerUnits.Contains(spawn)) {
				BoardManager.playerUnits.Remove(spawn);
			}
			Destroy(spawn);
			BoardManager.score += 1;
		}
	}

	private void SetHealthUI()
	{
		healthSlider.value = currentHealth;

		fillImage.color = Color.Lerp (noHealth, fullHealth, currentHealth / startingHealth);
	}
}
