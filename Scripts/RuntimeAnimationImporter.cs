using System;
using System.Collections;
using Siccity.GLTFUtility;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

namespace NootyBallScripts.GLBAnimationImporter
{
    public class RuntimeAnimationImporter : MonoBehaviour
    {
        [Tooltip("Do you want this to load right at the start?")]
        [SerializeField] private bool loadOnStart = true;
        [Tooltip("This is where the imported object will be parented to.")]
        public GameObject wrapper;
        [Tooltip("Where to fetch the GLB from.")]
        public string glbToImport = "https://drive.google.com/uc?export=download&id=1xUj_sTZdG0VeatA4XillupwhLNU-kfKl";
        [SerializeField]
        private Vector3 desiredScale = new Vector3(1,1,1);

        [SerializeField] [Tooltip("Shows up while loading")]
        private GameObject loadingVisual;
        // Scriptable Objects that contain animation clip data- not necessarily required.
        public ListOfAnimations possibleAnimationsList;
        // This is for calling animations to play. We have it here so that we can initialize it.
        private ImportedAnimationPlayer _importedAnimationPlayer;
        // Part of the new animation system
        private Animator animator;
        // The imported object
        GameObject result = null;
        // Unity's actual animation player for legacy animations
        private Animation animation;
        // The imported animation clips
        AnimationClip[] animClips;

        public static Action<GameObject> OnGlbImported;
        private void Awake()
        {
            StopAllCoroutines();
        }

        private void Start()
        {
            if(loadOnStart)
                DownloadFile(glbToImport);
        }

        public void DownloadFile(string url)
        {
            StartCoroutine(GetFileRequest(url, (UnityWebRequest req) =>
            {
                if (req.isNetworkError || req.isHttpError)
                {
                    // Log any errors that may happen
                    Debug.Log($"{req.error} : {req.downloadHandler.text}");
                }
                else
                {
                    // Save the model into our wrapper
                    ResetWrapper();
                    GameObject model = result.gameObject;
                    model.transform.SetParent(wrapper.transform);
                    model.transform.localPosition = Vector3.zero;
                    ScaleToFit scaleToFit = model.AddComponent<ScaleToFit>();
                    scaleToFit.desiredScale = desiredScale;
                    if (loadingVisual != null)
                    {
                        loadingVisual.SetActive(false);
                    }
                    model.gameObject.SetActive(true);
                    AddImportedAnimationPlayer();
                    // Broadcast the action
                    OnGlbImported?.Invoke(model);
                    //AddAnimator(model);
                }
            }));
        }

        /// <summary>
        /// This is a part of the new Animator system. It is still a work in progress...
        /// </summary>
        /// <param name="importedModel"></param>
        private void AddAnimator(GameObject importedModel)
        {
            Debug.Log("TRYING TO ADD THE ANIMATOR");
            animator = importedModel.AddComponent<Animator>();

            // Create an Animation Controller asset
            var controller = new UnityEditor.Animations.AnimatorController
            {
                name = "AnimatorController"
            };

            // Create an Animation Clip for each animation in your GLB model                anim = result.AddComponent<Animation>();
            if (animClips.Length > 0)
            {
                foreach (var clip in animClips)
                {
                    // Assign the Animation Clips to the Animation Controller
                    controller.AddMotion(clip as Motion);
                }
            }

            // Set the Animation Controller on the Animator component
            animator.runtimeAnimatorController = controller;

            // Play an animation
            animator.Play("SittingIdle");
        }

        private void AddImportedAnimationPlayer()
        {
            if(animation==null)
                return;
            if (_importedAnimationPlayer == null)
            {
                _importedAnimationPlayer = animation.AddComponent<ImportedAnimationPlayer>();
            }

            if (possibleAnimationsList != null)
            {
                _importedAnimationPlayer.Init(animation, possibleAnimationsList);    
            }
            else
            {
                _importedAnimationPlayer.Init(animation);
            }
            
            
            _importedAnimationPlayer.PlayAnimation(0);
        }

        // Imports the GLTF model with a UnityWebRequest
        IEnumerator GetFileRequest(string url, Action<UnityWebRequest> callback)
        {
            using (UnityWebRequest req = UnityWebRequest.Get(url))
            {
                req.downloadHandler = new DownloadHandlerBuffer();
                yield return req.SendWebRequest();
                if (req.isHttpError || req.isNetworkError)
                {
                    Debug.LogError(req.error);
                    yield break;
                }

                byte[] gltfBytes = req.downloadHandler.data;
                result = Importer.LoadFromBytes(gltfBytes, new ImportSettings(), out animClips);
                result.gameObject.SetActive(false);
                Debug.Log("loading");
                if (loadingVisual != null)
                {
                    loadingVisual.SetActive(true);
                }
                yield return new WaitUntil(()=> result != null);
                // Debug.Log("GLB Loaded");
                // for (int i = 0; i < animClips.Length; i++) {
                //     Debug.Log("Anim " + animClips[i].name+" imported.");
                // }
                if (animClips.Length > 0)
                {
                    animation = result.AddComponent<Animation>();
                    foreach (var clip in animClips)
                    {
                        clip.legacy = true;
                        //Todo: Could set clip to looping here, should depend on the type of animation of course...Perhaps could get the first and last frame? and compare?
                        animation.AddClip(clip,clip.name);
                    }
                }
                callback(req);
            }
        }



        void ResetWrapper()
        {
            if (wrapper != null)
            {
                foreach (Transform trans in wrapper.transform)
                {
                    Destroy(trans.gameObject);
                }
            }
        }
    }
}