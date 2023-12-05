using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManyToMany
{
    [Table("Person")]
    public class PersonEntity
    {
        [Key]
        public int PersonId { get; set; }

        public string Name { get; set; } = "";

        // Adding the keyword virtual will only make a difference if using 'optionsBuilder.UseLazyLoadingProxies(true)'
        public List<BookEntity> Books { get; set; } = new List<BookEntity>();
    }
}
