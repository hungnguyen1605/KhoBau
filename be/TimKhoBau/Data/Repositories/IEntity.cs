namespace TimKhoBau.Data.Repositories
{
    public interface IEntity<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }   
    }
}
