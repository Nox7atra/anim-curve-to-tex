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
        private SerializedProperty _materialName;
        
        public void OnEnable()
        {
            _material = serializedObject.FindProperty("_targetMaterial");
            _materialName = serializedObject.FindProperty("_materialName");

        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var matNames = GetMaterialPropertyNames();
            if (matNames != null)
            {
                var index = matNames.IndexOf(_materialName.stringValue);
                if (index < 0)
                {
                    index = 0;
                }
                _materialName.stringValue = matNames[EditorGUILayout.Popup(index, matNames.ToArray())];
                _materialName.serializedObject.ApplyModifiedProperties();
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