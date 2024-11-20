namespace Application.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public int EntityId { get; set; }  // ID de la entidad a la que pertenece el empleado
    }
}
