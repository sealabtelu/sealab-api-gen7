using System.ComponentModel.DataAnnotations.Schema;
using SealabAPI.Base;
using SealabAPI.DataAccess.Extensions;

namespace SealabAPI.DataAccess.Entities
{
    public class PreliminaryAssignmentQuestion : BaseEntity
    {
        public Guid IdModule { get; set; }
        public string Type { get; set; }
        public string Question { get; set; }
        public string AnswerKey { get; set; }
        public string FilePath
        {
            get => _filePath;
            set => _filePath = _filePath == null ? $"PreliminaryAssignment/{IdModule}/Submission/{File.SetFileName(value)}" : value;
        }
        [ForeignKey(nameof(IdModule))]
        public Module Module { get; set; }
    }
}
