using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [Header("CSV 파일 이름")] [SerializeField] private string _eventFileName;
    [SerializeField] private string _dialogueFileName;
    [SerializeField] private string _effectFileName;
    [SerializeField] private string _characterFileName;
    
    private ParseCSV _parser;
    private DialogueParser _dialogueParser;
    private Dictionary<string, EventStructure> _events = new Dictionary<string, EventStructure>();
    private Dictionary<string, DialogueStructure> _dialogues = new Dictionary<string, DialogueStructure>();
    private Dictionary<string, EffectStructure> _effects = new Dictionary<string, EffectStructure>();
    private Dictionary<string, CharacterStructure> _characters = new Dictionary<string, CharacterStructure>();

    void Start()
    {
        _parser = new ParseCSV();
        _dialogueParser = new DialogueParser();
        _events = _parser.Parse<EventStructure>(_eventFileName);
        _dialogues = _dialogueParser.Parse(_dialogueFileName);
        _effects = _parser.Parse<EffectStructure>(_effectFileName);
        _characters = _parser.Parse<CharacterStructure>(_characterFileName);
    }
}
