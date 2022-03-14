using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsContacts
{
    public partial class Main : Form
    {
        private BusinessLogicLayer _businessLogicLayer;
        public Main()
        {
            InitializeComponent();
            _businessLogicLayer = new BusinessLogicLayer();
            PopulateContacts();
        }

        #region EVENTS
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenContactDetailsDialog();
            
        }
        public void Main_Load(object sender, EventArgs e)
        {
            PopulateContacts();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateContacts(txtSearch.Text);
            txtSearch.Text = String.Empty;
        }
        #endregion

        #region PRIVATE METHOD
        private void OpenContactDetailsDialog()
        {
            ContactDetails contactDetails = new ContactDetails();
            contactDetails.ShowDialog(this);
        }
        private void gridContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewLinkCell cell = (DataGridViewLinkCell)gridContacts.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (cell.Value.ToString() == "Edit")
            {
                ContactDetails contactDetails = new ContactDetails();
                contactDetails.LoadContact(new Contact
                {
                    Id = int.Parse(gridContacts.Rows[e.RowIndex].Cells[0].Value.ToString()),
                    FirstName = gridContacts.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    LastName = gridContacts.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    Phone = gridContacts.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    Address = gridContacts.Rows[e.RowIndex].Cells[4].Value.ToString()
                });
                contactDetails.ShowDialog(this);
            }
            else if (cell.Value.ToString() == "Delete")
            {
                DeleteContact(int.Parse(gridContacts.Rows[e.RowIndex].Cells[0].Value.ToString()));
                PopulateContacts();
            }
        }

        private void DeleteContact(int id)
        {
            _businessLogicLayer.DeleteContact(id);
        }

       
        #endregion

        #region PUBLIC METHODS
        public void PopulateContacts(string searchText = null)
        {
            List<Contact> contacts = _businessLogicLayer.GetContacts(searchText);
            gridContacts.DataSource = contacts;
        }
        #endregion
        
    }
}
