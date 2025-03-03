using System.Collections.Generic;
using Event;
using UnityEngine;

public class DataManager : MonoBehaviour
    {
        private static DataManager _instance;
        
        [Header("CSV 파일 이름")] [SerializeField] private string _eventFileName;
        [SerializeField] private string _dialogueFileName;
        [SerializeField] private string _effectFileName;
        [SerializeField] private string _characterFileName;
    
        private ParseCSV _parser;
        private DialogueParser _dialogueParser;
        public Dictionary<string, EventStructure> events = new Dictionary<string, EventStructure>();
        public Dictionary<string, DialogueStructure> dialogues = new Dictionary<string, DialogueStructure>();
        public Dictionary<string, EffectStructure> effects = new Dictionary<string, EffectStructure>();
        public Dictionary<string, CharacterStructure> characters = new Dictionary<string, CharacterStructure>();
        
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

        void Start()
        {
            _parser = new ParseCSV();
            _dialogueParser = new DialogueParser();
            events = _parser.Parse<EventStructure>(_eventFileName);
            dialogues = _dialogueParser.Parse(_dialogueFileName);
            effects = _parser.Parse<EffectStructure>(_effectFileName);
            characters = _parser.Parse<CharacterStructure>(_characterFileName);
        }
    }
