using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Pages UI")]
    [SerializeField] private GameObject[] pages;
    [SerializeField] private GameObject[] hotSpots;
    [SerializeField] private GameObject[] buttons;

    [Header("Transition Settings")]
    [SerializeField] private float transitionDuration = 0.5f;
    [SerializeField] private float zoomScale = 0.9f;
    [SerializeField] private float slideDistance = 100f;

    [Header("Color Pallate")]
    [SerializeField] private Color _homeColor;

    private int currentPageIndex = 0;
    private bool isTransitioning = false;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one UIManager");
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        // Show first page instantly at startup
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == 0);

            if (pages[i].TryGetComponent(out CanvasGroup cg))
                cg.alpha = i == 0 ? 1 : 0;

            pages[i].transform.localScale = Vector3.one;
            pages[i].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        HotspotAnim(0);
    }

    private void Update()
    {
        AffectButtons();
    }

    public void ShowNextPage()
    {
        if (currentPageIndex + 1 < pages.Length)
        {
            ShowPage(currentPageIndex + 1);
            HotspotAnim(currentPageIndex + 1);

            AudioManager.instance.PlaySFXs(0);
        }
    }

    public void HomeButton()
    {
        AudioManager.instance.PlaySFXs(0);
        ShowPage(0);
    }

    public void ShowPreviousPage()
    {
        if (currentPageIndex - 1 >= 0)
        {
            ShowPage(currentPageIndex - 1);
            HotspotAnim(currentPageIndex - 1);

            AudioManager.instance.PlaySFXs(0);
        }
    }

    public void ShowPage(int _index)
    {
        if (_index < 0 || _index >= pages.Length || _index == currentPageIndex || isTransitioning)
            return;

        StartCoroutine(AnimateTransition(currentPageIndex, _index));
        HotspotAnim(_index);
        AudioManager.instance.PlaySFXs(0);
        AudioManager.instance.StopVoice();
    }

    private void HotspotAnim(int _index)
    {
        hotSpots[_index].GetComponent<Image>().color = Color.grey;

        foreach (var item in hotSpots)
        {
            if (item != hotSpots[_index] && _index != 0) 
            { 
                item.GetComponent<Image>().color = Color.white; 
            }
            else
            {
                item.GetComponent<Image>().color = _homeColor;
                hotSpots[_index].GetComponent<Image>().color = Color.grey;
            }
        }
    }

    private void AffectButtons()
    {
        if (hotSpots[0].GetComponent<Image>().color == Color.gray)
        {
            buttons[1].transform.parent.GetComponent<Button>().interactable = false;
            buttons[0].transform.parent.GetComponent<Button>().interactable = true;
            foreach (var item in buttons)
            {
                item.GetComponent<Image>().color = _homeColor;
            }
        }
        else
        {
            foreach (var item in buttons)
            {
                item.GetComponent<Image>().color = Color.white;
            }

            if(hotSpots[hotSpots.Length- 1].GetComponent<Image>().color == Color.gray)
            {
                buttons[0].transform.parent.GetComponent<Button>().interactable = false;
            }
            else { buttons[0].transform.parent.GetComponent<Button>().interactable = true; }

            buttons[1].transform.parent.GetComponent<Button>().interactable = true;
        }
    }

    private IEnumerator AnimateTransition(int fromIndex, int toIndex)
    {
        isTransitioning = true;

        GameObject fromPage = pages[fromIndex];
        GameObject toPage = pages[toIndex];

        CanvasGroup fromCG = fromPage.GetComponent<CanvasGroup>();
        CanvasGroup toCG = toPage.GetComponent<CanvasGroup>();

        RectTransform fromRT = fromPage.GetComponent<RectTransform>();
        RectTransform toRT = toPage.GetComponent<RectTransform>();

        if (fromCG == null || toCG == null)
        {
            Debug.LogError("Both pages must have CanvasGroup components.");
            yield break;
        }

        toPage.SetActive(true);
        toRT.localScale = Vector3.one * zoomScale;
        toCG.alpha = 0f;
        toRT.anchoredPosition = Vector2.zero;
        fromRT.anchoredPosition = Vector2.zero;

        float time = 0f;

        while (time < transitionDuration)
        {
            AudioManager.instance.PlayAudio();
            float t = time / transitionDuration;

            // Animate FROM page: fade out and slide left
            fromCG.alpha = Mathf.Lerp(1f, 0f, t);
            fromRT.anchoredPosition = Vector2.Lerp(Vector2.zero, new Vector2(-slideDistance, 0f), t);

            // Animate TO page: fade in and zoom in
            toCG.alpha = Mathf.Lerp(0f, 1f, t);
            toRT.localScale = Vector3.Lerp(Vector3.one * zoomScale, Vector3.one, t);

            time += Time.deltaTime;
            yield return null;
        }

        // Final state cleanup
        fromCG.alpha = 0f;
        fromPage.SetActive(false);
        fromRT.anchoredPosition = Vector2.zero;

        toCG.alpha = 1f;
        toRT.localScale = Vector3.one;

        currentPageIndex = toIndex;
        isTransitioning = false;
    }
}
