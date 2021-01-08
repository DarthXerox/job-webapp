using System.Collections.Generic;
using DAL.Entities;
using DAL.Enums;

namespace Infrastructure
{

    public class Seeder
    {
        public static void Seed(UnitOfWork unit)
        {
            // JobSeekers
            JobSeeker neo = new JobSeeker { Name = "Neo", Surname = "Anderson", Email = "neo@matrix.com",
                Skills = new List<string>{"C#", "Algorithms"}};
            JobSeeker hans = new JobSeeker { Name = "Hans", Surname = "Andersen", Email = "littleHans@email.com",
                Skills = new List<string>{"C#", "C++", "English"}};
            JobSeeker jason = new JobSeeker { Name = "Jason", Surname = "Statham", Email = "jayjay@gmail.com",
                Skills = new List<string>{"Python"}};
            JobSeeker susan = new JobSeeker { Name = "Susan", Surname = "Green", Email = "sus@gmail.com",
                Skills = new List<string>{"Algorithms", "NoSQLDatabases", "Java"}};
            JobSeeker bigshaq = new JobSeeker { Name = "Big", Surname = "Shaq", Email = "bigg@mail.com",
                Skills = new List<string>{"2+2isFor", "Minus1is3", "QuickMaths"}};
            unit.JobSeekerRepository.Add(neo);
            unit.JobSeekerRepository.Add(hans);
            unit.JobSeekerRepository.Add(jason);
            unit.JobSeekerRepository.Add(susan);
            unit.JobSeekerRepository.Add(bigshaq);

            // Questions
            var questionYears = new JobOfferQuestion() { Text = "Where do you see yourself in 5 years?" };
            var questionWtf = new JobOfferQuestion() { Text = "How gut ar youre englisch skillz?"};
            var questionRly = new JobOfferQuestion() { Text = "Do you really want this job?" };
            var questionDont = new JobOfferQuestion() { Text = "Don't bother, you're not getting this job anyway..." };
            var questionGoal = new JobOfferQuestion() { Text = "What is your life goal?" };
            unit.JobOfferQuestionRepository.Add(questionYears);
            unit.JobOfferQuestionRepository.Add(questionWtf);
            unit.JobOfferQuestionRepository.Add(questionRly);
            unit.JobOfferQuestionRepository.Add(questionDont);
            unit.JobOfferQuestionRepository.Add(questionGoal);

            // Companies
            var apple = new Company { Name = "Apple", Offers = new List<JobOffer>() };
            var mcsoft = new Company { Name = "Microsoft", Offers = new List<JobOffer>() };
            var tesla = new Company { Name = "Tesla", Offers = new List<JobOffer>() };
            var starbucks = new Company { Name = "Starbucks", Offers = new List<JobOffer>() };
            // These will be added after JobOffers

            // JobOffers
            var offerApple = new JobOffer()
            {
                City = "Los Angeles", Company = apple, Description = "Well-paid",
                Name = "Well-paid position at Apple",
                Questions = new List<JobOfferQuestion>() { questionRly, questionDont },
                RelevantSkills = new List<string>() {"C#", "English", "C++"}
            };

            var offerTesla = new JobOffer()
            {
                City = "San Carlos", Company = tesla, Description = "Cybertruck driving software development",
                Name = "Cybertruck driving software development at Tesla",
                Questions = new List<JobOfferQuestion>() { questionYears, questionGoal, questionRly },
                RelevantSkills = new List<string>() {"Algorithms", "NoSQLDatabases"}
            };

            var offerStarbucks = new JobOffer()
            {
                City = "Seattle", Company = starbucks, Description = "Come join us in Starbucks!",
                Name = "Pouring coffee and writing names on cups",
                Questions = new List<JobOfferQuestion>() { questionWtf },
                RelevantSkills = new List<string>() {"English", "QuickMaths"}
            };

            var offerMicrosoft = new JobOffer()
            {
                City = "Seattle", Company = mcsoft, Description = "Be a part of .NET Core SDK development!",
                Name = "Developing new .NET Core SDK",
                Questions = new List<JobOfferQuestion>() { questionYears, questionGoal },
                RelevantSkills = new List<string>() {"C#", "Algorithms"}
            };
            unit.JobOfferRepository.Add(offerApple);
            unit.JobOfferRepository.Add(offerMicrosoft);
            unit.JobOfferRepository.Add(offerStarbucks);
            unit.JobOfferRepository.Add(offerTesla);

            apple.Offers.Add(offerApple);
            tesla.Offers.Add(offerTesla);
            mcsoft.Offers.Add(offerMicrosoft);
            starbucks.Offers.Add(offerStarbucks);
            unit.CompanyRepository.Add(apple);
            unit.CompanyRepository.Add(mcsoft);
            unit.CompanyRepository.Add(tesla);
            unit.CompanyRepository.Add(starbucks);

            // Answers to the questions
            var answerYears = new JobApplicationAnswer()
            {
                Question = questionYears,
                Text = "In Apple working for 100k a month"
            };

            var answerWtf = new JobApplicationAnswer()
            {
                Question = questionWtf,
                Text = "Yes"
            };
            var answerDont = new JobApplicationAnswer()
            {
                Question = questionDont,
                Text = ""
            };
            var answerRly = new JobApplicationAnswer()
            {
                Question = questionRly,
                Text = "I guess..."
            };

            var answerGoal = new JobApplicationAnswer()
            {
                Question = questionGoal,
                Text = "Be happy"
            };
            unit.JobApplicationAnswerRepository.Add(answerDont);
            unit.JobApplicationAnswerRepository.Add(answerGoal);
            unit.JobApplicationAnswerRepository.Add(answerRly);
            unit.JobApplicationAnswerRepository.Add(answerWtf);
            unit.JobApplicationAnswerRepository.Add(answerYears);

            // Applications
            var applFromNeo = new JobApplication()
            {
                Applicant = neo,
                JobOffer = offerMicrosoft,
                Status = Status.Accepted,
                Text = "Microsoft > Apple",
                Answers = new List<JobApplicationAnswer>() { answerYears, answerGoal }
            };

            var applFromNeo2 = new JobApplication()
            {
                Applicant = neo,
                JobOffer = offerApple,
                Status = Status.Unresolved,
                Text = "Blue pill, red pill, I'll eat both",
                Answers = new List<JobApplicationAnswer>() { answerRly, answerDont }
            };

            var applFromJason = new JobApplication()
            {
                Applicant = jason,
                JobOffer = offerApple,
                Status = Status.Unresolved,
                Text = "I know how to fight",
                Answers = new List<JobApplicationAnswer>() { answerRly, answerDont }
            };

            var applFromSusan = new JobApplication()
            {
                Applicant = susan,
                JobOffer = offerTesla,
                Status = Status.Accepted,
                Text = "I know how to fight",
                Answers = new List<JobApplicationAnswer>() { answerYears, answerGoal, answerRly }
            };

            var applFromShaq = new JobApplication()
            {
                Applicant = bigshaq,
                JobOffer = offerStarbucks,
                Status = Status.Rejected,
                Text = "Man's not hot",
                Answers = new List<JobApplicationAnswer>() { answerWtf }
            };
            unit.JobApplicationRepository.Add(applFromJason);
            unit.JobApplicationRepository.Add(applFromNeo);
            unit.JobApplicationRepository.Add(applFromNeo2);
            unit.JobApplicationRepository.Add(applFromShaq);
            unit.JobApplicationRepository.Add(applFromSusan);

            unit.SaveChanges();
        }
    }
}
