﻿using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
#if UNITY_2019_1_OR_NEWER
using UnityEngine.UIElements;

#else
using UnityEngine.Experimental.UIElements;
#endif


namespace AlkimeeGames.TagLayerTypeGenerator.Editor
{
    /// <summary>Settings provider for <see cref="TypeGeneratorSettings" />.</summary>
    internal sealed class TypeGeneratorSettingsProvider : SettingsProvider
    {
        /// <summary>Path to the Project Settings.</summary>
        internal const string ProjectSettingPath = "Project/Alkimee/Type Generator Settings";

        /// <summary><see cref="TypeGeneratorSettings" /> wrapped in a <see cref="SerializedObject" />.</summary>
        private SerializedObject _tagGeneratorSettings;

        /// <inheritdoc />
        private TypeGeneratorSettingsProvider(string path, SettingsScope scope) : base(path, scope)
        {
        }

        /// <inheritdoc />
        public override void OnActivate(string searchContext, VisualElement rootElement) => _tagGeneratorSettings = TypeGeneratorSettings.GetSerializedSettings();

        /// <inheritdoc />
        public override void OnGUI(string searchContext)
        {
            EditorGUILayout.LabelField(nameof(TypeGeneratorSettings.Tag), EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_tagGeneratorSettings.FindProperty($"{nameof(TypeGeneratorSettings.Tag)}.{nameof(TypeGeneratorSettings.Tag.AutoGenerate)}"),
                Styles.AutoGenerate);
            EditorGUILayout.PropertyField(_tagGeneratorSettings.FindProperty($"{nameof(TypeGeneratorSettings.Tag)}.{nameof(TypeGeneratorSettings.Tag.TypeName)}"), Styles.TypeName);
            EditorGUILayout.PropertyField(_tagGeneratorSettings.FindProperty($"{nameof(TypeGeneratorSettings.Tag)}.{nameof(TypeGeneratorSettings.Tag.FilePath)}"), Styles.FilePath);
            EditorGUILayout.PropertyField(_tagGeneratorSettings.FindProperty($"{nameof(TypeGeneratorSettings.Tag)}.{nameof(TypeGeneratorSettings.Tag.Namespace)}"),
                Styles.Namespace);
            EditorGUILayout.PropertyField(_tagGeneratorSettings.FindProperty($"{nameof(TypeGeneratorSettings.Tag)}.{nameof(TypeGeneratorSettings.Tag.AssemblyDefinition)}"),
                Styles.AssemblyDefinition);

            EditorGUILayout.Space();

            EditorGUILayout.LabelField(nameof(TypeGeneratorSettings.Layer), EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_tagGeneratorSettings.FindProperty($"{nameof(TypeGeneratorSettings.Layer)}.{nameof(TypeGeneratorSettings.Layer.AutoGenerate)}"),
                Styles.AutoGenerate);
            EditorGUILayout.PropertyField(_tagGeneratorSettings.FindProperty($"{nameof(TypeGeneratorSettings.Layer)}.{nameof(TypeGeneratorSettings.Layer.TypeName)}"),
                Styles.TypeName);
            EditorGUILayout.PropertyField(_tagGeneratorSettings.FindProperty($"{nameof(TypeGeneratorSettings.Layer)}.{nameof(TypeGeneratorSettings.Layer.FilePath)}"),
                Styles.FilePath);
            EditorGUILayout.PropertyField(_tagGeneratorSettings.FindProperty($"{nameof(TypeGeneratorSettings.Layer)}.{nameof(TypeGeneratorSettings.Layer.Namespace)}"),
                Styles.Namespace);
            EditorGUILayout.PropertyField(_tagGeneratorSettings.FindProperty($"{nameof(TypeGeneratorSettings.Layer)}.{nameof(TypeGeneratorSettings.Layer.AssemblyDefinition)}"),
                Styles.AssemblyDefinition);

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Actions", EditorStyles.boldLabel);
            EditorGUI.BeginDisabledGroup(TagTypeGenerator.Generator.CanGenerate() == false);
            if (GUILayout.Button("Regenerate Tag Type File")) TagTypeGenerator.Generator.GenerateFile();
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(LayerTypeGenerator.Generator.CanGenerate() == false);
            if (GUILayout.Button("Regenerate Layer Type File")) LayerTypeGenerator.Generator.GenerateFile();
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.LabelField("Open", EditorStyles.boldLabel);
            if (GUILayout.Button("Settings Asset")) Selection.SetActiveObjectWithContext(_tagGeneratorSettings.targetObject, _tagGeneratorSettings.context);
            if (GUILayout.Button("Tags and Layers")) SettingsService.OpenProjectSettings("Project/Tags and Layers");

            _tagGeneratorSettings.ApplyModifiedPropertiesWithoutUndo();
        }

        /// <summary>Creates the <see cref="SettingsProvider" /> for the Project Settings window.</summary>
        /// <returns>The <see cref="SettingsProvider" /> for the Project Settings window.</returns>
        [SettingsProvider]
        [NotNull]
        private static SettingsProvider CreateTagClassGeneratorSettingsProvider() =>
            new TypeGeneratorSettingsProvider(ProjectSettingPath, SettingsScope.Project)
            {
                keywords = GetSearchKeywordsFromGUIContentProperties<Styles>()
            };

        /// <summary>Styles for the <see cref="SettingsProvider" />.</summary>
        private /*readonly*/ struct Styles
        {
            public static readonly GUIContent AutoGenerate = new GUIContent("Auto Generate");
            public static readonly GUIContent TypeName = new GUIContent("Type Name");
            public static readonly GUIContent FilePath = new GUIContent("File Path");
            public static readonly GUIContent Namespace = new GUIContent("Namespace");
            public static readonly GUIContent AssemblyDefinition = new GUIContent("Assembly Definition");
        }
    }
}