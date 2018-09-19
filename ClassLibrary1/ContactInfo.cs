namespace FluentApi.Ef
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContactInfos")]
    public partial class ContactInfo
    {
        private string phone;
        private string email;
        private int id;

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if(!Validator.IsEmailValid(value))
                {
                    throw new ArgumentException("eddfdf");
                }
                email = value;
            }
        }

        [StringLength(15)]
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                if(!Validator.IsPhoneNumberValid(value))
                {
                    throw new ArgumentException("Ukorrekt værdi, telefon nummer må kun indeholde tal og skal være 8 cifre.");
                }
                phone = value;
            }
        }

        public virtual Employee Employee
        {
            get; set;
        }
    }
}
