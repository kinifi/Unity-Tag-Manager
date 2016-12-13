using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tag))]
[CanEditMultipleObjects]
public class TagManager : Editor
{

    private TagDatabase Tags;
    private Tag m_TagObject;

    public override void OnInspectorGUI()
    {

        m_TagObject = (Tag)target;

        if (Tags == null)
        {
            GUILayout.Label("Assign A Tag Manager Asset", EditorStyles.boldLabel);
        }
        Tags = (TagDatabase)EditorGUILayout.ObjectField(Tags, typeof(TagDatabase), true);
        GUILayout.Space(5);

        if (Tags != null)
        {
            //make sure we dont have a tag named "none" because our system uses that
            databaseTagAdder();
            Tags.ValidateTags();
        }

        displayObjectTags();

        //DrawDefaultInspector();
    }

    private void displayObjectTags()
    {
        if (m_TagObject.m_Tags.Count != 0)
        {
            GUILayout.Label("Tags On GameObject: ");
            for (int i = 0; i < m_TagObject.m_Tags.Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(m_TagObject.m_Tags[i]);
                if (GUILayout.Button("-", GUILayout.Width(50)))
                {
                    m_TagObject.m_Tags.RemoveAt(i);
                }
                GUILayout.EndHorizontal();
            }
        }
        else
        {
            GUILayout.Label("No Tags assigned to GameObject", EditorStyles.boldLabel);
        }
        GUILayout.Space(10);
    }

    private void databaseTagAdder()
    {
        //display all the tags
        GUILayout.Label("Tags From Database: ", EditorStyles.boldLabel);
        for (int i = 0; i < Tags.m_Tags.Count; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(Tags.m_Tags[i]);
            if (GUILayout.Button("+", GUILayout.Width(50)))
            {
                if (Tags.hasTag(Tags.m_Tags[i]) == false)
                {
                    m_TagObject.m_Tags.Add(Tags.m_Tags[i]);
                }
                else
                {
                    Debug.Log("TagManager: GameObject already has the tag you are attempting to assign: " + Tags.m_Tags[i]);
                }
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.Space(10);
    }

}
