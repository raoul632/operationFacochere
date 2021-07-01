using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror; 

public class UIinfoPlayer : NetworkBehaviour
{

    [SerializeField]private GameObject _GOcanvas;
    [SerializeField] private Text _text;
    private GameObject _GOtext;
    private Canvas _canvas;
    private RectTransform rectTransform;

    [SyncVar(hook = nameof(HandleSetDisplayName))]
    [SerializeField]
    private string displayName = "Missing Name"; 

    // Start is called before the first frame update
    [Server]
    void Start()
    {
        #region createCanvas
        _GOcanvas.AddComponent<Canvas>();
        _GOcanvas.name = "Text Canvas";
        _GOcanvas.AddComponent<CanvasScaler>();
        _GOcanvas.AddComponent<GraphicRaycaster>();


        _canvas = _GOcanvas.GetComponent<Canvas>();
        _canvas.renderMode = RenderMode.WorldSpace;
        _canvas.gameObject.transform.position = new Vector3(0, 0, 0);

        RectTransform canvasTransform = _canvas.GetComponent<RectTransform>();
        canvasTransform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

        _GOtext = new GameObject();
        _GOtext.transform.parent = _GOcanvas.transform;
        _GOtext.name = "wibble";

        _text = _GOtext.AddComponent<Text>();
        _text.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        _text.text = displayName;
        _text.fontSize = 50;

        rectTransform = _text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.sizeDelta = new Vector2(200, 200);
        rectTransform.localScale = new Vector3(1f, 1f, 1f);
        #endregion

        #region testreseaux
        _GOcanvas.AddComponent<NetworkIdentity>();
        _GOcanvas.AddComponent<NetworkTransform>();
        NetworkServer.Spawn(_GOcanvas);
        #endregion
    }

    [Server]
    public void SetName(string text)
    {
        displayName = text; 
    }

    public void SetPosition(Vector3 pos)
    {
        _canvas.gameObject.transform.position = pos; 
    }

    private void HandleSetDisplayName(string oldname, string newname)
    {
        if (_text != null)
        {
            _text.text = newname;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
