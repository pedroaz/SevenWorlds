using FMOD.Studio;
using FMODUnity;
using SevenWorlds.Shared.UnityLog;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SongId
{
    Field_1,
    Opening
}

public enum SfxId
{
    BUTTON_CLICK_1
}

public class SoundService : GameService<SoundService>
{
    public bool SoundEnable;

    [Header("Songs")]
    [EventRef] public string SONG_FIELD_1 = "";
    [EventRef] public string SONG_OPENING = "";

    [Header("Sfx")]
    [EventRef] public string SFX_BUTTON_CLICK_1 = "";

    private Dictionary<SongId, EventInstance> songInstances = new Dictionary<SongId, EventInstance>();

    private void Awake()
    {
        Object = this;
        CreateEventInstances();
    }

    private void CreateEventInstances()
    {
        if (!SoundEnable) return;
        songInstances.Add(SongId.Field_1, RuntimeManager.CreateInstance(SONG_FIELD_1));
        songInstances.Add(SongId.Opening, RuntimeManager.CreateInstance(SONG_OPENING));
    }

    public void PlaySong(SongId id)
    {
        LOG.Log($"Starting to play song: {id}");
        if (!SoundEnable) return;
        StopAllSongs();
        songInstances[id].start();
    }

    public void PlaySound(SfxId id)
    {
        switch (id) {
            case SfxId.BUTTON_CLICK_1:
                RuntimeManager.PlayOneShot(SFX_BUTTON_CLICK_1);
                break;
        }
    }

    private void StopAllSongs()
    {
        if (!SoundEnable) return;
        foreach (var item in songInstances) {
            item.Value.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
