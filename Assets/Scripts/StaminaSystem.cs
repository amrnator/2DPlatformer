using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour {

    [Header("Stamina System")]

    [SerializeField]
    private int maxStaminaCount = 3;
	public float staminaRefreshDelay = 1f;
	public GameObject StaminaUI;
	public GameObject staminaBarPrefab;
	public Color fullStaminaBarColor;
	public Color emptyStaminaBarColor;

	public bool staminaFull = true;

    public int currentStamina;
	private Player player;
	private Image[] staminaBars;


    public int Stamina {
        get { return currentStamina; }
        set {
            currentStamina = value;
            UpdateStaminaUI();

			if (currentStamina < maxStaminaCount) {
				staminaFull = false;
			}
        }
    }

    void Start() {
		setStaminaBars ();

		player = GetComponent<Player> ();
		staminaBars = StaminaUI.GetComponentsInChildren<Image> ();

		RefreshStamina();

    }

    public void RefreshStamina() {
		//print ("Stamina Refreshed");
        Stamina = maxStaminaCount;
		staminaFull = true;
    }

	//instantiate staminabars as children to stamina UI
	void setStaminaBars(){

		for (int i = 0; i < maxStaminaCount; i++) {
			Instantiate (staminaBarPrefab, StaminaUI.transform);
		}
	}

    /// <summary>
    /// Update the UI associated with Stamina
    /// </summary>
    private void UpdateStaminaUI() {
		//print ("Stamina: " + currentStamina);

		//cylce thru each stamina bar
		int x = currentStamina;
		for (int i = 1; i < maxStaminaCount + 1; i++){
			if (x > 0) {
				staminaBars [i].color = fullStaminaBarColor;
				x--;
			} else {
				staminaBars [i].color = emptyStaminaBarColor;

			}
		}

    }

}
