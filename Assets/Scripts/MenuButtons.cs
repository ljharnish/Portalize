using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
	public GameObject MainUI;
	public GameObject LoadGameMenu;
	public GameObject LevelSelectMenu;
	public GameObject OptionsMenu;


	public void LoadGame(bool e)
	{
		if (e)
		{
			MainUI.SetActive(false);
			LoadGameMenu.SetActive(true);
			LevelSelectMenu.SetActive(false);
			OptionsMenu.SetActive(false);
		}
		else
		{
			MainUI.SetActive(true);
			LoadGameMenu.SetActive(false);
			LevelSelectMenu.SetActive(false);
			OptionsMenu.SetActive(false);
		}
	}
	public void LevelSelect(bool e)
	{
		if(e)
		{
			MainUI.SetActive(false);
			LoadGameMenu.SetActive(false);
			LevelSelectMenu.SetActive(true);
			OptionsMenu.SetActive(false);
		} else
		{
			MainUI.SetActive(true);
			LoadGameMenu.SetActive(false);
			LevelSelectMenu.SetActive(false);
			OptionsMenu.SetActive(false);
		}
	}
	public void Options(bool e)
	{
		if (e)
		{
			MainUI.SetActive(false);
			LoadGameMenu.SetActive(false);
			LevelSelectMenu.SetActive(false);
			OptionsMenu.SetActive(true);
		}
		else
		{
			MainUI.SetActive(true);
			LoadGameMenu.SetActive(false);
			LevelSelectMenu.SetActive(false);
			OptionsMenu.SetActive(false);
		}
	}
	public void Quit()
	{
		Application.Quit();
	}
}
