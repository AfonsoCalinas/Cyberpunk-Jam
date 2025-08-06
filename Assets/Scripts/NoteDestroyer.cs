using UnityEngine;

public class NoteDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out NoteObject note)) return;
        Destroy(note.gameObject);
        Debug.Log("Note destroyed by NoteDestroyer!");
    }
}
