using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterGameConstants {

	public static string THEME_1 = "theme1";
	public static string THEME_2 = "theme2";
	public static string THEME_3 = "theme3";
	public static string THEME_4 = "theme4";

	public static string BACKGROUND_THEME_1 = "simpleBackground";
	public static string BACKGROUND_THEME_2 = "BG-1";
	public static string BACKGROUND_THEME_3 = "BG-2";
	public static string BACKGROUND_THEME_4 = "maroonBackground";

	public static readonly List<string> THEMES= new List<string> { THEME_1, THEME_2, THEME_3, THEME_4};

	public static readonly Dictionary<string, string> BACKGROUND_IMAGES  = new Dictionary<string, string> {
		{THEME_1, BACKGROUND_THEME_1}, 
		{THEME_2, BACKGROUND_THEME_2},
		{THEME_3, BACKGROUND_THEME_3},
		{THEME_4, BACKGROUND_THEME_4}

	};
}
