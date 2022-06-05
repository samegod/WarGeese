/// Credit glennpow, Zarlang
/// Sourced from - http://forum.unity3d.com/threads/free-script-particle-systems-in-ui-screen-space-overlay.406862/
/// Updated by Zarlang with a more robust implementation, including TextureSheet annimation support

using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Additions.UI
{
#if UNITY_5_3_OR_NEWER
    [ExecuteInEditMode]
    [RequireComponent(typeof(CanvasRenderer), typeof(ParticleSystem))]
    [AddComponentMenu("UI/Effects/Extensions/UIParticleSystem")]
    public class UIParticleSystem : MaskableGraphic
    {
        [Tooltip("Having this enabled run the system in LateUpdate rather than in Update making it faster but less precise (more clunky)")]
        public bool fixedTime = true;

        [SerializeField] private Shader shaderForParticle;

        private Transform _transform;
        private ParticleSystem _pSystem;
        private ParticleSystem.Particle[] _particles;
        private UIVertex[] _quad = new UIVertex[4];
        private Vector4 _imageUV = Vector4.zero;
        private ParticleSystem.TextureSheetAnimationModule _textureSheetAnimation;
        private int _textureSheetAnimationFrames;
        private Vector2 _textureSheetAnimationFrameSize;
        private ParticleSystemRenderer _pRenderer;

        private Material _currentMaterial;

        private Texture _currentTexture;

#if UNITY_5_5_OR_NEWER
        private ParticleSystem.MainModule _mainModule;
#endif

        public override Texture mainTexture => _currentTexture;

        protected bool Initialize()
        {
            // initialize members
            if (_transform == null)
            {
                _transform = transform;
            }
            if (_pSystem == null)
            {
                _pSystem = GetComponent<ParticleSystem>();

                if (_pSystem == null)
                {
                    return false;
                }

#if UNITY_5_5_OR_NEWER
                _mainModule = _pSystem.main;
                if (_pSystem.main.maxParticles > 14000)
                {
                    _mainModule.maxParticles = 14000;
                }
#else
                    if (pSystem.maxParticles > 14000)
                        pSystem.maxParticles = 14000;
#endif

                _pRenderer = _pSystem.GetComponent<ParticleSystemRenderer>();
                if (_pRenderer != null)
                    _pRenderer.enabled = false;

                Shader foundShader = shaderForParticle;

                Material pMaterial = new Material(foundShader);

                if (material == null)
                    material = pMaterial;

                _currentMaterial = material;
                if (_currentMaterial && _currentMaterial.HasProperty("_MainTex"))
                {
                    _currentTexture = _currentMaterial.mainTexture;
                    if (_currentTexture == null)
                        _currentTexture = Texture2D.whiteTexture;
                }
                material = _currentMaterial;
                // automatically set scaling
#if UNITY_5_5_OR_NEWER
                _mainModule.scalingMode = ParticleSystemScalingMode.Hierarchy;
#else
                    pSystem.scalingMode = ParticleSystemScalingMode.Hierarchy;
#endif

                _particles = null;
            }
#if UNITY_5_5_OR_NEWER
            if (_particles == null)
                _particles = new ParticleSystem.Particle[_pSystem.main.maxParticles];
#else
                if (particles == null)
                    particles = new ParticleSystem.Particle[pSystem.maxParticles];
#endif

            _imageUV = new Vector4(0, 0, 1, 1);

            // prepare texture sheet animation
            _textureSheetAnimation = _pSystem.textureSheetAnimation;
            _textureSheetAnimationFrames = 0;
            _textureSheetAnimationFrameSize = Vector2.zero;
            if (_textureSheetAnimation.enabled)
            {
                _textureSheetAnimationFrames = _textureSheetAnimation.numTilesX * _textureSheetAnimation.numTilesY;
                _textureSheetAnimationFrameSize = new Vector2(1f / _textureSheetAnimation.numTilesX, 1f / _textureSheetAnimation.numTilesY);
            }

            return true;
        }

        protected override void Awake()
        {
            base.Awake();
            if (!Initialize())
                enabled = false;
        }


        protected override void OnPopulateMesh(VertexHelper vh)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                if (!Initialize())
                {
                    return;
                }
            }
#endif
            // prepare vertices
            vh.Clear();

            if (!gameObject.activeInHierarchy)
            {
                return;
            }

            Vector2 temp = Vector2.zero;
            Vector2 corner1 = Vector2.zero;
            Vector2 corner2 = Vector2.zero;
            // iterate through current particles
            int count = _pSystem.GetParticles(_particles);

            for (int i = 0; i < count; ++i)
            {
                ParticleSystem.Particle particle = _particles[i];

                // get particle properties
#if UNITY_5_5_OR_NEWER
                Vector2 position = (_mainModule.simulationSpace == ParticleSystemSimulationSpace.Local ? particle.position : _transform.InverseTransformPoint(particle.position));
#else
                    Vector2 position = (pSystem.simulationSpace == ParticleSystemSimulationSpace.Local ? particle.position : _transform.InverseTransformPoint(particle.position));
#endif
                float rotation = -particle.rotation * Mathf.Deg2Rad;
                float rotation90 = rotation + Mathf.PI / 2;
                Color32 color = particle.GetCurrentColor(_pSystem);
                float size = particle.GetCurrentSize(_pSystem) * 0.5f;

                // apply scale
#if UNITY_5_5_OR_NEWER
                if (_mainModule.scalingMode == ParticleSystemScalingMode.Shape)
                    position /= canvas.scaleFactor;
#else
                    if (pSystem.scalingMode == ParticleSystemScalingMode.Shape)
                        position /= canvas.scaleFactor;
#endif

                // apply texture sheet animation
                Vector4 particleUV = _imageUV;
                if (_textureSheetAnimation.enabled)
                {
#if UNITY_5_5_OR_NEWER
                    float frameProgress = 1 - (particle.remainingLifetime / particle.startLifetime);

                    if (_textureSheetAnimation.frameOverTime.curveMin != null)
                    {
                        frameProgress = _textureSheetAnimation.frameOverTime.curveMin.Evaluate(1 - (particle.remainingLifetime / particle.startLifetime));
                    }
                    else if (_textureSheetAnimation.frameOverTime.curve != null)
                    {
                        frameProgress = _textureSheetAnimation.frameOverTime.curve.Evaluate(1 - (particle.remainingLifetime / particle.startLifetime));
                    }
                    else if (_textureSheetAnimation.frameOverTime.constant > 0)
                    {
                        frameProgress = _textureSheetAnimation.frameOverTime.constant - (particle.remainingLifetime / particle.startLifetime);
                    }
#else
                    float frameProgress = 1 - (particle.lifetime / particle.startLifetime);
#endif

                    frameProgress = Mathf.Repeat(frameProgress * _textureSheetAnimation.cycleCount, 1);
                    int frame = 0;

                    switch (_textureSheetAnimation.animation)
                    {

                        case ParticleSystemAnimationType.WholeSheet:
                            frame = Mathf.FloorToInt(frameProgress * _textureSheetAnimationFrames);
                            break;

                        case ParticleSystemAnimationType.SingleRow:
                            frame = Mathf.FloorToInt(frameProgress * _textureSheetAnimation.numTilesX);

                            int row = _textureSheetAnimation.rowIndex;
                            //                    if (textureSheetAnimation.useRandomRow) { // FIXME - is this handled internally by rowIndex?
                            //                        row = Random.Range(0, textureSheetAnimation.numTilesY, using: particle.randomSeed);
                            //                    }
                            frame += row * _textureSheetAnimation.numTilesX;
                            break;

                    }

                    frame %= _textureSheetAnimationFrames;

                    particleUV.x = (frame % _textureSheetAnimation.numTilesX) * _textureSheetAnimationFrameSize.x;
                    particleUV.y = Mathf.FloorToInt(frame / _textureSheetAnimation.numTilesX) * _textureSheetAnimationFrameSize.y;
                    particleUV.z = particleUV.x + _textureSheetAnimationFrameSize.x;
                    particleUV.w = particleUV.y + _textureSheetAnimationFrameSize.y;
                }

                temp.x = particleUV.x;
                temp.y = particleUV.y;

                _quad[0] = UIVertex.simpleVert;
                _quad[0].color = color;
                _quad[0].uv0 = temp;

                temp.x = particleUV.x;
                temp.y = particleUV.w;
                _quad[1] = UIVertex.simpleVert;
                _quad[1].color = color;
                _quad[1].uv0 = temp;

                temp.x = particleUV.z;
                temp.y = particleUV.w;
                _quad[2] = UIVertex.simpleVert;
                _quad[2].color = color;
                _quad[2].uv0 = temp;

                temp.x = particleUV.z;
                temp.y = particleUV.y;
                _quad[3] = UIVertex.simpleVert;
                _quad[3].color = color;
                _quad[3].uv0 = temp;

                if (rotation == 0)
                {
                    // no rotation
                    corner1.x = position.x - size;
                    corner1.y = position.y - size;
                    corner2.x = position.x + size;
                    corner2.y = position.y + size;

                    temp.x = corner1.x;
                    temp.y = corner1.y;
                    _quad[0].position = temp;
                    temp.x = corner1.x;
                    temp.y = corner2.y;
                    _quad[1].position = temp;
                    temp.x = corner2.x;
                    temp.y = corner2.y;
                    _quad[2].position = temp;
                    temp.x = corner2.x;
                    temp.y = corner1.y;
                    _quad[3].position = temp;
                }
                else
                {
                    // apply rotation
                    Vector2 right = new Vector2(Mathf.Cos(rotation), Mathf.Sin(rotation)) * size;
                    Vector2 up = new Vector2(Mathf.Cos(rotation90), Mathf.Sin(rotation90)) * size;

                    _quad[0].position = position - right - up;
                    _quad[1].position = position - right + up;
                    _quad[2].position = position + right + up;
                    _quad[3].position = position + right - up;
                }

                vh.AddUIVertexQuad(_quad);
            }
        }

        void Update()
        {
            if (!fixedTime && Application.isPlaying)
            {
                _pSystem.Simulate(Time.unscaledDeltaTime, false, false, true);
                SetAllDirty();

                if ((_currentMaterial != null && _currentTexture != _currentMaterial.mainTexture) ||
                    (material != null && _currentMaterial != null && material.shader != _currentMaterial.shader))
                {
                    _pSystem = null;
                    Initialize();
                }
            }
        }

        void LateUpdate()
        {
            if (!Application.isPlaying)
            {
                SetAllDirty();
            }
            else
            {
                if (fixedTime)
                {
                    _pSystem.Simulate(Time.unscaledDeltaTime, false, false, true);
                    SetAllDirty();
                    if ((_currentMaterial != null && _currentTexture != _currentMaterial.mainTexture) ||
                        (material != null && _currentMaterial != null && material.shader != _currentMaterial.shader))
                    {
                        _pSystem = null;
                        Initialize();
                    }
                }
            }
            if (material == _currentMaterial)
                return;
            _pSystem = null;
            Initialize();
        }
    }
#endif
}
