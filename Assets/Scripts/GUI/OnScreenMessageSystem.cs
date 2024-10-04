using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnScreenMessage
{
    public GameObject textGO;
    public float appearTime;

    public OnScreenMessage(GameObject go)
    {
        textGO = go;
    }
}

public class OnScreenMessageSystem : MonoBehaviour
{
    [SerializeField] GameObject textPrefab;
    [SerializeField] float horizontalScatter = 0.5f;
    [SerializeField] float verticalScatter = 1f;
    [SerializeField] float appearTime = 3f;

    List<OnScreenMessage> onScreenMessagesList;
    List<OnScreenMessage> openedMessageList;

    private void Awake()
    {
        onScreenMessagesList = new List<OnScreenMessage>();
        openedMessageList = new List<OnScreenMessage>();
    }

    private void Update()
    {
        for (int i = onScreenMessagesList.Count - 1; i >= 0; --i)
        {
            onScreenMessagesList[i].appearTime -= Time.deltaTime;

            if (onScreenMessagesList[i].appearTime <= 0)
            {
                onScreenMessagesList[i].textGO.SetActive(false);
                openedMessageList.Add(onScreenMessagesList[i]);
                onScreenMessagesList.RemoveAt(i);
            }
        }
    }

    public void PostMessage(Vector3 worldPosition, string message)
    {
        worldPosition.z -= 1;
        worldPosition.x += Random.Range(-horizontalScatter, horizontalScatter);
        worldPosition.y += Random.Range(-verticalScatter, verticalScatter);

        if (openedMessageList.Count > 0)
        {
            ReuseMessageObject(worldPosition, message);
        }
        else
        {
            CreateNewMessageOnScreen(worldPosition, message);
        }
    }

    private void ReuseMessageObject(Vector3 worldPosition, string message)
    {
        OnScreenMessage osm = openedMessageList[0];
        osm.textGO.SetActive(true);
        osm.appearTime = appearTime;
        osm.textGO.GetComponent<TextMeshPro>().text = message;
        osm.textGO.transform.position = worldPosition;
        openedMessageList.RemoveAt(0);
        onScreenMessagesList.Add(osm);
    }

    private void CreateNewMessageOnScreen(Vector3 worldPosition, string message)
    {
        GameObject textGO = Instantiate(textPrefab, transform);
        textGO.transform.position = worldPosition;

        TextMeshPro tmp = textGO.GetComponent<TextMeshPro>();
        tmp.text = message;

        OnScreenMessage onScreenMessage = new OnScreenMessage(textGO);
        onScreenMessage.appearTime = appearTime;
        onScreenMessagesList.Add(onScreenMessage);
    }
}
