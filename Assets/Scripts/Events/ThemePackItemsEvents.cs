using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemePackItemsEvents : MonoBehaviour {

	public void SelectThemeButtonTapped()
	{
		ThemePackItemsReferences themePackItemRef = gameObject.GetComponent<ThemePackItemsReferences> ();
		GameController.Instance.SetTheme (themePackItemRef.theme);
	}
}
