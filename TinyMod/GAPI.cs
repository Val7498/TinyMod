using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

namespace TinyMod
{
    public class GAPI
    {
        GameObject CanvasObj, OutputObj, ListObj;
        public List<ModEntry> Entries = new List<ModEntry>();
        Text txt;
        Font font;
        string logPath;
        public GAPI()
        {
            logPath = Path.Combine(Application.dataPath, "TinyMod.log");
            font = Font.CreateDynamicFontFromOSFont("Arial", 10);
            setupCanvas();
            setupOutput();
            setupList();
        }

        void setupCanvas()
        {
            //Canvas canvas = GameObject.FindObjectOfType<Canvas>();
            //If CanvasObj isnt null then no need to initialize a new canvas
            //if (canvas != null)
            //{
             //   CanvasObj = canvas.gameObject;
            //    return;
            //}
            CanvasObj = new GameObject("ML Canvas");
            CanvasObj.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            CanvasScaler scaler = CanvasObj.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
            scaler.dynamicPixelsPerUnit = 0.8f;
            CanvasObj.AddComponent<GraphicRaycaster>();
        }

        #region ModList
        public class ModEntry
        {
            public GameObject parentObj, label, desc;
            public Text labelText, descText;
        }
        void setupList()
        {
            ListObj = new GameObject("Menu List");
            RectTransform mrt = ListObj.AddComponent<RectTransform>();
            mrt.SetParent(CanvasObj.transform);
            mrt.pivot = Vector2.right;
            mrt.anchorMax = Vector2.right;
            mrt.anchorMin = Vector2.right;
            mrt.sizeDelta = new Vector2(250, 350);
            mrt.anchoredPosition = new Vector2(-20, 20);

            VerticalLayoutGroup pvlg = ListObj.AddComponent<VerticalLayoutGroup>();
            pvlg.childForceExpandHeight = false;
            pvlg.childControlHeight = false;
            pvlg.childScaleHeight = true;
        }
        public void addModEntry(string Label, string Description, Color color)
        {
            ModEntry entry = new ModEntry();

            entry.parentObj = new GameObject("entry");
            entry.label = new GameObject(Label);
            entry.desc = new GameObject(Description);

            RectTransform prt = entry.parentObj.AddComponent<RectTransform>();
            prt.SetParent(ListObj.transform);
            prt.sizeDelta = new Vector2(250, 70);
            prt.anchoredPosition = new Vector2(0, 0);
            prt.pivot = new Vector2(0, 1);
            prt.anchorMax = new Vector2(0, 1);
            prt.anchorMin = new Vector2(0, 1);
            RectTransform lrt = entry.label.AddComponent<RectTransform>();
            lrt.SetParent(prt);
            lrt.sizeDelta = new Vector2(160, 22);
            lrt.anchoredPosition = new Vector2(5, 0);
            lrt.pivot = new Vector2(0, 1);
            lrt.anchorMax = new Vector2(0, 1);
            lrt.anchorMin = new Vector2(0, 1);

            RectTransform drt = entry.desc.AddComponent<RectTransform>();
            drt.SetParent(prt);
            drt.sizeDelta = new Vector2(240, 42);
            drt.anchoredPosition = new Vector2(5, -22);
            drt.pivot = new Vector2(0, 1);
            drt.anchorMax = new Vector2(0, 1);
            drt.anchorMin = new Vector2(0, 1);


            Image background = entry.parentObj.AddComponent<Image>();
            entry.labelText = entry.label.AddComponent<Text>();
            entry.descText = entry.desc.AddComponent<Text>();

            //Setting Opacity to 25%
            color.a = 0.25f;

            background.color = color;
            entry.labelText.text = Label;
            entry.labelText.font = font;
            entry.labelText.fontSize = 19;
            entry.labelText.fontStyle = FontStyle.Bold;
            entry.descText.text = Description;
            entry.descText.font = font;
            entry.descText.fontSize = 10;
            entry.descText.resizeTextForBestFit = true;
            entry.descText.resizeTextMinSize = 10;
            entry.descText.resizeTextMinSize = 15;


            Entries.Add(entry);
        }
        #endregion

        #region Output

        void setupOutput()
        {
            OutputObj = new GameObject("Output");
            RectTransform mrt = OutputObj.AddComponent<RectTransform>();
            mrt.SetParent(CanvasObj.transform);
            mrt.pivot = Vector2.one;
            mrt.anchorMax = Vector2.one;
            mrt.anchorMin = Vector2.one;
            mrt.sizeDelta = new Vector2(350, 300);
            mrt.anchoredPosition = new Vector2(-20, -20);

            txt = OutputObj.AddComponent<Text>();
            txt.verticalOverflow = VerticalWrapMode.Overflow;
            txt.alignment = TextAnchor.LowerLeft;
            txt.font = font;
            txt.fontSize = 14;
        }
        public void deleteOld()
        {
            string currentText = txt.text;
            int index = currentText.IndexOf("\n");
            currentText = currentText.Substring(index + 1);
            txt.text = currentText;
        }
        public void deleteAll()
        {
            txt.text = "";
        }

        //Supports Unity Rich Text https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/StyledText.html
        public void Log(string info)
        {
            //using (StreamWriter streamWriter = new StreamWriter(logPath))
            //{
            //    streamWriter.WriteLine(info) ;
            //    streamWriter.Close();
            //}
            Debug.Log(info);
            txt.text += info + "\n";
        }
        public void LogInfo(string info)
        {
            Log("<color=yellow>" + info + "</color>");
        }
        public void LogError(string info)
        {
            Log("<color=red>" + info + "</color>");
        }
        #endregion

        //Returns the current active GAPI instance in Unity
        //Please use this once
        //public static GAPI getInstance()
        //{
        //    GAPI[] instances = GameObject.FindObjectsOfType<GAPI>();
        //    if (instances.Length > 1)
        //    {
        //        throw new Exception("More than one instance of GAPI, there may be more than one instance of TinyMod installed!");
        //    }

        //    if(instances.Length == 0)
        //    {
        //        throw new Exception("TinyMod has not finished Initializing GAPI!");
        //    }

        //    return instances[0];
        //}

    }
}