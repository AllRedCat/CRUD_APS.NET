using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Para modificar esquema de anotação da tabela

namespace WebApplication2.Model;

public class Users
{
    public int Id { get; set; }
    
    [StringLength(100)] // determina o comprimento da string
    [Column(TypeName = "varchar(100)")] // determina o tipo da string
    public string Name { get; set; }
    
    [StringLength(100)]
    [Column(TypeName = "varchar(100)")]
    public string Email { get; set; }
    
    [StringLength(256)]
    [Column(TypeName = "varchar(256)")]
    public string PasswordHash { get; set; }
    
    [StringLength(50)]
    [Column(TypeName = "varchar(50)")]
    public string rule { get; set; }
}