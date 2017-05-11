using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSetter : MonoBehaviour { 

	private SpriteRenderer background;
	private string newBackgroundName;
	private string nameOfTheme;
	void Start()
	{
//		if (PlayerPrefs.HasKey ("ThemeChoice")) {
//			GameModel.Instance.SelectedThemeChoice = PlayerPrefs.GetString ("ThemeChoice");
//		} else {
			background = gameObject.GetComponent<SpriteRenderer> ();
			background.sprite = Resources.Load ("blueBackground", typeof(Sprite)) as Sprite;
//		}
	}

	void Update()
	{
		if (GameController.Instance.IsThemeChanged) {
			GameController.Instance.IsThemeChanged = false;
			nameOfTheme = PlayerPrefs.GetString ("ThemeChoice");
			newBackgroundName = ShutterGameConstants.BACKGROUND_IMAGES [nameOfTheme];
			background.sprite = Resources.Load (newBackgroundName, typeof(Sprite)) as Sprite;
		}
	}
}
