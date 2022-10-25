using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typewriterSpeed = 50f;

    public bool IsRunning { get; private set; }

    private readonly Dictionary<HashSet<char>, float> punctuations = new Dictionary<HashSet<char>, float>()
    {
        {new HashSet<char>(){ '.', '!', '?' }, 0.6f },
        {new HashSet<char>(){ ',', ';', ':' }, 0.3f },
    };

    private Coroutine typingCoroutine;

    public void Run(string textToEffect, TMP_Text textLabel) {
        typingCoroutine = StartCoroutine(TypeText(textToEffect, textLabel));
    }

    public void Stop()
    {
        StopCoroutine(typingCoroutine);
        IsRunning = false;
    }

    private IEnumerator TypeText(string textToEffect, TMP_Text textLabel)
    {
        IsRunning = true;
        textLabel.text = string.Empty;

       float t = 0f;
        int charIndex = 0;

        while(charIndex < textToEffect.Length)
        {
            int lastCharIndex = charIndex;

            t += Time.deltaTime * typewriterSpeed;

            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToEffect.Length);

            for(int i = lastCharIndex; i < charIndex; i++)
            {
                bool islast = i >= textToEffect.Length - 1;

                textLabel.text = textToEffect.Substring(0, i + 1);

                if (IsPunctuation(textToEffect[i], out float waitTime) && !islast && !IsPunctuation(textToEffect[i+1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }

            yield return null;
        }

        IsRunning = false;
    }

    private bool IsPunctuation(char character, out float waitTime) {
        foreach(KeyValuePair<HashSet<char>, float> punctuationCategory in punctuations)
        {
            if (punctuationCategory.Key.Contains(character))
            {
                waitTime = punctuationCategory.Value;
                return true;
            }
        }

        waitTime = default;
        return false;
    }

}
