using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class World : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Vector3 gridRange;
    public Vector2 worldGridCount;
    public GameObject groundPrefab;
    public Button btn;
    void Start()
    {
        groundPrefab = Resources.Load<GameObject>("Ground");


    }

    public void BuildWorld()
    {
        GroundItem[,] grounds = new GroundItem[(int)worldGridCount.x, (int)worldGridCount.y];

        for (int x = 0; x < grounds.GetLength(0); x++)
        {
            for (int y = 0; y < grounds.GetLength(1); y++)
            {
                GameObject go = Instantiate(groundPrefab);
                grounds[x, y] = new GroundItem(new Vector3(gridRange.x * x, 0, gridRange.y * y), gridRange, go);
            }
        }

    }

    public GameObject drapObj;
    public RectTransform rectTransform;
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Ray2D ray2D = new Ray2D(Input.mousePosition, transform.forward);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit);
        Debug.DrawLine(ray.origin * 1000, hit.point);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.enterEventCamera, out pos);
        rectTransform.position = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        Debug.Log("结束拖拽");
    }
}
public class GroundItem
{
    public Vector3 CenterPos;
    public Vector3 GroundSize;
    public BuildItem CurrentBuildItem;
    public GameObject GroundObj;
    public GroundItem(Vector3 centerPos, Vector3 groundSize, GameObject groundObj, BuildItem currentBuildItem = null)
    {
        this.CenterPos = centerPos;
        this.GroundSize = groundSize;
        if (currentBuildItem != null)
        {

            GroundObj.SetActive(false);
        }
        else
        {
            groundObj.transform.localPosition = centerPos;
            groundObj.transform.localRotation = Quaternion.identity;
            groundObj.transform.localScale = groundSize;
        }

    }
}
public class BuildItem
{
    public GameObject BuildObj;


}
