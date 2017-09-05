using Molecular.FileStore;
using System.ComponentModel.DataAnnotations;

namespace SchemaApi.Models.Stores
{

    public class StoreServerData : DataModelBase
    {

        [Required]
        public string ConnectionString { get; set; }

        [Required]
        public string Provider { get; set; }

    }

}