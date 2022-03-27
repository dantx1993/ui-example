using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static object _lock = new object();
    private static bool IsAvailable => _instance != null;
    private static DataManager _instance;
    public static DataManager Instance
    {
        get
        {
            lock(_lock)
            {
                if(!IsAvailable)
                {
                    _instance = FindObjectOfType<DataManager>();
                    if(IsAvailable)
                    {
                        return _instance;
                    }
                    GameObject go = new GameObject();
                    go.name = $"[{nameof(DataManager)}]";
                    _instance = go.AddComponent<DataManager>();
                }
                return _instance;
            }
        }
    }

    [SerializeField] private List<Sprite> _avatars;

    private List<GuildData> _guildDatas = new List<GuildData>();

    public List<GuildData> GuildDatas => _guildDatas;
    public List<Sprite> Avatars => _avatars;
}
