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
        Central _central = new Central();
        Locomotive _loco;

        bool _stopSignal = false;

        private void frmMain_Load(object sender, EventArgs e)
        {
            _loco = new Locomotive(int.Parse(this.cboAddress.Text), LocomotiveSpeedSections.x128);

            //var address = int.Parse(this.cboAddress.Text);

            //    var l = new Locomotive(address, LocomotiveSpeedSections.x128);
                _loco.AddFunction(new LocomotiveFunction(0, "Light", "turn on the lights", LocomotiveFunctionType.switching));
                _loco.RegisterCentral(_central);
                //l.ToggleFunction(0);
                //l.SetSpeedAndDirection(30, LocomotiveDirection.backward);
                //System.Threading.Thread.Sleep(5000);

                //l.SetSpeedAndDirection(30, LocomotiveDirection.forward);

                //System.Threading.Thread.Sleep(5000);
                //l.BreakToStop();
                //l.ToggleFunction(0);
            
        }

        private void trkSpeed_Scroll(object sender, EventArgs e)
        {
            if (!_stopSignal)
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

        private void rbtBackward_CheckedChanged(object sender, EventArgs e)
        {
            _loco.SetSpeedAndDirection(trkSpeed.Value, rbtForward.Checked ? LocomotiveDirection.forward : LocomotiveDirection.backward);
        }

        private void rbtForward_CheckedChanged(object sender, EventArgs e)
        {
            _loco.SetSpeedAndDirection(trkSpeed.Value, rbtForward.Checked ? LocomotiveDirection.forward : LocomotiveDirection.backward);
        }

        private void cmdBreak_Click(object sender, EventArgs e)
        {
            _stopSignal = true;
            _loco.BreakToStop();
            trkSpeed.Value = 0;
                _stopSignal = false;
        }
    }
}
