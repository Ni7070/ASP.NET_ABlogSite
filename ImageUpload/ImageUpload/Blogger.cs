//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImageUpload
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Blogger
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Error: Title can't be empty!")]
        public string Title { get; set; }
       
        public string Category { get; set; }
        [Required(ErrorMessage = "Error: A blog is not a blog without a Post!")]
        public string Post { get; set; }
        public System.DateTime Date { get; set; }
        public string PostedBy { get; set; }
        
        public string ImagePath { get; set; }
    }
}
