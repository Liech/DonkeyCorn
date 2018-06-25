using UnityEditor;
using UnityEngine;

//http://anchan828.hatenablog.jp/entry/2013/11/15/143139
public class SetObjectsToPrefab : EditorWindow
{

  [MenuItem("Tools/Set Objects To Prefab")]
  static void Do()
  { 
    GetWindow<SetObjectsToPrefab>();
  }

  Object selectedPrefab = null;

  void OnGUI()
  {
    if (GUILayout.Button("ShowObjectPicker"))
    {
      int controlID = EditorGUIUtility.GetControlID(FocusType.Passive);
      EditorGUIUtility.ShowObjectPicker<GameObject>(null, false, "", controlID);
    }

    if (GUILayout.Button("ReplaceWithSelectedPrefab"))
    {
      GameObject[] selection = Selection.gameObjects;
      
      if (selection.Length > 0 && selectedPrefab != null)
      {
        for (int i = 0; i < selection.Length; i++)
        {
          GameObject obj = selection[i];
          int parentIndex = obj.transform.GetSiblingIndex();
          Vector3 position = obj.transform.localPosition;
          Quaternion rotation = obj.transform.localRotation;
          Vector3 scale = obj.transform.localScale;
          Transform parent = obj.transform.parent;
          string name = obj.name;
          DestroyImmediate(obj);
          GameObject New = PrefabUtility.InstantiatePrefab(selectedPrefab) as GameObject;
          New.name = name;
          if (parent != null)
          {
            New.transform.parent = parent;
            New.transform.SetSiblingIndex(parentIndex);
          }
          New.transform.localPosition = position;
          New.transform.localRotation = rotation;
          New.transform.localScale = scale;
        }
      }
    }

    string commandName = Event.current.commandName;
    if (commandName == "ObjectSelectorUpdated")
    {
      selectedPrefab = EditorGUIUtility.GetObjectPickerObject();



      Repaint();
    }
    else if (commandName == "ObjectSelectorClosed")
    {
      selectedPrefab = EditorGUIUtility.GetObjectPickerObject();
    }
    
    EditorGUILayout.ObjectField(selectedPrefab, typeof(GameObject), true);
  }
}