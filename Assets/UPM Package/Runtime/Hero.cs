using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using GAS.Editor;

namespace GAS
{
    [CreateAssetMenu(fileName = "New Hero", menuName = "GAS/Hero")]
    public class Hero : ScriptableObject
    {
        public List<Ability> Abilities => _abilities;

        private List<Talent> GetTalents(int level)
        {
            level = Mathf.Min(level, _maxLevel);
            
            if(!_talents.ContainsKey(level))
            {
                _talents.Add(level, new List<Talent>());
            }
            return _talents[level];
        }

        private void AddTalent(int level, Talent talent)
        {
            var levelIndex = level - 1;
            if (!_talents.ContainsKey(levelIndex))
            {
                _talents.Add(levelIndex, new List<Talent>());
            }
            _talents[levelIndex].Add(talent);
        }

        private void RemoveTalent(int level, Talent talent)
        {
            var levelIndex = level - 1;
            if (!_talents.ContainsKey(levelIndex))
            {
                _talents.Add(levelIndex, new List<Talent>());
            }
            _talents[levelIndex].Remove(talent);
        }
        
        private Dictionary<int, List<Talent>> _talents = new Dictionary<int, List<Talent>>();
        [SerializeField] private List<Ability> _abilities = new List<Ability>();
        [SerializeField] private int _maxLevel = 30;

        public int MaxLevel
        {
            get { return _maxLevel; }
            set { _maxLevel = Mathf.Max(1, value); }
        }

        private void OnEnable()
        {
            if (_abilities.Count == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    _abilities.Add(null);
                }
            }

            if (_talents.Count != _maxLevel || _talents.Count == 0)
            {
                for (var i = 0; i < _talents.Count; i++)
                {
                    _talents[i] = new List<Talent>();
                }
            }
        }

        [MenuItem("Assets/Create/Hero")]
        public static void CreateHero()
        {
            var hero = CreateInstance<Hero>();
            AssetDatabase.CreateAsset(hero, "Assets/NewHero.asset");
            AssetDatabase.SaveAssets();

            // Open table editor window to set talents and modify abilities
            TableEditorWindow.OpenWindow(hero);
        }

        [CustomEditor(typeof(Hero))]
        public class HeroEditor : UnityEditor.Editor
        {
            private SerializedProperty maxLevelProp;

            private SerializedProperty abilitiesProp;
            private List<Type> abilityTypes;
            private bool[] showTalents;
            private int _currentTab = 0;
            

            private void OnEnable()
            {
                maxLevelProp = serializedObject.FindProperty("_maxLevel");
                showTalents = new bool[maxLevelProp.intValue];  
            }
            

            public override void OnInspectorGUI()
            {
                serializedObject.Update();
                Hero hero = (Hero) target;

                EditorGUILayout.Space();

                // Display max level value
                EditorGUILayout.PropertyField(maxLevelProp);

                EditorGUILayout.Space();

                _currentTab = GUILayout.Toolbar(_currentTab, new string[] { "Abilities", "Talents" });                

                EditorGUILayout.Space();
                
                

                switch (_currentTab)
                {
                    case 0:
                        DrawAbilities();
                        break;
                    case 1:
                        DrawTalents();
                        break;
                        
                }
            }

            private void DrawTalents()
            {

                var hero = (Hero)target;
                GUILayout.Label("Talent Editor", EditorStyles.boldLabel);

                // Draw the table
                GUILayout.Space(20f);

                for (var i = maxLevelProp.intValue; i > 0; i--)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"Level {i} Talent", GUILayout.Width(120f));

                    // Draw the "+" button to show/hide talents
                    if (GUILayout.Button(showTalents[i - 1] ? "-" : "+", GUILayout.Width(20f)))
                    {
                        showTalents[i - 1] = !showTalents[i - 1];
                    }

                    GUILayout.EndHorizontal();

                    // Check if talents should be shown for this ability
                    if (showTalents[i - 1])
                    {
                        GUILayout.BeginVertical(EditorStyles.helpBox);

                        // Draw talents content here
                        GUILayout.Label("Talents for Level " + i.ToString());
                        

                        foreach (var talent in hero.GetTalents(i - 1).ToList())
                        {
                            GUILayout.BeginVertical();
        
                            // Display talent properties
                            talent.DrawCustomProperties(hero);
                               
                            GUILayout.BeginHorizontal();
                            if (GUILayout.Button("Remove Talent"))
                            {
                                RemoveTalent(i, talent);
                            }
                            GUILayout.EndHorizontal();
                            GUILayout.EndVertical();
                        }
                        GUILayout.BeginHorizontal();
                        // Add new talent button
                        if (GUILayout.Button("Add Talent"))
                        {
                            AddNewTalent(i);
                        }
                        
                        GUILayout.EndHorizontal();

                        GUILayout.EndVertical();
                    }
                }
            }

            private void DrawAbilities()
            {
                var hero = target as Hero;
                
                
                GUILayout.Label("Ability Editor", EditorStyles.boldLabel);

                var abilitiesProperty = serializedObject.FindProperty("_abilities");

                EditorGUILayout.PropertyField(abilitiesProperty, true);

                serializedObject.ApplyModifiedProperties();
            }

            private void AddNewTalent(int level)
            {
                // Show a context menu to select the talent type
                var talentMenu = new GenericMenu();
                talentMenu.AddItem(new GUIContent("Cost Talent"), false, () => CreateTalent<CostTalent>(level));
                talentMenu.AddItem(new GUIContent("Range Talent"), false, () => CreateTalent<RangeTalent>(level));
                talentMenu.AddItem(new GUIContent("Damage VFX Talent"), false, () => CreateTalent<DamageVFXTalent>(level));
                talentMenu.ShowAsContext();
            }

            private void CreateTalent<T>(int level) where T : Talent, new()
            {
                Talent newTalent = new T();
                ((Hero)target).AddTalent(level,newTalent);
            }

            private void RemoveTalent(int level, Talent talent)
            {
                ((Hero)target).RemoveTalent(level, talent);
            }
        }

    }
}