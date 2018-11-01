﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;
using FMOD.Studio;

public class SoundManager : MonoBehaviour
{
    [EventRef]
    public string bagSpawnEvent;
    [EventRef]
    public string balloonMoveEvent;
	[EventRef]
	public string balloonHitEvent;
	[EventRef]
	public string musicEvent;

    public static SoundManager instance;
    private EventInstance bagSpawnInst;
    private EventInstance balloonMoveInst;
	private EventInstance musicInst;

    private void Start()
    {
        //Handle singleton
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);

        DontDestroyOnLoad(this);

		musicInst = RuntimeManager.CreateInstance(musicEvent);
		musicInst.start();
    }

    /// <summary>
    /// Launch when game starts
    /// </summary>
    public void GameStart()
    {
        balloonMoveInst = RuntimeManager.CreateInstance(balloonMoveEvent);
        balloonMoveInst.setParameterValue("speed", 0.0f);
        balloonMoveInst.start();
		musicInst.setParameterValue("progress", 0.6f);
    }

    /// <summary>
    /// Launch when game ends
    /// </summary>
    public void GameOver()
    {
		balloonMoveInst.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		musicInst.setParameterValue("progress", 1.0f);
    }

	/// <summary>
	/// Launch when coming back to menu
	/// </summary>
	public void ReturnToMenu()
	{
		musicInst.setParameterValue("progress", 0.0f);
	}

    /// <summary>
    /// Making the pif paf pouf right baby
    /// </summary>
    /// <param name="weight">Goes from 0 (minimum weight) to one (maximum weight)</param>
    public void BagSpawn(float weight)
    {
        bagSpawnInst = RuntimeManager.CreateInstance(bagSpawnEvent);
        bagSpawnInst.setParameterValue("weight", weight);
        bagSpawnInst.start();
    }

    /// <summary>
    /// Making a swoosh sound for the baloon
    /// </summary>
    /// <param name="speed">from 0 (minimum speed) to 1 (maximum speed)</param>
    public void BalloonSpeed(float speed)
    {
        balloonMoveInst.setParameterValue("speed", speed);
    }

	/// <summary>
	/// Play when balloon hitted
	/// </summary>
	public void BalloonHit()
	{
		SimplePlay(balloonHitEvent);
	}


	private void SimplePlay(string str)
	{
		EventInstance inst =RuntimeManager.CreateInstance(str);
		inst.start();
	}


}
