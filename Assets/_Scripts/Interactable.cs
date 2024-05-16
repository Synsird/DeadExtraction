using UnityEngine;

public class Interactable : MonoBehaviour, I_Interactable
{
    [SerializeField] private InteractableObject _interactableObj;

    public void Interact()
    {

    }

    public InteractableObject InteractableObject
    {
        get { return _interactableObj; }
        set { _interactableObj = value; }
    }
}
