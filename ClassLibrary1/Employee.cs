namespace FluentApi.Ef
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee //: Person
    {
        private int id;
        private string firstname;
        private int? teamId;
        private string lastname;
        private string cPRNumber;
        private decimal? salary;
        private DateTime? birthday;
        private DateTime? employmentDate;

        /*public Employee(int id, string firstname, int? teamId, string lastname, string cPRNumber, decimal? salary)
            :base(firstname, lastname)
        {
            this.id = id;
            this.firstname = firstname;
            this.teamId = teamId;
            this.lastname = lastname;
            this.cPRNumber = cPRNumber;
            this.salary = salary;
        }*/

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if(!Validator.IsIdValid(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(id), "Must be grater than 0");
                }
                id = value;
            }
        }

        [StringLength(50)]
        public string Firstname
        {
            get
            {
                return firstname;
            }
            set
            {
                if(!Validator.IsNameValid(value))
                {
                    throw new ArgumentException("Ukorrekt værdi, navn må kun indeholde bogstaver (Max. 50)");
                }

                firstname = value;
            }
        }

        public int? TeamId
        {
            get
            {
                return teamId;
            }
            set
            {
                teamId = value;
            }
        }

        [Column(TypeName = "date")]
        public DateTime? EmploymentDate
        {
            get
            {
                return employmentDate;
            }
            set
            {
                if(!Validator.IsHireDayValid(value.Value))
                {
                    throw new ArgumentOutOfRangeException("Ukorrect værdi, skal være over (1/1 1950)");
                }
                employmentDate = value;
            }
        }

        [StringLength(50)]
        public string Lastname
        {
            get
            {
                return lastname;
            }
            set
            {
                if(!Validator.IsNameValid(value))
                {
                    throw new ArgumentException("Ukorrekt værdi, navn må kun indeholde bogstaver (Max. 50)");
                }
                lastname = value;
            }
        }

        [Column(TypeName = "date")]
        public DateTime? Birthday
        {
            get
            {
                return birthday;
            }
            set
            {
                if(!Validator.IsBirthDayValid(value.Value))
                {
                    throw new ArgumentException("Ukorrekt værdi");
                }
                birthday = value;
            }
        }

        [StringLength(50)]
        public string CPRNumber
        {
            get
            {
                return cPRNumber;
            }
            set
            {
                if(!Validator.IsCprNumberValid(value))
                {
                    throw new ArgumentException("Ukorrekt værdi, CPR nummer må kun indeholde tal og skal have 10 cifre.");
                }
                cPRNumber = value;
            }
        }

        [Column(TypeName = "money")]
        public decimal? Salary
        {
            get
            {
                return salary;
            }
            set
            {
                if(!Validator.IsSalaryValid(value.ToString()))
                {
                    throw new ArgumentException("Ukorrekt værdi, løn må kun indeholde tal og skal være 0 eller større.");
                }
                salary = value;
            }
        }

        public virtual ContactInfo ContactInfo
        {
            get; set;
        }

        public virtual Team Team
        {
            get; set;
        }

        public string DisplayName
        {
            get
            {
                return this.Firstname + " " + this.Lastname;
            }
        }
    }
}
