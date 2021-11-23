using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF_Table_Extrator
{
    public class Aluno
    {
        public int Id { get; set; }
        [Required]
        public String Matricula { get; set; }
        [Required]
        public String Nome { get; set; }
    }
}
