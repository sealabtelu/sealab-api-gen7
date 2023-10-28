using SealabAPI.Base;
using System.ComponentModel.DataAnnotations.Schema;
using SealabAPI.DataAccess.Extensions;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Entities
{
    public class JournalAnswer : BaseEntity
    {
        public Guid IdStudent { get; set; }
        public Guid IdModule { get; set; }
        public string AssistantFeedback { get; set; }
        public string SessionFeedback { get; set; }
        public string LaboratoryFeedback { get; set; }
        public string FilePath
        {
            get => _filePath;
            set => _filePath = _filePath == null ? $"Journal/{value[..2]}/Submission/{File.SetFileName(value)}" : value;
        }
        public DateTime SubmitTime { get; set; } = DateTime.Now;
        [ForeignKey(nameof(IdStudent))]
        public Student Student { get; set; }
        [ForeignKey(nameof(IdModule))]
        public Module Module { get; set; }
    }
}
