using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;
using System;

public class OptZone : MonoBehaviour
{
    public static OptZone optZone;
    

    [SerializeField]
    private GameObject OptionContainer;

    [SerializeField]
    private GameObject OptionPrefab;

    [SerializeField]
    private List<GameObject> OptionList;

    private readonly (int, int)[] DefaultPattern = new (int, int)[] {
        (0,0),  // UnPractical 
        (1,0),
        (2,0),
        (2,1),
        (2,2),
        (3,2),
        (3,3)
    };

    private void OnEnable()
    {
        optZone = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Show(GameObject.Find("Player").transform, "有意思", "很有趣", "哈哈哈");
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("Clear")]
    void Clear()
    {
        foreach (var item in OptionList)
        {
            DestroyImmediate(item);
        }
        OptionList.Clear();
        // for (int i = OptionContainer.transform.childCount - 1; i >= 0; i--)
        // {
        //     DestroyImmediate(OptionContainer.transform.GetChild(i).gameObject);
        // }
    }

    [ContextMenu("Make")]
    void Make()
    {
        Make("In Editor");
    }
    void Make(string text = "")
    {
        GameObject instance = Instantiate(OptionPrefab, OptionContainer.transform.position, Quaternion.identity, OptionContainer.transform);
        RectTransform rectTransform = instance.GetComponent<RectTransform>();
        rectTransform.offsetMax = new Vector2(0, rectTransform.offsetMax.y);
        rectTransform.offsetMin = new Vector2(0, rectTransform.offsetMin.y);
        rectTransform.sizeDelta = new Vector2(OptionContainer.GetComponent<RectTransform>().sizeDelta.x / 2f, rectTransform.sizeDelta.y);

        instance.GetComponentInChildren<TMPro.TextMeshProUGUI>().SetText(text);
        instance.SetActive(true);
        OptionList.Add(instance);

        AutoReact(null);
    }

    [ContextMenu("AutoReact")]
    void AutoReact()
    {
        AutoReact(null);
    }


    private static float TotalTime = 0.4f;
    private Sequence allsequence;
    [ContextMenu("PlayEnterAnime")]
    void PlayEnterAnime()
    {
        allsequence = DOTween.Sequence();
        allsequence.SetAutoKill(false);

        foreach (GameObject obj in OptionList)
        {
            float angle = CaculateAngle(OptionList.IndexOf(obj), null);
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(OptionList.IndexOf(obj) * 0.1f);
            sequence.Append(obj.transform.DOLocalMove(new Vector3(40 * Mathf.Cos(angle * Mathf.PI / 180), 40 * Mathf.Sin(angle * Mathf.PI / 180), 0), TotalTime).From(new Vector3(1000 * Mathf.Cos(angle * Mathf.PI / 180), 1000 * Mathf.Sin(angle * Mathf.PI / 180), 0)).SetEase(Ease.InCubic));
            sequence.Join(obj.transform.DOScale(new Vector3(1, 1, 1), TotalTime).From(new Vector3(10, 6, 1)).SetEase(Ease.InCubic));
            // sequence.Play();

            allsequence.Join(sequence);
        }
        allsequence.Play();
        allsequence.OnComplete(() =>
        {
            foreach (GameObject obj in OptionList)
            {
                obj.GetComponentInChildren<Button>().onClick.AddListener(() =>
                {
                    Debug.Log(allsequence);
                    Debug.Log(OptionList.IndexOf(obj));
                    allsequence.PlayBackwards();
                    // allsequence.PLAY();
                    allsequence.OnRewind(() =>
                    {
                        allsequence.Kill();
                        int tmp = OptionList.IndexOf(obj);
                        Clear();
                        Callback?.Invoke(tmp);
                    });
                });
            }

        });
    }
    public Action<int> Callback;
    float CaculateAngle(int index, (int, int)? _pattern = null)
    {
        int len = OptionList.Count;
        (int left, int right) = _pattern.GetValueOrDefault(DefaultPattern[len]);
        if (index < left)
        {
            return 90f + 180f / (left + 1) * (index + 1);

        }
        else
        {
            return 90f - 180f / (right + 1) * (index - left + 1);
        }

    }
    void AutoReact((int, int)? _pattern = null)
    {
        int len = OptionList.Count;
        (int left, int right) = _pattern.GetValueOrDefault(DefaultPattern[len]);

        for (int i = 1; i <= left; i++)
        {
            // OptionList[i - 1].transform.eulerAngles = new Vector3(OptionList[i - 1].transform.eulerAngles.x, OptionList[i - 1].transform.eulerAngles.y, LeftZ);

            // OptionList[i - 1].transform.localScale = new Vector3(OptionList[i - 1].transform.localScale.x, -Mathf.Abs(OptionList[i - 1].transform.localScale.y), OptionList[i - 1].transform.localScale.z);
            float LeftZ = -90f + 180f / (left + 1) * i;

            RectTransform rectTransform = OptionList[i - 1].GetComponent<RectTransform>();
            rectTransform.pivot = new Vector2(1f, 0.5f);
            rectTransform.eulerAngles = new Vector3(rectTransform.eulerAngles.x, rectTransform.transform.eulerAngles.y, LeftZ);
        }
        for (int j = 1; j <= right; j++)
        {
            RectTransform rectTransform = OptionList[left + j - 1].GetComponent<RectTransform>();
            rectTransform.pivot = new Vector2(0, 0.5f);
            float rightZ = -90f + 180f / (right + 1) * j;
            rectTransform.eulerAngles = new Vector3(rectTransform.eulerAngles.x, rectTransform.transform.eulerAngles.y, rightZ);
        }
    }

    public static void Show(Vector3 position, string[] texts, Action<int> callback)
    {
        optZone.Callback = callback;

        optZone.Clear();

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
        optZone.OptionContainer.transform.position = new Vector2(screenPosition.x, Screen.height - screenPosition.y);

        foreach (string text in texts)
        {
            optZone.Make(text);
        }

        optZone.PlayEnterAnime();
    }
}
