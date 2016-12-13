using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TagDatabase", menuName = "TagDatabase")]
public class TagDatabase : ScriptableObject {

    public List<string> m_Tags = new List<string>();

    public void Awake()
    {
        ValidateTags();
    }

    //Our system uses the tag called "none" so check to see if a user has entered that name
    public void ValidateTags()
    {
        for (int i = 0; i < m_Tags.Count; i++)
        {
            if (m_Tags[i].ToLower() == "none")
            {
                Debug.LogError("A tag called none is in m_Tags. Remove it or hasTag() will not work properly.");
            }
        }
    }

    /// <summary>
    /// searches the tags database object for a specific tag
    /// </summary>
    /// <param name="tagtofind"></param>
    /// <returns>Returns 'none' if there isn't a tag that you are looking for</returns>
    public bool hasTag(string tagtofind)
    {
        for (int i = 0; i < m_Tags.Count; i++)
        {
            if (m_Tags[i].ToLower() == tagtofind.ToLower())
            {
                return true;
            }

        }

        return false;
    }
}
