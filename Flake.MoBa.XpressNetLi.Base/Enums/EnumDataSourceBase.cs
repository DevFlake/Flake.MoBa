namespace Flake.MoBa.XpressNetLi.Base.Enums
{
    /// <summary>
    /// This class is base for all EnumDataSources for binding lists of enums to controls.
    /// </summary>
    /// <remarks>derived classes must inherit an empty constructor and one with implementation of System.ComponentModel.IContainer</remarks>
    public class EnumDataSourceBase : System.ComponentModel.Component, System.ComponentModel.IListSource
    {
        public EnumDataSourceBase()
        {
            InitializeComponent();
        }

        public EnumDataSourceBase(System.ComponentModel.IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #region IListSource Members

        public bool ContainsListCollection
        {
            get { return false; }
        }

        protected virtual System.Collections.IList GetListCore()
        {
            return null;
        }

        public System.Collections.IList GetList()
        {
            return GetListCore();
        }

        #endregion IListSource Members

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion Component Designer generated code
    }
}