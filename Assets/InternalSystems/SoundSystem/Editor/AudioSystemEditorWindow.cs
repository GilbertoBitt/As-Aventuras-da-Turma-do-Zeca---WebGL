using System.Linq;
using InternalSystems.SoundSystem.Scripts;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace InternalSystems.SoundSystem{
	public class AudioSystemEditorWindow : OdinMenuEditorWindow {
		
		[MenuItem("Tools/Customized/Sound System")]
		private static void OpenWindow()
		{
			var window = GetWindow<AudioSystemEditorWindow>();
			window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
			window.titleContent = new GUIContent("Audio System Editor");
		}
		
		
		protected override OdinMenuTree BuildMenuTree(){
			OdinMenuTree tree = new OdinMenuTree(supportsMultiSelect: true)
			{
				{ "Player Settings",                Resources.FindObjectsOfTypeAll<AudioSystem>().FirstOrDefault()       }
			};
			return tree;
		}
	}
}
