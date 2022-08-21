using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;

public class DeathPhrases
{
    private List<string> _phrasesEN = new List<string>
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
        "Wow, i see a cake!",
        "So slippery",
        "I was blindfolded",
        "I wasn't ready!",
        "Agh, sliped",
        "It's just a flesh wound"
    };

    private List<string> _phrasesRU = new List<string>
    {
        "Роберррррррт!!",
        "Аааааа!",
        "Я вернусь",
        "Нееееет!",
        "Вау, я вижу монету!",
        "Аууч!",
        "Ууупс",
        "Я должен отскочить...",
        "Увидимся на другой стороне...",
        "Я могу летать!!",
        "На этом всё, ребята.",
        "Не поминайте лихом!",
        "Я надеюсь, что мы скоро увидимся",
        "Я не был к такому готов!",
        "Ах, поскользнулся",
        "Адиос, амигос!"
    };

    private List<string> _phrasesTR = new List<string>
    {
        "Roberrrrrrt!!",
        "Aaaaaa!",
        "Geri döneceğim",
        "Hayır! hayır!",
        "Vay canına, bir bozuk para görüyorum!",
        "Auch!",
        "Oops!",
        "Gitmem gerekiyor...",
        "Diğer tarafta görüşürüz...",
        "Uçabilirim!",
        "Bu konuda, herkes ne yazık ki",
        "Dikkatsizce anmayın!",
        "Umarım yakında görüşürüz",
        "Buna hazır değildim!",
        "Ah, kaydım",
        "Eyvallah, amigos!"
    };

    public string GetRandomPhrase()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return _phrasesRU[Random.Range(0, _phrasesRU.Count)];
#endif

#if YANDEX_GAMES
        switch (YandexGamesSdk.Environment.i18n.lang)
        {
            case "ru":
                return _phrasesRU[Random.Range(0, _phrasesRU.Count)];
            case "en":
                return _phrasesEN[Random.Range(0, _phrasesEN.Count)];
            case "tr":
                return _phrasesTR[Random.Range(0, _phrasesTR.Count)];
           default:
                throw new System.Exception();
        }
#endif

#if VK_GAMES
        return _phrasesRU[Random.Range(0, _phrasesRU.Count)];
#endif
    }
}
