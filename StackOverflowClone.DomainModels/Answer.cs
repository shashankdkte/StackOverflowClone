﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowClone.DomainModels
{
    [Table("Answers", Schema = "STACKOVERFLOW")]
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnswerID { get; set; }
        public string AnswerText { get; set; }

        public DateTime AnswerDateAndTime { get; set; } = DateTime.Now;
        public int UserID { get; set; }

        public int QuestionID { get; set; }

        public int VotesCount { get;set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        public virtual List<Vote> Votes { get; set; }
    }
}
