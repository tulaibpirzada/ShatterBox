using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeSelectionScreenController : Singleton<ThemeSelectionScreenController> {

	ThemeSelectionScreenReferences themeSelectionScreenRef;
	private List<GameObject> themeItemList;

	public void ShowThemeSelectionScreen(ThemeSelectionScreenReferences themeSelectionScreenReference)
	{
		themeSelectionScreenRef = themeSelectionScreenReference;
		themeSelectionScreenRef.gameObject.SetActive (true);
		PopulateScrollView ();
	}

	public void HideThemeSelectionScreen()
	{
		themeSelectionScreenRef.gameObject.SetActive (false);
		PlayerPrefs.Save ();
	}

	private void PopulateScrollView()
	{
		themeItemList = new List<GameObject> ();
		for (int index = 0; index < ShutterGameConstants.THEMES.Count; index++) {
			GameObject listItemGameObject = Instantiate(themeSelectionScreenRef.themePackListItem) as GameObject;
			listItemGameObject.transform.SetParent(themeSelectionScreenRef.scrollView.content, true);
			listItemGameObject.transform.localPosition = Vector3.zero;
			listItemGameObject.transform.localScale = Vector3.one;
			themeItemList.Add (listItemGameObject);
			ThemePackItemsReferences themePackItemsRef = listItemGameObject.GetComponent<ThemePackItemsReferences> ();
			themePackItemsRef.themeBackground.sprite = Resources.Load<Sprite> (ShutterGameConstants.BACKGROUNDS [index]);
			themePackItemsRef.theme = ShutterGameConstants.THEMES[index];
		}
	}

	public void LoadTheme (string selectedTheme)
	{
		GameModel.Instance.SelectedThemeChoice = selectedTheme;
		PlayerPrefs.SetString ("ThemeChoice", GameModel.Instance.SelectedThemeChoice);;
	}
}
