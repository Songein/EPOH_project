using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class DataManager : MonoBehaviour
{
    public Dictionary<string, EventStructure> Events = new Dictionary<string, EventStructure>();
    public Dictionary<string, DialogueStructure> Dialogues = new Dictionary<string, DialogueStructure>();
    public Dictionary<string, EffectStructure> Effects = new Dictionary<string, EffectStructure>();
    public Dictionary<string, CharacterStructure> Characters = new Dictionary<string, CharacterStructure>();
    
    private static DataManager _instance;

    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DataManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(DataManager).Name);
                    _instance = singletonObject.AddComponent<DataManager>();
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지됨
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // 중복 생성 방지
        }
    }
    
    public async UniTask InitializeData(Action<float> onProgressUpdated = null)
    {
        List<Func<UniTask>> loadSteps = new List<Func<UniTask>>()
        {
            async () => { Events = await LoadData<EventStructure>("Event"); },
            async () => { Effects = await LoadData<EffectStructure>("Effect"); },
            async () => { Characters = await LoadData<CharacterStructure>("Character"); },
            async () => { Dialogues = await LoadDialogueData(); },
        };

        int totalSteps = loadSteps.Count;
        int currentStep = 0;

        foreach (var loadStep in loadSteps)
        {
            await loadStep.Invoke();
            currentStep++;
            onProgressUpdated?.Invoke((float)currentStep / totalSteps);
        }
        Debug.LogWarning($"{Events.Count}");
        Debug.LogWarning($"{Effects.Count}");
        Debug.LogWarning($"{Characters.Count}");
        Debug.LogWarning($"{Dialogues.Count}");
    }
    private async UniTask<Dictionary<string, T>> LoadData<T>(string sheetName) where T : new()
    {
        ParseCSV parser = new ParseCSV();
        return await parser.Parse<T>(sheetName);
    }
    private async UniTask<Dictionary<string, DialogueStructure>> LoadDialogueData()
    {
        DialogueParser parser = new DialogueParser();
        return await parser.Parse("Dialogue");
    }
}
