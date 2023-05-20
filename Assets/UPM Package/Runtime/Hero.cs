using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using GAS.Editor;

namespace GAS
{
    [CreateAssetMenu(fileName = "New Hero", menuName = "GAS/Hero")]
    public class Hero : ScriptableObject
    {
        public List<Ability> abilities = new List<Ability>();
        public Dictionary<int, List<Talent>> talents = new Dictionary<int, List<Talent>>();
        [SerializeField] private int _maxLevel = 1;

        public int MaxLevel
        {
            get { return _maxLevel; }
            set { _maxLevel = Mathf.Max(1, value); }
        }

        private void OnEnable()
        {
            if (abilities.Count == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    abilities.Add(null);
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
            private int selectedTab = 0;

            private SerializedProperty maxLevelProp;

            private SerializedProperty abilitiesProp;
            private List<Type> abilityTypes;

            private void OnEnable()
            {
                maxLevelProp = serializedObject.FindProperty("_maxLevel");
                abilitiesProp = serializedObject.FindProperty("abilities");
                abilityTypes = GetNonAbstractAbilityTypes();
            }


            public override void OnInspectorGUI()
            {
                serializedObject.Update();
                Hero hero = (Hero) target;

                EditorGUILayout.Space();

                // Display max level value
                EditorGUILayout.PropertyField(maxLevelProp);

                EditorGUILayout.Space();

                // Draw tabs
                string[] tabOptions = {"Talents", "Abilities"};
                selectedTab = GUILayout.Toolbar(selectedTab, tabOptions);

                EditorGUILayout.Space();

                // Display content based on selected tab
                switch (selectedTab)
                {
                    case 0:
                        DrawTalents(hero);
                        break;
                    case 1:
                        DrawAbilities(hero);
                        break;
                }
            }

            private void DrawTalents(Hero hero)
            {
                // Draw the talents using EditorGUILayout
                // ...
            }

            private void DrawAbilities(Hero hero)
            {

                // Draw abilities section
                EditorGUILayout.LabelField("Abilities", EditorStyles.boldLabel);
                for (var i = 0; i < abilitiesProp.arraySize; i++)
                {
                    EditorGUILayout.BeginHorizontal();

                    SerializedProperty abilityProp = abilitiesProp.GetArrayElementAtIndex(i);
                    var ability =
                        EditorUtility.InstanceIDToObject(abilityProp.objectReferenceInstanceIDValue);
                    // Display ability field
                    ability = EditorGUILayout.ObjectField(ability, typeof(Ability), false) as Ability;
                    abilityProp.objectReferenceValue = ability;

                    // Remove ability button
                    if (GUILayout.Button("-", GUILayout.Width(20)))
                    {
                        abilitiesProp.DeleteArrayElementAtIndex(i);
                        i--;
                    }

                    EditorGUILayout.EndHorizontal();
                }

                // Add ability button
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("+", GUILayout.Width(30)))
                {
                    GenericMenu menu = new GenericMenu();

                    // Create menu items for each non-abstract ability type
                    foreach (Type abilityType in abilityTypes)
                    {
                        menu.AddItem(new GUIContent(abilityType.Name), false, () => AddAbility(abilityType));
                    }

                    menu.ShowAsContext();
                }

                EditorGUILayout.EndHorizontal();

                serializedObject.ApplyModifiedProperties();
            }

            private List<Type> GetNonAbstractAbilityTypes()
            {
                Type abilityType = typeof(Ability);
                List<Type> nonAbstractTypes = new List<Type>();

                // Get all non-abstract classes derived from Ability using reflection
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (abilityType.IsAssignableFrom(type) && !type.IsAbstract)
                        {
                            nonAbstractTypes.Add(type);
                        }
                    }
                }

                return nonAbstractTypes;
            }

            private void AddAbility(Type abilityType)
            {
                Ability newAbility = ScriptableObject.CreateInstance(abilityType) as Ability;
                abilitiesProp.arraySize++;
                abilitiesProp.GetArrayElementAtIndex(abilitiesProp.arraySize - 1).objectReferenceValue = newAbility;
            }
        }

    }
}