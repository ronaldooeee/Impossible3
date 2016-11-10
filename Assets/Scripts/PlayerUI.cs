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
            try{
                HealthSystem playerHP = Units[i].GetComponents<HealthSystem>()[i];
                float healthratio = (float)playerHP.currentHealth / (float)playerHP.startingHealth;
                bars0[0].color = Color.Lerp(playerHP.noHealth, playerHP.fullHealth, playerHP.currentHealth / playerHP.startingHealth);
                bars0[0].transform.localScale = new Vector3((float)7.5 * healthratio, bars0[0].transform.localScale.y, bars0[0].transform.localScale.z);
                //bars0[0].transform.position = new Vector3(0, bars0[0].transform.position.y, bars0[0].transform.position.z);
            }catch
            {
                //Temp workaround for when players are deleted so unity doesnt throw 1 million errors
            }
        }
	}
}
