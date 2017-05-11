using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterGameConstants {

	public static string THEME_1 = "theme1";
	public static string THEME_2 = "theme2";
	public static string THEME_3 = "theme3";
	public static string THEME_4 = "theme4";

	public static string BACKGROUND_1 = "blueBackground";
	public static string BACKGROUND_2 = "greenBackground";
	public static string BACKGROUND_3 = "purpleBackground";
	public static string BACKGROUND_4 = "maroonBackground";

	public static readonly List<string> THEMES= new List<string> { THEME_1, THEME_2, THEME_3, THEME_4};

	public static readonly Dictionary<string, string> BACKGROUND_IMAGES  = new Dictionary<string, string> {
		{THEME_1, BACKGROUND_1}, 
		{THEME_2, BACKGROUND_2},
		{THEME_3, BACKGROUND_3},
		{THEME_4, BACKGROUND_4}

	};
}
