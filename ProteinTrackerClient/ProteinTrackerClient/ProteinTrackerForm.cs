using ProteinTrackerClient.ProteinTrackerService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProteinTrackerClient
{
    public partial class ProteinTrackerForm : Form
    {
        private readonly ProteinTrackerServiceSoapClient service =
            new ProteinTrackerServiceSoapClient();
        private User[] users;

        public ProteinTrackerForm()
        {
            InitializeComponent();
        }

        private void ProteinTrackerForm_Load(object sender, EventArgs e)
        {
            users = service.ListUsers();
            cboSelectUser.DataSource = users;
            cboSelectUser.DisplayMember = "Name";
            cboSelectUser.ValueMember = "UserId";
        }

        private void OnAddNewUser(object sender, EventArgs e)
        {
            service.AddUser(tbName.Text, int.Parse(tbGoal.Text));
            users = service.ListUsers();
            cboSelectUser.DataSource = users;
        }

        private void OnUserChanged(object sender, EventArgs e)
        {
            var index = cboSelectUser.SelectedIndex;
            lblTotal.Text = users[index].Total.ToString();
            lblGoal.Text = users[index].Goal.ToString();
        }

        private void btnAddProtein_Click(object sender, EventArgs e)
        {
            var userId = users[cboSelectUser.SelectedIndex].UserId;
            var newTotal = service.AddProtien(int.Parse(tbAmount.Text), userId);
            users[cboSelectUser.SelectedIndex].Total = newTotal;
            lblTotal.Text = newTotal.ToString();
        }
    }
}
