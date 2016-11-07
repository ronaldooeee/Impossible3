using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerUI : MonoBehaviour {
    public List<GameObject> unitPrefabs;
    private List<GameObject> UIs = new List<GameObject>();
    private List<GameObject> Units = BoardManager.playerUnits;

    // Use this for initialization
    void Start () {
        GameObject player1UI = Instantiate(unitPrefabs[0], new Vector3(5.65f,11.9f,-5.85f), Camera.main.transform.rotation) as GameObject;
        //player1UI.transform.localScale = new Vector3(.005f,.005f,.005f);
        UIs.Add(player1UI);
    }
	
	// Update is called once per frame
	void Update () {
	    for(int i = 0; i<UIs.Count; i++)
        {
            Image[] bars = UIs[i].GetComponents<Image>();
            //HealthSystem playerHP = Units[i].GetComponents<HealthSystem>()[i];
            //foreach (GameObject hps in Units) { Debug.Log(hps); }
            //bars[0].color = Color.Lerp(playerHP.noHealth, playerHP.fullHealth, playerHP.currentHealth / playerHP.startingHealth);
        }
	}
}
