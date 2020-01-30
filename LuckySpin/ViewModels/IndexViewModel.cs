using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace LuckySpin.ViewModels
{
    public class IndexViewModel
    {
        [Display(Prompt = " Starting Balance $3 to $10")]
        public decimal StartingBalance { get; set; }

        [Display(Prompt = " Your First Name")]
        [Required(ErrorMessage = "Please enter your Name", AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Range(1, 9, ErrorMessage = "Choose a number")]
        public int Luck { get; set; }

    }
}
