using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace TappyTale
{
    public class EndScreenController : MonoBehaviour
    {
        public bool Victory;

        void Start()
        {
            var canvasGO = new GameObject("EndCanvas");
            var canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();

            var titleGO = new GameObject("Title");
            titleGO.transform.SetParent(canvasGO.transform);
            var title = titleGO.AddComponent<TextMeshProUGUI>();
            title.text = Victory ? "VICTORY!" : "DEFEAT";
            title.fontSize = 64;
            title.alignment = TextAlignmentOptions.Center;
            title.rectTransform.anchorMin = new Vector2(0.5f, 0.7f);
            title.rectTransform.anchorMax = new Vector2(0.5f, 0.7f);
            title.rectTransform.anchoredPosition = Vector2.zero;

            CreateButton(canvasGO.transform, "Retry", new Vector2(0.5f, 0.45f), () => SceneManager.LoadScene(SceneNames.Arena));
            CreateButton(canvasGO.transform, "Main Menu", new Vector2(0.5f, 0.35f), () => SceneManager.LoadScene(SceneNames.MainMenu));
        }

        void CreateButton(Transform parent, string label, Vector2 anchor, System.Action onClick)
        {
            var go = new GameObject(label);
            go.transform.SetParent(parent);
            var img = go.AddComponent<Image>();
            img.color = new Color(1,1,1,0.1f);
            var rt = img.rectTransform;
            rt.anchorMin = anchor; rt.anchorMax = anchor; rt.pivot = new Vector2(0.5f,0.5f);
            rt.sizeDelta = new Vector2(260, 60);

            var txtGO = new GameObject("Text");
            txtGO.transform.SetParent(go.transform);
            var txt = txtGO.AddComponent<TextMeshProUGUI>();
            txt.text = label; txt.fontSize = 28; txt.alignment = TextAlignmentOptions.Center;
            txt.rectTransform.anchorMin = new Vector2(0,0); txt.rectTransform.anchorMax = new Vector2(1,1);
            txt.rectTransform.offsetMin = Vector2.zero; txt.rectTransform.offsetMax = Vector2.zero;

            var btn = go.AddComponent<Button>();
            btn.onClick.AddListener(()=> onClick?.Invoke());
        }
    }
}
