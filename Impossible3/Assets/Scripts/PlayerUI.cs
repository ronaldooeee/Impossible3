using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class PlayerUI : MonoBehaviour
{
    public List<GameObject> unitPrefabs;
    private List<GameObject> UIs;
    public Canvas mainCanvas;
    public GameObject killScore;
    public GameObject mainUI;
    public GameObject[] units;

    void Start()
    {
        units = new GameObject[] { BoardManager.playerUnits[0], BoardManager.playerUnits[1], BoardManager.playerUnits[2] };
        float width = mainCanvas.transform.localScale.x;
        float height = mainCanvas.transform.localScale.y;

        UIs = new List<GameObject>();
        GameObject player1UI = Instantiate(mainUI, new Vector3(0, 0, 0), Camera.main.transform.rotation) as GameObject;
        player1UI.transform.parent = mainCanvas.transform;
        player1UI.transform.localScale = new Vector3(.2f, .2f, .2f);
        player1UI.GetComponentInChildren<RectTransform>().anchorMin = new Vector2(0, 1);
        player1UI.GetComponentInChildren<RectTransform>().anchorMax = new Vector2(0, 1);
        player1UI.GetComponentInChildren<RectTransform>().localPosition = new Vector3(0 - Screen.width / 2 + 100, 0 + Screen.height / 2 - 50, 0);

        GameObject player2UI = Instantiate(mainUI, new Vector3(0, 0, 0), Camera.main.transform.rotation) as GameObject;
        player2UI.transform.parent = mainCanvas.transform;
        player2UI.transform.localScale = new Vector3(.2f, .2f, .2f);
        player2UI.GetComponentInChildren<RectTransform>().anchorMin = new Vector2(0, 1);
        player2UI.GetComponentInChildren<RectTransform>().anchorMax = new Vector2(0, 1);
        player2UI.GetComponentInChildren<RectTransform>().localPosition = new Vector3(0 - Screen.width / 2 + 100, 0 + Screen.height / 2 - 150, 0);

        GameObject player3UI = Instantiate(mainUI, new Vector3(0, 0, 0), Camera.main.transform.rotation) as GameObject;
        player3UI.transform.parent = mainCanvas.transform;
        player3UI.transform.localScale = new Vector3(.2f, .2f, .2f);
        player3UI.GetComponentInChildren<RectTransform>().anchorMin = new Vector2(0, 1);
        player3UI.GetComponentInChildren<RectTransform>().anchorMax = new Vector2(0, 1);
        player3UI.GetComponentInChildren<RectTransform>().localPosition = new Vector3(0 - Screen.width / 2 + 100, 0 + Screen.height / 2 - 250, 0);

        UIs.Add(player1UI);
        UIs.Add(player2UI);
        UIs.Add(player3UI);

        //Kill Score

        killScore = Instantiate(killScore, new Vector3(0, 0, 0), Camera.main.transform.rotation) as GameObject;
        killScore.transform.localScale = new Vector3(.02f, .02f, .02f);
        killScore.GetComponentsInChildren<RectTransform>()[0].anchorMin = new Vector2(0, 1);
        killScore.GetComponentsInChildren<RectTransform>()[0].anchorMax = new Vector2(0, 1);
        killScore.transform.parent = mainCanvas.transform;
        killScore.GetComponentsInChildren<RectTransform>()[0].localPosition = new Vector3(0, 0 + Screen.height / 2 - 50, 0);
    }


    // Update is called once per frame
    void Update()
    {
        killScore.GetComponentsInChildren<RectTransform>()[0].localPosition = new Vector3(0, 0 + Screen.height / 2 - 50, 0);
        killScore.GetComponent<Text>().text = BoardManager.score.ToString();

        foreach (GameObject unit in units)
        {
            GameObject UI = null;
            if(unit == null) { return; }
            else if (unit.name == "Warrior(Clone)") { UI = UIs[0]; }
            else if (unit.name == "Mage(Clone)") { UI = UIs[1]; }
            else if (unit.name == "Ranger(Clone)") { UI = UIs[2]; }
            if (unit.GetComponent<HealthSystem>().isDead)
            {
                UI.GetComponentsInChildren<Image>()[1].GetComponent<Image>().transform.localScale = Vector3.zero;
                UI.GetComponentsInChildren<Image>()[2].GetComponent<Image>().transform.localScale = Vector3.zero;
                UI.GetComponentsInChildren<Image>()[3].GetComponent<Image>().transform.localScale = Vector3.zero;
                return;
            }

            Image[] bars = UI.GetComponentsInChildren<Image>();
            Image bars0 = bars[1].GetComponent<Image>();
            Image bars1 = bars[2].GetComponent<Image>();
            Image bars2 = bars[3].GetComponent<Image>();

            //HP Bar UI
            HealthSystem playerHP = unit.GetComponents<HealthSystem>()[0];
            float healthratio = (float)playerHP.currentHealth / (float)playerHP.startingHealth;
            if (healthratio >= .66) { bars0.color = new Color(0, 1, 0, 1); }
            else if (healthratio >= .33) { bars0.color = new Color(1, 0.92f, 0.016f, 1); }
            else { bars0.color = new Color(1, 0, 0, 1); }
            //bars0[0].color = Color.Lerp(playerHP.noHealth, playerHP.fullHealth, playerHP.currentHealth / playerHP.startingHealth);
            bars0.transform.localScale = new Vector3((float)7 * healthratio, bars0.transform.localScale.y, bars0.transform.localScale.z);
            //bars0[0].transform.position = new Vector3(0, bars0[0].transform.position.y, bars0[0].transform.position.z);

            //Movement Cooldown UI
            float playerMoveCooldown = unit.GetComponent<PlayerUnit>().timeStampMove - Time.time;
            float maxMoveCooldown = unit.GetComponent<PlayerUnit>().cooldownMoveSeconds;
            if (playerMoveCooldown >= 0)
            {
                bars1.transform.localScale = new Vector3((float)7 * (1 - (playerMoveCooldown / maxMoveCooldown)), bars1.transform.localScale.y, bars1.transform.localScale.z);
            }

            //Attack Cooldown UI
            float playerAttackCooldown = unit.GetComponent<PlayerUnit>().timeStampAttack - Time.time;
            float maxAttackCooldown = unit.GetComponent<PlayerUnit>().cooldownAttackSeconds;
            if (playerAttackCooldown >= 0)
            {
                bars2.transform.localScale = new Vector3((float)7 * (1 - (playerAttackCooldown / maxAttackCooldown)), bars2.transform.localScale.y, bars2.transform.localScale.z);
            }


        }
    }


}
