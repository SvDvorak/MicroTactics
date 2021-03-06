﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Entitas;
using Entitas.CodeGenerator;
using UnityEditor;
using UnityEngine;

namespace Entitas.Unity.VisualDebugging
{
    [CustomEditor(typeof(EntityBehaviour)), CanEditMultipleObjects]
    public class EntityEditor : Editor
    {
        GUIStyle _foldoutStyle;
        static IDefaultInstanceCreator[] _defaultInstanceCreators;
        static ITypeDrawer[] _typeDrawers;

        void Awake()
        {
            _foldoutStyle = new GUIStyle(EditorStyles.foldout);
            _foldoutStyle.fontStyle = FontStyle.Bold;

            var types = Assembly.GetAssembly(typeof(EntityEditor)).GetTypes();

            _defaultInstanceCreators = types
                .Where(type => type.GetInterfaces().Contains(typeof(IDefaultInstanceCreator)))
                .Select(type => (IDefaultInstanceCreator)Activator.CreateInstance(type))
                .ToArray();

            _typeDrawers = types
                .Where(type => type.GetInterfaces().Contains(typeof(ITypeDrawer)))
                .Select(type => (ITypeDrawer)Activator.CreateInstance(type))
                .ToArray();
        }

        public override void OnInspectorGUI()
        {
            if (targets.Length == 1)
            {
                drawSingleTarget();
            }
            else
            {
                drawMultiTargets();
            }
            EditorUtility.SetDirty(target);
        }

        void drawSingleTarget()
        {
            var entityBehaviour = (EntityBehaviour)target;
            var pool = entityBehaviour.pool;
            var entity = entityBehaviour.entity;

            var affectingSystems = GetAffectingSystems(entity);

            if (affectingSystems.Count != 0)
            {
                EditorGUILayout.BeginVertical(GUI.skin.box);
                EditorGUILayout.LabelField("Affecting systems (" + affectingSystems.Count + ")", EditorStyles.boldLabel);
                foreach (var system in affectingSystems)
                {
                    EditorGUILayout.LabelField(system);
                }
                EditorGUILayout.EndVertical();
            }

            if (GUILayout.Button("Destroy Entity"))
            {
                entityBehaviour.DestroyBehaviour();
                pool.DestroyEntity(entity);
            }

            EditorGUILayout.BeginHorizontal();
            entityBehaviour.componentToAdd = EditorGUILayout.Popup(entityBehaviour.componentToAdd, entityBehaviour.componentNames);
            if (GUILayout.Button("Add Component"))
            {
                var index = entityBehaviour.componentToAdd;
                var componentType = entityBehaviour.componentTypes[index];
                var component = (IComponent)Activator.CreateInstance(componentType);
                entity.AddComponent(index, component);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Components (" + entity.GetComponents().Length + ")", EditorStyles.boldLabel);
            if (GUILayout.Button("▸", GUILayout.Width(21), GUILayout.Height(14)))
            {
                entityBehaviour.FoldAllComponents();
            }
            if (GUILayout.Button("▾", GUILayout.Width(21), GUILayout.Height(14)))
            {
                entityBehaviour.UnfoldAllComponents();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            var indices = entity.GetComponentIndices();
            var components = entity.GetComponents();
            for (int i = 0; i < components.Length; i++)
            {
                drawComponent(entityBehaviour, entity, indices[i], components[i]);
            }
            EditorGUILayout.EndVertical();
        }

        private static List<string> GetAffectingSystems(Entity entity)
        {
            var types = Assembly.GetAssembly(typeof(ReactiveSystem)).GetTypes();

            var reactiveSystems = types
                .Where(type => type.GetInterfaces().Contains(typeof (IReactiveSystem)))
                .Select(type => CreateReactiveSystem(type))
                .Where(system => HasTriggerComponents(entity, (IReactiveSystem)system));

            var multiReactiveSystems = types
                .Where(type => type.GetInterfaces().Contains(typeof(IMultiReactiveSystem)))
                .Select(type => CreateReactiveSystem(type))
                .Where(system => HasTriggerComponents(entity, (IMultiReactiveSystem)system));

            return reactiveSystems
                .Concat(multiReactiveSystems)
                .Where(system =>
                    HasEnsuredComponents(entity, system as IEnsureComponents) &&
                    DoesNotHaveExcludedComponents(entity, system as IExcludeComponents))
                .Select(x => x.GetType().Name).ToList();
        }

        private static IReactiveExecuteSystem CreateReactiveSystem(Type type)
        {
            return (type.GetConstructor(new Type[0]).Invoke(new object[0]) as IReactiveExecuteSystem);
        }

        private static bool HasTriggerComponents(Entity entity, IReactiveSystem system)
        {
            return system.trigger.trigger.Matches(entity);
        }

        private static bool HasTriggerComponents(Entity entity, IMultiReactiveSystem system)
        {
            return system.triggers.Any(trigger => trigger.trigger.Matches(entity));
        }

        private static bool HasEnsuredComponents(Entity entity, IEnsureComponents ensureComponents)
        {
            return ensureComponents == null || ensureComponents.ensureComponents.Matches(entity);
        }

        private static bool DoesNotHaveExcludedComponents(Entity entity, IExcludeComponents excludeComponents)
        {
            return excludeComponents == null || !excludeComponents.excludeComponents.Matches(entity);
        }

        void drawMultiTargets()
        {
            EditorGUILayout.BeginHorizontal();
            var aEntityBehaviour = (EntityBehaviour)targets[0];
            aEntityBehaviour.componentToAdd = EditorGUILayout.Popup(aEntityBehaviour.componentToAdd, aEntityBehaviour.componentNames);
            if (GUILayout.Button("Add Component"))
            {
                var index = aEntityBehaviour.componentToAdd;
                var componentType = aEntityBehaviour.componentTypes[index];
                foreach (var t in targets)
                {
                    var entity = ((EntityBehaviour)t).entity;
                    var component = (IComponent)Activator.CreateInstance(componentType);
                    entity.AddComponent(index, component);
                }
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Destroy selected entities"))
            {
                foreach (var t in targets)
                {
                    var entityBehaviour = (EntityBehaviour)t;
                    var pool = entityBehaviour.pool;
                    var entity = entityBehaviour.entity;
                    entityBehaviour.DestroyBehaviour();
                    pool.DestroyEntity(entity);
                }
            }

            EditorGUILayout.Space();

            foreach (var t in targets)
            {
                var entityBehaviour = (EntityBehaviour)t;
                var pool = entityBehaviour.pool;
                var entity = entityBehaviour.entity;

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(entity.ToString());
                if (GUILayout.Button("Destroy Entity"))
                {
                    entityBehaviour.DestroyBehaviour();
                    pool.DestroyEntity(entity);
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        void drawComponent(EntityBehaviour entityBehaviour, Entity entity, int index, IComponent component)
        {
            var componentType = component.GetType();
            var fields = componentType.GetFields(BindingFlags.Public | BindingFlags.Instance);

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.BeginHorizontal();
            if (fields.Length == 0)
            {
                EditorGUILayout.LabelField(componentType.RemoveComponentSuffix(), EditorStyles.boldLabel);
            }
            else
            {
                entityBehaviour.unfoldedComponents[index] = EditorGUILayout.Foldout(entityBehaviour.unfoldedComponents[index], componentType.RemoveComponentSuffix(), _foldoutStyle);
            }
            if (GUILayout.Button("-", GUILayout.Width(19), GUILayout.Height(14)))
            {
                entity.RemoveComponent(index);
            }
            EditorGUILayout.EndHorizontal();

            if (entityBehaviour.unfoldedComponents[index])
            {
                foreach (var field in fields)
                {
                    var value = field.GetValue(component);
                    DrawAndSetElement(field.FieldType, field.Name, value,
                        entity, index, component, newValue => field.SetValue(component, newValue));
                }
            }
            EditorGUILayout.EndVertical();
        }

        public static void DrawAndSetElement(Type type, string fieldName, object value, Entity entity, int index, IComponent component, Action<object> setValue)
        {
            var newValue = DrawAndGetNewValue(type, fieldName, value, entity, index, component);
            if (DidValueChange(value, newValue))
            {
                setValue(newValue);
                entity.ReplaceComponent(index, component);
            }
        }

        public static bool DidValueChange(object value, object newValue)
        {
            return (value == null && newValue != null) ||
                   (value != null && newValue == null) ||
                   ((value != null && newValue != null &&
                   !newValue.Equals(value)));
        }

        public static object DrawAndGetNewValue(Type type, string fieldName, object value, Entity entity, int index, IComponent component)
        {
            if (value == null)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(fieldName, "null");
                if (GUILayout.Button("Create", GUILayout.Height(14)))
                {
                    object defaultValue;
                    if (CreateDefault(type, out defaultValue))
                    {
                        value = defaultValue;
                    }
                }
                EditorGUILayout.EndHorizontal();
                return value;
            }

            if (!type.IsValueType)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
            }

            var typeDrawer = getTypeDrawer(type);
            if (typeDrawer != null)
            {
                value = typeDrawer.DrawAndGetNewValue(type, fieldName, value, entity, index, component);
            }
            else
            {
                drawUnsupportedType(type, fieldName, value);
            }

            if (!type.IsValueType)
            {
                EditorGUILayout.EndVertical();
                if (GUILayout.Button("x", GUILayout.Width(19), GUILayout.Height(14)))
                {
                    value = null;
                }
                EditorGUILayout.EndHorizontal();
            }

            return value;
        }

        static ITypeDrawer getTypeDrawer(Type type)
        {
            foreach (var drawer in _typeDrawers)
            {
                if (drawer.HandlesType(type))
                {
                    return drawer;
                }
            }

            return null;
        }

        static void drawUnsupportedType(Type type, string fieldName, object value)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(fieldName, value.ToString());
            if (GUILayout.Button("Missing ITypeDrawer", GUILayout.Height(14)))
            {
                var typeName = TypeGenerator.Generate(type);
                if (EditorUtility.DisplayDialog(
                        "No ITypeDrawer found",
                        "There's no ITypeDrawer implementation to handle the type '" + typeName + "'.\n" +
                        "Providing an ITypeDrawer enables you draw instances for that type.\n\n" +
                        "Do you want to generate an ITypeDrawer implementation for '" + typeName + "'?\n",
                        "Generate",
                        "Cancel"
                    ))
                {
                    generateITypeDrawer(typeName);
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        public static bool CreateDefault(Type type, out object defaultValue)
        {
            try
            {
                defaultValue = Activator.CreateInstance(type);
                return true;
            }
            catch (Exception)
            {
                foreach (var creator in _defaultInstanceCreators)
                {
                    if (creator.HandlesType(type))
                    {
                        defaultValue = creator.CreateDefault(type);
                        return true;
                    }
                }
            }

            var typeName = TypeGenerator.Generate(type);
            if (EditorUtility.DisplayDialog(
                    "No IDefaultInstanceCreator found",
                    "There's no IDefaultInstanceCreator implementation to handle the type '" + typeName + "'.\n" +
                    "Providing an IDefaultInstanceCreator enables you to create instances for that type.\n\n" +
                    "Do you want to generate an IDefaultInstanceCreator implementation for '" + typeName + "'?\n",
                    "Generate",
                    "Cancel"
                ))
            {
                generateIDefaultInstanceCreator(typeName);
            }

            defaultValue = null;
            return false;
        }

        static void generateIDefaultInstanceCreator(string typeName)
        {
            var config = new VisualDebuggingConfig(EntitasPreferencesEditor.LoadConfig());
            var folder = config.defaultInstanceCreatorFolderPath;
            var filePath = folder + "Default_type_InstanceCreator.cs";
            var template = string.Format(DEFAULT_INSTANCE_CREATOR_TEMPLATE_FORMAT, typeName);
            generateTemplate(folder, filePath, template);
        }

        static void generateITypeDrawer(string typeName)
        {
            var config = new VisualDebuggingConfig(EntitasPreferencesEditor.LoadConfig());
            var folder = config.typeDrawerFolderPath;
            var filePath = folder + "Type_TypeDrawer.cs";
            var template = string.Format(TYPE_DRAWER_TEMPLATE_FORMAT, typeName);
            generateTemplate(folder, filePath, template);
        }

        static void generateTemplate(string folder, string filePath, string template)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            File.WriteAllText(filePath, template);
            AssetDatabase.Refresh();
            EditorApplication.isPlaying = false;
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(filePath);
        }

        const string DEFAULT_INSTANCE_CREATOR_TEMPLATE_FORMAT = @"using System;
using Entitas.Unity.VisualDebugging;

// Please rename class name and file name
public class Default_type_InstanceCreator : IDefaultInstanceCreator {{
    public bool HandlesType(Type type) {{
        return type == typeof({0});
    }}

    public object CreateDefault(Type type) {{
        // return your implementation to create an instance of type {0}
        throw new NotImplementedException();
    }}
}}
";

        const string TYPE_DRAWER_TEMPLATE_FORMAT = @"using System;
using Entitas;
using Entitas.Unity.VisualDebugging;

public class Type_TypeDrawer : ITypeDrawer {{
    public bool HandlesType(Type type) {{
        return type == typeof({0});
    }}

    public object DrawAndGetNewValue(Type type, string fieldName, object value, Entity entity, int index, IComponent component) {{
        // return your implementation to draw the type {0}
        throw new NotImplementedException();
    }}
}}";
    }
}

