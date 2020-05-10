using UnityEngine;
using System.Collections;
using UnityEditor;

public class MapMenu : EditorWindow {

	[MenuItem ("MapCreater/Create Background", false, 0)]
	static void ModelCreater() {
		EditorWindow.GetWindow(typeof(MapEditor)); 
	}
}
