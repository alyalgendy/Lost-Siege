using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [Header("Image References")]
    [SerializeField] private Image image1;
    [SerializeField] private Image image2;
    [SerializeField] private Image image3;
    [SerializeField] private Button continueButton;

    [Header("Timing Settings")]
    [SerializeField] private float image1StartTime = 1f;
    [SerializeField] private float image2StartTime = 4f;
    [SerializeField] private float image3StartTime = 7f;

    [Header("Image 1 Duration")]
    [SerializeField] private float fadeInDuration1 = 1f;
    [SerializeField] private float displayDuration1 = 2f;
    [SerializeField] private float fadeOutDuration1 = 1f;

    [Header("Image 2 Duration")]
    [SerializeField] private float fadeInDuration2 = 1f;
    [SerializeField] private float displayDuration2 = 3f;
    [SerializeField] private float fadeOutDuration2 = 1f;

    [Header("Image 3 Duration")]
    [SerializeField] private float fadeInDuration3 = 1f;
    [SerializeField] private float displayDuration3 = 4f;
    [SerializeField] private float fadeOutDuration3 = 1f;

    [Header("Button Timing")]
    [SerializeField] private float buttonAppearTime = 15f; // Time when button appears

    void Start()
    {
        // Initialize all images and button as invisible
        SetupInitialState();
        // Start the sequences
        StartCoroutine(PlayImageSequence());
        StartCoroutine(ButtonTimer()); // Start button timer separately
    }

    private void SetupInitialState()
    {
        // Set all images to transparent
        image1.color = new Color(1, 1, 1, 0);
        image2.color = new Color(1, 1, 1, 0);
        image3.color = new Color(1, 1, 1, 0);
        
        // Hide the continue button
        if (continueButton != null)
        {
            continueButton.gameObject.SetActive(false);
        }
    }

    private IEnumerator PlayImageSequence()
    {
        // Wait before starting the sequence
        yield return new WaitForSeconds(image1StartTime);
        
        // Image 1
        yield return StartCoroutine(FadeImage(image1, true, fadeInDuration1));  // Fade in
        yield return new WaitForSeconds(displayDuration1);
        yield return StartCoroutine(FadeImage(image1, false, fadeOutDuration1)); // Fade out
        
        // Wait for second image
        yield return new WaitForSeconds(image2StartTime - (image1StartTime + fadeInDuration1 + displayDuration1 + fadeOutDuration1));
        
        // Image 2
        yield return StartCoroutine(FadeImage(image2, true, fadeInDuration2));
        yield return new WaitForSeconds(displayDuration2);
        yield return StartCoroutine(FadeImage(image2, false, fadeOutDuration2));
        
        // Wait for third image
        yield return new WaitForSeconds(image3StartTime - (image2StartTime + fadeInDuration2 + displayDuration2 + fadeOutDuration2));
        
        // Image 3
        yield return StartCoroutine(FadeImage(image3, true, fadeInDuration3));
        yield return new WaitForSeconds(displayDuration3);
        yield return StartCoroutine(FadeImage(image3, false, fadeOutDuration3));
    }

    private IEnumerator ButtonTimer()
    {
        yield return new WaitForSeconds(buttonAppearTime);
        
        if (continueButton != null)
        {
            continueButton.gameObject.SetActive(true);
        }
    }

    private IEnumerator FadeImage(Image image, bool fadeIn, float duration)
    {
        float elapsed = 0;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = fadeIn ? 
                Mathf.Lerp(0, 1, elapsed / duration) : 
                Mathf.Lerp(1, 0, elapsed / duration);
            
            image.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        
        // Ensure we end up at exactly the target alpha
        image.color = new Color(1, 1, 1, fadeIn ? 1 : 0);
    }
}
