using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelScaler : MonoBehaviour
{
    [SerializeField] private RectTransform targetPanel;
    [SerializeField] private float scaleDuration = 0.3f;
    [SerializeField] private Vector3 scaledUpSize = new Vector3(1.2f, 1.2f, 1f); // target scale
    [SerializeField] private string scaleUpTrigger = "ScaleUp"; // Animator trigger name
    [SerializeField] private Animator animator; // Animator to play trigger on scale up
    [SerializeField] private Button playButton;

    private bool isScaledUp = false;
    private Coroutine scaleCoroutine;

    private void Start()
    {
        playButton.onClick.AddListener(ToggleScale);
    }

    public void ToggleScale()
    {
        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        if (isScaledUp)
        {
            // Scale down to default
            scaleCoroutine = StartCoroutine(ScaleTo(Vector3.one));
        }
        else
        {
            // Scale up and play animation
            scaleCoroutine = StartCoroutine(ScaleTo(scaledUpSize));
            if (animator != null)
            {
                animator.SetTrigger(scaleUpTrigger);
            }
        }

        isScaledUp = !isScaledUp;

        AudioManager.instance.PlaySFXs(0);
    }

    private IEnumerator ScaleTo(Vector3 targetScale)
    {
        Vector3 initialScale = targetPanel.localScale;
        float elapsed = 0f;

        while (elapsed < scaleDuration)
        {
            targetPanel.localScale = Vector3.Lerp(initialScale, targetScale, elapsed / scaleDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        targetPanel.localScale = targetScale;
    }
}
