using SealabAPI.Base;
using SealabAPI.DataAccess.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace SealabAPI.DataAccess.Entities
{
    public class PreliminaryAssignmentAnswer : BaseEntity
    {
        public Guid IdStudent { get; set; }
        public Guid IdModule { get; set; }
        public string FilePath
        {
            get => _filePath;
            set => _filePath = _filePath == null ? $"PreliminaryAssignment/{value[..3]}/Submission/{File.SetFileName(value)}" : value;
        }
        public DateTime SubmitTime { get; set; } = DateTime.Now;
        [ForeignKey(nameof(IdStudent))]
        public Student Student { get; set; }
        [ForeignKey(nameof(IdModule))]
        public Module Module { get; set; }
    }
}
