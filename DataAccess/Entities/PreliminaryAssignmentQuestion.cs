using SealabAPI.Base;
using SealabAPI.DataAccess.Extensions;
using SealabAPI.Helpers;
using static SealabAPI.Helpers.FileHelper;

namespace SealabAPI.DataAccess.Entities
{
    public class PreliminaryAssignmentQuestion : BaseEntity
    {
        public int Module { get; set; }
        public string Type { get; set; }
        public string Question { get; set; }
        public string AnswerKey { get; set; }
        public string FilePath
        {
            get => _filePath;
            set => _filePath = _filePath == null ? Path.Combine("PreliminaryAssignment", Module.ToString(), File.SetFileName(value)) : value;
        }
    }
}
