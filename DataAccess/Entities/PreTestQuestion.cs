using SealabAPI.Base;
using System.ComponentModel.DataAnnotations.Schema;
using SealabAPI.Helpers;
using SealabAPI.DataAccess.Extensions;

namespace SealabAPI.DataAccess.Entities
{
    public class PreTestQuestion : BaseEntity
    {
        public Guid IdModule { get; set; }
        public string Type { get; set; }
        public string Question { get; set; }
        public string FilePath
        {
            get => _filePath;
            set => _filePath = _filePath == null ? Path.Combine("PreTest", Module.SeelabsId.ToString(), File.SetFileName(value)) : value;
        }
        public ICollection<PreTestOption> PRTOptions { get; set; } = new HashSet<PreTestOption>();
        [ForeignKey(nameof(IdModule))]
        public Module Module { get; set; }
    }
}
