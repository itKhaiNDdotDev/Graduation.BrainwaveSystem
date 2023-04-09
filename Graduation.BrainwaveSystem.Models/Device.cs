namespace Graduation.BrainwaveSystem.Models
{
    public class Device : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime ActiveDate { get; set; } // Lần cuối khởi động
    }
}