using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

namespace TappyTale
{
    public class CutSceneController : MonoBehaviour
    {
        public PlayableDirector Director;

        void Start()
        {
            if (Director == null) Director = gameObject.AddComponent<PlayableDirector>();
            var timeline = ScriptableObject.CreateInstance<TimelineAsset>();
            Director.playableAsset = timeline;

            // Add a camera animation track (simple yaw)
            var track = timeline.CreateTrack<AnimationTrack>(null, "CameraTrack");
            var cam = Camera.main != null ? Camera.main.gameObject : new GameObject("Main Camera", typeof(Camera));
            var clip = track.CreateDefaultClip();
            var curveClip = new AnimationClip();
            var curve = AnimationCurve.EaseInOut(0, 0, 3, 30);
            curveClip.SetCurve("", typeof(Transform), "localEulerAngles.y", curve);
            (clip.asset as AnimationPlayableAsset).clip = curveClip;
            Director.SetGenericBinding(track, cam.GetComponent<Animator>());

            Director.stopped += OnStopped;
            Director.Play();
        }

        void Update()
        {
            if (UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Director.time = Director.duration;
                Director.Evaluate();
                Director.Stop();
            }
        }

        void OnStopped(PlayableDirector d)
        {
            SceneManager.LoadScene(SceneNames.Arena);
        }
    }
}
