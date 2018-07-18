using SpriteHelper.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class AddEditElevatorDialog : Form
    {
        public AddEditElevatorDialog(
            Elevator existingElevator,
            Func<AddEditElevatorDialog, string> validationFunction)
        {
            InitializeComponent();
        }

        // Set to true if OK is clicked.
        public bool Succeeded { get; private set; }

        // Set to null if elevator is being added.
        public Enemy ExistingElevator { get; private set; }

        //
        // Value getters.
        //

        public bool TryGetElevator(out Elevator enemy)
        {
            enemy = null;
            return false;
        }

        private void AddEditElevatorDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
