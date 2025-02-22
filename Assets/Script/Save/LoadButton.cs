using UnityEngine;
using UnityEngine.UI;

public class LoadButton : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener( () => FindObjectOfType<SaveLoadManager>().OpenLoadPanel() );
    }
}
