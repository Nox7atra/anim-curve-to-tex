using System;
using UnityEngine;
using UnityEngine.Video;

namespace Nox7atra
{
    public class AnimCurveToTex : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _animationCurveR;
        [SerializeField] private AnimationCurve _animationCurveG;
        [SerializeField] private AnimationCurve _animationCurveB;
        [SerializeField] private TextureWrapMode _wrapMode;
        [Range(1, 2048)]
        [SerializeField] private int _textureResoluton = 128;
        [SerializeField] private Material _targetMaterial;
        [HideInInspector]
        [SerializeField] private string _materialName;
        
        private Texture2D _texture;
        private void OnValidate()
        {
        
        }

        private void OnEnable()
        {
            UpdateCurveTex();
        }
    
        private void UpdateCurveTex()
        {
            if(_targetMaterial == null) return;
            _texture = new Texture2D(_textureResoluton, 1);
            _texture.wrapMode = TextureWrapMode.Clamp;
            _texture.filterMode = FilterMode.Point;
            for (int i = 0; i < _texture.width; i++)
            {
                var phase = i / 255f;
                _texture.SetPixel(i, 0, new Color(
                    _animationCurveR.Evaluate(phase), 
                    _animationCurveG.Evaluate(phase),
                    _animationCurveB.Evaluate(phase)));
            }
            _texture.Apply();
            _targetMaterial.SetTexture(_materialName, _texture);
        }
        private void OnDestroy()
        {
            Destroy(_texture);
        }

    }
}

