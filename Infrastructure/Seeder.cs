using System.Collections.Generic;
using DAL.Entities;
using DAL.Enums;

namespace Infrastructure
{

    public static class Seeder
    {
        public static void Seed(UnitOfWork unit)
        {
            int id = 1;
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
            // Each question should be tied to one specific joboffer
            var questionYearsTesla = new JobOfferQuestion() { Text = "Where do you see yourself in 5 years?" };
            var questionYearsMicrosoft = new JobOfferQuestion() { Text = "Where do you see yourself in 10 years?" };
            var questionWtf = new JobOfferQuestion() { Text = "How gut ar youre englisch skillz?"};
            var questionRlyApple = new JobOfferQuestion() { Text = "Do you really want this job?" };
            var questionRlyTesla = new JobOfferQuestion() { Text = "Are you sure you want this job?" };
            var questionDont = new JobOfferQuestion() { Text = "Don't bother, you're not getting this job anyway..." };
            var questionGoalTesla = new JobOfferQuestion() { Text = "What is your life goal?" };
            var questionGoalMicrosoft = new JobOfferQuestion() { Text = "What is your career goal?" };

            unit.JobOfferQuestionRepository.Add(questionYearsTesla);
            unit.JobOfferQuestionRepository.Add(questionYearsMicrosoft);
            unit.JobOfferQuestionRepository.Add(questionWtf);
            unit.JobOfferQuestionRepository.Add(questionRlyApple);
            unit.JobOfferQuestionRepository.Add(questionRlyTesla);
            unit.JobOfferQuestionRepository.Add(questionDont);
            unit.JobOfferQuestionRepository.Add(questionGoalTesla);
            unit.JobOfferQuestionRepository.Add(questionGoalMicrosoft);
            unit.SaveChanges();

            // Companies
            var apple = new Company { Name = "Apple", Offers = new List<JobOffer>() };
            var microsoft = new Company { Name = "Microsoft", Offers = new List<JobOffer>() };
            var tesla = new Company { Name = "Tesla", Offers = new List<JobOffer>() };
            var starbucks = new Company { Name = "Starbucks", Offers = new List<JobOffer>() };
            // These will be added after JobOffers

            // JobOffers
            var offerApple = new JobOffer()
            {
                City = "Los Angeles", Company = apple, Description = "Well-paid",
                Name = "Well-paid position at Apple",
                Questions = new List<JobOfferQuestion>() { questionRlyApple, questionDont },
                RelevantSkills = new List<string>() {"C#", "English", "C++"}
            };

            var offerTesla = new JobOffer()
            {
                City = "San Carlos", Company = tesla, Description = "Cybertruck driving software development",
                Name = "Cybertruck driving software development at Tesla",
                Questions = new List<JobOfferQuestion>() { questionYearsTesla, questionGoalTesla, questionRlyTesla },
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
                City = "Seattle", Company = microsoft, Description = "Be a part of .NET Core SDK development!",
                Name = "Developing new .NET Core SDK",
                Questions = new List<JobOfferQuestion>() { questionYearsMicrosoft, questionGoalMicrosoft },
                RelevantSkills = new List<string>() {"C#", "Algorithms"}
            };
            apple.Offers.Add(offerApple);
            tesla.Offers.Add(offerTesla);
            microsoft.Offers.Add(offerMicrosoft);
            starbucks.Offers.Add(offerStarbucks);
            unit.JobOfferRepository.Add(offerApple);
            unit.JobOfferRepository.Add(offerMicrosoft);
            unit.JobOfferRepository.Add(offerStarbucks);
            unit.JobOfferRepository.Add(offerTesla);


            unit.CompanyRepository.Add(apple);
            unit.CompanyRepository.Add(microsoft);
            unit.CompanyRepository.Add(tesla);
            unit.CompanyRepository.Add(starbucks);
            unit.SaveChanges();

            // Answers to the questions
            // Answers MUST be specific for each application
            var answerYearsNeo = new JobApplicationAnswer()
            {
                Question = questionYearsMicrosoft,
                Text = "In Apple working for 100k a month"
            };

            var answerYearsSusan = new JobApplicationAnswer()
            {
                Question = questionYearsTesla,
                Text = "In Tesla, working for Elon"
            };

            var answerWtfShaq = new JobApplicationAnswer()
            {
                Question = questionWtf,
                Text = "Yes"
            };
            var answerDontNeo = new JobApplicationAnswer()
            {
                Question = questionDont,
                Text = "No, I AM getting this job!"
            };
            var answerDontJason = new JobApplicationAnswer()
            {
                Question = questionDont,
                Text = ""
            };
            var answerRlyNeo = new JobApplicationAnswer()
            {
                Question = questionRlyApple,
                Text = "I guess..."
            };

            var answerRlyJason = new JobApplicationAnswer()
            {
                Question = questionRlyApple,
                Text = "I surely do"
            };

            var answerRlySusan = new JobApplicationAnswer()
            {
                Question = questionRlyTesla,
                Text = "Of course"
            };

            var answerGoalSusan = new JobApplicationAnswer()
            {
                Question = questionGoalTesla,
                Text = "Be happy"
            };

            var answerGoalNeo = new JobApplicationAnswer()
            {
                Question = questionGoalMicrosoft,
                Text = "Be rich"
            };
            unit.JobApplicationAnswerRepository.Add(answerDontNeo);
            unit.JobApplicationAnswerRepository.Add(answerDontJason);
            unit.JobApplicationAnswerRepository.Add(answerGoalNeo);
            unit.JobApplicationAnswerRepository.Add(answerGoalSusan);
            unit.JobApplicationAnswerRepository.Add(answerRlyNeo);
            unit.JobApplicationAnswerRepository.Add(answerRlySusan);
            unit.JobApplicationAnswerRepository.Add(answerRlyJason);
            unit.JobApplicationAnswerRepository.Add(answerWtfShaq);
            unit.JobApplicationAnswerRepository.Add(answerYearsNeo);
            unit.JobApplicationAnswerRepository.Add(answerYearsSusan);
            unit.SaveChanges();

            // Applications
            var applFromNeo = new JobApplication()
            {
                Applicant = neo,
                JobOffer = offerMicrosoft,
                Status = Status.Accepted,
                Text = "Microsoft > Apple",
                Answers = new List<JobApplicationAnswer>() { answerYearsNeo, answerGoalNeo }
            };

            var applFromNeo2 = new JobApplication()
            {
                Applicant = neo,
                JobOffer = offerApple,
                Status = Status.Rejected,
                Text = "Blue pill, red pill, I'll eat both",
                Answers = new List<JobApplicationAnswer>() { answerRlyNeo, answerDontNeo }
            };

            var applFromJason = new JobApplication()
            {
                Applicant = jason,
                JobOffer = offerApple,
                Status = Status.Unresolved,
                Text = "I know how to fight",
                Answers = new List<JobApplicationAnswer>() { answerRlyJason, answerDontJason }
            };

            var applFromSusan = new JobApplication()
            {
                Applicant = susan,
                JobOffer = offerTesla,
                Status = Status.Accepted,
                Text = "I know things...",
                Answers = new List<JobApplicationAnswer>() { answerYearsSusan, answerGoalSusan, answerRlySusan }
            };

            var applFromShaq = new JobApplication()
            {
                Applicant = bigshaq,
                JobOffer = offerStarbucks,
                Status = Status.Unresolved,
                Text = "Man's not hot",
                Answers = new List<JobApplicationAnswer>() { answerWtfShaq }
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
