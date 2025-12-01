using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace TappyTale
{
    public class MainMenuController : MonoBehaviour
    {

        // Awake(), check for eventsystem and camera, if not add it here:
        void Awake()
        {
            // Ensure there is an EventSystem in the scene
            if (FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
            {
                var eventSystemGO = new GameObject("EventSystem");
                eventSystemGO.AddComponent<UnityEngine.EventSystems.EventSystem>();
                eventSystemGO.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
            }

            // Ensure there is a Camera in the scene
            if (Camera.main == null)
            {
                var cameraGO = new GameObject("MainCamera");
                var camera = cameraGO.AddComponent<Camera>();
                camera.tag = "MainCamera";
                camera.clearFlags = CameraClearFlags.SolidColor;
                camera.backgroundColor = Color.black; // Set background color to black
                camera.orthographic = true;
            }
            else
            {
                // Set existing main camera to orthographic and black background
                Camera.main.clearFlags = CameraClearFlags.SolidColor;
                Camera.main.backgroundColor = Color.black;
                Camera.main.orthographic = true;
            }
        }


#if TT_IS_DEBUG_MODE
        void Start()
        {
            // Build minimalist menu UI at runtime
            var canvasGO = new GameObject("MainMenuCanvas");
            var canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();

            // Cavas Scaler setup
            var canvasScaler = canvasGO.GetComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080);

            var titleGO = new GameObject("Title");
            titleGO.transform.SetParent(canvasGO.transform);
            var title = titleGO.AddComponent<TextMeshProUGUI>();
            // Game title retrieve from Playe's product name:
            title.text = Application.productName;
            title.fontSize = 48;
            title.alignment = TextAlignmentOptions.Center;
            title.rectTransform.anchorMin = new Vector2(0.5f, 0.8f);
            title.rectTransform.anchorMax = new Vector2(0.5f, 0.8f);
            title.rectTransform.anchoredPosition = Vector2.zero;

            // TODO: Add proper menu UI design in Unity Editor later.
            CreateButton(canvasGO.transform, "Start Run", new Vector2(0.5f, 0.6f), () => SceneManager.LoadScene(SceneNames.CutScene));
            CreateButton(canvasGO.transform, "Cosmetics", new Vector2(0.5f, 0.5f), () => { Debug.Log("Open cosmetics"); /* open cosmetics UI stub */});
            CreateButton(canvasGO.transform, "Quit", new Vector2(0.5f, 0.4f), () => Application.Quit());

            // Note: Application.Quit() does not work in the editor.
            // Buttons should be spaced out properly in a real implementation.
            // This is just a minimalist runtime UI for demonstration purposes.
            // In a real game, the main menu would be designed in the Unity Editor with proper UI layout.   

            // Debug.Log to confirm button creation
            Debug.Log("[TAPPY TALE | debug mode] Main Menu UI created at runtime.");
        }


        void CreateButton(Transform parent, string label, Vector2 anchor, System.Action onClick)
        {
            var go = new GameObject(label);
            go.transform.SetParent(parent);
            var img = go.AddComponent<Image>();
            img.color = new Color(1, 1, 1, 0.1f);
            var rt = img.rectTransform;
            rt.anchorMin = anchor; rt.anchorMax = anchor;
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.sizeDelta = new Vector2(260, 60);

            var txtGO = new GameObject("Text");
            txtGO.transform.SetParent(go.transform);
            var txt = txtGO.AddComponent<TextMeshProUGUI>();
            txt.text = label; txt.fontSize = 28;
            txt.alignment = TextAlignmentOptions.Center;
            txt.rectTransform.anchorMin = new Vector2(0, 0);
            txt.rectTransform.anchorMax = new Vector2(1, 1);
            txt.rectTransform.offsetMin = Vector2.zero;
            txt.rectTransform.offsetMax = Vector2.zero;

            var btn = go.AddComponent<Button>();
            btn.onClick.AddListener(() => onClick?.Invoke());
            Debug.Log($"Created button: {label}");
        }
#endif
    }
}
