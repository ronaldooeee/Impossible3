using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerUI : MonoBehaviour {
    public List<GameObject> unitPrefabs;
    private List<GameObject> UIs;
    private List<GameObject> Units;

    // Use this for initialization
    void Start () {
        UIs = new List<GameObject>();
        GameObject player1UI = Instantiate(unitPrefabs[0], new Vector3(5.65f,11.9f,-5.85f), Camera.main.transform.rotation) as GameObject;
        //player1UI.transform.localScale = new Vector3(.005f,.005f,.005f);
        UIs.Add(player1UI);
    }
	
	// Update is called once per frame
	void Update () {
        Units = BoardManager.playerUnits;
        for (int i = 0; i<UIs.Count; i++)
        {
            Image[] bars = UIs[i].GetComponentsInChildren<Image>();
            Image[] bars0 = bars[1].GetComponents<Image>();
            Image[] bars1 = bars[2].GetComponents<Image>();
            Image[] bars2 = bars[3].GetComponents<Image>();
            try
            {
                //HP Bar UI
                HealthSystem playerHP = Units[i].GetComponents<HealthSystem>()[0];
                float healthratio = (float)playerHP.currentHealth / (float)playerHP.startingHealth;
                bars0[0].color = Color.Lerp(playerHP.noHealth, playerHP.fullHealth, playerHP.currentHealth / playerHP.startingHealth);
                bars0[0].transform.localScale = new Vector3((float)7 * healthratio, bars0[0].transform.localScale.y, bars0[0].transform.localScale.z);
                //bars0[0].transform.position = new Vector3(0, bars0[0].transform.position.y, bars0[0].transform.position.z);

                //Movement Cooldown UI
                float playerMoveCooldown = Units[i].GetComponents<PlayerUnit>()[0].timeStampMove - Time.time;
                float maxMoveCooldown = Units[i].GetComponents<PlayerUnit>()[0].cooldownMoveSeconds;
                if (playerMoveCooldown >= 0)
                {
                    bars1[0].transform.localScale = new Vector3((float)7 * (1-(playerMoveCooldown/maxMoveCooldown)), bars1[0].transform.localScale.y, bars1[0].transform.localScale.z);
                }
                //Debug.Log(playerCooldown);

                //Attack Cooldown UI
                float playerAttackCooldown = Units[i].GetComponents<PlayerUnit>()[0].timeStampAttack - Time.time;
                float maxAttackCooldown = Units[i].GetComponents<PlayerUnit>()[0].cooldownAttackSeconds;
                if (playerAttackCooldown >= 0)
                {
                    bars2[0].transform.localScale = new Vector3((float)7 * (1 - (playerAttackCooldown / maxAttackCooldown)), bars2[0].transform.localScale.y, bars2[0].transform.localScale.z);
                }

            }
            catch
            {
                //Temp workaround for when players are deleted so unity doesnt throw 1 million errors
            }
        }
	}
}
