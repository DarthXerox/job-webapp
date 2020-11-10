using System;
using System.Linq;
using DAL.Entities;

namespace BL.Entities.Dto
{
    public class JobApplicationAnswerDto
    {
        public string Text { get; set; }

        public int QuestionId { get; set; }

        public JobOfferQuestionDto Question { get; set; }
    }
}
