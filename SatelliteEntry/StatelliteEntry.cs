using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SatelliteEntry.Controllers;

namespace SatelliteEntry
{
    public partial class SatelliteEntry : Form
    {
        #region Private Members
        private List<int> _idList = new List<int>();
        private readonly int _maxIDs = 30; // Limit 30 IDs to query
        private SatelliteEntryController _satController;
        #endregion

        #region Public Members
        public bool IsInitialized { get; set; }
        #endregion

        #region Constructor
        public SatelliteEntry()
        {
            InitializeComponent();
            
            try
            {
                _satController = new SatelliteEntryController();
                IsInitialized = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Failed to initialize connections, ensure system configuration is correct.", "Initialization Failed", MessageBoxButtons.OK);
                IsInitialized = false;
            }
        }
        #endregion

        #region Event Handlers
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddId();
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            if (_idList.Count >= 1)
            {
                // Perform Query and Database Update
                try
                {
                    _satController.QueryIdAndStoreData(_idList);

                    MessageBox.Show("Satellite Entry Successfully Queried API and Updated Database.", "Success!", MessageBoxButtons.OK);
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show("Invalid Space-Track Credentials, verify config file.", "Authorization Error", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);

                    MessageBox.Show("Satellite Entry Operation Failed.", "Operation Failed", MessageBoxButtons.OK);
                }
            }

            TxtBoxID.Clear();
            TxtBoxIdList.Clear();
            _idList.Clear();
        }

        private void TxtBoxID_Validating(object sender, CancelEventArgs e)
        {
            string entry = TxtBoxID.Text;

            if (_idList.Count == _maxIDs)
            {
                TxtBoxID.Focus();
                LblValidateMessage.Text = "Maximum IDs Entered. Select the Run Button.";
                LblValidateMessage.ForeColor = Color.Red;
                LblValidateMessage.Visible = true;
                e.Cancel = true;
            }

            if (!string.IsNullOrWhiteSpace(entry))
            {
                Regex stringCheck = new Regex(@"^[0-9\s]{0,9}$");

                if (!stringCheck.IsMatch(entry))
                {
                    TxtBoxID.Clear();
                    LblValidateMessage.Text = "Invalid ID";
                    LblValidateMessage.ForeColor = Color.Red;
                    LblValidateMessage.Visible = true;
                    e.Cancel = true;
                }
            }
        }

        private void TxtBoxID_TextChanged(object sender, EventArgs e)
        {
            if (LblValidateMessage.Visible)
                LblValidateMessage.Visible = false;
        }

        private void TxtBoxIdList_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtBoxIdList.Text))
            {
                BtnRun.Enabled = false;
            }
            else
            {
                BtnRun.Enabled = true;
            }
        }

        private void TxtBoxID_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ValidateChildren())
                {
                    AddId();
                }

                e.Handled = true;
            }
        }
        #endregion

        #region Private Utilities
        /// <summary>
        /// Adds Entered Id to list
        /// </summary>
        private void AddId()
        {
            string entry = TxtBoxID.Text;
            TxtBoxID.Clear();

            if (!string.IsNullOrWhiteSpace(entry))
            {
                UpdateIDList(entry);
            }
        }

        /// <summary>
        /// Updates ID list on GUI and stored data list
        /// </summary>
        /// <param name="entry">Entered NORAD ID</param>
        private void UpdateIDList(string entry)
        {
            string delim = ", ";
            if (int.TryParse(entry, out int enteredId))
            {
                if (_idList.Count == 0)
                {
                    _idList.Add(enteredId);

                    TxtBoxIdList.Text = enteredId.ToString();
                }
                else
                {
                    if (!_idList.Contains(enteredId))
                    {
                        _idList.Add(enteredId);

                        TxtBoxIdList.Text += delim + enteredId.ToString();
                    }
                    else
                    {
                        LblValidateMessage.Text = "Duplicate Entry";
                        LblValidateMessage.Visible = true;
                    }
                }
            }
        }
        #endregion
    }
}
