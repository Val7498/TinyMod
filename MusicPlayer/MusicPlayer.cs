using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using FMODUnity;

namespace Vander
{
    public class MusicPlayer : MonoBehaviour
    {
        FMOD.RESULT result;
        FMOD.Sound soundObject;
        FMOD.System system;
        FMOD.Channel channel;
        FMOD.ChannelGroup channelGroup;
        public string[] songlist;
        public int currentSong = 0;
        public bool playing;
        float volume = 0.5f;
        TinyMod.ModLoader tm;

        void Awake()
        {
            tm = TinyMod.ModLoader.Instance;
            tm.logDebug("Loading songs");
            tm.logDebug("Loaded songs");

        }
        void Start()
        {
            tm.logDebug("seting up FMOD system");
            system = RuntimeManager.CoreSystem;
            result = system.createChannelGroup(null, out channelGroup);
            channel.setChannelGroup(channelGroup);
            tm.logDebug("finished FMOD setup");
            GetSongs();

        }
        void Update()
        {
            channel.isPlaying(out playing);
            if (playing)
            {
                if (Input.GetKey(KeyCode.F9))
                {
                    volume -= Time.deltaTime;
                    volume = Mathf.Clamp01(volume);
                    channel.setVolume(volume);
                }
                if (Input.GetKey(KeyCode.F10))
                {
                    volume += Time.deltaTime;
                    volume = Mathf.Clamp01(volume);
                    channel.setVolume(volume);
                }
            }
            if (Input.GetKeyUp(KeyCode.F7)) PlayPause();
            if (Input.GetKeyUp(KeyCode.F6)) loadPreviousTrack();
            if (Input.GetKeyUp(KeyCode.F8)) loadNextTrack();
        }

        void GetSongs()
        {
            try
            {
                string path = Path.Combine(Application.dataPath,"..", "mods", "MusicPlayer", "songs");
                //songlist = Directory.GetFiles(path, "*.ogg");
                songlist = Directory.EnumerateFiles(path)
                    .Where(song => song.EndsWith(".mp3") || song.EndsWith(".wav") || song.EndsWith(".ogg"))
                    .ToArray();
                foreach (string songs in songlist)
                {
                    tm.gAPI.Log("Found " + songs);
                }
            }

            catch (Exception e)
            {
                tm.gAPI.LogError(e.Message);
                tm.gAPI.LogInfo("Aborting...");
            }
        }
        void loadPreviousTrack()
        {
            if (songlist.Length == 0) return;
            if (currentSong - 1 >= 0)
            {
                currentSong--;
            }
            else
            {
                currentSong = songlist.Length - 1;
            }
            loadSong(currentSong);
        }
        void loadNextTrack()
        {
            if (songlist.Length == 0) return;
            if (currentSong + 1 < songlist.Length)
            {
                currentSong++;
            }
            else
            {
                currentSong = 0;
            }
            loadSong(currentSong);
        }
        void loadSong(int index)
        {
            result = soundObject.release();
            tm.Log(string.Format("Playing {0}!", Path.GetFileName(songlist[index])));
            result = system.createSound(songlist[index], FMOD.MODE.DEFAULT, out soundObject);
            result = system.playSound(soundObject, channelGroup, false, out channel);
            channel.setVolume(volume);
        }
        void PlayPause()
        {
            bool state;
            channel.getPaused(out state);
            channel.setPaused(!state);
            tm.Log(state ? "Playing.." : "Pausing..");
        }
    }
}
