using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LevelUpScreen : MonoBehaviour
{
    private List<Action> powerupMethods;
    private List<string> powerupCardText;

    private List<Action> selectedPowerupMethods;

    private List<(Vector3 position, Quaternion rotation)> originalStimuliTransforms;
    private List<(Vector3 position, Quaternion rotation)> goalStimuliTransforms;

    [SerializeField] private ability_giver abilityGiver;
    [SerializeField] private List<GameObject> cards;
    [SerializeField] private List<GameObject> stimuli;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        powerupCardText = new List<string> {
            "Magic Bullets \nAll Damage UP",
            "Sharp Bullets \nNose to the Grindstone",
            "Sharp Bullets \nNose to the Grindstone",
            "Rapid Fire \nBullet spam",
            "Rapid Fire \nBullet spam",
            "Vampiric Bullets \nOne AH AH AH",
            "Cooled Off \nSpammer",
            "Increased Magic \nBullets Ain't Magic",
            "Increased Magic \nBullets Ain't Magic",
            "Chunky \nBIG MAN",
            "Tanky \nDurable",
            "Slippery \nCatch Me If You Can",
            "Plasma Ball \nIncreased Growth",
            "Sawblade \nLonger Lasting",
            "Electricity Charge \nMore Splits",
            "Laser Beam \nBIGGER BEAM",
            "GAMBLING! \nSurprise!",
            "Slot Machine \n90% of Gamblers quit before they hit it big!",
            "BIG MAN \nBIG HEALTH BIG REDUCTION"
        };

        powerupMethods = new List<Action>
        {
            abilityGiver.Magic_bullets,
            abilityGiver.sharp_bullets,
            abilityGiver.sharp_bullets,
            abilityGiver.rapid_fire,
            abilityGiver.rapid_fire,
            abilityGiver.vampiric_bullets,
            abilityGiver.cooled_off,
            abilityGiver.increased_magic,
            abilityGiver.increased_magic,
            abilityGiver.chunky,
            abilityGiver.tanky,
            abilityGiver.slippery,
            abilityGiver.IncreasePlasmaBallLevel,
            abilityGiver.IncreaseSawbladeLevel,
            abilityGiver.IncreaseElectricityChargeLevel,
            abilityGiver.IncreaseLaserLevel,
            abilityGiver.GiveRandomPowerup,
            abilityGiver.IncreaseSlotMachineLevel,
            abilityGiver.BIG_MAN
        };

        selectedPowerupMethods = new List<Action> {null, null, null};

        originalStimuliTransforms = new List<(Vector3, Quaternion)>();
        foreach (GameObject stim in stimuli)
            originalStimuliTransforms.Add((stim.transform.position, stim.transform.rotation));

        goalStimuliTransforms = new List<(Vector3, Quaternion)>
        {
            (new Vector3(cards[0].transform.localPosition.x, -8f, 0f), Quaternion.identity),  // card 1
            (new Vector3(cards[1].transform.localPosition.x, -8f, 0f), Quaternion.identity),    // card 2
            (new Vector3(cards[2].transform.localPosition.x, -8f, 0f), Quaternion.identity)    // card 2
        };
    }

    private List<int> SelectPowerups()
    {
        var random = new System.Random();
        var chosen = new HashSet<int>();
    
        while (chosen.Count < cards.Count)
            chosen.Add(random.Next(0, powerupCardText.Count));

        return new List<int>(chosen);
    }

    private void MoveStimuli()
    {
        for (int i = 0; i < selectedPowerupMethods.Count; i++)
        {
            stimuli[i].transform.SetLocalPositionAndRotation(goalStimuliTransforms[i].position, goalStimuliTransforms[i].rotation);
        }
        for(int i = selectedPowerupMethods.Count; i < stimuli.Count; i++)
        {
            foreach (SpriteRenderer sr in stimuli[i].GetComponentsInChildren<SpriteRenderer>())
            sr.enabled = false;
        }
    }

    public void DisplayPowerupsForSelect()
    {
        gameObject.SetActive(true);
        MoveStimuli();

        List<int> selectedIndices = SelectPowerups();

        for (int i = 0; i < selectedIndices.Count; i++) 
        {
            // add text to the card
            TextMeshPro card_text = cards[i].GetComponentInChildren<TextMeshPro>();
            card_text.text = powerupCardText[selectedIndices[i]];
            
            // display the card
            cards[i].SetActive(true);

            // update the methods 
            selectedPowerupMethods[i] = powerupMethods[selectedIndices[i]];
        }
    }

    public void OnBCISelect(int index)
    {
        Debug.Log("SELECTED OPTION " + index);
        selectedPowerupMethods[index]();
        Cleanup();
    }

    private void Cleanup()
    {
        GameManager.Instance.currentlyLevellingUp = false;
        selectedPowerupMethods = new List<Action> {null, null, null};
        gameObject.SetActive(false);

        // reset stimulus locations
        for (int i = 0; i < selectedPowerupMethods.Count; i++)
        {
            stimuli[i].transform.SetPositionAndRotation(originalStimuliTransforms[i].position, originalStimuliTransforms[i].rotation);
        }
        for(int i = selectedPowerupMethods.Count; i < stimuli.Count; i++)
        {
            foreach (SpriteRenderer sr in stimuli[i].GetComponentsInChildren<SpriteRenderer>())
            sr.enabled = true;
        }
    }
}
