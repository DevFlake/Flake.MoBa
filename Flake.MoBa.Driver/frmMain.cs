using Flake.MoBa.XpressNetLi;
using Flake.MoBa.XpressNetLi.Base.Enums.LocomotiveDirection;
using Flake.MoBa.XpressNetLi.Base.Enums.LocomotiveSpeedSections;
using Flake.MoBa.XpressNetLi.Entities.Locomotive;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flake.MoBa.Driver
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        Central _central;
        Locomotive _loco;

        bool _breakSignal = false;

        private void frmMain_Load(object sender, EventArgs e)
        {
            _loco = new Locomotive(int.Parse(this.cboAddress.Text), LocomotiveSpeedSections.x128);
        }

        private void InitializeDigitalComponents()
        {
            try
            {
                _central = new Central();
                _loco.AddFunction(new LocomotiveFunction(0, "Light", "turn on the lights", LocomotiveFunctionType.switching));
                _loco.RegisterCentral(_central);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); // TODO
            }
        }

        private void trkSpeed_Scroll(object sender, EventArgs e)
        {
            if (!_breakSignal)
            {
                lblSpeed.Text = trkSpeed.Value.ToString();
                _loco.SetSpeedAndDirection(trkSpeed.Value, rbtForward.Checked ? LocomotiveDirection.forward : LocomotiveDirection.backward);
                if (trkSpeed.Value == 0) _loco.BreakToStop();
            }
        }

        private void cmdLight_Click(object sender, EventArgs e)
        {
            _loco.ToggleFunction(0);
        }

        private void rbtForwardBackward_CheckedChanged(object sender, EventArgs e)
        {
            _loco.SetSpeedAndDirection(trkSpeed.Value, rbtForward.Checked ? LocomotiveDirection.forward : LocomotiveDirection.backward);
        }

        private void cmdBreak_Click(object sender, EventArgs e)
        {
            _breakSignal = true;
            _loco.BreakToStop();
            trkSpeed.Value = 0;
            _breakSignal = false;
        }

        private void initializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeDigitalComponents();
        }

        private void loadLocomotiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.cboAddress.Text = "32";
            
        }
    }
}
