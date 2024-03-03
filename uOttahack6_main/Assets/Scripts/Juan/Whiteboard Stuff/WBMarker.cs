using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WBMarker : MonoBehaviour
{
    [SerializeField] private Transform _tip;
    [SerializeField] private int _radius = 5;

    private Renderer _renderer;
    private Color[] _colors;
    private float _tipHeight;

    private RaycastHit _touch;
    private Whiteboard _whiteboard;
    private Vector2 _touchPos;
    private bool _touchedLastF;

    private Vector2 _lastTouchPos;
    private Quaternion _lastTouchRot;
    
    public bool isEraser = false;


    void Start()
    {
        _renderer = _tip.GetComponent<Renderer>();
        _colors = (isEraser) ? new Color[_radius * _radius] : Enumerable.Repeat(_renderer.material.color, _radius * _radius).ToArray();
        _tipHeight = _tip.localScale.y;
    }

    // Update is called once per frame
    void Draw()
    {
        if (Physics.Raycast(_tip.position, transform.right, out _touch, _tipHeight * 2) &&
            _touch.transform.CompareTag("Whiteboard"))
        {
            // Debug.Log("Raycast hit whiteboard!");
            if (_whiteboard is null)
            {
                _whiteboard = _touch.transform.GetComponent<Whiteboard>();
            }

            _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

            var x = (int)(_touchPos.x * _whiteboard.texSize.x - (_radius / 2));
            var y = (int)(_touchPos.y * _whiteboard.texSize.y - (_radius / 2));

            if (y < 0 || y > _whiteboard.texSize.y || x < 0 || x > _whiteboard.texSize.x - _radius) return;

            if (_touchedLastF)
            {
                _whiteboard.tex.SetPixels(x, y, _radius, _radius, _colors);

                for (float f = 0.01f; f < 1.00; f += 0.03f)
                {
                    var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                    var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                    _whiteboard.tex.SetPixels(lerpX, lerpY, _radius, _radius, _colors);
                }

                transform.rotation = _lastTouchRot;
                _whiteboard.tex.Apply();
            }

            _lastTouchPos = new Vector2(x, y);
            _lastTouchRot = transform.rotation;
            _touchedLastF = true;
            return;
        }

        _touchedLastF = false;
        _whiteboard = null;
    }

    void Update()
    {
        Draw();
    }
}