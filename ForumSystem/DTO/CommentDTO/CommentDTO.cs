//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DTO.CommentDTO
//{
//    public class CommentDTO
//    {
//        // CommentDTO class mirrors the properties of the Comment class
//        // but doesn't include any data annotation attributes
//        // used to transfer data without the validation rules and other metadata
//        // that might be specific to the data access layer

//        public int Id { get; set; }

//        public string Content { get; set; }

//        public int AuthorId { get; set; }

//        public int PostId { get; set; }

//        public bool IsDeleted { get; set; }

//        public DateTime? DeletedOn { get; set; }

//        public int Likes { get; set; }

//        public int Dislikes { get; set; }
//    }
//}
