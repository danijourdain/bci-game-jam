using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUpScreen : MonoBehaviour
{
    private List<Action> powerupMethods;
    private List<string> powerupCardText;

    private List<Action> selectedPowerupMethods;

    [SerializeField] private ability_giver abilityGiver;
    [SerializeField] private List<GameObject> cards;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        powerupCardText = new List<string> {
            "Increase max HP by 3",
            "Increase attack damage by 1",
            "Speed up shooting by 10%",
            "Heal +10% of damage dealt",
            "Decrease ability cooldown by 10%",
            "Increase magic damage by 1",
            "Increase max HP by 10",
            "Take 10% less damage from attacks",
            "Dodge chance +10%",
            "Decrease sawblade cooldown by 10%",
            "Decrease plasma ball cooldown by 10%"
        };

        powerupMethods = new List<Action>
        {
            abilityGiver.health_example,
            abilityGiver.sharp_bullets,
            abilityGiver.rapid_fire,
            abilityGiver.vampiric_bullets,
            abilityGiver.cooled_off,
            abilityGiver.increased_magic,
            abilityGiver.chunky,
            abilityGiver.tanky,
            abilityGiver.slippery,
            abilityGiver.IncreasePlasmaBallLevel,
            abilityGiver.IncreaseSawbladeLevel
        };

        selectedPowerupMethods = new List<Action> {null, null, null};
    }

    private List<int> SelectPowerups()
    {
        var random = new System.Random();
        var chosen = new HashSet<int>();
    
        while (chosen.Count < cards.Count)
            chosen.Add(random.Next(0, powerupCardText.Count));

        return new List<int>(chosen);
    }

    public void DisplayPowerupsForSelect()
    {
        gameObject.SetActive(true);
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
        selectedPowerupMethods[index]();
        Cleanup();
    }

    private void Cleanup()
    {
        GameManager.Instance.currentlyLevellingUp = false;
        selectedPowerupMethods = new List<Action> {null, null, null};
        gameObject.SetActive(false);
    }
}
