using StudentAndFa;

namespace StudentAndFA
{
    internal class Faculty
    {
        private string? dean, nameOfFaculty, phoneNumber;
        private const string uk = "Неизвестно";
        public string? Dean
        {
            get { return dean; }
            set { dean = value ?? uk; }
        }
        public string? NameOfFaculty
        {
            get { return nameOfFaculty; }
            set
            {
                if(nameOfFaculty != value)
                {
                    nameOfFaculty = value ?? uk;
                    ChangeFacultyName?.Invoke(this,EventArgs.Empty);
                }
            }
        }
        public string? PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value ?? uk; }
        }
        public Faculty()
        {
            dean = uk;
            nameOfFaculty = uk;
            phoneNumber = uk;
        }
        public Faculty(string dean, string nameOfFaculty, string phoneNumber)
        {
            Dean = dean ?? uk;
            NameOfFaculty = nameOfFaculty ?? uk;
            PhoneNumber = phoneNumber ?? uk;
        }
        public event EventHandler? ChangeFacultyName;
        public event BonusEventHandler? Bonus;
        public void Bonuses(double bonusAmount) => Bonus?.Invoke(bonusAmount);
        public override string ToString() => Dean + " " + NameOfFaculty + " " + PhoneNumber;
        
    }
}
