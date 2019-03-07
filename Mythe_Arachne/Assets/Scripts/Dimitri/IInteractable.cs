public interface IInteractable{
	void OnInteract();
}

public interface IHitable : IInteractable
{
	void OnHit();
}
