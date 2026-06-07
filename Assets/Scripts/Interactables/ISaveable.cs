namespace Scripts.Interactables
{
	public interface ISaveable
	{
		public abstract string Id { get; }
		public abstract string Serialize();
		public abstract void Deserialize(string data);
	}
}