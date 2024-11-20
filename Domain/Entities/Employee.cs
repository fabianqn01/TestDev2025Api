namespace Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }

        // Clave foránea a la entidad
        public int EntityId { get; set; }
        public Entity Entity { get; set; }
    }
}
