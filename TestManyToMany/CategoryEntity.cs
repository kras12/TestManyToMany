using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManyToMany
{
    [Table("Categories")]
    public class CategoryEntity
    {
        [Key]
        public int CategoryEntityId { get; set; }

        public string CategoryName { get; set; } = "";

        // Adding the keyword virtual will only make a difference if using 'optionsBuilder.UseLazyLoadingProxies(true)'
        public List<BookEntity> Books { get; set; } = new List<BookEntity>();
    }
}
