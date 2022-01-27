using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntryExitCivy
{
    class Civy
    {
        private string id;
        private string fullname, phone, home_address, occupation;
        private string nationality;
        private DateTime birthday;

        public string Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }

        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }


        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Occupation
        {
            get { return occupation; }
            set { occupation = value; }
        }

        public string Home_address
        {
            get { return home_address; }
            set { home_address = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Fullname
        {
            get { return fullname; }
            set { fullname = value; }
        }
        private bool gender;

        public bool Gender
        {
            get { return gender; }
            set { gender = value; }
        }


        public Civy(string id, string fullname, string nationality, string phone
                    , string home_address, string occupation, bool gender, DateTime birthday)
        {
            this.id = id;
            this.fullname = fullname;
            this.nationality = nationality;
            this.phone = phone;
            this.home_address = home_address;
            this.occupation = occupation;
            this.gender = gender;
            this.birthday = birthday;
        }

        public Civy()
        { }
    }


    class Entry
    {
        private string civy_id;
        private DateTime arrival_date, visa_expiration, passport_expiration;
        private string expected_destination;
        private Purpose purpose;

        public string Civy_id
        {
            get { return civy_id; }
            set { civy_id = value; }
        }
        

        public DateTime Arrival_date
        {
            get { return arrival_date; }
            set { arrival_date = value; }
        }

        public DateTime Visa_expiration
        {
            get { return visa_expiration; }
            set { visa_expiration = value; }
        }

        public DateTime Passport_expiration
        {
            get { return passport_expiration; }
            set { passport_expiration = value; }
        } 

        public string Expected_destination
        {
            get { return expected_destination; }
            set { expected_destination = value; }
        }
        

        public Purpose Purpose
        {
            get { return purpose; }
            set { purpose = value; }
        }

        public Entry()
        { }

        public Entry(string civy_id, DateTime arrival_date, DateTime visa_expiration
                    , DateTime passport_expiration, string expected_destination, Purpose purpose)
        {
            this.civy_id = civy_id;
            this.arrival_date = arrival_date;
            this.visa_expiration = visa_expiration;
            this.passport_expiration = passport_expiration;
            this.expected_destination = expected_destination;
            this.purpose = purpose;
        }
    }


    class Exit 
    {
        private string civy_id;
        private DateTime depart_date, visa_expiration, passport_expiration;
        private string destination;
        private Purpose purpose;

        public string Civy_id
        {
            get { return civy_id; }
            set { civy_id = value; }
        }


        public DateTime Depart_date
        {
            get { return depart_date; }
            set { depart_date = value; }
        }

        public DateTime Visa_expiration
        {
            get { return visa_expiration; }
            set { visa_expiration = value; }
        }

        public DateTime Passport_expiration
        {
            get { return passport_expiration; }
            set { passport_expiration = value; }
        }

        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }


        public Purpose Purpose
        {
            get { return purpose; }
            set { purpose = value; }
        }

        public Exit()
        { }

        public Exit(string civy_id, DateTime depart_date, DateTime visa_expiration
                    , DateTime passport_expiration, string destination, Purpose purpose)
        {
            this.civy_id = civy_id;
            this.depart_date = depart_date;
            this.visa_expiration = visa_expiration;
            this.passport_expiration = passport_expiration;
            this.destination = destination;
            this.purpose = purpose;
        }
    }


    class Nation
    {
        private string id;
        private string name;      

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Nation(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }

    public enum Purpose
    { 
        STUDY = 1,
        EMPLOYMENT = 2,
        BUSINESS = 3,
        TRAVEL = 4,
        OTHER = 5
    }

}
