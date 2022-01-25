using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntryExitCivy
{
    class Civy
    {
        private string id, fullname, nationality, phone, home_address, occupation;
        private bool gender;
        private DateTime birthday;

        public Civy() { }

        public Civy(string id, string fullname, bool gender, DateTime birthday,
            string nationality, string phone, string home_address, string ocupation)
        {
            this.id = id;
            this.fullname = fullname;
            this.gender = gender;
            this.birthday = birthday;
            this.nationality = nationality;
            this.phone = phone;
            this.home_address = home_address;
            this.occupation = occupation;

        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return fullname; }
            set { fullname = value; }
        }

        public bool Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        public string Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Address
        {
            get { return home_address; }
            set { home_address = value; }
        }

        public string Occupation
        {
            get { return occupation; }
            set { occupation = value; }
        }
    }


    class Entry
    {
        private string civy_id, expected_destination, purpose;
        private DateTime arrival_date, visa_expiration, passport_expiration;

        public Entry() { }

        public Entry(string civy_id, DateTime arrival_date, string expected_destination, 
            DateTime visa_expiration, DateTime passport_expiration, string purpose)
        {
            this.civy_id = civy_id;
            this.arrival_date = arrival_date;
            this.expected_destination = expected_destination;
            this.visa_expiration = visa_expiration;
            this.passport_expiration = passport_expiration;
            this.purpose = purpose;
        }

        public string Civy_id
        {
            get { return civy_id; }
            set { civy_id = value; }
        }

        public string Expected_destination
        {
            get { return expected_destination; }
            set { expected_destination = value; }
        }

        public string Purpose
        {
            get { return purpose; }
            set { purpose = value; }
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
    }


    class Exit 
    {
        private string civy_id, destination, purpose;
        private DateTime depart_date, visa_expiration, passport_expiration;

        public Exit() { }

        public Exit(string civy_id, DateTime depart_date, string destination, 
            DateTime visa_expiration, DateTime passport_expiration, string purpose)
        {
            this.civy_id = civy_id;
            this.depart_date = depart_date;
            this.destination = destination;
            this.visa_expiration = visa_expiration;
            this.passport_expiration = passport_expiration;
            this.purpose = purpose;
        }

        public string Civy_id
        {
            get { return civy_id; }
            set { civy_id = value; }
        }

        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        public string Purpose
        {
            get { return purpose; }
            set { purpose = value; }
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
        WORKING = 2,
        TRAVEL = 3,
        OTHER = 4,
    }

}
