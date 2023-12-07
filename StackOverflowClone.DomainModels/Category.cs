using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace StackOverflowClone.DomainModels
{
    [Table("Categories", Schema = "STACKOVERFLOW")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
