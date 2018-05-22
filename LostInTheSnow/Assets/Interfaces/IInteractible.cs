using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IInteractible : IEventSystemHandler{
    void Interact();
	void AlternateInteract();
}
