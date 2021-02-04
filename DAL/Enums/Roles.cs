namespace DAL.Enums
{
    public enum Roles
    {
        None = 1,
        JobSeeker = 2,
        Company = 4,
        SuperUser = JobSeeker | Company
    }
}
