using Game;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private AudioSource grab;

    private void Start()
    {
        grab = GetComponent<AudioSource>();
        InputHandler.OnObjectClick += OnObjectClick;
    }

    private void OnObjectClick(string _)
    {
        grab.Play();

    }

    private void OnDestroy()
    {
        InputHandler.OnObjectClick -= OnObjectClick;
    }
}
