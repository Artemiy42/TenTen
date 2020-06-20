using System.Collections.Generic;
using UnityEngine;

class SaveLoad
{
    private List<ISaveable> _saveables;
    private static SaveLoad _instance;

    private string _saveKey = "Game save";
    private int _saveValue = 1;

    private SaveLoad() 
    {
        _saveables = new List<ISaveable>();
    }

    public static SaveLoad Instance()
    {
        if (_instance == null)
            _instance = new SaveLoad();

        return _instance;
    }

    public void AddToList(ISaveable saveable)
    {
        Debug.Log("SaveLoad Add new object to list");
        _saveables.Add(saveable);
    }

    public void Save()
    {
        PlayerPrefs.SetInt(_saveKey, _saveValue);
        foreach (ISaveable saveable in _saveables)
        {
            saveable.Save();
        }
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(_saveKey))
        {
            foreach (ISaveable saveable in _saveables)
            {
                saveable.Load();
            }
        }
    }

    public void DeleteSave()
    {
        Debug.Log("SaveLoad delete save");
        PlayerPrefs.DeleteKey(_saveKey);
    }

    public void Clear()
    {
        Debug.Log("SaveLoad clear list");
        _saveables.Clear();
    }
}
