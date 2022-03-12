using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPhrases
{
    private List<string> _phrases = new List<string>
    {
        "Roberrrrrrt!!",
        "Aaaaaa!",
        "I'll be back",
        "Noooooo!",
        "Wow, i see a coin!",
        "Aghhh!",
        "Ooops",
        "Come and get me, maggot",
        "To infinity, and beyond!",
        "I can fly!!",
        "Wow, i see a cake!"
    };
    public string GetRandomPhrase()
    {
        return _phrases[Random.Range(0, _phrases.Count)];
    }
}
