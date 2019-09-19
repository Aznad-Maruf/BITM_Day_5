using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student
{
    public partial class Student : Form
    {

        private struct AllInfo
        {
            public string id, name, mobile, age, address, gpaPoint;
        }

        private string allData, lastData, maxHolder, minHolder, searchedData, searchBy, searchKey;
        private double maxGPA, minGPA, avgGPA, totalGPA, cuGPA;
        List<AllInfo> allDataList = new List<AllInfo>();
        private AllInfo allInfo;

        public Student()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            ClearCalculatedTextBox();ClearInputTextBox("searchTextBox"); displayRichTextBox.Clear();
            if (!idRadioButton.Checked && !nameRadioButton.Checked && !mobileRadioButton.Checked)
            {
                MessageBox.Show("Select a Search by option");
            }
            else if (String.IsNullOrEmpty(searchTextBox.Text))
            {
                MessageBox.Show("Enter Search Keyword!");
            }
            else
            {
                searchKey = searchTextBox.Text;
                if (idRadioButton.Checked) searchBy = "id";
                if (nameRadioButton.Checked) searchBy = "name";
                if (mobileRadioButton.Checked) searchBy = "mobile";

                if (isFound())
                {
                    ClearCalculatedTextBox();
                    DisplayIt(searchedData);
                }
                else
                {
                    MessageBox.Show("No Data Found of Your Search");
                }
            }
        }

        private bool isFound()
        {
            searchedData = "";
            foreach (AllInfo cuAllInfo in allDataList)
            {
                if (searchBy.Equals("id"))
                {
                    if (cuAllInfo.id.Equals(searchKey))
                    {
                        searchedData = makePrintable(cuAllInfo);
                        return true;
                    }


                }
                if (searchBy.Equals("name"))
                {
                    if (cuAllInfo.name.Equals(searchKey))
                    {
                        searchedData = makePrintable(cuAllInfo);
                        return true;
                    }


                }
                if (searchBy.Equals("mobile"))
                {
                    if (cuAllInfo.mobile.Equals(searchKey))
                    {
                        searchedData = makePrintable(cuAllInfo);
                    return true;
                    }


                }
            }

            return false;
        }

        private string makePrintable( AllInfo cuAllInfo )
        {
            return "Name : " + cuAllInfo.name + "\n" + "ID : " + cuAllInfo.id + "\nMobile : " + cuAllInfo.mobile + "\nAge : " + cuAllInfo.age + "\nAddress : " + cuAllInfo.address + "\nGPA : " + cuAllInfo.gpaPoint + "\n"; ;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            ClearCalculatedTextBox();
            getAllData();
            if (CanBeAdded())
            {
                MakeLastDataAndAllData();
                allDataList.Add(allInfo);
                DisplayIt(lastData);
                ClearInputTextBox("all");
            }
        }

        private void showAllButton_Click(object sender, EventArgs e)
        {
            ClearInputTextBox("all");
            DisplayIt(allData);
            CalculateCalculatedData();
            maxGPATextBox.Text = maxGPA.ToString();
            minGPATextBox.Text = minGPA.ToString();
            maxGPAHolderTextBox.Text = maxHolder;
            minGPAHolderTextBox.Text = minHolder;
            totalGPATextBox.Text = totalGPA.ToString();
            averageGPATextBox.Text = avgGPA.ToString();

        }

        private void DisplayIt( string displayIt )
        {
            displayRichTextBox.Text = displayIt;
        }

        private void CalculateCalculatedData()
        {
            maxGPA = 0;
            minGPA = 4;
            totalGPA = 0;
            foreach (AllInfo cuAllInfo in allDataList)
            {
                cuGPA = Convert.ToDouble(cuAllInfo.gpaPoint);
                if (cuGPA > maxGPA)
                {
                    maxGPA = cuGPA;
                    maxHolder = cuAllInfo.name;
                }
                if (cuGPA < minGPA)
                {
                    minGPA = cuGPA;
                    minHolder = cuAllInfo.name;
                }

                totalGPA += cuGPA;
            }

            avgGPA = totalGPA / allDataList.Count();

        }

        private void MakeLastDataAndAllData()
        {
            lastData = makePrintable(allInfo);
            allData = allData + lastData + "\n------o-------\n\n";
        }

        private void ClearInputTextBox(string except)
        {
            idTextBox.Clear();
            nameTextBox.Clear();
            mobileTextBox.Clear();
            ageTextBox.Clear();
            addressTextBox.Clear();
            gpaPointTextBox.Clear();

            if(!except.Equals("searchTextBox")) searchTextBox.Clear();
        }

        private void ClearCalculatedTextBox()
        {
            maxGPATextBox.Clear();
            minGPATextBox.Clear();
            maxGPAHolderTextBox.Clear();
            minGPAHolderTextBox.Clear();
            averageGPATextBox.Clear();
            totalGPATextBox.Clear();
        }

        void getAllData()
        {
            allInfo.id = idTextBox.Text;
            allInfo.name = nameTextBox.Text;
            allInfo.mobile = mobileTextBox.Text;
            allInfo.age = ageTextBox.Text;
            allInfo.address = addressTextBox.Text;
            allInfo.gpaPoint = gpaPointTextBox.Text;
        }

        bool CanBeAdded()
        {
            if (String.IsNullOrEmpty(allInfo.id))
            {
                MessageBox.Show("ID can't be empty!");
                return false;
            }
            if (String.IsNullOrEmpty(allInfo.name))
            {
                MessageBox.Show("Name can't be empty!");
                return false;
            }
            if (String.IsNullOrEmpty(allInfo.mobile))
            {
                MessageBox.Show("Mobile can't be empty!");
                return false;
            }

            if (allInfo.id.Length > 4)
            {
                MessageBox.Show("Id can have at most 4 character!");
                return false;
            }
            if (allInfo.name.Length > 30)
            {
                MessageBox.Show("Name can have at most 30 character!");
                return false;
            }
            if (allInfo.mobile.Length > 11)
            {
                MessageBox.Show("Mobile can have at most 11 character!");
                return false;
            }

            try
            {
                if (Convert.ToDouble(allInfo.gpaPoint) > 4 || Convert.ToDouble(allInfo.gpaPoint) < 0)
                {
                    MessageBox.Show("GPA Must be between 0 and 4");
                    return false;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }

            foreach (AllInfo cuAllInfo in allDataList)
            {
                if (cuAllInfo.id.Equals(allInfo.id))
                {
                    MessageBox.Show("ID you entered is already taken!");
                    return false;
                }
            }
            foreach (AllInfo cuAllInfo in allDataList)
            {
                if (cuAllInfo.mobile.Equals(allInfo.mobile))
                {
                    MessageBox.Show("Mobile you entered is already taken!");
                    return false;
                }
            }

            return true;
        }

    }
}
