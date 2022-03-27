using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GuildData
{
    public int id;
    public string name;
    public string description;
    public string rule;
    public int avatarIndex;

    public GuildData() {}
    public GuildData(int id, string name, string description, string rule, int avatarIndex)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.rule = rule;
        this.avatarIndex = avatarIndex;
    }
}
