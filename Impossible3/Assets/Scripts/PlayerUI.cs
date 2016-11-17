﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerUI : MonoBehaviour
{
    public List<GameObject> unitPrefabs;
    private List<GameObject> UIs;
    private List<GameObject> Units;
    public Canvas mainCanvas;
    public GameObject mainUI;


    void Start()
    {

        UIs = new List<GameObject>();
        GameObject player1UI = Instantiate(mainUI, new Vector3(0, 0, 0), Camera.main.transform.rotation) as GameObject;
        player1UI.transform.parent = mainCanvas.transform;
        player1UI.transform.localScale = new Vector3(.2f, .2f, .2f);
        player1UI.GetComponentsInChildren<RectTransform>()[0].anchorMin = new Vector2(0, 1);
        player1UI.GetComponentsInChildren<RectTransform>()[0].anchorMax = new Vector2(0, 1);
        player1UI.GetComponentsInChildren<RectTransform>()[0].localPosition = new Vector3(0 - (float)(Screen.width / 2.5), 0 + (float)(Screen.height / 3), 0);
        //player1UI.GetComponentsInChildren<RectTransform>()[0].localPosition = new Vector3(-350,190,0);

        GameObject player2UI = Instantiate(mainUI, new Vector3(0, 0, 0), Camera.main.transform.rotation) as GameObject;
        player2UI.transform.parent = mainCanvas.transform;
        player2UI.transform.localScale = new Vector3(.2f, .2f, .2f);
        player2UI.GetComponentsInChildren<RectTransform>()[0].anchorMin = new Vector2(0, 1);
        player2UI.GetComponentsInChildren<RectTransform>()[0].anchorMax = new Vector2(0, 1);
        player2UI.GetComponentsInChildren<RectTransform>()[0].localPosition = new Vector3(0 - (float)(Screen.width / 2.5), 0 + (float)(Screen.height / 7.5), 0);

        GameObject player3UI = Instantiate(mainUI, new Vector3(0, 0, 0), Camera.main.transform.rotation) as GameObject;
        player3UI.transform.parent = mainCanvas.transform;
        player3UI.transform.localScale = new Vector3(.2f, .2f, .2f);
        player3UI.GetComponentsInChildren<RectTransform>()[0].anchorMin = new Vector2(0, 1);
        player3UI.GetComponentsInChildren<RectTransform>()[0].anchorMax = new Vector2(0, 1);
        player3UI.GetComponentsInChildren<RectTransform>()[0].localPosition = new Vector3(0 - (float)(Screen.width / 2.5), 0 - (float)(Screen.height / 14), 0);

        UIs.Add(player1UI);
        UIs.Add(player2UI);
        UIs.Add(player3UI);
    }


    // Update is called once per frame
    void Update()
    {

        Units = BoardManager.playerUnits;
        for (int i = 0; i < UIs.Count; i++)
        {
            Image[] bars = UIs[i].GetComponentsInChildren<Image>();
            Image[] bars0 = bars[1].GetComponents<Image>();
            Image[] bars1 = bars[2].GetComponents<Image>();
            Image[] bars2 = bars[3].GetComponents<Image>();
            //try
            {
                //HP Bar UI
                HealthSystem playerHP = Units[i].GetComponents<HealthSystem>()[0];
                float healthratio = (float)playerHP.currentHealth / (float)playerHP.startingHealth;
                if (healthratio >= .66) { bars0[0].color = new Color(0,1,0,1); }
                else if (healthratio >= .33) { bars0[0].color = new Color(1, 0.92f, 0.016f, 1); }
                else { bars0[0].color = new Color(1, 0, 0, 1); }
                //bars0[0].color = Color.Lerp(playerHP.noHealth, playerHP.fullHealth, playerHP.currentHealth / playerHP.startingHealth);
                bars0[0].transform.localScale = new Vector3((float)7 * healthratio, bars0[0].transform.localScale.y, bars0[0].transform.localScale.z);
                //bars0[0].transform.position = new Vector3(0, bars0[0].transform.position.y, bars0[0].transform.position.z);

                //Movement Cooldown UI
                float playerMoveCooldown = Units[i].GetComponents<PlayerUnit>()[0].timeStampMove - Time.time;
                float maxMoveCooldown = Units[i].GetComponents<PlayerUnit>()[0].cooldownMoveSeconds;
                if (playerMoveCooldown >= 0)
                {
                    bars1[0].transform.localScale = new Vector3((float)7 * (1 - (playerMoveCooldown / maxMoveCooldown)), bars1[0].transform.localScale.y, bars1[0].transform.localScale.z);
                }

                //Attack Cooldown UI
                float playerAttackCooldown = Units[i].GetComponents<PlayerUnit>()[0].timeStampAttack - Time.time;
                float maxAttackCooldown = Units[i].GetComponents<PlayerUnit>()[0].cooldownAttackSeconds;
                if (playerAttackCooldown >= 0)
                {
                    bars2[0].transform.localScale = new Vector3((float)7 * (1 - (playerAttackCooldown / maxAttackCooldown)), bars2[0].transform.localScale.y, bars2[0].transform.localScale.z);
                }

            }
            //catch
            //{
            //Temp workaround for when players are deleted so unity doesnt throw 1 million errors
            //}
        }
    }
}
