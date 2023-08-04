using SealabAPI.Base;
using System.ComponentModel.DataAnnotations.Schema;
using SealabAPI.Helpers;
using SealabAPI.DataAccess.Extensions;

namespace SealabAPI.DataAccess.Entities
{
    public class PreTestQuestion : BaseEntity
    {
        public int Module { get; set; }
        public string Type { get; set; }
        public string Question { get; set; }
        public string FilePath
        {
            get => _filePath;
            set => _filePath = _filePath == null ? Path.Combine("PreTest", Module.ToString(), File.SetFileName(value)) : value;
        }
        public ICollection<PreTestOption> PTOptions { get; set; } = new HashSet<PreTestOption>();
    }
}
