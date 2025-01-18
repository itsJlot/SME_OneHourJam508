using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using Player;

public class PauseMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        pauseInput = input.actions["Pause"];
		PauseMenuManager.instance = this;
	}

    // Update is called once per frame
    void Update()
    {
        if (pauseInput.triggered) PausePress();
		sensM = m_scrollbar.value;
		sensM = Mathf.Clamp(sensM, 0.1f, 1f);
		m_sensVis.text = m_scrollbar.value.ToString("0.00");
    }
	
	void RenderUI(bool yeah)
	{
		ui_rend.SetActive(yeah);
	}
	
	void PausePress()
	{
		isInPause = !isInPause;
		RenderUI(isInPause);
		PlayerController.instance.CursorMode(!isInPause);
		Time.timeScale = (isInPause ? 0f : 1f);
	}
	
	public void Quit()
	{
		Application.Quit();
	}
	
	//-----------------------------------------------------------------
	
	public PlayerInput input;
	InputAction pauseInput;
	
	//-----------------------------------------------------------------
	
	public bool isInPause;
	
	//-----------------------------------------------------------------
	
	public GameObject ui_rend;
	public Scrollbar m_scrollbar;
	public TextMeshProUGUI m_sensVis;
	public float sensM; 
	//public for debug purposes and controller ref
	
	//-----------------------------------------------------------------
	
	public static PauseMenuManager instance;
}
