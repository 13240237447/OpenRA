using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Util
{
    public class UIRoot
    {
        public static GameObject Instance { private set; get; }
        
        public static Canvas RootCanvas { private set; get; }
        
        public static Camera UICamera { private set; get; }
        public static GameObject PoolRoot { private set; get; }
        
        public static GameObject BgMask { private set; get; }
        
        public static GameObject ClickMask { private set; get; }

        private static int uiLayer;
        
        /// <summary>
        /// 点击遮罩的层级
        /// </summary>
        private static readonly short clickBgLayerSort = 32767 - 10;
        
        public static void Init()
        {
            Instance = new GameObject("UI");
            GameObject.DontDestroyOnLoad(Instance);
            uiLayer = LayerMask.NameToLayer("UI");
            RootCanvas = new GameObject("RootCanvas").AddComponent<Canvas>();
            RootCanvas.transform.SetParent(Instance.transform);
            RootCanvas.gameObject.layer = uiLayer;
            
            PoolRoot = new GameObject("UIPool");
            PoolRoot.transform.SetParent(Instance.transform);
            PoolRoot.SetActive(false);

            InitCamera();
            var canvasScaler = RootCanvas.gameObject.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            canvasScaler.referenceResolution = new Vector2(1920, 1080);
            
            var eventSystem = EventSystem.current;
            if (eventSystem == null)
            {
                var eventSystemObj = new GameObject("UI_EventSystem");
                eventSystem = eventSystemObj.GetOrAddComponent<EventSystem>();
                eventSystem.gameObject.SetActive(false);
                eventSystem.gameObject.SetActive(true);
                eventSystem.transform.SetParent(Instance.transform);
            }
            else
            {
                eventSystem.transform.SetParent(Instance.transform);
            }

            InitWindowMask();
            InitClickMask();
        }

        private static void InitCamera()
        {
            GameObject objCam = new GameObject("UICamera");
            objCam.layer = uiLayer;
            objCam.transform.parent = RootCanvas.transform.parent;
            var uiCamera = objCam.AddComponent<Camera>();
            uiCamera.depth = 6;
            uiCamera.backgroundColor = Color.black;
            uiCamera.cullingMask = 1 << uiLayer | 1 << uiLayer;
            uiCamera.clearFlags = CameraClearFlags.Depth;

            //Use Simple2D Camera
            uiCamera.orthographicSize = 100f;
            uiCamera.orthographic = true;
            uiCamera.nearClipPlane = -2f;
            uiCamera.farClipPlane = 200f; //distance of canvas in screen space camera mode is 100

            //Disable extra functions
            uiCamera.allowHDR = false;
            uiCamera.allowMSAA = false;
            uiCamera.useOcclusionCulling = false;

            RootCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            RootCanvas.worldCamera = uiCamera;

            UICamera = uiCamera;
        }
        
        private static void InitWindowMask()
        {
            BgMask = new GameObject("BgMask", new Type[] { typeof(Canvas), typeof(GraphicRaycaster) });
            BgMask.layer = uiLayer;
            BgMask.transform.SetParent(RootCanvas.transform, false);
            
            Canvas canvas = BgMask.GetOrAddComponent<Canvas>();
            canvas.overrideSorting = true;
            BgMask.GetOrAddComponent<GraphicRaycaster>();
            
            RectTransform rectTransform = BgMask.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            
            GameObject maskImg = new GameObject("BackGround", typeof(Image));
            maskImg.GetComponent<Image>().color = new Color(0, 0, 0, 0.7f);
            maskImg.transform.SetParent(BgMask.transform, false);
            maskImg.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
            maskImg.transform.localScale = Vector3.one * 2;
            maskImg.layer = uiLayer;
            BgMask.gameObject.SetActive(false);
        }

        private static void InitClickMask()
        {
            ClickMask = new GameObject("ClickMask", new Type[] { typeof(Canvas), typeof(GraphicRaycaster) });
            ClickMask.layer = uiLayer;
            ClickMask.transform.SetParent(RootCanvas.transform, false);
            
            Canvas canvas = ClickMask.GetOrAddComponent<Canvas>();
            canvas.overrideSorting = true;
            canvas.sortingOrder = clickBgLayerSort;
            ClickMask.GetOrAddComponent<GraphicRaycaster>();
            
            RectTransform rectTransform = ClickMask.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            
            GameObject maskImg = new GameObject("BackGround", typeof(Image));
            maskImg.GetComponent<Image>().color = Color.clear;
            maskImg.transform.SetParent(ClickMask.transform, false);
            maskImg.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
            maskImg.transform.localScale = Vector3.one * 2;
            maskImg.layer = uiLayer;
            ClickMask.gameObject.SetActive(false);
        }
    }
}