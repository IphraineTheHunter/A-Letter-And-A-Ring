using UnityEngine;
using System.Collections.Generic;
using Assets;
using System;

public class CityContext : MonoBehaviour
{
    public static CityContext context = null;
    public List<PersonOfInterest> _pois = new List<PersonOfInterest>();
    public List<Quest> _quests = new List<Quest>();
    public List<ActiveEvent> _events = new List<ActiveEvent>();
    public RandomCustom random = new RandomCustom();
    public List<POIGoal> _goals = new List<POIGoal>();
    public List<Title> _titles = new List<Title>();
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
        new ActiveEvent();
        new PersonOfInterest();
        new PersonOfInterest();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        for (int questIndex = 0; questIndex < _quests.Count; questIndex++)
        {
            Quest quest = _quests[questIndex];
            string buttonText = string.Format("Quest for {0}", quest.offerer.name);
            if (GUI.Button(new Rect(10, 100 + questIndex * 30, 200, 30), buttonText))
            {
                quest.Complete();
            }
        }
        for (int eventIndex = 0; eventIndex < _events.Count; eventIndex++ )
        {
            ActiveEvent activeEvent = _events[eventIndex];
            string labelText = activeEvent.name + " - " + activeEvent.power;
            GUI.Label(new Rect(300, 100 + eventIndex * 30, 100, 30), labelText);
        }
    }

    internal static void Tick()
    {
        POIGoal.Tick();
        ActiveEvent.Tick();
    }
}
