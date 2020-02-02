using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    [Header("操作状态")]
    public bool selected;

    [Header("Cursor材质")]
    public Texture2D cursor_Normal;
    public Texture2D cursor_Search;

    [Header("Audio管理器")]
    public PlayerAudio playerAudio;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        SetCursorToSearch(false);
    }

    void Update()
    {
        if (!GameManager.instance.GameStatu()) return;

        CheckCursor();
        SelectCell();
        CancelSelected();
        CallNucleBasePanel();
    }
        
    void SelectCell() {
        if (selected) {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(ray.origin.x, ray.origin.y), Vector2.zero);
            if (hit && hit.transform.tag.Equals("Cell"))
            {
                playerAudio.Play_ClickCell();
                hit.transform.SendMessage("OnClick");
            }
        }
    }
     
    void CancelSelected() {
        if (!selected) {
            return;
        }

        if (!Input.GetAxis("Mouse ScrollWheel").Equals(0))
        {
            GUIManager.instance.DisableDNAPanel();
        }
    }

    void CallNucleBasePanel() {
        if (!selected)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1)) {
            GUIManager.instance.DisplayNucleBasePanel();
        }
    }

    public void SetSelected(bool _v) {
        selected = _v;
    }

    public void CheckCursor() {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(ray.origin.x, ray.origin.y), Vector2.zero);
        if (hit && hit.transform.tag.Equals("Cell"))
        {
            SetCursorToSearch(true);
        }
        else SetCursorToSearch(false);
    }

    public void SetCursorToSearch(bool _v) {
        if (_v) {
            Cursor.SetCursor(cursor_Search, Vector2.zero, CursorMode.Auto);
        }
        else {
            Cursor.SetCursor(cursor_Normal, Vector2.zero, CursorMode.Auto);
        }
    }
}
