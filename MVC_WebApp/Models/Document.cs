using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace MVC_WebApp.Models
{
    public class Document
    {
        public int Id { get; set; }

        public string Number { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; } = DateTime.Now;
        //[DisplayFormat(DataFormatString = "{0:#.00}", ApplyFormatInEditMode = true)]
        public double Value { get; set; } = 0;
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<DocumentDetail> Details { get; set; }
    }
}
