using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace GAS.Editor
{   
    public class TableEditorWindow : EditorWindow
    {
        private Hero hero; // Reference to the hero being edited
        private bool[] showTalents; // Indicates if talents are shown for each row
        public static void OpenWindow(Hero hero)
        {
            TableEditorWindow window = GetWindow<TableEditorWindow>("Table Editor");
            window.hero = hero;
            window.InitializeTalents();
        }

        private void InitializeTalents()
        {
            var rowCount = hero.MaxLevel;
            showTalents = new bool[rowCount];
        }
        

        private void OnGUI()
        {
            GUILayout.Label("Table Editor", EditorStyles.boldLabel);

            // Draw the table
            GUILayout.Space(20f);

            for (var i = hero.MaxLevel; i > 0; i--)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Ability " + i.ToString(), GUILayout.Width(80f));

                // Draw the "+" button to show/hide talents
                if (GUILayout.Button(showTalents[i - 1] ? "-" : "+", GUILayout.Width(20f)))
                {
                    showTalents[i - 1] = !showTalents[i - 1];
                }

                GUILayout.EndHorizontal();
/*
                // Check if talents should be shown for this ability
                if (showTalents[i - 1])
                {
                    GUILayout.BeginVertical(EditorStyles.helpBox);

                    // Draw talents content here
                    GUILayout.Label("Talents for Ability " + i.ToString());

                    // Draw the talents using EditorGUILayout
                    // Draw the talents using EditorGUILayout
                    foreach (var talent in hero.talents[i - 1])
                    {
                        GUILayout.BeginVertical();

                        // Display talent properties
                        talent.Name = EditorGUILayout.TextField("Name", talent.Name);
                        talent.Description = EditorGUILayout.TextField("Description", talent.Description);
                        talent.LevelRequirement =
                            EditorGUILayout.IntField("Level Requirement", talent.LevelRequirement);

                        GUILayout.EndHorizontal();
                    }

                    // Add new talent button
                    if (GUILayout.Button("Add Talent"))
                    {
                        AddNewTalent(i - 1);
                    }

                    GUILayout.EndVertical();
                }
                */
            }
        }

        private void AddNewTalent(int level)
        {
            // Show a context menu to select the talent type
            var talentMenu = new GenericMenu();
            talentMenu.AddItem(new GUIContent("Cost Talent"), false, () => CreateTalent<CostTalent>(level));
            talentMenu.AddItem(new GUIContent("Range Talent"), false, () => CreateTalent<RangeTalent>(level));
            talentMenu.ShowAsContext();
        }

        private void CreateTalent<T>(int row) where T : Talent, new()
        {
            Talent newTalent = new T();
           // hero.talents[row].Add(newTalent);
        }
    }

}