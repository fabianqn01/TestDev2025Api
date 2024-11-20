namespace Domain.Entities
{
    public class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Relación con los Empleados
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

    }
}
