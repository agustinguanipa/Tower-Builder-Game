using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class MapEditor : EditorWindow {

	Color currentColor = Color.red;
	string fileName = "save";
	GameObject house;

	public void OnEnable(){
		house = (GameObject)Resources.Load("house", typeof(GameObject));
		SceneView.onSceneGUIDelegate += OnSceneGui;
		
		//Debug.Log ("OnEnable");
	}

	public void OnDisable()
	{
		//SceneView.onSceneGUIDelegate -= OnSceneGui;
		//Debug.Log ("OnDisable");
	}

	public void OnGUI(){
		GUILayout.BeginVertical(); GUILayout.Label(" AutoModel Ver.1.0 ");
		GUILayout.BeginHorizontal(); 
		GUILayout.Label("Create Map "); 
		if (GUILayout.Button ("Load", GUILayout.Width (50))) {
			//read
			if(File.Exists(fileName)){
				var src = File.OpenText(fileName);
				string line = src.ReadLine();
				while(line != null){
					line = src.ReadLine();
					char[] splitchar = { ',' };
					string[] data = line.Split(splitchar);

					GameObject container = GameObject.Find ("container");
					GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(house);
					Vector3 position = new Vector3(float.Parse(data[0]), float.Parse(data[1]), 0); 
					obj.transform.position = position;
					obj.transform.parent = container.transform;
					
					Color color = HexToColor(data[2]);
					MyBall myball = obj.gameObject.GetComponent<MyBall>();
					Renderer rander = myball.GetComponent<Renderer>();
					rander.material.color = color;
				}  
			} else {
				//Debug.Log("Could not Open the file: " + fileName + " for reading.");
				return;
			}
		}
		if (GUILayout.Button ("Create", GUILayout.Width (50))) {
			//Debug.Log("Button Click");
			for (int i=0; i<10; i++)
			{
				for (int j=0; j<10; j++)
				{
					//GameObject house = (GameObject)Resources.Load("house", typeof(GameObject)); 
					GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(house);
					if (obj)
					{
						GameObject container = GameObject.Find ("container");
						obj.transform.position = new Vector3(0.156f*i, 0.7f - j*0.156f, 0f);
						obj.transform.parent = container.transform;
					}
				}
			}
		}
		if (GUILayout.Button ("Clear", GUILayout.Width (50))) {
			GameObject container = GameObject.Find ("container");
			int childs = container.transform.childCount;
			for (int i = childs - 1; i >= 0; i--)
			{
				GameObject.Destroy(container.transform.GetChild(i).gameObject);
			}

			StreamWriter sr = File.CreateText(fileName);
			sr.WriteLine("");
			sr.Close();
		}
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();


		//choose color
		GUILayout.BeginVertical();
		GUILayout.BeginHorizontal(); 
		GUILayout.Label("Choose Color."); 
		if (GUILayout.Button ("Red", GUILayout.Width (50))) {
			currentColor = Color.red;
		}
		if (GUILayout.Button ("Blue", GUILayout.Width (50))) {
			currentColor = Color.blue;
		}
		if (GUILayout.Button ("Green", GUILayout.Width (50))) {
			currentColor = Color.green;
		}
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();

		Handles.BeginGUI();
		GUI.Button(new Rect(Screen.width-110,Screen.height-60,100,20), "Create Bomb"); 
		Handles.EndGUI();
	}

	public void OnSceneGui(SceneView sceneView)
	{
		Event e = Event.current;
		if (!e.alt) 
		{
			if (e.type == EventType.MouseDown && e.button == 1)
			{
				e.Use();

				Ray mouseRay = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, 
				                                                           Camera.current.pixelHeight - e.mousePosition.y, 
				                                                           0.0f));
				//Debug.Log("ray y:" + mouseRay.direction.y);
				if (mouseRay.direction.z >= 0.0f)
				{
					float t = -mouseRay.origin.z / mouseRay.direction.z;
					Vector3 mouseWorldPos = mouseRay.origin + t * mouseRay.direction; 
					mouseWorldPos.y = mouseRay.origin.y;

					GameObject container = GameObject.Find ("container");
					GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(house);
					obj.transform.position = mouseWorldPos;
					obj.transform.parent = container.transform;

					MyBall myball = obj.gameObject.GetComponent<MyBall>();
					myball.ballcolor = currentColor;
					Renderer rander = myball.GetComponent<Renderer>();
					rander.material.color = currentColor;
				}
			}
		}

		if (e.type == EventType.MouseUp) 
		{
			if (File.Exists(fileName))
			{
				Debug.Log(fileName + " already exists.");
			}

			StreamWriter sr = File.CreateText(fileName);
			GameObject container = GameObject.Find ("container");
			foreach (Transform ball in container.transform)
			{
				MyBall myball = ball.gameObject.GetComponent<MyBall>();
				Renderer rander = myball.GetComponent<Renderer>();
				Color color = rander.material.color;
				//Debug.Log("ball x:" + ball.transform.position.x + " ,y" + ball.transform.position.y + " ,color:" + ColorToHex(color));
				sr.WriteLine ("{0},{1},{2}", ball.transform.position.x, ball.transform.position.y, ColorToHex(color));
			}
			sr.Close();
		}
	}

	string ColorToHex(Color32 color)
	{
		string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
		return hex;
	}
	
	Color HexToColor(string hex)
	{
		byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r,g,b, 255);
	}
}
