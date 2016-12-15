using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class HealthSystem : MonoBehaviour
{

    public int startingHealth;
    public int currentHealth;
    public GameObject unit;

    public GameObject missHit;
    public bool isDead = false;

    public Slider healthSlider;
    public Image fillImage;
    public Color fullHealth = Color.green;
    public Color noHealth = Color.red;

    public List<GameObject> damages = new List<GameObject>();


    private void Start()
    {
        startingHealth = this.GetComponentInParent<PlayerUnit>().health;

        currentHealth = (int)startingHealth;

        unit = this.gameObject;

        SetHealthUI();
    }

    private void Update()
    {
        foreach (GameObject hitText in damages)
        {
            {
                Vector3 position = hitText.GetComponentInChildren<RectTransform>().localPosition;
                Vector3 newPositon = new Vector3(position.x, position.y + 0.05f, position.z);
                hitText.GetComponentInChildren<RectTransform>().localPosition = newPositon;
                if (position.y > 1.75)
                {
                    if (isDead)
                    {
                        Destroy(unit);                        
                    }else
                    {
                        Destroy(hitText);
                        damages.Remove(hitText);
                        break;
                    }
                }
            }
        }
    }

    public void takeDamageAndDie(int amount)
    {
        currentHealth -= amount;
        this.GetComponentInParent<PlayerUnit>().health = currentHealth;
        SetHealthUI();
        ConfirmHit(amount);
        if (currentHealth <= 0)
        {
            if (BoardManager.enemyUnits.Contains(unit))
            {
                BoardManager.enemyUnits.Remove(unit);
                BoardManager.score += 1; // Only update the score if an enemy is killed, not a player
                BoardManager.Units[unit.GetComponent<PlayerUnit>().CurrentX, unit.GetComponent<PlayerUnit>().CurrentY] = null;
            }
            if (BoardManager.playerUnits.Contains(unit))
            {
                BoardManager.playerUnits.Remove(unit);
                BoardHighlights.Instance.Hidehighlights();
                PlayerUnit pos = unit.GetComponent<PlayerUnit>();
                BoardManager.Units[pos.CurrentX, pos.CurrentY] = null;
                //Drop a Grave
                BoardManager.Instance.SpawnObstacle(pos.CurrentX, pos.CurrentY, "grave");
            }
            isDead = true;
            foreach (Component child in unit.GetComponents<Component>())
            {
                if(child.GetType() != this.GetType() && child.GetType() != typeof(UnityEngine.Transform))
                {
                    Destroy(child);
                }
            }
        }

    }

    private void SetHealthUI()
    {
        healthSlider.value = currentHealth;

        fillImage.color = Color.Lerp(noHealth, fullHealth, currentHealth / startingHealth);
    }

    public void ConfirmHit(int? damage, string buff =null)
    {
        PlayerUnit character = this.GetComponentInParent<PlayerUnit>();
        GameObject hitText = Instantiate(this.missHit, character.transform.GetChild(0).transform.position, Camera.main.transform.rotation) as GameObject;
        //hitText.GetComponentInChildren<RectTransform>().localPosition = character.transform.position;
        hitText.GetComponentInChildren<RectTransform>().localScale = new Vector3(0.017f, 0.017f, 0.017f);
        hitText.transform.SetParent(character.transform.GetChild(0).transform);
        hitText.GetComponentInChildren<RectTransform>().anchorMin = new Vector2(0, 1);
        hitText.GetComponentInChildren<RectTransform>().anchorMax = new Vector2(0, 1);
        if(buff != null)
        {
            hitText.GetComponent<TextMesh>().text = buff;
            hitText.GetComponent<TextMesh>().color = Color.green;
        }
        else if(damage == null)
        {
            hitText.GetComponent<TextMesh>().text = "Miss!";
        }        
        else if (Convert.ToInt32(damage) > 0)
        {
            hitText.GetComponent<TextMesh>().text = "-" + damage;
        }
        else
        {
            hitText.GetComponent<TextMesh>().text = "+" + Math.Abs(Convert.ToInt32(damage));
            hitText.GetComponent<TextMesh>().color = Color.green;
        }
        damages.Add(hitText);
        
    }

}
