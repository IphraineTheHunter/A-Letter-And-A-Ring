using UnityEngine;
using System.Collections.Generic;
using Assets;
using System;
using Assets.Scripts._cityScripts;
using Assets.Scripts._cityScripts.Quests;
using Assets.Scripts._PersonOfInterest;

public class CityContext : MonoBehaviour
{
    public static CityContext context = null;

    public PlayerMap _playerMap = new PlayerMap();

    void Awake()
    {
        //Debug.Log("City Context Awake");
        if (context == null)
        {
            context = this;
        }
        else if (context != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        City city = new City();
        
        new ActiveEvent(city);
        Greedy greedy = new Greedy(city);
        city.title = new Title(greedy);
        greedy.name = "Scrooge McDuck";
        Pride pride = new Pride(city);
        pride.name = "Scar";
        Humane humane = new Humane(city);
        humane.name = "Muhatma Ghandi";

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        string labelText = "Your Wealth: " + _playerMap.wealth;
        GUI.Label(new Rect(200, 100, 100, 30), labelText);

        for (int questIndex = 0; questIndex < Quest._all.Count; questIndex++)
        {
            Quest quest = Quest._all[questIndex];
            string buttonText = string.Format("{0} - {1}", quest.offerer.name, quest.name);
            if (GUI.Button(new Rect(15, 100 + questIndex * 30, 200, 30), buttonText))
            {
                quest.Accept();
                quest.Complete();
            }
        }
        for (int eventIndex = 0; eventIndex < ActiveEvent._all.Count; eventIndex++ )
        {
            ActiveEvent activeEvent = ActiveEvent._all[eventIndex];
            labelText = activeEvent.name + " - " + activeEvent.power;
            GUI.Label(new Rect(300, 100 + eventIndex * 30, 100, 30), labelText);
        }
    }

    internal static void Tick()
    {
        POIGoal.Tick();
        ActiveEvent.Tick();
    }
}
