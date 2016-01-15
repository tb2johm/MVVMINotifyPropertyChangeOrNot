#define INPC
#if INPC
#define WORKS
#endif

/* INSTRUCTIONS:
 * 1) Ok so first just run the code
 *    Now you can see that if you change the Resident (the combobox)
 *    The Friendes list will change (as expected)
 * 2) So let's comment out line 3 (#define WORKS)
 *    Since I have no OnPropertyChanged callbacks on the updated properties it doesn't work
 *    And I think that this is expected as well
 * 3) Now lets comment out line 1 as well (#define INPC)
 *    So really all that differes from step 2 is that we no longer implements the INotifyPropertyChange interface
 *    However this time the program works again...
  */

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MVVMNotifyPropertyChangeOrNot
{
#if INPC
    public class ViewModel : INotifyPropertyChanged
    {
#else
    public class ViewModel
    {
#endif
#if WORKS
        private List<Person> _residents;
        private Person _selectedResident;

        public List<Person> Residents
        {
            get { return _residents; }
            set
            {
                _residents = value;
                OnPropertyChanged("Residents");
            }
        }

        public Person SelectedResident
        {
            get { return _selectedResident; }
            set
            {
                _selectedResident = value;
                OnPropertyChanged("SelectedResident");
            }
        }

#else
        public List<Person> Residents { get; set; }
        public Person SelectedResident { get; set; }
#endif
        #region CreateExampleData
        public ViewModel()
        {
            var me = new Person("Markus");
            var p1 = new Person("Emil");
            var p2 = new Person("Per");
            var p3 = new Person("James");
            var p4 = new Person("Lena");

            me.Friends.Add(p1);
            me.Friends.Add(p2);

            p1.Friends.Add(p3);
            p1.Friends.Add(me);
            p2.Friends.Add(p3);
            p2.Friends.Add(p4);
            p3.Friends.Add(me);
            p3.Friends.Add(p2);
            p4.Friends.Add(p1);

            Residents = new List<Person>() {me, p1, p2, p3, p4};
        }
        #endregion

#if INPC
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
#endif
    }

    public class Person
    {
        public String Name { get; set; }
        public List<Person> Friends { get; set; }

        public Person()
        {
            Friends = new List<Person>();
        }
        public Person(String name) : this()
        {
            this.Name = name;
        }

    }
}
