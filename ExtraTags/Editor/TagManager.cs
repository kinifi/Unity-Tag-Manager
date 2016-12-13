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
        //add a object field so we can get the database
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
            GUILayout.Label("Tags On GameObject: ", EditorStyles.miniBoldLabel);
            for (int i = 0; i < m_TagObject.m_Tags.Count; i++)
            {
                GUILayout.BeginHorizontal(EditorStyles.helpBox);
                GUILayout.Label(m_TagObject.m_Tags[i], EditorStyles.miniLabel);
                if (GUILayout.Button("-", EditorStyles.miniButton, GUILayout.Width(30)))
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
        GUILayout.Label("Tags From Database: ", EditorStyles.miniBoldLabel);
        for (int i = 0; i < Tags.m_Tags.Count; i++)
        {
            GUILayout.BeginHorizontal(EditorStyles.helpBox);
            GUILayout.Label("- " + Tags.m_Tags[i], EditorStyles.miniLabel);
            if (GUILayout.Button("+", EditorStyles.miniButton, GUILayout.Width(30)))
            {
                m_TagObject.m_Tags.Add(Tags.m_Tags[i]);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.Space(10);
    }

}
