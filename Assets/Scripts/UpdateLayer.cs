using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts
{
    public class UpdateLayer : MonoBehaviour
    {
        public void ToDefault(SelectExitEventArgs args)
        {
            args.interactableObject.transform.gameObject.layer = 0;
        }

        public void ToInteractable(SelectEnterEventArgs args)
        {
            args.interactableObject.transform.gameObject.layer = LayerMask.NameToLayer("Interacting");
        }
    }
}
