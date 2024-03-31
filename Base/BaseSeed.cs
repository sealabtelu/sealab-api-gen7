using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SealabAPI.Helpers.FileHelper;

namespace SealabAPI.Base
{
    public interface IBaseSeed<TSeed> where TSeed : BaseEntity
    {
        List<TSeed> GetSeeder();
    }
}
