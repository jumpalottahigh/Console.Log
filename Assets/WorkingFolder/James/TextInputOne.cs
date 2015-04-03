using UnityEngine;
using System.Collections;

public class TextInputOne : MonoBehaviour {
		private string text = "using UnityEngine;\nusing System.Collections;\n\npublic class TextInputOne : MonoBehaviour {\n\tpublic string name = \"\";\n}";
		public Rect windowRect = new Rect ();
		public Rect windowRect2 = new Rect();
		//public Rect window = new Rect (10, 10, Screen.width, Screen.height - 30);

		void OnGUI(){
			Rect textWindow = new Rect (200, 10, Screen.width - 210, Screen.height - 20);
			Rect browseWindow = new Rect (10, 10, 180, Screen.height - 20);
			windowRect = GUI.Window(0, textWindow, windowFunc, "Console.Log");
			windowRect2 = GUI.Window(1, browseWindow, tmpFunc, "Browser bar");
			//GUI.Box(new Rect(10,10, 180, Screen.height - 20), "Browser bar");
		}

			private void windowFunc(int id){
				text = GUILayout.TextArea(text, GUILayout.Width(windowRect.width - 20), GUILayout.Height(windowRect.height - 30));
		}
			private void tmpFunc(int id){

			}
}
