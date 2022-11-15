using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Nox7atra
{
    [CustomEditor(typeof(AnimCurveToTex))]
    public class AnimCurveToTex_Editor : Editor
    {
        private AnimCurveToTex _target;
        private SerializedProperty _material;
        private SerializedProperty _texturePropertyName;
        
        public void OnEnable()
        {
            _material = serializedObject.FindProperty("_targetMaterial");
            _texturePropertyName = serializedObject.FindProperty("_texturePropertyName");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var matNames = GetMaterialPropertyNames();
            if (matNames != null)
            {
                var index = matNames.IndexOf(_texturePropertyName.stringValue);
                if (index < 0)
                {
                    index = 0;
                }
                _texturePropertyName.stringValue = matNames[EditorGUILayout.Popup(index, matNames.ToArray())];
                _texturePropertyName.serializedObject.ApplyModifiedProperties();
            }
        }

        public List<string> GetMaterialPropertyNames()
        {
            var shader = (_material.objectReferenceValue as Material)?.shader;
            if (shader == null)
                return null;
            var count = shader.GetPropertyCount();
            List<string> materialPropertyNames = new List<string>();

            for (int i = 0; i < count; i++)
            {
                if (shader.GetPropertyType(i) == ShaderPropertyType.Texture)
                {
                    var property = shader.GetPropertyName(i);
                    if (!materialPropertyNames.Contains(property))
                    {
                        materialPropertyNames.Add(property);
                    }
                }
            }
            return materialPropertyNames;
        }
    }
}