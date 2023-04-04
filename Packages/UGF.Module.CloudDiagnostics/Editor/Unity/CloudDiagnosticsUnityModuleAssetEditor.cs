using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.CloudDiagnostics.Runtime.Unity;
using UnityEditor;

namespace UGF.Module.CloudDiagnostics.Editor.Unity
{
    [CustomEditor(typeof(CloudDiagnosticsUnityModuleAsset), true)]
    internal class CloudDiagnosticsUnityModuleAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyEnableCaptureExceptions;
        private SerializedProperty m_propertyLogBufferSize;
        private ReorderableListKeyAndValueDrawer m_listMetadata;

        private void OnEnable()
        {
            m_propertyEnableCaptureExceptions = serializedObject.FindProperty("m_enableCaptureExceptions");
            m_propertyLogBufferSize = serializedObject.FindProperty("m_logBufferSize");

            m_listMetadata = new ReorderableListKeyAndValueDrawer(serializedObject.FindProperty("m_metadata"))
            {
                DisplayLabels = true
            };

            m_listMetadata.Enable();
        }

        private void OnDisable()
        {
            m_listMetadata.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertyEnableCaptureExceptions);
                EditorGUILayout.PropertyField(m_propertyLogBufferSize);

                m_listMetadata.DrawGUILayout();
            }
        }
    }
}
