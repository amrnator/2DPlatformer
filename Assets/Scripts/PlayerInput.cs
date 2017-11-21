using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{

    Player player;
	Blink blink;
    StaminaSystem staminaSystem;
    Camera camera;

    void Start()
    {
        player = GetComponent<Player>();
		blink = GetComponent<Blink> ();
        staminaSystem = GetComponent<StaminaSystem>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        player.SetDirectionalInput(directionalInput * 2);

		Vector2 BlinkInput = new Vector2(Input.GetAxisRaw("BlinkHorizontal"), Input.GetAxisRaw("BlinkVertical"));

		//jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.OnJumpInputDown();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            player.OnJumpInputUp();
        }

        //exit game, pause menu later
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
