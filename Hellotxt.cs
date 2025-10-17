using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JerseyStore
{
    public partial class Hellotxt : UserControl
    {
        public Hellotxt()
        {
            InitializeComponent();
        }

        private void Session_UsernameChanged(object sender, EventArgs e)
        {
            // When session changes, update the Username if not explicitly set.
            // If caller explicitly set Username property, we keep that value.
            if (string.IsNullOrEmpty(_explicitlySetUsername))
            {
                _username = Session.LoggedInUsername ?? string.Empty;
                UpdateLabel();
            }
        }

        // Public property to get/set displayed username
        private string _username = string.Empty;
        private string _explicitlySetUsername; // store when user sets via property

        [Browsable(true)]
        [Category("Data")]
        [Description("Username to display after the greeting text.")]
        public string Username
        {
            get => _username;
            set
            {
                _username = value ?? string.Empty;
                _explicitlySetUsername = _username;
                UpdateLabel();
            }
        }

        // Convenience method callers can use
        public void SetUsername(string username)
        {
            Username = username;
        }

        private void UpdateLabel()
        {
            // label6 is defined in the designer partial class
            if (label6 != null)
            {
                if (string.IsNullOrEmpty(_username))
                    label6.Text = "Hello, ";
                else
                    label6.Text = $"Hello, {_username}";
            }
        }

        // Ensure the label is correct when the control loads in the designer/runtime
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // If username hasn't been explicitly set, initialize from session
            if (string.IsNullOrEmpty(_explicitlySetUsername))
            {
                _username = Session.LoggedInUsername ?? string.Empty;
            }

            // Subscribe to session changes so the control updates automatically
            if (!DesignMode)
            {
                try
                {
                    Session.UsernameChanged += Session_UsernameChanged;
                }
                catch
                {
                    // ignore
                }
            }

            UpdateLabel();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            try
            {
                Session.UsernameChanged -= Session_UsernameChanged;
            }
            catch
            {
                // ignore
            }

            base.OnHandleDestroyed(e);
        }
    }
}
