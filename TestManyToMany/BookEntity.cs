using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManyToMany
{
    [Table("Books")]
    public class BookEntity
    {
        [Key]
        public int BookEntityId { get; set; }

        public string Title { get; set; } = "";

        // Adding the keyword virtual will only make a difference if using 'optionsBuilder.UseLazyLoadingProxies(true)'
        public List<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();
    }
}
