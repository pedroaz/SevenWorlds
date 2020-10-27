using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PersistenceConfig
{
    SoundEnabled
}

public class PersistenceService : GameService<PersistenceService>
{
    private void Awake()
    {
        Object = this;
    }

    public void SetConfig(PersistenceConfig config, string value)
    {
        PlayerPrefs.SetString(config.ToString(), value);
    }

    public string GetConfig(PersistenceConfig config, string value)
    {
        return PlayerPrefs.GetString(config.ToString());
    }
}
