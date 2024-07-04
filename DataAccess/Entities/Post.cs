using SealabAPI.Base;
using System.ComponentModel.DataAnnotations.Schema;
using SealabAPI.DataAccess.Extensions;
using SealabAPI.Helpers;
using static SealabAPI.Helpers.FileHelper;

namespace SealabAPI.DataAccess.Entities
{
    public class Post : BaseEntity
    {
        public override UploadFileInfo GetFileInfo()
        {
            return new UploadFileInfo
            {
                File = File,
                FilePath = _thumbnailUrl
            };
        }
        private string _thumbnailUrl;
        public Guid IdAssistant { get; set; }
        public Guid IdCategory { get; set; }
        public string ThumbnailUrl
        {
            get => _thumbnailUrl;
            set => _thumbnailUrl = _thumbnailUrl == null ? $"Article/Thumbnail/{File.SetFileName(value)}" : value;
        }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        [ForeignKey(nameof(IdCategory))]
        public PostCategory Category { get; set; }
        [ForeignKey(nameof(IdAssistant))]
        public Assistant Assistant { get; set; }
    }
}
