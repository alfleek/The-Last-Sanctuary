using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GetButtonName : MonoBehaviour
{

    public void OnClick()
    {
        // Print the name of the button
        Debug.Log($"Button clicked: {gameObject.name}");
    }
}
