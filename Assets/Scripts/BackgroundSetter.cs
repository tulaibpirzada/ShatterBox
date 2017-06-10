using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSetter : MonoBehaviour
{

	private SpriteRenderer background;
	private string newBackgroundName;
	private string backgroundName;

	void Start ()
	{
		string nameOfTheme = GameModel.Instance.SelectedThemeChoice;
		backgroundName = ShutterGameConstants.BACKGROUND_IMAGES [nameOfTheme];
		background = gameObject.GetComponent<SpriteRenderer> ();
		background.sprite = Resources.Load (backgroundName, typeof(Sprite)) as Sprite;
	}

	void Update ()
	{
		if (GameController.Instance.IsThemeChanged) {
			GameController.Instance.IsThemeChanged = false;
			string nameOfTheme = GameModel.Instance.SelectedThemeChoice;
			newBackgroundName = ShutterGameConstants.BACKGROUND_IMAGES [nameOfTheme];
			background.sprite = Resources.Load (newBackgroundName, typeof(Sprite)) as Sprite;
		}
	}
}
