namespace MVCAssigment.Models
{
    public class Person
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? BirthPlace { get; set; }
        public uint Age { get; set; }
        public bool? IsGraduated { get; set; }

        public Person(string firstname, string lastname, string gender, DateTime dob,
         string phonenumber, string birthplace, uint age, bool isgraduated)
        {
            FirstName = firstname;
            LastName = lastname;
            Gender = gender;
            DateOfBirth = dob;
            PhoneNumber = phonenumber;
            BirthPlace = birthplace;
            Age = age;
            IsGraduated = isgraduated;
        }

        public Person()
        {
        }
    }
}