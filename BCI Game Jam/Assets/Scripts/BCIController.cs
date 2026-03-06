using System;
using System.Collections.Generic;
using Cognixion;
using Cognixion.Services;
using Cognixion.Services.AndroidMessaging.DataFormats;
using Cognixion.Services.BCI;
using UnityEngine;

[RequireComponent(typeof(DelayAction))]
public class BCIController : MonoBehaviour
{
    // time to wait before starting classification for the first time
    private const float BCI_START_DELAY = 2f;

    // time to wait before restarting classification after a selection
    private const float BCI_RESTART_DELAY = 0.5f;

    // reference to script that allows delaying action by amount of time
    // must be attached to the same game object as this script
    private DelayAction _delayAction;

    // tool to handle messaging with Android services during classification
    private IBCIClassificationTool _bciTool;

    // reference to service locator singleton that provides services for Cognixion apps
    private ISingletons _singletons;

    [SerializeField] private shoot_and_turn _shootAndTurn;

    // list of stimuli to be flashed and classified
    // max length: 13
    [SerializeField] private List<BCIStimulus_MonoBehaviour> _stimuliList;

    // list of frequencies 
    // must have length >= _stimuliList length
    [SerializeField] private List<float> _frequencies;

    private bool shouldFlash = true;


    #region Unity Lifecycle 

    private void Awake()
    {
        _delayAction = GetComponent<DelayAction>();

        _singletons = SingletonManager.Instance;
        _bciTool = BCIToolFactory.BuildClassificationTool(_singletons, _delayAction);
        // channel mask and sample rate set by default in BCISettings
        // change them here if we want to 
        _bciTool.ChannelMask = BCISettings.ChannelMask;
        _bciTool.SampleRate = BCISettings.SamplingRateHz;
    }

    private void OnEnable()
    {
        // subscribe to events with our own handlers
        _bciTool.OnStimuliFlashingChanged += HandleStimuliFlashingChanged;
        _bciTool.OnStimuliIdSelected += HandleStimuliIDSelected;
    }

    private void OnDisable()
    {
        if (this == null) return;

        // un-subscribe from events
        _bciTool.OnStimuliFlashingChanged -= HandleStimuliFlashingChanged;
        _bciTool.OnStimuliIdSelected -= HandleStimuliIDSelected;

        // ensures no action taken when script is disabled
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        shouldFlash = true;
        CongfigureBCI();
        HandleStimuliFlashingChanged(false);    // ensure stimuli are not flashing

        // wait before starting classification
        _delayAction.DelayEvent(BCI_START_DELAY, () =>
        {
            TriggerFlashStart();
        });
    }

    private void OnApplicationPause(bool paused)
    {
        if (!paused) CongfigureBCI();
    }

    #endregion

    #region Classification Control
    public void StopFlashing()
    {
        Debug.Log("DISABLE FLASHING");
        shouldFlash = false;
        _bciTool.InterruptClassification();
        HandleStimuliFlashingChanged(false);
    }

    public void StartFlashing()
    {
        Start();
    }
    #endregion

    #region Event Handlers

    // handle the IBCIClassificationTool StimuliFlashingChanged event
    private void HandleStimuliFlashingChanged(bool on)
    {
        if (_stimuliList == null) return;

        for (int i = 0; i < _stimuliList.Count; i++)
        {
            // if on, get desired frequency, otherwise set frequency to 0
            float freq = on ? _frequencies[i] : 0;
            _stimuliList[i].Stimulus.SetStimEffects(1, freq);
        }
    }

    // handle the IBCIClassificationTool StimuliIDSelected event
    private void HandleStimuliIDSelected(int stimulusID)
    {
        if(!GameManager.Instance.currentlyLevellingUp)    // add boolean here for handling if you're in game or selecting reward
        {
            switch (stimulusID)
            {
                case 0:
                    // lane 1
                    _shootAndTurn.RotateAndScheduleShoot(59f);
                    break;
                case 1:
                    //lane 2
                    _shootAndTurn.RotateAndScheduleShoot(16f);
                    break;
                case 2:
                    //lane 3
                    _shootAndTurn.RotateAndScheduleShoot(-16f);
                    break;
                case 3:
                    //lane 4
                    _shootAndTurn.RotateAndScheduleShoot(-59f);
                    break;
                default:
                    Debug.Log("Invalid ID selected");
                    break;
            }
        }
        else
        {
            switch (stimulusID)
            {
                case 0:
                    // lane 1
                    GameManager.Instance.SelectPowerup(0);
                    break;
                case 1:
                    //lane 2
                    GameManager.Instance.SelectPowerup(1);
                    break;
                case 2:
                    //lane 3
                    GameManager.Instance.SelectPowerup(2);
                    break;
                default:
                    Debug.Log("Invalid ID selected");
                    break;
            }
        }
        

        // pause before resuming flashing
        _delayAction.DelayEvent(BCI_RESTART_DELAY, () =>
        {
            TriggerFlashStart();
        });
    }

    private void TriggerFlashStart()
    {
        if (!shouldFlash)
        {
            return;   // something else (likely game over UI) is blocking
        }

        Debug.Log("BEGIN FLASHING");

        _bciTool.ResetStimuli();
        _bciTool.StartBCI(_frequencies, _stimuliList);
        HandleStimuliFlashingChanged(true); // start flashing
    }

    #endregion

    #region BCI Service Comms
    private void CongfigureBCI()
    {
        var config = new BCIConfigurationMessage()
        {
            majorityVoting = false,
            numEpochsToVote = 4,
            numWinsRequired = 3,
            epochWindowSize = 1.25,
            slidingWindowHistory = 2,
            useDynamicThreshold = true,
            dynamicThreshold = 0.15,
            dynamicThresholdEpochWindow = 10

        };
        _singletons.MessageHandler.SendBCIConfigurationMessage(config);
    }
    #endregion
}
