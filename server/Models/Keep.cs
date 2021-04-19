using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Keep
    {
        public int Id { get; set; }

        public string CreatorId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public string Img { get; set; }

        public int Views { get; set; }

        public int Shares { get; set; }

        public int Keeps { get; set; }

        public Profile Creator { get; set; }
    }

    public class VaultKeepModel : Keep
    {
        public int VaultKeepId { get; set; }
    }
}