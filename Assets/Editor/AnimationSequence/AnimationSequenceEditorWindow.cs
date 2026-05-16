using System;
using Game.Animation;
using UnityEditor;
using UnityEngine;

namespace Game.Editor.Animation
{
    /// <summary>
    /// 动画序列编辑窗口, 只负责编辑 ScriptableObject 数据, 不写具体播放逻辑.
    /// </summary>
    public class AnimationSequenceEditorWindow : EditorWindow
    {
        private const string DefaultFolder = "Assets/GameDataSO/DOTweenAnimationSequence";
        private const string StepsPropertyName = "steps";

        private AnimationSequenceAsset currentAsset;
        private SerializedObject serializedAsset;
        private Vector2 scrollPosition;
        private string newAssetName = "NewAnimationSequence";
        private Transform targetPathRoot;
        private AnimationEffectType addEffectType = AnimationEffectType.FadeIn;

        [MenuItem("Tools/Animation Sequence Editor")]
        public static void Open()
        {
            GetWindow<AnimationSequenceEditorWindow>("Animation Sequence");
        }

        private void OnGUI()
        {
            DrawAssetToolbar();

            if (currentAsset == null)
            {
                EditorGUILayout.HelpBox("请选择或创建一个 AnimationSequenceAsset.", MessageType.Info);
                return;
            }

            EnsureSerializedAsset();
            serializedAsset.Update();

            DrawBatchTools();
            DrawSteps();

            serializedAsset.ApplyModifiedProperties();
        }

        private void DrawAssetToolbar()
        {
            EditorGUILayout.LabelField("动画资产", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();
            currentAsset = (AnimationSequenceAsset)EditorGUILayout.ObjectField("当前资产", currentAsset, typeof(AnimationSequenceAsset), false);
            if (EditorGUI.EndChangeCheck())
            {
                serializedAsset = currentAsset != null ? new SerializedObject(currentAsset) : null;
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                newAssetName = EditorGUILayout.TextField("新资产名", newAssetName);
                if (GUILayout.Button("创建", GUILayout.Width(80f)))
                {
                    CreateNewAsset();
                }
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("保存资产"))
                {
                    SaveCurrentAsset();
                }

                if (GUILayout.Button("定位资产"))
                {
                    Selection.activeObject = currentAsset;
                    EditorGUIUtility.PingObject(currentAsset);
                }
            }

            EditorGUILayout.Space(8f);
        }

        private void DrawBatchTools()
        {
            EditorGUILayout.LabelField("批量添加", EditorStyles.boldLabel);

            targetPathRoot = (Transform)EditorGUILayout.ObjectField("路径根节点", targetPathRoot, typeof(Transform), true);
            addEffectType = (AnimationEffectType)EditorGUILayout.EnumPopup("添加效果", addEffectType);

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("添加空步骤"))
                {
                    AddStep(null, addEffectType);
                }

                if (GUILayout.Button("从当前 Selection 批量添加"))
                {
                    AddStepsFromSelection();
                }
            }

            EditorGUILayout.HelpBox("场景对象引用不会稳定保存到项目资产中, 窗口会同步写入层级路径供 AnimationPlayer 运行时解析.", MessageType.None);
            EditorGUILayout.Space(8f);
        }

        private void DrawSteps()
        {
            var steps = serializedAsset.FindProperty(StepsPropertyName);
            EditorGUILayout.LabelField($"动画步骤: {steps.arraySize}", EditorStyles.boldLabel);

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            var changedList = false;

            for (var i = 0; i < steps.arraySize; i++)
            {
                var step = steps.GetArrayElementAtIndex(i);
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField($"Step {i + 1}", EditorStyles.boldLabel);

                    GUI.enabled = i > 0;
                    if (GUILayout.Button("上移", GUILayout.Width(48f)))
                    {
                        steps.MoveArrayElement(i, i - 1);
                        changedList = true;
                    }

                    GUI.enabled = i < steps.arraySize - 1;
                    if (GUILayout.Button("下移", GUILayout.Width(48f)))
                    {
                        steps.MoveArrayElement(i, i + 1);
                        changedList = true;
                    }

                    GUI.enabled = true;
                    if (GUILayout.Button("删除", GUILayout.Width(48f)))
                    {
                        steps.DeleteArrayElementAtIndex(i);
                        changedList = true;
                    }
                }

                if (changedList)
                {
                    EditorGUILayout.EndVertical();
                    break;
                }

                DrawStepFields(step);
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space(4f);
            }

            EditorGUILayout.EndScrollView();
        }

        private void DrawStepFields(SerializedProperty step)
        {
            var targetProperty = step.FindPropertyRelative("target");
            var targetPathProperty = step.FindPropertyRelative("targetPath");
            var effectTypeProperty = step.FindPropertyRelative("effectType");

            EditorGUI.BeginChangeCheck();
            var target = (GameObject)EditorGUILayout.ObjectField("目标物体", targetProperty.objectReferenceValue, typeof(GameObject), true);
            if (EditorGUI.EndChangeCheck())
            {
                targetProperty.objectReferenceValue = target;
                targetPathProperty.stringValue = target != null ? AnimationStepData.BuildTargetPath(target.transform, targetPathRoot) : string.Empty;
            }

            EditorGUILayout.PropertyField(targetPathProperty, new GUIContent("目标路径"));
            EditorGUILayout.PropertyField(step.FindPropertyRelative("startupActiveState"), new GUIContent("开始激活状态"));
            EditorGUILayout.PropertyField(effectTypeProperty, new GUIContent("效果类型"));
            EditorGUILayout.PropertyField(step.FindPropertyRelative("duration"), new GUIContent("持续时间"));
            EditorGUILayout.PropertyField(step.FindPropertyRelative("delay"), new GUIContent("延迟"));
            EditorGUILayout.PropertyField(step.FindPropertyRelative("ease"), new GUIContent("Ease"));

            DrawEffectParams(step, (AnimationEffectType)effectTypeProperty.enumValueIndex);
        }

        private void DrawEffectParams(SerializedProperty step, AnimationEffectType effectType)
        {
            switch (effectType)
            {
                case AnimationEffectType.FadeIn:
                case AnimationEffectType.FadeOut:
                    EditorGUILayout.PropertyField(step.FindPropertyRelative("autoAddCanvasGroup"), new GUIContent("自动添加 CanvasGroup"));
                    break;
                case AnimationEffectType.SlideUp:
                    EditorGUILayout.PropertyField(step.FindPropertyRelative("slideOffset"), new GUIContent("起始偏移"));
                    break;
                case AnimationEffectType.Shake:
                    EditorGUILayout.PropertyField(step.FindPropertyRelative("shakeStrength"), new GUIContent("抖动强度"));
                    EditorGUILayout.PropertyField(step.FindPropertyRelative("shakeVibrato"), new GUIContent("震动次数"));
                    EditorGUILayout.PropertyField(step.FindPropertyRelative("shakeRandomness"), new GUIContent("随机角度"));
                    break;
                case AnimationEffectType.ScaleIn:
                    EditorGUILayout.PropertyField(step.FindPropertyRelative("scaleFromMultiplier"), new GUIContent("起始缩放倍率"));
                    break;
                case AnimationEffectType.ScaleOut:
                    EditorGUILayout.PropertyField(step.FindPropertyRelative("scaleToMultiplier"), new GUIContent("目标缩放倍率"));
                    break;
                case AnimationEffectType.MoveTo:
                    EditorGUILayout.PropertyField(step.FindPropertyRelative("moveOffset"), new GUIContent("相对位移"));
                    break;
                case AnimationEffectType.Rotate:
                    EditorGUILayout.PropertyField(step.FindPropertyRelative("rotationEuler"), new GUIContent("旋转角度"));
                    break;
            }
        }

        private void AddStep(GameObject target, AnimationEffectType effectType)
        {
            Undo.RecordObject(currentAsset, "Add Animation Step");
            currentAsset.AddStep(new AnimationStepData(target, effectType, targetPathRoot));
            EditorUtility.SetDirty(currentAsset);
            EnsureSerializedAsset();
            serializedAsset.Update();
        }

        private void AddStepsFromSelection()
        {
            var selectedObjects = Selection.gameObjects;
            if (selectedObjects == null || selectedObjects.Length == 0)
            {
                EditorUtility.DisplayDialog("Animation Sequence", "当前没有选中的 GameObject.", "OK");
                return;
            }

            Array.Sort(selectedObjects, CompareSelectedObjects);
            Undo.RecordObject(currentAsset, "Batch Add Animation Steps");
            foreach (var selected in selectedObjects)
            {
                currentAsset.AddStep(new AnimationStepData(selected, addEffectType, targetPathRoot));
            }

            EditorUtility.SetDirty(currentAsset);
            EnsureSerializedAsset();
            serializedAsset.Update();
        }

        private int CompareSelectedObjects(GameObject left, GameObject right)
        {
            var leftPath = left != null ? AnimationStepData.BuildTargetPath(left.transform, targetPathRoot) : string.Empty;
            var rightPath = right != null ? AnimationStepData.BuildTargetPath(right.transform, targetPathRoot) : string.Empty;
            return string.Compare(leftPath, rightPath, StringComparison.Ordinal);
        }

        private void CreateNewAsset()
        {
            EnsureDefaultFolder();

            var asset = CreateInstance<AnimationSequenceAsset>();
            var safeName = string.IsNullOrWhiteSpace(newAssetName) ? "NewAnimationSequence" : newAssetName.Trim();
            var path = AssetDatabase.GenerateUniqueAssetPath($"{DefaultFolder}/{safeName}.asset");
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();

            currentAsset = asset;
            serializedAsset = new SerializedObject(currentAsset);
            Selection.activeObject = currentAsset;
            EditorGUIUtility.PingObject(currentAsset);
        }

        private void SaveCurrentAsset()
        {
            if (currentAsset == null) return;

            serializedAsset?.ApplyModifiedProperties();
            EditorUtility.SetDirty(currentAsset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void EnsureSerializedAsset()
        {
            if (currentAsset != null && (serializedAsset == null || serializedAsset.targetObject != currentAsset))
            {
                serializedAsset = new SerializedObject(currentAsset);
            }
        }

        private static void EnsureDefaultFolder()
        {
            if (!AssetDatabase.IsValidFolder("Assets/GameDataSO"))
            {
                AssetDatabase.CreateFolder("Assets", "GameDataSO");
            }

            if (!AssetDatabase.IsValidFolder(DefaultFolder))
            {
                AssetDatabase.CreateFolder("Assets/GameDataSO", "DOTweenAnimationSequence");
            }
        }
    }
}
