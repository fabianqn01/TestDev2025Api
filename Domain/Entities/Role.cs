namespace Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Relación muchos a muchos con ApplicationUser
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}
