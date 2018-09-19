namespace FluentApi.Ef
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Project
    {
        private int id;
        private string name;
        private string description;
        private decimal? budgetLimet;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project()
        {
            Teams = new HashSet<Team>();
        }

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
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if(!Validator.IsNameValid(value))
                {
                    throw new ArgumentException("Ukorrekt værdi, navn må kun indeholde bogstaver (Max. 50)");
                }

                name = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if(!Validator.IsNameValid(value))
                {
                    throw new ArgumentException("Ukorrekt værdi, navn må kun indeholde bogstaver (Max. 50)");
                }

                description = value;
            }
        }

        [Column(TypeName = "date")]
        public DateTime? StartDate
        {
            get; set;
        }

        [Column(TypeName = "date")]
        public DateTime? EndDate
        {
            get; set;
        }

        public decimal? BudgetLimet
        {
            get
            {
                return budgetLimet;
            }
            set
            {
                if(!Validator.IsSalaryValid(value.ToString()))
                {
                    throw new ArgumentException("Ukorrekt værdi, løn må kun indeholde tal og skal være 0 eller større.");
                }
                budgetLimet = value;
            }
        }

        public int? MembersOfProject
        {
            get; set;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Team> Teams
        {
            get; set;
        }

        public decimal? ProjectExpenses { get; set; }
    }
}
