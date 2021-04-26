using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiMultiTenant.Models
{
     [Table("Users")]
    public class Users
    {
        [Key]
        public int id{get;set;}
        public string name{get;set;}
    }
}