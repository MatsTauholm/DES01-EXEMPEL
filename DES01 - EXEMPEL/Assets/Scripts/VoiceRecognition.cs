using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Windows.Speech;

public class VoiceRecognition : MonoBehaviour
{
    [SerializeField] private string[] keywords; // Array of keywords to recognize
    [SerializeField] private Light2D lamp;

    private KeywordRecognizer keywordRecognizer;
    private bool isListening;
    private Coroutine listening;

    void Start()
    {
        keywordRecognizer = new KeywordRecognizer(keywords);
        keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            keywordRecognizer.Start();
        }
        else if (Input.GetKeyUp(KeyCode.V))
        {
            keywordRecognizer.Stop();
        }
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if (args.text == "Unity")
        {
            Debug.Log("Keyword 'Unity' recognized!");
            listening = StartCoroutine(Listening());
        }

        if (isListening) 
        {
            if (args.text == "on")
            {
                Debug.Log("Keyword 'On' recognized!");
                lamp.enabled = true;
            }
            else if (args.text == "off")
            {
                Debug.Log("Keyword 'Off' recognized!");
                lamp.enabled = false;
            }
        }
        
    }

    private IEnumerator Listening()
    {
            isListening = true;
            yield return new WaitForSeconds(5f); // Listen for 5 seconds
            isListening = false;
    }
}
